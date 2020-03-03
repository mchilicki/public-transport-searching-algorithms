using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Domain.Search.Factories.Dijkstra;
using Chilicki.Ptsa.Domain.Search.Factories.StopConnections;
using Chilicki.Ptsa.Domain.Search.Managers;
using Chilicki.Ptsa.Domain.Search.ManualMappers;
using Chilicki.Ptsa.Domain.Search.Services;
using Chilicki.Ptsa.Domain.Search.Services.Base;
using Chilicki.Ptsa.Domain.Search.Services.Dijkstra;
using Chilicki.Ptsa.Domain.Search.Services.GraphFactories;
using Chilicki.Ptsa.Domain.Search.Services.GraphFactories.Base;
using Chilicki.Ptsa.Domain.Search.Services.Path;
using Chilicki.Ptsa.Domain.Search.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace Chilicki.Ptsa.Domain.Search.Configurations.DependencyInjection
{
    public class DomainSearchDependencyInjection
    {
        public void Configure(IServiceCollection services)
        {
            ConfigureServices(services);
            ConfigureValidators(services);
            ConfigureMappers(services);
            ConfigureFactories(services);
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<SearchManager>();
            services.AddTransient<IConnectionSearchEngine, DijkstraConnectionSearchEngine>();
            services.AddTransient<DijkstraFastestConnectionReplacer>();
            services.AddTransient<DijkstraNextVertexResolver>();
            services.AddTransient<DijkstraConnectionService>();
            services.AddTransient<DijkstraGraphService>();
            services.AddTransient<FastestPathResolver>();
            services.AddTransient<FastestPathTimeCalculator>();
            services.AddTransient<FastestPathTransferService>();
        }

        private void ConfigureFactories(IServiceCollection services)
        {
            services.AddTransient<DijkstraEmptyFastestConnectionsFactory>();
            services.AddTransient<ConnectionFactory>();
            services.AddTransient<IGraphFactory<Graph>, GraphFactory>();
            services.AddTransient<ConnectionCloner>();
        }

        private void ConfigureValidators(IServiceCollection services)
        {
            services.AddTransient<SearchValidator>();
        }

        private void ConfigureMappers(IServiceCollection services)
        {
            services.AddTransient<SearchInputManualMapper>();
        }
    }
}
