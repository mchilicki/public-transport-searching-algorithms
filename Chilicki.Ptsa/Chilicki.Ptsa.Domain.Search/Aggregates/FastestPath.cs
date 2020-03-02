using Chilicki.Ptsa.Domain.Search.Aggregates.Graphs;
using System.Collections.Generic;

namespace Chilicki.Ptsa.Domain.Search.Aggregates
{
    public class FastestPath
    {
        public IEnumerable<StopConnection> Path { get; set; }
        public IEnumerable<StopConnection> FlattenPath { get; set; }
    }
}
