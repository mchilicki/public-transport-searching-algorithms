using Chilicki.Ptsa.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chilicki.Ptsa.Data.Repositories.Base
{
    public interface ISimilarVertexRepository
    {
        Task<IEnumerable<SimilarVertex>> GetAllAsync();
        Task<SimilarVertex> FindAsync(Guid vertexId, Guid similarId);
        Task<SimilarVertex> AddAsync(SimilarVertex entity);
        Task AddRangeAsync(IEnumerable<SimilarVertex> entities);
        Task<int> GetCountAsync();
    }
}