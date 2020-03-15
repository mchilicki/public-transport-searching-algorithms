using Chilicki.Ptsa.Domain.Search.Aggregates;
using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Domain.Search.Exceptions;
using Chilicki.Ptsa.Domain.Search.Factories.Dijkstra;
using Chilicki.Ptsa.Domain.Search.Services.Base;
using Chilicki.Ptsa.Domain.Search.Services.Dijkstra;
using System.Collections.Generic;

namespace Chilicki.Ptsa.Domain.Search.Services
{
    public class DijkstraConnectionSearchEngine : IConnectionSearchEngine
    {
        readonly DijkstraEmptyFastestConnectionsFactory emptyFastestConnectionsFactory;
        readonly DijkstraNextVertexResolver nextVertexResolver;
        readonly DijkstraFastestConnectionReplacer fastestConnectionReplacer;
        readonly DijkstraConnectionService connectionsService;
        readonly DijkstraGraphService graphService;
        readonly DijkstraContinueChecker continueChecker;

        public DijkstraConnectionSearchEngine(
            DijkstraEmptyFastestConnectionsFactory emptyFastestConnectionsFactory,
            DijkstraNextVertexResolver nextVertexResolver,
            DijkstraFastestConnectionReplacer fastestConnectionReplacer,
            DijkstraConnectionService connectionsService,
            DijkstraGraphService graphService,
            DijkstraContinueChecker continueChecker)
        {
            this.emptyFastestConnectionsFactory = emptyFastestConnectionsFactory;
            this.nextVertexResolver = nextVertexResolver;
            this.fastestConnectionReplacer = fastestConnectionReplacer;
            this.connectionsService = connectionsService;
            this.graphService = graphService;
            this.continueChecker = continueChecker;
        }

        public VertexFastestConnections SearchConnections(SearchInput search, Graph graph)
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

        private (VertexFastestConnections, Vertex) PrepareVerticesForFirstIteration(SearchInput search, Graph graph)
        {
            var vertexFastestConnections = emptyFastestConnectionsFactory.Create(graph, search);
            var currentVertex = nextVertexResolver.GetFirstVertex(graph, search.StartStop);
            vertexFastestConnections = graphService.SetTransferConnectionsToSimilarVertices(
                vertexFastestConnections, currentVertex, currentVertex.SimilarVertices);
            return (vertexFastestConnections, currentVertex);
        }

        private (VertexFastestConnections, Vertex) MakeIteration(
            SearchInput search, VertexFastestConnections vertexFastestConnections, Vertex currentVertex)
        {
            var allConnections = graphService.GetConnectionsFromSimilarVertices(currentVertex, search);
            foreach (var connection in allConnections)
            {
                ReplaceFastestConnectionIfShould(search, vertexFastestConnections, connection);
            }
            (vertexFastestConnections, currentVertex) =
                PrepareVerticesForNextIteration(vertexFastestConnections, currentVertex);
            return (vertexFastestConnections, currentVertex);
        }

        private void ReplaceFastestConnectionIfShould(
            SearchInput search, VertexFastestConnections vertexFastestConnections, Connection connection)
        {
            var destinationStopFastestConnection = connectionsService
                .GetDestinationStopFastestConnection(vertexFastestConnections, connection);
            var connectionFromPreviousVertex = connectionsService
                .GetConnectionFromPreviousVertex(vertexFastestConnections, connection);
            if (fastestConnectionReplacer
                .ShouldConnectionBeReplaced(search, connectionFromPreviousVertex,
                    destinationStopFastestConnection, connection))
            {
                fastestConnectionReplacer
                    .ReplaceWithNewFastestConnection(destinationStopFastestConnection, connection);
            }
        }

        private (VertexFastestConnections, Vertex) PrepareVerticesForNextIteration(
            VertexFastestConnections vertexFastestConnections, Vertex currentVertex)
        {
            graphService.MarkVertexAsVisited(currentVertex);
            currentVertex = nextVertexResolver.GetNextVertex(vertexFastestConnections);
            ValidatePathExists(currentVertex);
            vertexFastestConnections = graphService.SetTransferConnectionsToSimilarVertices(
                vertexFastestConnections, currentVertex, currentVertex.SimilarVertices);
            return (vertexFastestConnections, currentVertex);
        }

        private void ValidatePathExists(Vertex currentVertex)
        {
            if (currentVertex == null)
                throw new DijkstraNoFastestPathExistsException();
        }
    }
}
