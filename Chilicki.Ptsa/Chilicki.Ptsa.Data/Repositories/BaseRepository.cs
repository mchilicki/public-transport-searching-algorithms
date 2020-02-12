using Chilicki.Ptsa.Data.Entities.Base;
using Chilicki.Ptsa.Data.Repositories.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chilicki.Ptsa.Data.Repositories
{
    public class BaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected DbContext context;
        protected DbSet<TEntity> entities;

        public BaseRepository(DbContext context)
        {
            this.context = context;
            entities = this.context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await entities.IncludeAll().ToListAsync();
        }

        public async Task<TEntity> FindAsync(params Guid[] keyValues)
        {
            if (keyValues.Count() > 1)
                throw new InvalidOperationException("Wrong number of keyValues");
            return await entities.IncludeAll().SingleOrDefaultAsync(p => p.Id == keyValues[0]);
        }

        public TEntity Find(params Guid[] keyValues)
        {
            if (keyValues.Count() > 1)
                throw new InvalidOperationException("Wrong number of keyValues");
            return entities.IncludeAll().SingleOrDefault(p => p.Id == keyValues[0]);
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var entry = await entities.AddAsync(entity);
            return entry.Entity;
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await this.entities.AddRangeAsync(entities);
        }

        public async Task<int> GetCountAsync()
        {
            return await entities.CountAsync();
        }
    }
}
