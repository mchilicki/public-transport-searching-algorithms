using Chilicki.Ptsa.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chilicki.Ptsa.Data.Repositories
{
    public class RouteRepository : BaseRepository<Route>
    {
        public RouteRepository(DbContext context) : base(context)
        {
        }

        public async Task<ICollection<Route>> GetAllWithTripsAsync()
        {
            return await entities
                .Include(p => p.Trips)
                .ToListAsync();
        }
    }
}
