using Chilicki.Ptsa.Data.Configurations.ProjectConfiguration;
using Chilicki.Ptsa.Data.Databases;
using Chilicki.Ptsa.Search.Configurations.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Chilicki.Ptsa.Search.Configurations.Startup
{
    public class StartupService
    {
        public IServiceProvider ServiceProvider { get; private set; }
        public IConfiguration Configuration { get; private set; }

        public async Task Run()
        {
            Configure();
            await MigrateDatabase();
            await RunService();
        }

        public IServiceProvider Configure()
        {
            ConfigureAppSettings();
            return ConfigureDependencyInjection();
        }

        private IServiceProvider ConfigureDependencyInjection()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureAppSettingsSections(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();
            var searchDependencyInjection = new SearchDependencyInjection();
            var connectionStrings = ServiceProvider.GetService<IOptions<ConnectionStrings>>().Value;
            searchDependencyInjection.Configure(serviceCollection, connectionStrings);
            ServiceProvider = serviceCollection.BuildServiceProvider();
            return ServiceProvider;
        }

        private void ConfigureAppSettingsSections(ServiceCollection serviceCollection)
        {
            serviceCollection.Configure<GraphCreationSettings>
                (Configuration.GetSection(nameof(GraphCreationSettings)));
            serviceCollection.Configure<PathSettings>
                (Configuration.GetSection(nameof(PathSettings)));
            serviceCollection.Configure<ConnectionStrings>
                (Configuration.GetSection(nameof(ConnectionStrings)));
            serviceCollection.Configure<SearchSettings>
                (Configuration.GetSection(nameof(SearchSettings)));
            serviceCollection.Configure<CityCenterSettings>
                (Configuration.GetSection(nameof(CityCenterSettings)));
            serviceCollection.Configure<ModuleTypes>
                (Configuration.GetSection(nameof(ModuleTypes)));
            serviceCollection.Configure<SummarySettings>
                (Configuration.GetSection(nameof(SummarySettings)));
        }

        private void ConfigureAppSettings()
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var builder = new ConfigurationBuilder()
                .SetBasePath($"{Directory.GetCurrentDirectory()}//Settings//")
                .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        private async Task MigrateDatabase()
        {
            var dbContext = ServiceProvider.GetRequiredService<DbContext>();
            await dbContext.Database.MigrateAsync();
        }

        private async Task RunService()
        {
            var service = ServiceProvider.GetRequiredService<ConsoleSearchService>();
            await service.Run();
        }
    }
}
