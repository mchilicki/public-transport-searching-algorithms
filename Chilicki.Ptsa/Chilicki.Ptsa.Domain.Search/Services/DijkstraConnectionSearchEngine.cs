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
        readonly DijkstraEmptyFastestConnectionsFactory dijkstraEmptyFastestConnectionsFactory;
        readonly DijkstraNextVertexResolver dijkstraNextVertexResolver;
        readonly DijkstraFastestConnectionReplacer dijkstraFastestConnectionReplacer;
        readonly DijkstraConnectionService dijkstraConnectionsService;
        readonly DijkstraGraphService dijkstraGraphService;

        public DijkstraConnectionSearchEngine(
            DijkstraEmptyFastestConnectionsFactory dijkstraEmptyFastestConnectionsFactory,
            DijkstraNextVertexResolver dijkstraNextVertexResolver,
            DijkstraFastestConnectionReplacer dijkstraFastestConnectionReplacer,
            DijkstraConnectionService dijkstraConnectionsService,
            DijkstraGraphService dijkstraGraphService)
        {
            this.dijkstraEmptyFastestConnectionsFactory = dijkstraEmptyFastestConnectionsFactory;
            this.dijkstraNextVertexResolver = dijkstraNextVertexResolver;
            this.dijkstraFastestConnectionReplacer = dijkstraFastestConnectionReplacer;
            this.dijkstraConnectionsService = dijkstraConnectionsService;
            this.dijkstraGraphService = dijkstraGraphService;
        }

        public IEnumerable<Connection> SearchConnections(SearchInput search, Graph graph)
        {
            var vertexFastestConnections = dijkstraEmptyFastestConnectionsFactory
                .Create(graph, search);
            var currentVertex = dijkstraNextVertexResolver.GetFirstVertex(graph, search.StartStop);
            vertexFastestConnections = dijkstraGraphService.SetTransferConnectionsToSimilarVertices(
                vertexFastestConnections, currentVertex, currentVertex.SimilarVertices);
            var interationCount = 1;
            while (ShouldSearchingContinue(search, currentVertex))
            {
                var allConnections = dijkstraGraphService.GetConnectionsFromSimilarVertices
                    (currentVertex);
                foreach (var connection in allConnections)
                {
                    var destinationStopFastestConnection = dijkstraConnectionsService
                        .GetDestinationStopFastestConnection(vertexFastestConnections, connection);
                    var connectionFromPreviousVertex = dijkstraConnectionsService
                        .GetConnectionFromPreviousVertex(vertexFastestConnections, connection);
                    if (dijkstraFastestConnectionReplacer
                        .ShouldConnectionBeReplaced(search, connectionFromPreviousVertex, 
                            destinationStopFastestConnection, connection))
                    {
                        dijkstraFastestConnectionReplacer
                            .ReplaceWithNewFastestConnection(destinationStopFastestConnection, connection);
                    }
                }
                dijkstraGraphService.MarkVertexAsVisited(currentVertex);
                currentVertex = dijkstraNextVertexResolver.GetNextVertex(vertexFastestConnections);
                if (currentVertex == null)
                    throw new DijkstraNoFastestPathExistsException();
                vertexFastestConnections = dijkstraGraphService.SetTransferConnectionsToSimilarVertices(
                    vertexFastestConnections, currentVertex, currentVertex.SimilarVertices);
                interationCount++;
            }            
            return vertexFastestConnections;
        }

        private bool ShouldSearchingContinue(SearchInput search, Vertex currentVertex)
        {
            bool isCurrentVertexDestinationStop = currentVertex != null &&
                currentVertex.Stop.Id == search.DestinationStop.Id;
            if (isCurrentVertexDestinationStop == true)
                return false;
            foreach (var similarVertex in currentVertex.SimilarVertices)
            {
                if (similarVertex.Stop.Id == search.DestinationStop.Id)
                    return false;
            }
            return true;
        }
    }
}
