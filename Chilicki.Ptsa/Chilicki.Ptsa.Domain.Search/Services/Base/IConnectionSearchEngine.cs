using Chilicki.Ptsa.Domain.Search.Aggregates;
using Chilicki.Ptsa.Domain.Search.Aggregates.Graphs;
using System.Collections.Generic;

namespace Chilicki.Ptsa.Domain.Search.Services.Base
{
    public interface IConnectionSearchEngine
    {
        IEnumerable<StopConnection> SearchConnections(SearchInput search, StopGraph graph);
    }
}
