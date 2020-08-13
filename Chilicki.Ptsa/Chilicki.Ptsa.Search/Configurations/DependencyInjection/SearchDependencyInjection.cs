using Chilicki.Ptsa.Data.Configurations.DependencyInjection;
using Chilicki.Ptsa.Data.Configurations.ProjectConfiguration;
using Chilicki.Ptsa.Domain.Gtfs.Configurations.DependencyInjection;
using Chilicki.Ptsa.Domain.Search.Configurations.DependencyInjection;
using Chilicki.Ptsa.Search.Benchmarks.Singletons;
using Chilicki.Ptsa.Search.Configurations.Startup;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Chilicki.Ptsa.Search.Configurations.DependencyInjection
{
    public class SearchDependencyInjection
    {
        public void Configure(IServiceCollection services, ConnectionStrings connectionStrings)
        {
            ConfigureConsoleServices(services);
            ConfigureGraphSingleton(services);
            new DomainSearchDependencyInjection().Configure(services);
            new DomainGtfsDependencyInjection().Configure(services);
            new DataDependencyInjection().Configure(services, connectionStrings);
        }

        private void ConfigureGraphSingleton(IServiceCollection services)
        {
            services.AddSingleton<GraphSingleton>();
        }

        private static void ConfigureConsoleServices(IServiceCollection services)
        {
            services.AddTransient<ConsoleSearchService>();
        }
    }
}
