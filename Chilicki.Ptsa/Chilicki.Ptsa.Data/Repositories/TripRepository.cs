using Chilicki.Ptsa.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chilicki.Ptsa.Data.Repositories
{
    public class TripRepository : BaseRepository<Trip>
    {
        public TripRepository(DbContext context) : base(context)
        {
        }

        public async Task<Trip> FindWithRouteAsync(Guid id)
        {
            return await entities
                .Include(p => p.Route)
                .SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<ICollection<Trip>> FindByContainedStop(Guid stopId)
        {
            return await entities
                .Where(p => p.StopTimes.Any(e => e.StopId == stopId))
                .ToListAsync();
        }
    }
}
