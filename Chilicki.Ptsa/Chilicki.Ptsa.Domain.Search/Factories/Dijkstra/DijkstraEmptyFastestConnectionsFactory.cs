using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Domain.Search.Aggregates;
using Chilicki.Ptsa.Domain.Search.Services.GraphFactories;
using System.Linq;

namespace Chilicki.Ptsa.Domain.Search.Factories.Dijkstra
{
    public class DijkstraEmptyFastestConnectionsFactory
    {
        private readonly ConnectionFactory factory;

        public DijkstraEmptyFastestConnectionsFactory(
            ConnectionFactory factory)
        {
            this.factory = factory;
        }

        public FastestConnections Create(Graph graph, SearchInput search)
        {
            var fastestConnections = new FastestConnections();
            var startVertex = graph.Vertices
                .First(p => p.StopId == search.StartStop.Id);
            var startConnection = factory
                .CreateStartingConnection(graph, startVertex, search);
            fastestConnections.Add(startVertex.Id, startConnection);
            foreach (var vertex in graph.Vertices)
            {
                if (vertex.StopId != search.StartStop.Id)
                {
                    var connection = factory.CreateEmptyConnection(graph, vertex);
                    fastestConnections.Add(vertex.Id, connection);
                }                
            }
            return fastestConnections;
        }
    }
}
