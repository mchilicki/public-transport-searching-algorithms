using Chilicki.Ptsa.Data.Entities;
using System;
using System.Collections.Generic;

namespace Chilicki.Ptsa.Domain.Search.Aggregates
{
    public class FastestPath
    {
        public Stop StartStop { get; set; }
        public Stop DestinationStop { get; set; }
        public TimeSpan StartTime { get; set; }
        public IEnumerable<Connection> Path { get; set; }
        public IEnumerable<Connection> FlattenPath { get; set; }

        public static FastestPath Create(SearchInput search)
        {
            return Create(search, fastestPath: null, flattenPath: null);
        }

        public static FastestPath Create(
            SearchInput search,
            IEnumerable<Connection> fastestPath, IEnumerable<Connection> flattenPath)
        {
            return new FastestPath()
            {
                StartStop = search.StartStop,
                DestinationStop = search.DestinationStop,
                StartTime = search.StartTime,
                Path = fastestPath,
                FlattenPath = flattenPath,
            };
        }
    }
}
