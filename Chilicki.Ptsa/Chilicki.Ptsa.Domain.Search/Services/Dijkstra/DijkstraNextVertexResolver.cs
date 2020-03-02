using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Domain.Search.Aggregates.Graphs;
using System.Collections.Generic;

namespace Chilicki.Ptsa.Domain.Search.Services.Dijkstra
{
    public class DijkstraNextVertexResolver
    {
        readonly DijkstraStopConnectionService dijkstraStopConnectionsService;
        readonly DijkstraStopGraphService dijkstraStopGraphService; 

        public DijkstraNextVertexResolver(
            DijkstraStopConnectionService dijkstraStopConnectionsService,
            DijkstraStopGraphService dijkstraStopGraphService)
        {
            this.dijkstraStopConnectionsService = dijkstraStopConnectionsService;
            this.dijkstraStopGraphService = dijkstraStopGraphService;
        }

        public StopVertex GetFirstVertex(StopGraph graph, Stop startingStop)
        {
            return dijkstraStopGraphService
                .GetStopVertexByStop(graph, startingStop);         
        }

        public StopVertex GetNextVertex(IEnumerable<StopConnection> vertexFastestConnections)
        {
            StopConnection fastestConnection = null;
            foreach (var maybeNewFastestConnection in vertexFastestConnections)
            {
                if (!dijkstraStopConnectionsService.IsConnectionEmpty(maybeNewFastestConnection))
                {
                    if (!maybeNewFastestConnection.EndStopVertex.IsVisited)
                    {
                        if (fastestConnection == null || 
                            fastestConnection.StartStopTime.DepartureTime > 
                                maybeNewFastestConnection.EndStopTime.DepartureTime)
                        {
                            fastestConnection = maybeNewFastestConnection;
                        }                            
                    }
                }
            }
            if (fastestConnection == null)
                return null;
            return fastestConnection.EndStopVertex;
        }
    }
}
