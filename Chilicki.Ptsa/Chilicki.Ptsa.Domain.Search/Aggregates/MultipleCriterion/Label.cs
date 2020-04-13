using Chilicki.Ptsa.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chilicki.Ptsa.Domain.Search.Aggregates.MultipleCriterion
{
    public class Label
    {
        public Vertex Vertex { get; set; }
        public Connection Connection { get; set; }
        public Criterion TimeCriterion { get; set; }
        public Criterion TransferCriterion { get; set; }
        public Criterion StopCountCriterion { get; set; }

        public TimeSpan GetArrivalTime()
        {
            if (Connection != null)
                return Connection.ArrivalTime;
            return TimeSpan.MaxValue;
        }

        public IList<Criterion> GetAllCriteria()
        {
            return new List<Criterion>
            {
                TimeCriterion,
                TransferCriterion,
                StopCountCriterion
            };
        }

        public override string ToString()
        {
            return $"(Time: {TimeCriterion.Value}, Transfer: {TransferCriterion.Value}, Stops: {StopCountCriterion})";
        }
    }
}
