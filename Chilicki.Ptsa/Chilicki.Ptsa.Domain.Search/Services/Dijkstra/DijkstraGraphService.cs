using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Domain.Search.Aggregates;
using Chilicki.Ptsa.Domain.Search.Services.GraphFactories;
using System.Collections.Generic;
using System.Linq;

namespace Chilicki.Ptsa.Domain.Search.Services.Dijkstra
{
    public class DijkstraGraphService
    {
        readonly ConnectionFactory factory;

        public DijkstraGraphService(
            ConnectionFactory factory)
        {
            this.factory = factory;
        }

        public Vertex GetStopVertexByStop(Graph graph, Stop stop)
        {
            return graph.Vertices
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

        public FastestConnections SetTransferConnectionsToSimilarVertices (
            FastestConnections fastestConnections, 
            Vertex startVertex, 
            IEnumerable<SimilarVertex> similarVertices)
        {
            var connectionToVertex = fastestConnections.Find(startVertex.Id);
            foreach (var similarVertex in similarVertices)
            {
                var similar = fastestConnections.Find(similarVertex.SimilarId);
                factory.FillInZeroCostTransfer(
                    similar, startVertex, similarVertex.Similar, 
                    connectionToVertex.DepartureTime);
            }
            return fastestConnections;
        }

        public IEnumerable<Connection> GetPossibleConnections(Vertex vertex, SearchInput search)
        {
            var connections = new List<Connection>();
            connections.AddRange(
                GetValidConnections(vertex.Connections, search));
            foreach (var similar in vertex.SimilarVertices)
            {
                connections.AddRange(GetValidConnections(similar.Similar.Connections, search));
            }
            return connections;
        }

        private IEnumerable<Connection> GetValidConnections(
            ICollection<Connection> connections, SearchInput search)
        {
            return connections
                .Where(p => p.DepartureTime >= search.StartTime);
        }
    }
}
