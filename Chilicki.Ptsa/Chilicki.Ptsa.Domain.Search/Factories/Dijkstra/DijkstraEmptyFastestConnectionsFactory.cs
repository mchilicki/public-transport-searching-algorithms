using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Domain.Search.Aggregates;
using Chilicki.Ptsa.Domain.Search.Services.GraphFactories;
using System.Collections.Generic;
using System.Linq;

namespace Chilicki.Ptsa.Domain.Search.Factories.Dijkstra
{
    public class DijkstraEmptyFastestConnectionsFactory
    {
        private readonly ConnectionFactory connectionFactory;

        public DijkstraEmptyFastestConnectionsFactory(
            ConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public IEnumerable<Connection> Create(Graph graph, SearchInput search)
        {
            var vertexFastestConnections = new List<Connection>();
            var startingVertex = graph
                .Vertices
                .First(p => p.Stop.Id == search.StartStop.Id);
            var startingConnection = connectionFactory
                .Create(graph, startingVertex, null, startingVertex, null);
            vertexFastestConnections.Add(startingConnection);
            foreach (var vertex in graph.Vertices)
            {
                if (vertex.Stop.Id != search.StartStop.Id)
                {
                    var connection = connectionFactory
                        .Create(graph, null, null, vertex, null);
                    vertexFastestConnections.Add(connection);
                }                
            }
            return vertexFastestConnections;
        }
    }
}
