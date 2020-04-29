using Chilicki.Ptsa.Data.Configurations.ProjectConfiguration;
using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Data.Repositories.Base;
using Chilicki.Ptsa.Domain.Search.Factories.SimilarVertices;
using Chilicki.Ptsa.Domain.Search.Services.Calculations;
using Chilicki.Ptsa.Domain.Search.Services.GraphFactories.Base;
using Microsoft.Extensions.Options;
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
        private readonly double NEIGHBOUR_MAXIMUM_DISTANCE = 0.5;

        public GraphFactory(
            ConnectionFactory connectionFactory,
            IBaseRepository<Connection> connectionRepository,
            IBaseRepository<Vertex> vertexRepository,
            SimilarVertexFactory similarVertexFactory,
            ISimilarVertexRepository similarVertexRepository,
            HaversineDistanceCalculator distanceCalculator,
            KilometersToDistanceMinutesConverter minutesConverter,
            IOptions<AppSettings> options)
        {
            this.connectionFactory = connectionFactory;
            this.connectionRepository = connectionRepository;
            this.vertexRepository = vertexRepository;
            this.similarVertexFactory = similarVertexFactory;
            this.similarVertexRepository = similarVertexRepository;
            this.distanceCalculator = distanceCalculator;
            this.minutesConverter = minutesConverter;
            NEIGHBOUR_MAXIMUM_DISTANCE = options.Value.SimilarVertexMaximumDistanceInKm;
        }

        public async Task<Graph> CreateGraph(IEnumerable<Stop> stops)
        {
            var graph = new Graph();
            var vertices = CreateEmptyVertices(graph, stops);
            await vertexRepository.AddRangeAsync(vertices);            
            await FillVerticesWithConnections(graph, vertices);
            await FillVerticesWithSimilarVertices(vertices);
            return graph;
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
                    StopName = stop.Name,
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
                var connections = new List<Connection>();
                foreach (var departureStopTime in vertex.Stop.StopTimes)
                {
                    var arrivalStopTime = departureStopTime.GetTripNextStopTime();
                    if (arrivalStopTime != null)
                    {
                        var departureVertex = allVertices
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
            return allVertices;
        }

        private async Task FillVerticesWithSimilarVertices(ICollection<Vertex> vertices)
        {
            foreach (var vertex in vertices)
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

        private IEnumerable<(Vertex Vertex, int DistanceInMinutes)> FindSimilarVerticesByDistance(
            IEnumerable<Vertex> vertices, Vertex currentVertex)
        {
            var list = new List<(Vertex, int)>();
            foreach (var vertex in vertices)
            {
                if (vertex.Stop.Id == currentVertex.Stop.Id)
                    continue;
                var distanceInKm = distanceCalculator.CalculateDistance(vertex, currentVertex);
                if (distanceInKm > NEIGHBOUR_MAXIMUM_DISTANCE)
                    continue;
                var distanceInMinutes = minutesConverter.ConvertToDistanceInMinutes(distanceInKm);
                list.Add((vertex, distanceInMinutes));
            }
            return list;
        }
    }
}