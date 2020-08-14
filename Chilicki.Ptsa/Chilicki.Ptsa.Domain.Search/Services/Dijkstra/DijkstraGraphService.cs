using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Domain.Search.Aggregates;
using Chilicki.Ptsa.Domain.Search.Services.Base;
using Chilicki.Ptsa.Domain.Search.Services.GraphFactories;
using Chilicki.Ptsa.Domain.Search.Services.SimilarVertices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chilicki.Ptsa.Domain.Search.Services.Dijkstra
{
    public class DijkstraGraphService : GraphService
    {
        private readonly ConnectionFactory factory;
        private readonly DijkstraFastestConnectionReplacer replacer;
        private readonly SimilarVerticesService similarVerticesService;

        public DijkstraGraphService(
            ConnectionFactory factory,
            DijkstraFastestConnectionReplacer replacer,
            SimilarVerticesService similarVerticesService)
        {
            this.factory = factory;
            this.replacer = replacer;
            this.similarVerticesService = similarVerticesService;
        }        

        public Vertex MarkVertexAsVisited(Vertex vertex, SearchInput search)
        {
            vertex.IsVisited = true;
            var possibleSimilarVertices = similarVerticesService.GetPossibleSimilarVertices(vertex.SimilarVertices, search);
            foreach (var similarStopVertex in possibleSimilarVertices)
            {
                similarStopVertex.Similar.IsVisited = true;
            }
            return vertex;
        }

        public FastestConnections SetTransferConnectionsToSimilarVertices(
            SearchInput search,
            FastestConnections fastestConnections, 
            Vertex vertex, 
            IEnumerable<SimilarVertex> similarVertices)
        {
            var connectionToVertex = fastestConnections.Find(vertex.Id);
            foreach (var similarVertex in similarVertices)
            {
                var currentSimilar = fastestConnections.Find(similarVertex.SimilarId);
                var possibleSimilar = factory.CreateTransfer(vertex, similarVertex.Similar, 
                    connectionToVertex.ArrivalTime, similarVertex.DistanceInMinutes);
                if (replacer.ShouldConnectionBeReplaced(search, fastestConnections, currentSimilar, possibleSimilar))
                {
                    replacer.ReplaceWithNewFastestConnection(currentSimilar, possibleSimilar);
                }
            }
            return fastestConnections;
        }

        public IEnumerable<Connection> GetPossibleConnections(Vertex vertex, SearchInput search, TimeSpan earliestTime)
        {
            var connections = new List<Connection>();
            connections.AddRange(GetValidConnections(vertex.Connections, earliestTime));
            AddSimilarVerticesTransfers(vertex, connections, search, earliestTime);
            return connections.OrderBy(p => p.ArrivalTime);
        }

        private void AddSimilarVerticesTransfers(
            Vertex vertex, List<Connection> connections, SearchInput search, TimeSpan earliestTime)
        {
            var possibleSimilarVertices = similarVerticesService.GetPossibleSimilarVertices(vertex.SimilarVertices, search);
            foreach (var similar in possibleSimilarVertices)
            {
                var earliestTimeAfterTransfer = earliestTime.Add(TimeSpan.FromMinutes(similar.DistanceInMinutes));
                var similarConns = GetValidConnections(similar.Similar.Connections, earliestTimeAfterTransfer);
                connections.AddRange(similarConns);
            }
        }
    }
}
