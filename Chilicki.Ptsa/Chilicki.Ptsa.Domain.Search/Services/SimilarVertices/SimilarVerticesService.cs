using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Domain.Search.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chilicki.Ptsa.Domain.Search.Services.SimilarVertices
{
    public class SimilarVerticesService
    {
        public IEnumerable<SimilarVertex> GetPossibleSimilarVertices(
            ICollection<SimilarVertex> similarVertices, SearchInput search)
        {
            var possibleSimilarVertices = similarVertices
                .Where(p => p.DistanceInMinutes <= search.Parameters.MaximalTransferInMinutes && 
                    p.DistanceInMinutes >= search.Parameters.MinimalTransferInMinutes);
            return possibleSimilarVertices;
        }
    }
}
