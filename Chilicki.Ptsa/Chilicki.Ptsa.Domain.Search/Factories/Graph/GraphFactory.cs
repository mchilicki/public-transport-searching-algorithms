using Chilicki.Ptsa.Data.Configurations.ProjectConfiguration;
using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Data.Repositories.Base;
using Chilicki.Ptsa.Domain.Search.Factories.SimilarVertices;
using Chilicki.Ptsa.Domain.Search.Services.Calculations;
using Chilicki.Ptsa.Domain.Search.Services.GraphFactories.Base;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
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
        readonly SimilarVertexFactory similarVertexFactory;
        readonly ISimilarVertexRepository similarVertexRepository;
        private readonly HaversineDistanceCalculator distanceCalculator;
        private readonly KilometersToDistanceMinutesConverter minutesConverter;
        private readonly double SimilarVertexMaximalDistanceInKm;

        public GraphFactory(
            ConnectionFactory connectionFactory,
            IBaseRepository<Connection> connectionRepository,
            IBaseRepository<Vertex> vertexRepository,
            SimilarVertexFactory similarVertexFactory,
            ISimilarVertexRepository similarVertexRepository,
            HaversineDistanceCalculator distanceCalculator,
            KilometersToDistanceMinutesConverter minutesConverter,
            IOptions<GraphCreationSettings> options)
        {
            this.connectionFactory = connectionFactory;
            this.connectionRepository = connectionRepository;
            this.vertexRepository = vertexRepository;
            this.similarVertexFactory = similarVertexFactory;
            this.similarVertexRepository = similarVertexRepository;
            this.distanceCalculator = distanceCalculator;
            this.minutesConverter = minutesConverter;
            SimilarVertexMaximalDistanceInKm = options.Value.SimilarVertexMaximumDistanceInKm;
        }

        public async Task<Graph> CreateGraph(IEnumerable<Stop> stops)
        {
            Console.WriteLine($"Started creating graph");
            var graph = new Graph();
            Console.WriteLine($"Started creating vertices");
            var vertices = await CreateEmptyVertices(graph, stops);
            await vertexRepository.AddRangeAsync(vertices);
            Console.WriteLine($"Vertices created: {vertices.Count}");
            Console.WriteLine($"Started creating connections");
            await FillVerticesWithConnections(graph, vertices);
            Console.WriteLine($"Connections created: {vertices.Sum(p => p.Connections.Count)}");
            Console.WriteLine($"Started creating similar vertices");
            await FillVerticesWithSimilarVertices(vertices);
            Console.WriteLine($"Similar vertices created: {vertices.Sum(p => p.SimilarVertices.Count)}");
            return graph;
        }
        
        private async Task<ConcurrentBag<Vertex>> CreateEmptyVertices(
            Graph graph, IEnumerable<Stop> stops)
        {
            Console.WriteLine($"Creating tasks for vertices");
            var vertices = new ConcurrentBag<Vertex>();
            var tasks = new List<Task>();
            foreach (var stop in stops)
            {
                Task task = AddVertexToList(graph, vertices, stop);
                tasks.Add(task);
            }
            Console.WriteLine($"Executing vertices tasks");
            await Task.WhenAll(tasks);
            return vertices;

            async Task AddVertexToList(Graph graph, ConcurrentBag<Vertex> vertices, Stop stop)
            {
                vertices.Add(new Vertex()
                {
                    Graph = graph,
                    Stop = stop,
                    StopName = stop.Name,
                    Connections = new List<Connection>(),
                    IsVisited = false,
                });
            }
        }

        private async Task<ConcurrentBag<Vertex>> FillVerticesWithConnections(
            Graph graph, ConcurrentBag<Vertex> vertices)
        {
            Console.WriteLine($"Creating tasks for connections");
            var tasks = new List<Task>();
            foreach (var vertex in vertices)
            {
                Task task = FillVertexWithConnections(graph, vertices, vertex);
                tasks.Add(task);
            }
            Console.WriteLine($"Executing connections tasks");
            await Task.WhenAll(tasks);
            return vertices;

            async Task FillVertexWithConnections(Graph graph, ConcurrentBag<Vertex> vertices, Vertex vertex)
            {
                var connections = new List<Connection>();
                foreach (var departureStopTime in vertex.Stop.StopTimes)
                {
                    var arrivalStopTime = departureStopTime.GetTripNextStopTime();
                    if (arrivalStopTime != null)
                    {
                        var departureVertex = vertices
                            .SingleOrDefault(p => p.Stop.Id == arrivalStopTime.Stop.Id);
                        var connection = connectionFactory
                            .CreateConnection(graph, departureStopTime.Trip.Id, vertex,
                                departureStopTime.DepartureTime, departureVertex,
                                arrivalStopTime.DepartureTime);
                        connections.Add(connection);
                    }
                }
                vertex.Connections = connections;
                await connectionRepository.AddRangeAsync(connections);
            }
        }

        private async Task FillVerticesWithSimilarVertices(ConcurrentBag<Vertex> vertices)
        {
            Console.WriteLine($"Creating tasks for similar vertices");
            var tasks = new List<Task>();
            foreach (var vertex in vertices)
            {
                var task = FillVertexWithSimilarVertices(vertices, vertex);
                tasks.Add(task);
            }
            Console.WriteLine($"Executing similar vertices tasks");
            await Task.WhenAll(tasks);

            async Task FillVertexWithSimilarVertices(ConcurrentBag<Vertex> vertices, Vertex vertex)
            {
                var similarVertices = FindSimilarVerticesByDistance(vertices, vertex);
                if (vertex.SimilarVertices == null)
                    vertex.SimilarVertices = new List<SimilarVertex>();
                foreach (var similarVertex in similarVertices)
                {
                    var similar = similarVertexFactory.Create(
                        vertex, similarVertex.Vertex, similarVertex.DistanceInMinutes);
                    await similarVertexRepository.AddAsync(similar);
                }
            }
        }

        private ConcurrentBag<(Vertex Vertex, int DistanceInMinutes)> FindSimilarVerticesByDistance(
            ConcurrentBag<Vertex> vertices, Vertex currentVertex)
        {
            var list = new ConcurrentBag<(Vertex, int)>();
            foreach (var vertex in vertices)
            {
                if (vertex.Stop.Id == currentVertex.Stop.Id)
                    continue;
                var distanceInKm = distanceCalculator.CalculateDistance(vertex, currentVertex);
                if (distanceInKm > SimilarVertexMaximalDistanceInKm)
                    continue;
                var distanceInMinutes = minutesConverter.ConvertToDistanceInMinutes(distanceInKm);
                list.Add((vertex, distanceInMinutes));
            }
            return list;
        }
    }
}