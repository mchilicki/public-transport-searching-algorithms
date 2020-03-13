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
            var vertexFastestConnections = emptyFastestConnectionsFactory
                .Create(graph, search);
            var currentVertex = nextVertexResolver.GetFirstVertex(graph, search.StartStop);
            vertexFastestConnections = graphService.SetTransferConnectionsToSimilarVertices(
                vertexFastestConnections, currentVertex, currentVertex.SimilarVertices);
            int iteration = 0;
            while (continueChecker.ShouldContinue(search.DestinationStop.Id, currentVertex))
            {
                var allConnections = graphService.GetConnectionsFromSimilarVertices
                    (currentVertex, search);
                foreach (var connection in allConnections)
                {
                    var destinationStopFastestConnection = connectionsService
                        .GetDestinationStopFastestConnection(vertexFastestConnections, connection);
                    var connectionFromPreviousVertex = connectionsService
                        .GetConnectionFromPreviousVertex(vertexFastestConnections, connection);
                    if (fastestConnectionReplacer
                        .ShouldConnectionBeReplaced(search, connectionFromPreviousVertex, 
                            destinationStopFastestConnection, connection))
                    {

                        // TODO This place is slow, because it gets StartStopTime and EndStopTime
                        fastestConnectionReplacer
                            .ReplaceWithNewFastestConnection(destinationStopFastestConnection, connection);
                    }
                }
                graphService.MarkVertexAsVisited(currentVertex);
                currentVertex = nextVertexResolver.GetNextVertex(vertexFastestConnections);
                if (currentVertex == null)
                    throw new DijkstraNoFastestPathExistsException();
                vertexFastestConnections = graphService.SetTransferConnectionsToSimilarVertices(
                    vertexFastestConnections, currentVertex, currentVertex.SimilarVertices);
                iteration++;
            }            
            return vertexFastestConnections;
        }        
    }
}
