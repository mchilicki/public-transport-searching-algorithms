using Chilicki.Ptsa.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chilicki.Ptsa.Data.Repositories
{
    public class TripRepository : BaseRepository<Trip>
    {
        public TripRepository(DbContext context) : base(context)
        {
        }
    }
}
