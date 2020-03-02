using Chilicki.Ptsa.Data.Entities;
using System.Collections.Generic;

namespace Chilicki.Ptsa.Domain.Search.Aggregates.Graphs
{
    public class StopVertex
    {
        public Stop Stop { get; set; }
        public IEnumerable<StopConnection> StopConnections { get; set; }
        public bool IsVisited { get; set; }
        public IEnumerable<StopVertex> SimilarStopVertices { get; set; }
    }
}
