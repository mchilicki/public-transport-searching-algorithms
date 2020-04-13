using Chilicki.Ptsa.Domain.Search.Aggregates;
using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Domain.Search.Helpers.Exceptions;
using Chilicki.Ptsa.Domain.Search.Factories.Dijkstra;
using Chilicki.Ptsa.Domain.Search.Services.Base;
using Chilicki.Ptsa.Domain.Search.Services.Dijkstra;
using Chilicki.Ptsa.Domain.Search.Aggregates.MultipleCriterion;
using System;
using Chilicki.Ptsa.Domain.Search.Factories.MultipleCriterion;
using Chilicki.Ptsa.Domain.Search.Services.MultipleCriterion;
using System.Linq;

namespace Chilicki.Ptsa.Domain.Search.Services
{
    public class MultipleCriterionDijkstraSearch 
    {
        private readonly EmptyBestConnectionsFactory emptyBestFactory;
        private readonly MultipleCriterionGraphService graphService;
        private readonly LabelPriorityQueueFactory priorityQueueFactory;
        private readonly PossibleConnectionsService connectionsService;
        private readonly LabelFactory labelFactory;
        private readonly DominationService dominationService;

        public MultipleCriterionDijkstraSearch(
            EmptyBestConnectionsFactory emptyBestFactory,
            MultipleCriterionGraphService graphService,
            LabelPriorityQueueFactory priorityQueueFactory,
            PossibleConnectionsService connectionsService,
            LabelFactory labelFactory,
            DominationService dominationService)
        {
            this.emptyBestFactory = emptyBestFactory;
            this.graphService = graphService;
            this.priorityQueueFactory = priorityQueueFactory;
            this.connectionsService = connectionsService;
            this.labelFactory = labelFactory;
            this.dominationService = dominationService;
        }

        public BestConnections SearchConnections(SearchInput search, Graph graph)
        {
            var (bestConnections, priorityQueue) = PrepareFirstIteration(search, graph);
            int iteration = 0;
            while (priorityQueue.Any())
            {
                (bestConnections, priorityQueue) = 
                    MakeIteration(search, bestConnections, priorityQueue);
                iteration++;
            }
            return bestConnections;
        }

        private (BestConnections, LabelPriorityQueue) PrepareFirstIteration(
            SearchInput search, Graph graph)
        {
            var bestConnections = emptyBestFactory.Create(graph, search);
            var startVertex = graphService.GetStopVertexByStop(graph, search.StartStop);
            var priorityQueue = priorityQueueFactory.Create(startVertex, search);
            return (bestConnections, priorityQueue);
        }

        private (BestConnections, LabelPriorityQueue) MakeIteration(
            SearchInput search, 
            BestConnections bestConnections,
            LabelPriorityQueue priorityQueue)
        {
            var currentLabel = priorityQueue.Dequeue();
            var possibleConnections = connectionsService.GetPossibleConnections(
                currentLabel.Vertex.Connections, currentLabel.Connection.ArrivalTime);
            foreach (var connection in possibleConnections)
            {
                var newLabel = labelFactory.CreateLabel(currentLabel, connection);
                var currentLabels = bestConnections.Find(currentLabel.Vertex.Id);
                if (dominationService.IsNewLabelDominated(newLabel, currentLabels))
                    continue;
                priorityQueue.Enqueue(newLabel);
                dominationService.RemoveDominatedLabels(newLabel, currentLabels);
                currentLabels.Add(newLabel);
            }
            return (bestConnections, priorityQueue);
        }        
    }
}
