using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Domain.Search.Aggregates;
using Chilicki.Ptsa.Domain.Search.Aggregates.MultipleCriterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chilicki.Ptsa.Domain.Search.Services
{
    public class GoogleSearchEngine
    {
        public (List<FastestPath>, BestConnections) SearchConnections(
            SearchInput search, Graph graph)
        {
            var directPaths = FindDirectPaths(search, graph).ToList();
            return (null, null);
        }

        private IEnumerable<FastestPath> FindDirectPaths(SearchInput search, Graph graph)
        {
            throw new NotImplementedException();
        }
    }
}
