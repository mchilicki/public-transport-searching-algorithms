using Chilicki.Ptsa.Data.Entities;
using System.Collections.Generic;

namespace Chilicki.Ptsa.Domain.Search.Aggregates
{
    public class FastestPath
    {
        public IEnumerable<Connection> Path { get; set; }
        public IEnumerable<Connection> FlattenPath { get; set; }
    }
}
