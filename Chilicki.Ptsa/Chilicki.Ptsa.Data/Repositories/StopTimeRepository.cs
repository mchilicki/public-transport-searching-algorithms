using Chilicki.Ptsa.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chilicki.Ptsa.Data.Repositories
{
    public class StopTimeRepository : BaseRepository<StopTime>
    {
        public StopTimeRepository(DbContext context) : base(context)
        {
        }
    }
}
