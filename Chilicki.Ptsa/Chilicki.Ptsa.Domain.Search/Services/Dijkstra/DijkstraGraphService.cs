using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Domain.Search.Aggregates;
using Chilicki.Ptsa.Domain.Search.Services.Base;
using Chilicki.Ptsa.Domain.Search.Services.GraphFactories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chilicki.Ptsa.Domain.Search.Services.Dijkstra
{
    public class DijkstraGraphService : GraphService
    {
        private readonly ConnectionFactory factory;
        private readonly DijkstraFastestConnectionReplacer replacer;

        public DijkstraGraphService(
            ConnectionFactory factory,
            DijkstraFastestConnectionReplacer replacer) : base(factory)
        {
            this.factory = factory;
            this.replacer = replacer;
        }        

        public Vertex MarkVertexAsVisited(Vertex stopVertex)
        {
            stopVertex.IsVisited = true;
            foreach (var similarStopVertex in stopVertex.SimilarVertices)
            {
                similarStopVertex.Similar.IsVisited = true;
            }
            return stopVertex;
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
            if (earliestTime == TimeSpan.Zero)
            {
                earliestTime = search.StartTime;
            }
            connections.AddRange(GetValidConnections(vertex.Connections, search.StartTime));
            AddSimilarVerticesTransfers(vertex, connections, search.StartTime);
            TimeSpan latestTime;
            if (earliestTime == search.StartTime)
            {
                latestTime = new TimeSpan(23, 59, 59);
            }
            else
            {
                latestTime = earliestTime.Add(TimeSpan.FromMinutes(search.Parameters.MaxTimeAheadFetchingPossibleConnections));
            }            
            var possibleConnections = connections.Where(p => p.DepartureTime <= latestTime);
            if (possibleConnections.Count() >= search.Parameters.MinimumPossibleConnectionsFetched)
                return possibleConnections.OrderBy(p => p.ArrivalTime);
            return connections.Take(search.Parameters.MinimumPossibleConnectionsFetched).OrderBy(p => p.ArrivalTime);
        }

        private void AddSimilarVerticesTransfers(
            Vertex vertex, List<Connection> connections, TimeSpan earliestTime)
        {
            foreach (var similar in vertex.SimilarVertices)
            {
                var earliestTimeAfterTransfer = earliestTime.Add(TimeSpan.FromMinutes(similar.DistanceInMinutes));
                var similarConns = GetValidConnections(similar.Similar.Connections, earliestTimeAfterTransfer);
                connections.AddRange(similarConns);
            }
        }
    }
}
