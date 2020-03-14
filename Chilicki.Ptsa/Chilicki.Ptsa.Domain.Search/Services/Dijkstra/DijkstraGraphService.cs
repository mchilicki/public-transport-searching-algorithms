using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Domain.Search.Aggregates;
using Chilicki.Ptsa.Domain.Search.Services.GraphFactories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chilicki.Ptsa.Domain.Search.Services.Dijkstra
{
    public class DijkstraGraphService
    {
        readonly ConnectionFactory connectionFactory;

        public DijkstraGraphService(
            ConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

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
            Vertex startVertex, 
            IEnumerable<SimilarVertex> similarStopVertices)
        {
            var connectionToStopVertex = vertexFastestConnections.Find(startVertex.Id);
            foreach (var similarVertex in similarStopVertices)
            {
                var similar = vertexFastestConnections.Find(similarVertex.SimilarId);
                connectionFactory.FillInZeroCostTransfer(
                    similar, startVertex, similarVertex.Similar, 
                    connectionToStopVertex.DepartureTime);
            }
            return vertexFastestConnections;
        }

        public IEnumerable<Connection> GetConnectionsFromSimilarVertices(Vertex stopVertex, SearchInput search)
        {
            var allStopConnections = new List<Connection>();
            allStopConnections.AddRange(
                GetValidConnections(stopVertex.Connections, search));
            foreach (var similarVertex in stopVertex.SimilarVertices)
            {
                allStopConnections.AddRange(
                    GetValidConnections(similarVertex.Similar.Connections, search));
            }
            return allStopConnections;
        }

        private IEnumerable<Connection> GetValidConnections(ICollection<Connection> connections, SearchInput search)
        {
            return connections
                .Where(p => p.DepartureTime >= search.StartTime);
        }
    }
}
