using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Data.Repositories;
using Chilicki.Ptsa.Data.Repositories.Base;
using Chilicki.Ptsa.Domain.Search.Factories.SimilarVertices;
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
        readonly SimilarVertexFactory similarVertexFactory;
        readonly ISimilarVertexRepository similarVertexRepository;

        public GraphFactory(
            ConnectionFactory connectionFactory,
            IBaseRepository<Connection> connectionRepository,
            IBaseRepository<Vertex> vertexRepository,
            SimilarVertexFactory similarVertexFactory,
            ISimilarVertexRepository similarVertexRepository)
        {
            this.connectionFactory = connectionFactory;
            this.connectionRepository = connectionRepository;
            this.vertexRepository = vertexRepository;
            this.similarVertexFactory = similarVertexFactory;
            this.similarVertexRepository = similarVertexRepository;
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
                var similarVertices = FindSimilarVerticesByName(vertices, vertex);
                if (vertex.SimilarVertices == null)
                    vertex.SimilarVertices = new List<SimilarVertex>();
                foreach (var similarVertex in similarVertices)
                {
                    var similar = similarVertexFactory.Create(vertex, similarVertex);
                    await similarVertexRepository.AddAsync(similar);
                }
            }
        }

        private IEnumerable<Vertex> FindSimilarVerticesByName(
            IEnumerable<Vertex> vertices, Vertex vertex)
        {
            return vertices
                .Where(p => p.StopName == vertex.StopName &&
                    p.Stop.Id != vertex.Stop.Id);
        }
    }
}