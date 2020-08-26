using Chilicki.Ptsa.Data.Configurations.ProjectConfiguration;
using Chilicki.Ptsa.Data.Databases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.IO;

namespace Chilicki.Ptsa.Search.Configurations.Migrations
{
    public class PtsaDbContextFactoryForEFCoreMigrations : IDesignTimeDbContextFactory<PtsaDbContext>
    {
        public PtsaDbContext CreateDbContext(string[] args)
        {            
            string databaseConnectionString = GetCurrentConnectionString();
            var optionsBuilder = new DbContextOptionsBuilder<PtsaDbContext>();
            optionsBuilder.UseSqlServer(
                databaseConnectionString,
                b => b.MigrationsAssembly(typeof(PtsaDbContext).Assembly.GetName().Name)
            );
            optionsBuilder.EnableSensitiveDataLogging(true);
            return new PtsaDbContext(optionsBuilder.Options);
        }

        private string GetCurrentConnectionString()
        {
            var configuration = GetConfiguration();
            var serviceCollection = new ServiceCollection();
            serviceCollection.Configure<ConnectionStrings>
                (configuration.GetSection(nameof(ConnectionStrings)));
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var connectionStrings = serviceProvider.GetRequiredService<IOptions<ConnectionStrings>>().Value;
            var databaseConnectionString = connectionStrings.CurrentDatabase;
            return databaseConnectionString;
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
