using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Data.Repositories;
using Chilicki.Ptsa.Domain.Search.Services.GraphFactories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chilicki.Ptsa.Domain.Search.Services.GraphFactories
{
    public class GraphFactory : IGraphFactory<Graph>
    {
        readonly ConnectionFactory connectionFactory;
        readonly IBaseRepository<Connection> connectionRepository;
        readonly IBaseRepository<Vertex> vertexRepository;

        public GraphFactory(
            ConnectionFactory connectionFactory,
            IBaseRepository<Connection> connectionRepository,
            IBaseRepository<Vertex> vertexRepository)
        {
            this.connectionFactory = connectionFactory;
            this.connectionRepository = connectionRepository;
            this.vertexRepository = vertexRepository;
        }

        public async Task<Graph> CreateGraph(IEnumerable<Stop> stops)
        {
            var graph = new Graph();
            var vertices = CreateEmptyVertices(graph, stops);
            vertices = await FillVerticesWithConnections(graph, vertices);
            await vertexRepository.AddRangeAsync(vertices);
            return graph;
        }

        public void FillVerticesWithSimilarVertices(
            Graph graph, IEnumerable<Stop> stops)
        {
            foreach (var vertex in graph.Vertices)
            {
                var similarVertices = new List<Vertex>();
                var sameStops = stops
                    .Where(p => p.Name == vertex.Stop.Name &&
                        p.Id != vertex.Stop.Id);
                foreach (var sameStop in sameStops)
                {
                    var similarVertex = graph.Vertices
                        .FirstOrDefault(p => p.Stop.Id == sameStop.Id);
                    if (similarVertex != null)
                    {
                        similarVertices.Add(similarVertex);
                    }
                }
                vertex.SimilarVertices = similarVertices;
            }
        }

        private ICollection<Vertex> CreateEmptyVertices(
            Graph graph, IEnumerable<Stop> stops)
        {
            var stopVertices = new List<Vertex>();
            foreach (var stop in stops)
            {
                stopVertices.Add(new Vertex()
                {
                    Graph = graph,
                    Stop = stop,
                    Connections = new List<Connection>(),
                    IsVisited = false,
                });
            }
            return stopVertices;
        }

        private async Task<ICollection<Vertex>> FillVerticesWithConnections(
            Graph graph, ICollection<Vertex> allVertices)
        {
            foreach (var vertex in allVertices)
            {
                var connections = vertex.Connections.ToList();
                foreach (var stopTime in vertex.Stop.StopTimes)
                {
                    var departureStopTime = stopTime.GetTripNextStopTime();
                    if (departureStopTime != null)
                    {
                        var departureVertex = allVertices
                            .Where(p => p.Stop.Id == departureStopTime.Stop.Id)
                            .First();
                        var connection = connectionFactory
                            .Create(graph, vertex, stopTime, departureVertex, departureStopTime);
                        connections.Add(connection);
                    }
                }
                vertex.Connections = connections;
                await connectionRepository.AddRangeAsync(connections);
            }
            return allVertices;
        }        
    }
}