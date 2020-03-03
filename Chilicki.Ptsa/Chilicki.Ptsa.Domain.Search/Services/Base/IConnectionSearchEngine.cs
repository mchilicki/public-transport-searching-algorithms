using Chilicki.Ptsa.Domain.Search.Aggregates;
using Chilicki.Ptsa.Data.Entities;
using System.Collections.Generic;

namespace Chilicki.Ptsa.Domain.Search.Services.Base
{
    public interface IConnectionSearchEngine
    {
        IEnumerable<Connection> SearchConnections(SearchInput search, Graph graph);
    }
}
