using Chilicki.Ptsa.Domain.Search.Aggregates;
using Chilicki.Ptsa.Data.Entities;

namespace Chilicki.Ptsa.Domain.Search.Services.Base
{
    public interface IConnectionSearchEngine
    {
        VertexFastestConnections SearchConnections(SearchInput search, Graph graph);
    }
}
