using Chilicki.Ptsa.Domain.Search.Aggregates;
using Chilicki.Ptsa.Domain.Search.Aggregates.Graphs;
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
        readonly DijkstraStopConnectionService dijkstraStopConnectionsService;
        readonly DijkstraStopGraphService dijkstraStopGraphService;

        public DijkstraConnectionSearchEngine(
            DijkstraEmptyFastestConnectionsFactory dijkstraEmptyFastestConnectionsArrayFactory,
            DijkstraNextVertexResolver dijkstraNextVertexResolver,
            DijkstraFastestConnectionReplacer dijkstraReplaceFastestConnectionService,
            DijkstraStopConnectionService dijkstraStopConnectionsService,
            DijkstraStopGraphService dijkstraStopGraphService)
        {
            dijkstraEmptyFastestConnectionsFactory = dijkstraEmptyFastestConnectionsArrayFactory;
            this.dijkstraNextVertexResolver = dijkstraNextVertexResolver;
            dijkstraFastestConnectionReplacer = dijkstraReplaceFastestConnectionService;
            this.dijkstraStopConnectionsService = dijkstraStopConnectionsService;
            this.dijkstraStopGraphService = dijkstraStopGraphService;
        }

        public IEnumerable<StopConnection> SearchConnections(SearchInput search, StopGraph graph)
        {
            var vertexFastestConnections = dijkstraEmptyFastestConnectionsFactory
                .Create(graph, search);
            var currentVertex = dijkstraNextVertexResolver.GetFirstVertex(graph, search.StartStop);
            vertexFastestConnections = dijkstraStopGraphService.SetTransferConnectionsToSimilarVertices(
                    vertexFastestConnections, currentVertex, currentVertex.SimilarStopVertices);
            while (ShouldSearchingContinue(search, currentVertex))
            {
                var allStopConnections = dijkstraStopGraphService.GetConnectionsFromSimilarVertices
                    (currentVertex, currentVertex.SimilarStopVertices);
                foreach (var stopConnection in allStopConnections)
                {
                    var destinationStopFastestConnection = dijkstraStopConnectionsService
                        .GetDestinationStopFastestConnection(vertexFastestConnections, stopConnection);
                    var stopConnectionFromPreviousVertex = dijkstraStopConnectionsService
                        .GetStopConnectionFromPreviousVertex(vertexFastestConnections, stopConnection);
                    if (dijkstraFastestConnectionReplacer
                        .ShouldConnectionBeReplaced(search, stopConnectionFromPreviousVertex, 
                            destinationStopFastestConnection, stopConnection))
                    {
                        dijkstraFastestConnectionReplacer
                            .ReplaceWithNewFastestConnection(destinationStopFastestConnection, stopConnection);
                    }
                }
                dijkstraStopGraphService.MarkVertexAsVisited(currentVertex);
                currentVertex = dijkstraNextVertexResolver.GetNextVertex(vertexFastestConnections);
                if (currentVertex == null)
                    throw new DijkstraNoFastestPathExistsException();
                vertexFastestConnections = dijkstraStopGraphService.SetTransferConnectionsToSimilarVertices(
                    vertexFastestConnections, currentVertex, currentVertex.SimilarStopVertices);                
            }            
            return vertexFastestConnections;
        }

        private bool ShouldSearchingContinue(SearchInput search, StopVertex currentVertex)
        {
            bool isCurrentVertexDestinationStop = currentVertex != null &&
                currentVertex.Stop.Id == search.DestinationStop.Id;
            if (isCurrentVertexDestinationStop == true)
                return false;
            foreach (var similarVertex in currentVertex.SimilarStopVertices)
            {
                if (similarVertex.Stop.Id == search.DestinationStop.Id)
                    return false;
            }
            return true;
        }
    }
}
