﻿using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Data.Repositories;
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

        // TODO
        // Correct SimilarVertices saving to database

        public async Task<Graph> CreateGraph(IEnumerable<Stop> stops)
        {
            var graph = new Graph();
            var vertices = CreateEmptyVertices(graph, stops);
            vertices = await FillVerticesWithConnections(graph, vertices);
            await FillVerticesWithSimilarVertices(graph);
            await vertexRepository.AddRangeAsync(vertices);
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
                foreach (var stopTime in vertex.Stop.StopTimes)
                {
                    var departureStopTime = stopTime.GetTripNextStopTime();
                    if (departureStopTime != null)
                    {
                        var departureVertex = allVertices
                            .SingleOrDefault(p => p.Stop.Id == departureStopTime.Stop.Id);
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

        private async Task FillVerticesWithSimilarVertices(Graph graph)
        {
            foreach (var vertex in graph.Vertices)
            {
                var readySimilarVertices = new List<SimilarVertex>();
                var similarVertices = FindSimilarVerticesByName(graph.Vertices, vertex);
                foreach (var similarVertex in similarVertices)
                {
                    var similar = similarVertexFactory.Create(vertex, similarVertex);
                    readySimilarVertices.Add(similar);
                }
                vertex.SimilarVertices = readySimilarVertices;
                await similarVertexRepository.AddRangeAsync(readySimilarVertices);
            }
        }

        private IEnumerable<Vertex> FindSimilarVerticesByName(
            IEnumerable<Vertex> vertices, Vertex vertex)
        {
            return vertices
                .Where(p => p.StopName == vertex.StopName &&
                    p.Id != vertex.Id);
        }
    }
}