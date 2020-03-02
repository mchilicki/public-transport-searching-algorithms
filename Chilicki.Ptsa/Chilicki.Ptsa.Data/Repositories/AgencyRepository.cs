using Chilicki.Ptsa.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chilicki.Ptsa.Data.Repositories
{
    public class AgencyRepository : BaseRepository<Agency>
    {
        public AgencyRepository(DbContext context) : base(context)
        {
        }
    }
}
