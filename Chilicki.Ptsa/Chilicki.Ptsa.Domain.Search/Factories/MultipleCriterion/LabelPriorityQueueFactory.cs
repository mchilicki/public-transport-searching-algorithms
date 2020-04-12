using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Domain.Search.Aggregates;
using Chilicki.Ptsa.Domain.Search.Aggregates.MultipleCriterion;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chilicki.Ptsa.Domain.Search.Factories.MultipleCriterion
{
    public class LabelPriorityQueueFactory
    {
        readonly LabelFactory labelFactory;

        public LabelPriorityQueueFactory(
            LabelFactory labelFactory)
        {
            this.labelFactory = labelFactory;
        }

        public LabelPriorityQueue Create(Vertex vertex, SearchInput search)
        {
            var priorityQueue = new LabelPriorityQueue();
            var startLabels = labelFactory.CreateStartLabels(vertex, search);
            priorityQueue.EnqueueRange(startLabels);
            return priorityQueue;
        }
    }
}
