using Chilicki.Ptsa.Data.Configurations.ProjectConfiguration;
using Chilicki.Ptsa.Search.Configurations.DependencyInjection;
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
            await RunService();
        }

        private void Configure()
        {
            ConfigureAppSettings();
            ConfigureDependencyInjection();
        }

        private void ConfigureDependencyInjection()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.Configure<AppSettings>
                (Configuration.GetSection(nameof(AppSettings)));
            serviceCollection.Configure<ConnectionStrings>
                (Configuration.GetSection(nameof(ConnectionStrings)));
            ServiceProvider = serviceCollection.BuildServiceProvider();
            var searchDependencyInjection = new SearchDependencyInjection();
            var connectionStrings = ServiceProvider.GetService<IOptions<ConnectionStrings>>().Value;
            searchDependencyInjection.Configure(serviceCollection, connectionStrings);
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        private void ConfigureAppSettings()
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        private async Task RunService()
        {
            var service = ServiceProvider.GetRequiredService<ConsoleSearchService>();
            await service.Run();
        }
    }
}
