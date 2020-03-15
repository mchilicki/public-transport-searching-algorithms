using Chilicki.Ptsa.Data.Databases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Chilicki.Ptsa.Search.Configurations.Migrations
{
    public class PtsaDbContextFactoryForEFCoreMigrations : IDesignTimeDbContextFactory<PtsaDbContext>
    {
        public PtsaDbContext CreateDbContext(string[] args)
        {
            var configuration = GetConfiguration();
            var databaseConnectionString = configuration.GetConnectionString("PtsaDatabase");
            var optionsBuilder = new DbContextOptionsBuilder<PtsaDbContext>();
            optionsBuilder.UseSqlServer(
                databaseConnectionString,
                b => b.MigrationsAssembly(typeof(PtsaDbContext).Assembly.GetName().Name)
            );
            optionsBuilder.EnableSensitiveDataLogging(true);
            return new PtsaDbContext(optionsBuilder.Options);
        }

        private IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath($"{Directory.GetCurrentDirectory()}//Settings//")
                .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            var configuration = builder.Build();
            return configuration;
        }
    }
}
