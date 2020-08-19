using Chilicki.Ptsa.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chilicki.Ptsa.Data.Repositories
{
    public class ConnectionRepository : BaseRepository<Connection>
    {
        public ConnectionRepository(DbContext context) : base(context)
        {
        }
    }
}
