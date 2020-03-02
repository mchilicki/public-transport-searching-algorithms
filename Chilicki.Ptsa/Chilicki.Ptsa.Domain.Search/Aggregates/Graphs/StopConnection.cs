using Chilicki.Ptsa.Data.Entities;
using System;

namespace Chilicki.Ptsa.Domain.Search.Aggregates.Graphs
{
    public class StopConnection
    {
        public Trip Trip { get; set; }
        public StopVertex SourceStopVertex { get; set; }
        public StopVertex EndStopVertex { get; set; }
        public StopTime StartStopTime { get; set; }
        public StopTime EndStopTime { get; set; }
        public bool IsTransfer { get; set; }
    }
}
