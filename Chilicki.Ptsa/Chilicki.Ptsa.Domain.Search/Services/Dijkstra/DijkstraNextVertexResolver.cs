using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Domain.Search.Aggregates;
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

        public Vertex GetNextVertex(VertexFastestConnections vertexFastestConnections)
        {
            Connection fastestConnection = null;
            foreach (var (vertexId, maybeNewFastestConnection) in vertexFastestConnections.Dictionary)
            {
                if (!dijkstraConnectionsService.IsConnectionEmpty(maybeNewFastestConnection))
                {
                    if (!maybeNewFastestConnection.EndVertex.IsVisited)
                    {
                        if (fastestConnection == null || 
                            fastestConnection.DepartureTime > 
                                maybeNewFastestConnection.ArrivalTime)
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
