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

namespace Chilicki.Ptsa.Domain.Search.Services
{
    public class MultipleCriterionDijkstraSearch 
    {
        readonly EmptyBestConnectionsFactory emptyBestFactory;
        readonly MultipleCriterionGraphService graphService;
        readonly LabelPriorityQueueFactory priorityQueueFactory;

        readonly DijkstraFastestConnectionReplacer replacer;
        readonly DijkstraConnectionService connectionsService;
        readonly DijkstraContinueChecker continueChecker;

        public MultipleCriterionDijkstraSearch(
            EmptyBestConnectionsFactory emptyBestFactory,
            MultipleCriterionGraphService graphService,
            LabelPriorityQueueFactory priorityQueueFactory,


            DijkstraFastestConnectionReplacer replacer,
            DijkstraConnectionService connectionsService,
            DijkstraContinueChecker continueChecker)
        {
            this.emptyBestFactory = emptyBestFactory;
            this.graphService = graphService;
            this.priorityQueueFactory = priorityQueueFactory;
            
            this.replacer = replacer;
            this.connectionsService = connectionsService;
            this.continueChecker = continueChecker;
        }

        public BestConnections SearchConnections(SearchInput search, Graph graph)
        {
            var (bestConnections, currentVertex, priorityQueue) = PrepareFirstIteration(search, graph);
            int iteration = 0;
            while (continueChecker.ShouldContinue(search.DestinationStop.Id, currentVertex))
            {
                (bestConnections, currentVertex, priorityQueue) = 
                    MakeIteration(search, bestConnections, currentVertex, priorityQueue);
                iteration++;
            }
            return bestConnections;
        }

        private (BestConnections, Vertex, LabelPriorityQueue) MakeIteration(
            SearchInput search, 
            BestConnections bestConnections,
            Vertex currentVertex, 
            LabelPriorityQueue priorityQueue)
        {
            throw new NotImplementedException();
        }

        private (BestConnections, Vertex, LabelPriorityQueue) PrepareFirstIteration(
            SearchInput search, Graph graph)
        {
            var bestConnections = emptyBestFactory.Create(graph, search);
            var startVertex = graphService.GetStopVertexByStop(graph, search.StartStop);
            var priorityQueue = priorityQueueFactory.Create(startVertex, search);
            return (bestConnections, startVertex, priorityQueue);
        }
    }
}
