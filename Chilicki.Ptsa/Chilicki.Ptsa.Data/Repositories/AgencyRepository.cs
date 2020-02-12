using Chilicki.Ptsa.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chilicki.Ptsa.Data.Repositories
{
    public class AgencyRepository : BaseRepository<Agency>
    {
        public AgencyRepository(DbContext context) : base(context)
        {
        }
    }
}
