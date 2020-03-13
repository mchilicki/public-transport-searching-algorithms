using Chilicki.Ptsa.Data.Entities;
using System;

namespace Chilicki.Ptsa.Domain.Search.Aggregates
{
    public class SearchInput
    {
        public Stop StartStop { get; set; }
        public Stop DestinationStop { get; set; }
        public Vertex StartVertex { get; set; }
        public Vertex DestinationVertex { get; set; }
        public TimeSpan StartTime { get; set; }
    }
}
