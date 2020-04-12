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

        public BestConnections Create(
            Graph graph, SearchInput search)
        {
            var bestConnections = new BestConnections();
            var (startVertex, startLabels) = GetStartVertexAndEmptyLabels(graph, search);
            bestConnections.Add(startVertex.Id, startLabels);
            foreach (var vertex in graph.Vertices)
            {
                if (vertex.StopId != search.StartStop.Id)
                {
                    var connection = connFactory.CreateEmptyConnection(graph, vertex);
                    var labels = labelFactory.CreateEmptyLabels();
                    bestConnections.Add(vertex.Id, labels);
                }
            }
            return bestConnections;
        }

        private (Vertex, ICollection<Label>) GetStartVertexAndEmptyLabels(
            Graph graph, SearchInput search)
        {
            var startVertex = graph.Vertices
                .First(p => p.StopId == search.StartStop.Id);
            var startConnection = connFactory
                .CreateStartingConnection(graph, startVertex, search);
            var labels = labelFactory.CreateEmptyLabels();
            return (startVertex, labels);
        }
    }
}
