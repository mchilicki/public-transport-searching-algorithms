using Chilicki.Ptsa.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chilicki.Ptsa.Data.Repositories
{
    public class StopRepository : BaseRepository<Stop>
    {
        public StopRepository(DbContext context) : base(context)
        {
        }

        public async Task<bool> DoesStopWithIdExist(Guid id)
        {
            return await entities
                .SingleOrDefaultAsync(p => p.Id == id) != null;
        }

        public async Task<IList<Guid>> GetStopIds()
        {
            return await entities
                .Select(p => p.Id)
                .ToListAsync();
        }
    }
}
