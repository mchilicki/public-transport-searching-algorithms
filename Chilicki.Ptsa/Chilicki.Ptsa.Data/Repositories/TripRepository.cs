using Chilicki.Ptsa.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chilicki.Ptsa.Data.Repositories
{
    public class TripRepository : BaseRepository<Trip>
    {
        public TripRepository(DbContext context) : base(context)
        {
        }
    }
}
