using Chilicki.Ptsa.Domain.Search.Aggregates;
using Chilicki.Ptsa.Domain.Search.Aggregates.Graphs;
using Chilicki.Ptsa.Domain.Search.Services.GraphFactories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chilicki.Ptsa.Domain.Search.Factories.Dijkstra
{
    public class DijkstraEmptyFastestConnectionsFactory
    {
        private readonly StopConnectionFactory stopConnectionFactory;

        public DijkstraEmptyFastestConnectionsFactory(
            StopConnectionFactory stopConnectionFactory)
        {
            this.stopConnectionFactory = stopConnectionFactory;
        }

        public IEnumerable<StopConnection> Create(StopGraph graph, SearchInput search)
        {
            var vertexFastestConnections = new List<StopConnection>();
            var startingVertex = graph
                .StopVertices
                .First(p => p.Stop.Id == search.StartStop.Id);
            var startingConnection = stopConnectionFactory
                .Create(startingVertex, null, startingVertex, null);
            vertexFastestConnections.Add(startingConnection);
            foreach (var vertex in graph.StopVertices)
            {
                if (vertex.Stop.Id != search.StartStop.Id)
                {
                    var stopConnection = stopConnectionFactory
                        .Create(null, null, vertex, null);
                    vertexFastestConnections.Add(stopConnection);
                }                
            }
            return vertexFastestConnections;
        }
    }
}
