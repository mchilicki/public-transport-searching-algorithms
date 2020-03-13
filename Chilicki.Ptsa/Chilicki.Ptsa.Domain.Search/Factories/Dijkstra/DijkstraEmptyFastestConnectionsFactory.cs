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

        public VertexFastestConnections Create(Graph graph, SearchInput search)
        {
            var vertexFastestConnections = new VertexFastestConnections();
            var startingVertex = graph
                .Vertices
                .First(p => p.StopId == search.StartStop.Id);
            var startingConnection = connectionFactory
                .Create(graph, startingVertex, null, startingVertex, null);
            vertexFastestConnections.Add(startingVertex.Id, startingConnection);
            foreach (var vertex in graph.Vertices)
            {
                if (vertex.StopId != search.StartStop.Id)
                {
                    var connection = connectionFactory
                        .Create(graph, null, null, vertex, null);
                    vertexFastestConnections.Add(vertex.Id, connection);
                }                
            }
            return vertexFastestConnections;
        }
    }
}
