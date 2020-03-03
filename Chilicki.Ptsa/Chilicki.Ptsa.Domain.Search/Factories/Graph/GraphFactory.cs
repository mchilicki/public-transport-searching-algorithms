using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Domain.Search.Services.GraphFactories.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chilicki.Ptsa.Domain.Search.Services.GraphFactories
{
    public class GraphFactory : IGraphFactory<Graph>
    {
        readonly ConnectionFactory connectionFactory;

        public GraphFactory(
            ConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public Graph CreateGraph(IEnumerable<Stop> stops, TimeSpan timeSpan)
        {
            var stopVertices = CreateEmptyVertices(stops);
            stopVertices = FillVerticesWithSimilarVertices(stopVertices, stops);
            stopVertices = FillVerticesWithConnections(stopVertices);
            return new Graph()
            {
                Vertices = stopVertices,
            };
        }

        private IEnumerable<Vertex> CreateEmptyVertices(IEnumerable<Stop> stops)
        {
            var stopVertices = new List<Vertex>();
            foreach (var stop in stops)
            {
                stopVertices.Add(new Vertex()
                {
                    Stop = stop,
                    Connections = new List<Connection>(),
                    IsVisited = false,
                });
            }
            return stopVertices;
        }

        private IEnumerable<Vertex> FillVerticesWithConnections(
            IEnumerable<Vertex> allVertices)
        {
            foreach (var vertex in allVertices)
            {
                var stopConnections = vertex.Connections.ToList();
                foreach (var stopTime in vertex.Stop.StopTimes)
                {
                    var departureStopTime = stopTime.GetTripNextStopTime();
                    if (departureStopTime != null)
                    {
                        var departureVertex = allVertices
                            .Where(p => p.Stop.Id == departureStopTime.Stop.Id)
                            .First();
                        var stopConnection = connectionFactory
                            .Create(vertex, stopTime, departureVertex, departureStopTime);
                        stopConnections.Add(stopConnection);
                    }
                }
                vertex.Connections = stopConnections;
            }
            return allVertices;
        }

        private IEnumerable<Vertex> FillVerticesWithSimilarVertices(
            IEnumerable<Vertex> allVertices, IEnumerable<Stop> stops)
        {
            foreach(var vertex in allVertices)
            {
                var similarVertices = new List<Vertex>();
                var sameStops = stops
                    .Where(p => p.Name == vertex.Stop.Name &&
                        p.Id != vertex.Stop.Id);
                foreach (var sameStop in sameStops)
                {
                    var similarVertex = allVertices
                        .FirstOrDefault(p => p.Stop.Id == sameStop.Id);
                    if (similarVertex != null)
                    {
                        similarVertices.Add(similarVertex);
                    }
                }
                vertex.SimilarVertices = similarVertices;
            }
            return allVertices;
        }
    }
}