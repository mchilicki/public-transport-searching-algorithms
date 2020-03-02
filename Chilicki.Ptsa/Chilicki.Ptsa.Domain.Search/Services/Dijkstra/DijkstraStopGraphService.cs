using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Domain.Search.Aggregates.Graphs;
using System.Collections.Generic;
using System.Linq;

namespace Chilicki.Ptsa.Domain.Search.Services.Dijkstra
{
    public class DijkstraStopGraphService
    {
        public StopVertex GetStopVertexByStop(StopGraph graph, Stop stop)
        {
            return graph
                .StopVertices
                .First(p => p.Stop.Id == stop.Id);
        }

        public StopVertex MarkVertexAsVisited(StopVertex stopVertex)
        {
            stopVertex.IsVisited = true;
            foreach (var similarStopVertex in stopVertex.SimilarStopVertices)
            {
                similarStopVertex.IsVisited = true;
            }
            return stopVertex;
        }

        public IEnumerable<StopConnection> SetTransferConnectionsToSimilarVertices (
            IEnumerable<StopConnection> vertexFastestConnections, 
            StopVertex stopVertex, 
            IEnumerable<StopVertex> similarStopVertices)
        {
            var connectionToStopVertex = vertexFastestConnections
                    .FirstOrDefault(p => p.EndStopVertex.Stop.Id == stopVertex.Stop.Id);
            foreach (var similarVertex in similarStopVertices)
            {
                var similarVertexFastestConnection = vertexFastestConnections
                    .FirstOrDefault(p => p.EndStopVertex.Stop.Id == similarVertex.Stop.Id);
                similarVertexFastestConnection.SourceStopVertex = stopVertex;
                similarVertexFastestConnection.StartStopTime = connectionToStopVertex.EndStopTime;
                similarVertexFastestConnection.EndStopTime = connectionToStopVertex.EndStopTime;
                similarVertexFastestConnection.Trip = null;
                similarVertexFastestConnection.IsTransfer = true;
            }
            return vertexFastestConnections;
        }

        public IEnumerable<StopConnection> GetConnectionsFromSimilarVertices(StopVertex stopVertex, IEnumerable<StopVertex> similarStopVertices)
        {
            var allStopConnections = new List<StopConnection>();
            allStopConnections.AddRange(stopVertex.StopConnections);
            foreach (var similarVertex in stopVertex.SimilarStopVertices)
            {
                allStopConnections.AddRange(similarVertex.StopConnections);
            }
            return allStopConnections;
        }
    }
}
