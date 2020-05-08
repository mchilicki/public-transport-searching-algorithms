using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Domain.Search.Aggregates.MultipleCriterion;
using Chilicki.Ptsa.Domain.Search.Services.Base;
using Chilicki.Ptsa.Domain.Search.Services.GraphFactories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Priority_Queue;

namespace Chilicki.Ptsa.Domain.Search.Services.MultipleCriterion
{
    public class MultipleCriterionGraphService : GraphService
    {
        private readonly ConnectionFactory connectionFactory;

        public MultipleCriterionGraphService(
            ConnectionFactory connectionFactory) : base(connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public IEnumerable<Connection> GetPossibleConnections(
            Vertex vertex, TimeSpan earliestTime, bool isPreviousConnTransfer = false)
        {
            var connections = new List<Connection>();
            connections.AddRange(GetValidConnections(vertex.Connections, earliestTime));
            AddSimilarVerticesTransfers(vertex, connections, earliestTime, isPreviousConnTransfer);
            return connections.OrderBy(p => p.ArrivalTime);
        }

        private void AddSimilarVerticesTransfers(
            Vertex vertex, ICollection<Connection> connections, TimeSpan earliestTime, bool isPreviousConnTransfer)
        {
            if (!isPreviousConnTransfer)
            {
                foreach (var similar in vertex.SimilarVertices)
                {
                    var transferConn = connectionFactory
                        .CreateTransfer(vertex, similar.Similar, earliestTime, similar.DistanceInMinutes);
                    connections.Add(transferConn);
                }
            }
        }
    }
}
