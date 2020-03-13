using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Chilicki.Ptsa.Data.Repositories
{
    public class VertexRepository : BaseRepository<Vertex>
    {
        public VertexRepository(DbContext context) : base(context)
        {
        }

        public async Task<Vertex> GetByStopId(Guid stopId)
        {
            return await entities
                .FirstOrDefaultAsync(p => p.StopId == stopId);
        }
    }
}
