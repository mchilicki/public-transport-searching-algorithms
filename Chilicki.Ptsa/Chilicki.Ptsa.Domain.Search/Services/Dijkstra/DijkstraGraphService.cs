using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Domain.Search.Aggregates;
using System;
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

        public VertexFastestConnections SetTransferConnectionsToSimilarVertices (
            VertexFastestConnections vertexFastestConnections, 
            Vertex stopVertex, 
            IEnumerable<SimilarVertex> similarStopVertices)
        {
            var connectionToStopVertex = vertexFastestConnections.Find(stopVertex.Id);
            foreach (var similarVertex in similarStopVertices)
            {
                var similar = vertexFastestConnections.Find(similarVertex.SimilarId);
                similar.StartVertex = stopVertex;
                similar.StartVertexId = stopVertex?.Id;
                similar.StartStopTime = connectionToStopVertex.EndStopTime;
                similar.EndVertex = similarVertex.Similar;
                similar.EndVertexId = similarVertex.SimilarId;
                similar.EndStopTime = connectionToStopVertex.EndStopTime;
                var endTime = similar.EndStopTime != null ? 
                    similar.EndStopTime.DepartureTime : TimeSpan.Zero;
                similar.DepartureTime = endTime;
                similar.ArrivalTime = endTime;
                similar.Trip = null;
                similar.IsTransfer = true;
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
