using Chilicki.Ptsa.Data.Entities;
using System;

namespace Chilicki.Ptsa.Domain.Search.Aggregates
{
    public class SearchInput
    {
        public Stop StartStop { get; set; }
        public Stop DestinationStop { get; set; }
        public TimeSpan StartTime { get; set; }
    }
}
