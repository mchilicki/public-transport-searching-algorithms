using Chilicki.Ptsa.Data.Configurations.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Chilicki.Ptsa.Data.Databases
{
    public class PtsaDbContext : DbContext
    {
        public PtsaDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AgencyConfiguration());
            builder.ApplyConfiguration(new RouteConfiguration());
            builder.ApplyConfiguration(new StopConfiguration());
            builder.ApplyConfiguration(new StopTimeConfiguration());
            builder.ApplyConfiguration(new TripConfiguration());
            builder.ApplyConfiguration(new ConnectionConfiguration());
            builder.ApplyConfiguration(new GraphConfiguration());
            builder.ApplyConfiguration(new VertexConfiguration());
        }
    }
}
