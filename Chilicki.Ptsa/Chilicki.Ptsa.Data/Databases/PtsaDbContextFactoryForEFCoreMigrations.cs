using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chilicki.Ptsa.Data.Databases
{
    public class PtsaDbContextFactoryForEFCoreMigrations : IDesignTimeDbContextFactory<PtsaDbContext>
    {
        public PtsaDbContext CreateDbContext(string[] args)
        {
            var databaseConnectionString = "data source=localhost;initial catalog=PtsaDb;integrated security=True;MultipleActiveResultSets=True;";
            var optionsBuilder = new DbContextOptionsBuilder<PtsaDbContext>();
            optionsBuilder.UseSqlServer(
                databaseConnectionString,
                b => b.MigrationsAssembly(typeof(PtsaDbContext).Assembly.GetName().Name)
            );
            return new PtsaDbContext(optionsBuilder.Options);
        }
    }
}
