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
        private readonly InfeasiblityService infeasiblityService;

        public MultipleCriterionDijkstraSearch(
            EmptyBestConnectionsFactory emptyBestFactory,
            MultipleCriterionGraphService graphService,
            LabelPriorityQueueFactory priorityQueueFactory,
            PossibleConnectionsService connectionsService,
            LabelFactory labelFactory,
            DominationService dominationService,
            InfeasiblityService infeasiblityService)
        {
            this.emptyBestFactory = emptyBestFactory;
            this.graphService = graphService;
            this.priorityQueueFactory = priorityQueueFactory;
            this.connectionsService = connectionsService;
            this.labelFactory = labelFactory;
            this.dominationService = dominationService;
            this.infeasiblityService = infeasiblityService;
        }

        public BestConnections SearchConnections(SearchInput search, Graph graph)
        {
            var (bestConnections, priorityQueue) = PrepareFirstIteration(search, graph);
            int iteration = 0;
            while (priorityQueue.Any())
            {
                (bestConnections, priorityQueue) = 
                    MakeIteration(bestConnections, priorityQueue); 
                iteration++;
            }
            return bestConnections;
        }

        private (BestConnections, LabelPriorityQueue) PrepareFirstIteration(
            SearchInput search, Graph graph)
        {
            var bestConnections = emptyBestFactory.Create(graph);
            var startVertex = graphService.GetStopVertexByStop(graph, search.StartStop);
            var priorityQueue = priorityQueueFactory.Create(startVertex, search);
            return (bestConnections, priorityQueue);
        }

        private (BestConnections, LabelPriorityQueue) MakeIteration(
            BestConnections bestConnections,
            LabelPriorityQueue priorityQueue)
        {
            var currentLabel = priorityQueue.Dequeue();
            var possibleConnections = connectionsService.GetPossibleConnections(
                currentLabel.Vertex, currentLabel.Connection.ArrivalTime);
            foreach (var connection in possibleConnections)
            {
                var currentLabels = bestConnections.Get(connection.EndVertexId);
                if (infeasiblityService.IsInfeasible(connection, currentLabels))
                    continue;
                var newLabel = labelFactory.CreateLabel(currentLabel, connection);                                      
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
