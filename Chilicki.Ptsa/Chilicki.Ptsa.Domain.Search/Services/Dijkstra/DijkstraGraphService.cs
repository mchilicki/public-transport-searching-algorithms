using Chilicki.Ptsa.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Chilicki.Ptsa.Domain.Search.Services.Dijkstra
{
    public class DijkstraGraphService
    {
        public Vertex GetStopVertexByStop(Graph graph, Stop stop)
        {
            return graph
                .Vertices
                .First(p => p.StopId == stop.Id);
        }

        public Vertex MarkVertexAsVisited(Vertex stopVertex)
        {
            stopVertex.IsVisited = true;
            foreach (var similarStopVertex in stopVertex.SimilarVertices)
            {
                similarStopVertex.Similar.IsVisited = true;
            }
            return stopVertex;
        }

        public IEnumerable<Connection> SetTransferConnectionsToSimilarVertices (
            IEnumerable<Connection> vertexFastestConnections, 
            Vertex stopVertex, 
            IEnumerable<SimilarVertex> similarStopVertices)
        {
            var connectionToStopVertex = vertexFastestConnections
                    .FirstOrDefault(p => p.EndVertex.StopId == stopVertex.StopId);
            foreach (var similarVertex in similarStopVertices)
            {
                var similarVertexFastestConnection = vertexFastestConnections
                    .FirstOrDefault(p => p.EndVertex.StopId == similarVertex.Similar.StopId);
                similarVertexFastestConnection.StartVertex = stopVertex;
                similarVertexFastestConnection.StartStopTime = connectionToStopVertex.EndStopTime;
                similarVertexFastestConnection.EndStopTime = connectionToStopVertex.EndStopTime;
                similarVertexFastestConnection.DepartureTime = similarVertexFastestConnection.EndStopTime.DepartureTime;
                similarVertexFastestConnection.ArrivalTime = similarVertexFastestConnection.EndStopTime.DepartureTime;
                similarVertexFastestConnection.Trip = null;
                similarVertexFastestConnection.IsTransfer = true;
            }
            return vertexFastestConnections;
        }

        public IEnumerable<Connection> GetConnectionsFromSimilarVertices(Vertex stopVertex)
        {
            var allStopConnections = new List<Connection>();
            allStopConnections.AddRange(stopVertex.Connections);
            foreach (var similarVertex in stopVertex.SimilarVertices)
            {
                allStopConnections.AddRange(similarVertex.Similar.Connections);
            }
            return allStopConnections;
        }
    }
}
