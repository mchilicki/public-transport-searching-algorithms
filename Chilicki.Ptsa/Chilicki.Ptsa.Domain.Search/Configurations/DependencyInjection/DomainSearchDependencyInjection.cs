using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Domain.Search.Factories.Dijkstra;
using Chilicki.Ptsa.Domain.Search.Factories.MultipleCriterion;
using Chilicki.Ptsa.Domain.Search.Factories.SimilarVertices;
using Chilicki.Ptsa.Domain.Search.Managers;
using Chilicki.Ptsa.Domain.Search.Mappers;
using Chilicki.Ptsa.Domain.Search.Services;
using Chilicki.Ptsa.Domain.Search.Services.Base;
using Chilicki.Ptsa.Domain.Search.Services.Calculations;
using Chilicki.Ptsa.Domain.Search.Services.Dijkstra;
using Chilicki.Ptsa.Domain.Search.Services.GraphFactories;
using Chilicki.Ptsa.Domain.Search.Services.GraphFactories.Base;
using Chilicki.Ptsa.Domain.Search.Services.Measures;
using Chilicki.Ptsa.Domain.Search.Services.MultipleCriterion;
using Chilicki.Ptsa.Domain.Search.Services.Path;
using Chilicki.Ptsa.Domain.Search.Services.SearchInputs;
using Chilicki.Ptsa.Domain.Search.Services.SimilarVertices;
using Chilicki.Ptsa.Domain.Search.Services.Summary;
using Chilicki.Ptsa.Domain.Search.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace Chilicki.Ptsa.Domain.Search.Configurations.DependencyInjection
{
    public class DomainSearchDependencyInjection
    {
        public void Configure(IServiceCollection services)
        {
            ConfigureManagers(services);
            ConfigureServices(services);
            ConfigureValidators(services);
            ConfigureMappers(services);
            ConfigureFactories(services);
        }

        private void ConfigureManagers(IServiceCollection services)
        {
            services.AddTransient<SearchManager>();
            services.AddTransient<GraphManager>();
        }

        private void ConfigureServices(IServiceCollection services)
        {            
            services.AddTransient<IConnectionSearchEngine, DijkstraSearch>();
            services.AddTransient<DijkstraFastestConnectionReplacer>();
            services.AddTransient<DijkstraNextVertexResolver>();
            services.AddTransient<DijkstraConnectionService>();
            services.AddTransient<DijkstraGraphService>();
            services.AddTransient<DijkstraContinueChecker>();
            services.AddTransient<FastestPathResolver>();
            services.AddTransient<FastestPathTransferService>();
            services.AddTransient<FastestPathFlattener>();
            services.AddTransient<RandomSearchInputGenerator>();
            services.AddTransient<MeasureLogger>();
            services.AddTransient<MultipleCriteriaSearchManager>();
            services.AddTransient<MultipleCriterionDijkstraSearch>();
            services.AddTransient<MultipleCriterionGraphService>();
            services.AddTransient<DominationService>();
            services.AddTransient<PossibleConnectionsService>();
            services.AddTransient<ConnectionTimeCalculator>();
            services.AddTransient<TransferCalculator>();
            services.AddTransient<BestPathResolver>();
            services.AddTransient<CurrentConnectionService>();
            services.AddTransient<InfeasiblityService>();
            services.AddTransient<HaversineDistanceCalculator>();
            services.AddTransient<KilometersToDistanceMinutesConverter>();
            services.AddTransient<SimilarVerticesService>();
            services.AddTransient<DataSummaryService>();
        }

        private void ConfigureFactories(IServiceCollection services)
        {
            services.AddTransient<DijkstraEmptyFastestConnectionsFactory>();
            services.AddTransient<ConnectionFactory>();
            services.AddTransient<IGraphFactory<Graph>, GraphFactory>();
            services.AddTransient<SimilarVertexFactory>();
            services.AddTransient<EmptyBestConnectionsFactory>();
            services.AddTransient<LabelPriorityQueueFactory>();
            services.AddTransient<LabelFactory>();
        }

        private void ConfigureValidators(IServiceCollection services)
        {
            services.AddTransient<SearchValidator>();
        }

        private void ConfigureMappers(IServiceCollection services)
        {
            services.AddTransient<SearchInputMapper>();
        }
    }
}
