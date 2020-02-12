using Chilicki.Ptsa.Data.Configurations.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chilicki.Ptsa.Data.Databases
{
    public class PtsaDbContext : DbContext
    {
        public PtsaDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AgencyConfiguration());
            modelBuilder.ApplyConfiguration(new RouteConfiguration());
            modelBuilder.ApplyConfiguration(new StopConfiguration());
            modelBuilder.ApplyConfiguration(new StopTimeConfiguration());
            modelBuilder.ApplyConfiguration(new TripConfiguration());
        }
    }
}
