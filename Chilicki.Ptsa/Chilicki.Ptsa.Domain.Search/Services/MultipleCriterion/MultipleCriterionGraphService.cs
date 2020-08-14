using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Domain.Search.Aggregates.MultipleCriterion;
using Chilicki.Ptsa.Domain.Search.Services.Base;
using Chilicki.Ptsa.Domain.Search.Services.GraphFactories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Priority_Queue;
using Chilicki.Ptsa.Domain.Search.Aggregates;
using Chilicki.Ptsa.Domain.Search.Services.SimilarVertices;

namespace Chilicki.Ptsa.Domain.Search.Services.MultipleCriterion
{
    public class MultipleCriterionGraphService : GraphService
    {
        private readonly ConnectionFactory connectionFactory;
        private readonly SimilarVerticesService similarVerticesService;

        public MultipleCriterionGraphService(
            ConnectionFactory connectionFactory,
            SimilarVerticesService similarVerticesService) 
        {
            this.connectionFactory = connectionFactory;
            this.similarVerticesService = similarVerticesService;
        }

        public IEnumerable<Connection> GetPossibleConnections(
            Vertex vertex, SearchInput search, TimeSpan earliestTime, bool isPreviousConnTransfer = false)
        {
            var connections = GetValidConnections(vertex.Connections, earliestTime).ToList();
            AddSimilarVerticesTransfers(vertex, connections, search, earliestTime, isPreviousConnTransfer);
            return connections.OrderBy(p => p.ArrivalTime);
        }

        private void AddSimilarVerticesTransfers(
            Vertex vertex, ICollection<Connection> connections, SearchInput search, TimeSpan earliestTime, bool isPreviousConnTransfer)
        {
            if (!isPreviousConnTransfer)
            {
                var possibleSimilarVertices = similarVerticesService.GetPossibleSimilarVertices(vertex.SimilarVertices, search);
                foreach (var similar in possibleSimilarVertices)
                {                    
                    var transferConn = connectionFactory
                        .CreateTransfer(vertex, similar.Similar, earliestTime, similar.DistanceInMinutes);
                    connections.Add(transferConn);                                       
                }
            }
        }
    }
}
