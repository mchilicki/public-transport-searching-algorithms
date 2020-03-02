using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Domain.Search.Aggregates.Graphs;
using Chilicki.Ptsa.Domain.Search.Services.GraphFactories.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chilicki.Ptsa.Domain.Search.Services.GraphFactories
{
    public class StopGraphFactory : IGraphFactory<StopGraph>
    {
        readonly StopConnectionFactory stopConnectionFactory;

        public StopGraphFactory(
            StopConnectionFactory stopConnectionFactory)
        {
            this.stopConnectionFactory = stopConnectionFactory;
        }

        public StopGraph CreateGraph(IEnumerable<Stop> stops, TimeSpan timeSpan)
        {
            var stopVertices = GenerateEmptyStopVertices(stops);
            stopVertices = FillStopVerticesWithSimilarStopVertices(stopVertices, stops);
            stopVertices = FillStopVerticesWithStopConnections(stopVertices, stops, timeSpan);
            return new StopGraph()
            {
                StopVertices = stopVertices,
            };
        }

        private IEnumerable<StopVertex> GenerateEmptyStopVertices(IEnumerable<Stop> stops)
        {
            var stopVertices = new List<StopVertex>();
            foreach (var stop in stops)
            {
                stopVertices.Add(new StopVertex()
                {
                    Stop = stop,
                    StopConnections = new List<StopConnection>(),
                    IsVisited = false,
                });
            }
            return stopVertices;
        }

        private IEnumerable<StopVertex> FillStopVerticesWithStopConnections
            (IEnumerable<StopVertex> allStopVertices, IEnumerable<Stop> stops, TimeSpan connectionTimeSpan)
        {
            foreach (var vertex in allStopVertices)
            {
                var stopConnections = vertex.StopConnections.ToList();
                foreach (var stopTime in vertex.Stop.StopTimes)
                {
                    var departureStopTime = stopTime.GetTripNextStopTime();
                    if (departureStopTime != null)
                    {
                        var departureVertex = allStopVertices
                            .Where(p => p.Stop.Id == departureStopTime.Stop.Id)
                            .First();
                        var stopConnection = stopConnectionFactory
                            .Create(vertex, stopTime, departureVertex, departureStopTime);
                        stopConnections.Add(stopConnection);
                    }
                }
                vertex.StopConnections = stopConnections;
            }
            return allStopVertices;
        }

        private IEnumerable<StopVertex> FillStopVerticesWithSimilarStopVertices
            (IEnumerable<StopVertex> allStopVertices, IEnumerable<Stop> stops)
        {
            foreach(var vertex in allStopVertices)
            {
                var similarVertices = new List<StopVertex>();
                var sameStops = stops
                    .Where(p => p.Name == vertex.Stop.Name &&
                        p.Id != vertex.Stop.Id);
                foreach (var sameStop in sameStops)
                {
                    var similarVertex = allStopVertices
                        .FirstOrDefault(p => p.Stop.Id == sameStop.Id);
                    if (similarVertex != null)
                    {
                        similarVertices.Add(similarVertex);
                    }
                }
                vertex.SimilarStopVertices = similarVertices;
            }
            return allStopVertices;
        }
    }
}