using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chilicki.Ptsa.Data.Repositories
{
    public class SimilarVertexRepository : ISimilarVertexRepository
    {
        protected DbContext context;
        protected DbSet<SimilarVertex> entities;

        public SimilarVertexRepository(DbContext context)
        {
            this.context = context;
            entities = this.context.Set<SimilarVertex>();
        }

        public async Task<IEnumerable<SimilarVertex>> GetAllAsync()
        {
            return await entities.ToListAsync();
        }

        public async Task<SimilarVertex> FindAsync(Guid vertexId, Guid similarId)
        {
            return await entities
                .SingleOrDefaultAsync(p => p.VertexId == vertexId && p.SimilarId == similarId);
        }

        public async Task<SimilarVertex> AddAsync(SimilarVertex entity)
        {
            var entry = await entities.AddAsync(entity);
            return entry.Entity;
        }

        public async Task AddRangeAsync(IEnumerable<SimilarVertex> entities)
        {
            await this.entities.AddRangeAsync(entities);
        }

        public async Task<int> GetCountAsync()
        {
            return await entities.CountAsync();
        }
    }
}
