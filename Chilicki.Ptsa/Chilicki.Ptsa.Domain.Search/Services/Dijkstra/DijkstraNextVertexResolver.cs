using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Domain.Search.Aggregates;

namespace Chilicki.Ptsa.Domain.Search.Services.Dijkstra
{
    public class DijkstraNextVertexResolver
    {
        readonly DijkstraConnectionService connService;
        readonly DijkstraGraphService graphService; 

        public DijkstraNextVertexResolver(
            DijkstraConnectionService connService,
            DijkstraGraphService graphService)
        {
            this.connService = connService;
            this.graphService = graphService;
        }

        public Vertex GetFirstVertex(Graph graph, Stop startStop)
        {
            return graphService
                .GetStopVertexByStop(graph, startStop);         
        }

        public Vertex GetNextVertex(FastestConnections fastestConnections)
        {
            Connection fastestConnection = null;
            foreach (var (vertexId, possibleConn) in fastestConnections.Dictionary)
            {
                if (!connService.IsConnectionEmpty(possibleConn))
                {
                    if (!possibleConn.EndVertex.IsVisited)
                    {
                        if (fastestConnection == null || 
                            fastestConnection.DepartureTime > 
                                possibleConn.ArrivalTime)
                        {
                            fastestConnection = possibleConn;
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
