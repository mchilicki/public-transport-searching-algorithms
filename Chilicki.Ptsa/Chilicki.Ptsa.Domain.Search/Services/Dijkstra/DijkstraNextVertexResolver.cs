using Chilicki.Ptsa.Data.Entities;
using System.Collections.Generic;

namespace Chilicki.Ptsa.Domain.Search.Services.Dijkstra
{
    public class DijkstraNextVertexResolver
    {
        readonly DijkstraConnectionService dijkstraConnectionsService;
        readonly DijkstraGraphService dijkstraGraphService; 

        public DijkstraNextVertexResolver(
            DijkstraConnectionService dijkstraConnectionsService,
            DijkstraGraphService dijkstraGraphService)
        {
            this.dijkstraConnectionsService = dijkstraConnectionsService;
            this.dijkstraGraphService = dijkstraGraphService;
        }

        public Vertex GetFirstVertex(Graph graph, Stop startingStop)
        {
            return dijkstraGraphService
                .GetStopVertexByStop(graph, startingStop);         
        }

        public Vertex GetNextVertex(IEnumerable<Connection> vertexFastestConnections)
        {
            Connection fastestConnection = null;
            foreach (var maybeNewFastestConnection in vertexFastestConnections)
            {
                if (!dijkstraConnectionsService.IsConnectionEmpty(maybeNewFastestConnection))
                {
                    if (!maybeNewFastestConnection.EndVertex.IsVisited)
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
            return fastestConnection.EndVertex;
        }
    }
}
