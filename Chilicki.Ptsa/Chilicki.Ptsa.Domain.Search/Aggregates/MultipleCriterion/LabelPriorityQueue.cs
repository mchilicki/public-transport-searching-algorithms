using Priority_Queue;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chilicki.Ptsa.Domain.Search.Aggregates.MultipleCriterion
{
    public class LabelPriorityQueue : SimplePriorityQueue<Label, TimeSpan>
    {
        public void Enqueue(Label label)
        {
            Enqueue(label, label.GetArrivalTime());
        }

        public void EnqueueRange(ICollection<Label> labels)
        {
            foreach (var label in labels)
            {
                Enqueue(label);
            }
        }
    }
}
