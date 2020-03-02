using Chilicki.Ptsa.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chilicki.Ptsa.Data.Repositories
{
    public class RouteRepository : BaseRepository<Route>
    {
        public RouteRepository(DbContext context) : base(context)
        {
        }
    }
}
