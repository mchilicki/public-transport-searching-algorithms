using Chilicki.Ptsa.Domain.Search.Aggregates;
using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Domain.Search.Helpers.Exceptions;
using Chilicki.Ptsa.Domain.Search.Factories.Dijkstra;
using Chilicki.Ptsa.Domain.Search.Services.Base;
using Chilicki.Ptsa.Domain.Search.Services.Dijkstra;

namespace Chilicki.Ptsa.Domain.Search.Services
{
    public class DijkstraSearch : IConnectionSearchEngine
    {
        readonly DijkstraEmptyFastestConnectionsFactory emptyConnFactory;
        readonly DijkstraNextVertexResolver resolver;
        readonly DijkstraFastestConnectionReplacer replacer;
        readonly DijkstraConnectionService connectionsService;
        readonly DijkstraGraphService graphService;
        readonly DijkstraContinueChecker continueChecker;

        public DijkstraSearch(
            DijkstraEmptyFastestConnectionsFactory emptyConnFactory,
            DijkstraNextVertexResolver resolver,
            DijkstraFastestConnectionReplacer replacer,
            DijkstraConnectionService connectionsService,
            DijkstraGraphService graphService,
            DijkstraContinueChecker continueChecker)
        {
            this.emptyConnFactory = emptyConnFactory;
            this.resolver = resolver;
            this.replacer = replacer;
            this.connectionsService = connectionsService;
            this.graphService = graphService;
            this.continueChecker = continueChecker;
        }

        public FastestConnections SearchConnections(SearchInput search, Graph graph)
        {
            var (vertexFastestConnections, currentVertex) = PrepareVerticesForFirstIteration(search, graph);
            int iteration = 0;
            while (continueChecker.ShouldContinue(search.DestinationStop.Id, currentVertex))
            {
                (vertexFastestConnections, currentVertex) = 
                    MakeIteration(search, vertexFastestConnections, currentVertex);
                iteration++;
            }
            return vertexFastestConnections;
        }

        private (FastestConnections, Vertex) PrepareVerticesForFirstIteration(
            SearchInput search, Graph graph)
        {
            var vertexFastestConnections = emptyConnFactory.Create(graph, search);
            var currentVertex = resolver.GetFirstVertex(graph, search.StartStop);
            vertexFastestConnections = graphService.SetTransferConnectionsToSimilarVertices(
                vertexFastestConnections, currentVertex, currentVertex.SimilarVertices);
            return (vertexFastestConnections, currentVertex);
        }

        private (FastestConnections, Vertex) MakeIteration(
            SearchInput search, FastestConnections fastestConnections, Vertex currentVertex)
        {
            var possibleConnections = graphService.GetPossibleConnections(currentVertex, search);
            foreach (var possibleConn in possibleConnections)
            {
                ReplaceFastestConnectionIfShould(search, fastestConnections, possibleConn);
            }
            (fastestConnections, currentVertex) =
                PrepareVerticesForNextIteration(fastestConnections, currentVertex);
            return (fastestConnections, currentVertex);
        }

        private void ReplaceFastestConnectionIfShould(
            SearchInput search, FastestConnections fastestConnections, Connection possibleConn)
        {
            var currentConn = connectionsService.GetCurrentConnection(fastestConnections, possibleConn);
            var previousVertexConn = connectionsService.GetPreviousVertexConnection(
                fastestConnections, possibleConn);
            if (replacer.ShouldConnectionBeReplaced(search, previousVertexConn,
                    currentConn, possibleConn))
            {
                replacer.ReplaceWithNewFastestConnection(currentConn, possibleConn);
            }
        }

        private (FastestConnections, Vertex) PrepareVerticesForNextIteration(
            FastestConnections fastestConnections, Vertex currentVertex)
        {
            graphService.MarkVertexAsVisited(currentVertex);
            currentVertex = resolver.GetNextVertex(fastestConnections);
            ValidatePathExists(currentVertex);
            fastestConnections = graphService.SetTransferConnectionsToSimilarVertices(
                fastestConnections, currentVertex, currentVertex.SimilarVertices);
            return (fastestConnections, currentVertex);
        }

        private void ValidatePathExists(Vertex currentVertex)
        {
            if (currentVertex == null)
                throw new DijkstraNoFastestPathExistsException();
        }
    }
}
