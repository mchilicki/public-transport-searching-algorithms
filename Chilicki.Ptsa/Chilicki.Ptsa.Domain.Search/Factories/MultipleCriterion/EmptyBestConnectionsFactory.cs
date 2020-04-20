using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Domain.Search.Aggregates;
using Chilicki.Ptsa.Domain.Search.Aggregates.MultipleCriterion;
using Chilicki.Ptsa.Domain.Search.Services.GraphFactories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chilicki.Ptsa.Domain.Search.Factories.MultipleCriterion
{
    public class EmptyBestConnectionsFactory
    {
        private readonly ConnectionFactory connFactory;
        private readonly LabelFactory labelFactory;

        public EmptyBestConnectionsFactory(
            ConnectionFactory connFactory,
            LabelFactory labelFactory)
        {
            this.connFactory = connFactory;
            this.labelFactory = labelFactory;
        }

        public BestConnections Create(Graph graph)
        {
            var bestConnections = new BestConnections();
            foreach (var vertex in graph.Vertices)
            {
                var labels = labelFactory.CreateEmptyLabels();
                bestConnections.Add(vertex.Id, labels);
            }
            return bestConnections;
        }
    }
}
