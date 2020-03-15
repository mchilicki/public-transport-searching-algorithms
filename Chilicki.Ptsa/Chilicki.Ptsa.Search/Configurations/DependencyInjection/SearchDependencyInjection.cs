using Chilicki.Ptsa.Data.Configurations.DependencyInjection;
using Chilicki.Ptsa.Data.Configurations.ProjectConfiguration;
using Chilicki.Ptsa.Domain.Gtfs.Configurations.DependencyInjection;
using Chilicki.Ptsa.Domain.Search.Configurations.DependencyInjection;
using Chilicki.Ptsa.Search.Configurations.Startup;
using Microsoft.Extensions.DependencyInjection;

namespace Chilicki.Ptsa.Search.Configurations.DependencyInjection
{
    public class SearchDependencyInjection
    {
        public void Configure(IServiceCollection services, ConnectionStrings connectionStrings)
        {
            ConfigureConsoleServices(services);
            new DomainSearchDependencyInjection().Configure(services);
            new DomainGtfsDependencyInjection().Configure(services);
            new DataDependencyInjection().Configure(services, connectionStrings);
        }

        private static void ConfigureConsoleServices(IServiceCollection services)
        {
            services.AddTransient<ConsoleSearchService>();
        }
    }
}
