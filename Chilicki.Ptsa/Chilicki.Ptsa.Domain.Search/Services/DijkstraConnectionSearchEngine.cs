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
        readonly DijkstraEmptyFastestConnectionsFactory _dijkstraEmptyFastestConnectionsFactory;
        readonly DijkstraNextVertexResolver _dijkstraNextVertexResolver;
        readonly DijkstraFastestConnectionReplacer _dijkstraFastestConnectionReplacer;
        readonly DijkstraStopConnectionsService _dijkstraStopConnectionsService;
        readonly DijkstraStopGraphService _dijkstraStopGraphService;

        public DijkstraConnectionSearchEngine(
            DijkstraEmptyFastestConnectionsFactory dijkstraEmptyFastestConnectionsArrayFactory,
            DijkstraNextVertexResolver dijkstraNextVertexResolver,
            DijkstraFastestConnectionReplacer dijkstraReplaceFastestConnectionService,
            DijkstraStopConnectionsService dijkstraStopConnectionsService,
            DijkstraStopGraphService dijkstraStopGraphService)
        {
            _dijkstraEmptyFastestConnectionsFactory = dijkstraEmptyFastestConnectionsArrayFactory;
            _dijkstraNextVertexResolver = dijkstraNextVertexResolver;
            _dijkstraFastestConnectionReplacer = dijkstraReplaceFastestConnectionService;
            _dijkstraStopConnectionsService = dijkstraStopConnectionsService;
            _dijkstraStopGraphService = dijkstraStopGraphService;
        }

        public IEnumerable<StopConnection> SearchConnections(SearchInput search, StopGraph graph)
        {
            var vertexFastestConnections = _dijkstraEmptyFastestConnectionsFactory
                .Create(graph, search);
            var currentVertex = _dijkstraNextVertexResolver.GetFirstVertex(graph, search.StartStop);
            vertexFastestConnections = _dijkstraStopGraphService.SetTransferConnectionsToSimilarVertices(
                    vertexFastestConnections, currentVertex, currentVertex.SimilarStopVertices);
            while (ShouldSearchingContinue(search, currentVertex))
            {
                var allStopConnections = _dijkstraStopGraphService.GetConnectionsFromSimilarVertices
                    (currentVertex, currentVertex.SimilarStopVertices);
                foreach (var stopConnection in allStopConnections)
                {
                    var destinationStopFastestConnection = _dijkstraStopConnectionsService
                        .GetDestinationStopFastestConnection(vertexFastestConnections, stopConnection);
                    var stopConnectionFromPreviousVertex = _dijkstraStopConnectionsService
                        .GetStopConnectionFromPreviousVertex(vertexFastestConnections, stopConnection);
                    if (_dijkstraFastestConnectionReplacer
                        .ShouldConnectionBeReplaced(search, stopConnectionFromPreviousVertex, 
                            destinationStopFastestConnection, stopConnection))
                    {
                        _dijkstraFastestConnectionReplacer
                            .ReplaceWithNewFastestConnection(destinationStopFastestConnection, stopConnection);
                    }
                }
                _dijkstraStopGraphService.MarkVertexAsVisited(currentVertex);
                currentVertex = _dijkstraNextVertexResolver.GetNextVertex(vertexFastestConnections);
                if (currentVertex == null)
                    throw new DijkstraNoFastestPathExistsException();
                vertexFastestConnections = _dijkstraStopGraphService.SetTransferConnectionsToSimilarVertices(
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
