using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Data.Repositories;
using Chilicki.Ptsa.Domain.Search.Aggregates;
using Chilicki.Ptsa.Domain.Search.Aggregates.MultipleCriterion;
using Chilicki.Ptsa.Domain.Search.Configurations.Options;
using Chilicki.Ptsa.Domain.Search.Dtos;
using Chilicki.Ptsa.Domain.Search.InputSearches;
using Chilicki.Ptsa.Domain.Search.Managers;
using Chilicki.Ptsa.Domain.Search.Mappers;
using Chilicki.Ptsa.Search.Benchmarks.Singletons;
using Chilicki.Ptsa.Search.Configurations.Startup;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Chilicki.Ptsa.Benchmarks
{
    [SimpleJob(RunStrategy.Monitoring, warmupCount: 3, targetCount: 3)]
    [MinColumn, MaxColumn, MeanColumn, MedianColumn]
    [CsvExporter]
    [CsvMeasurementsExporter]
    [HtmlExporter]
    public class SingleCriteriaDijkstraVsMultipleCriterionDijkstra
    {
        private readonly SearchInputMapper searchInputMapper;
        private readonly GraphRepository graphRepository;
        private readonly SearchManager dijkstraSearchManager;
        private readonly MultipleCriteriaSearchManager multipleCriteriaSearchManager;

        public Graph Graph { get; set; }
        public IEnumerable<SearchInputDto> Searches { get; set; } = CurrentInputSearches.Searches;

        public SingleCriteriaDijkstraVsMultipleCriterionDijkstra()
        {
            Console.WriteLine("Constructor - Started");
            var startupService = new StartupService();
            IServiceProvider serviceProvider = startupService.Configure();
            searchInputMapper = serviceProvider.GetRequiredService<SearchInputMapper>();
            graphRepository = serviceProvider.GetRequiredService<GraphRepository>();
            dijkstraSearchManager = serviceProvider.GetRequiredService<SearchManager>();
            multipleCriteriaSearchManager = serviceProvider.GetRequiredService<MultipleCriteriaSearchManager>();
            Console.WriteLine("Constructor - Done");
        }

        [GlobalSetup]
        public async Task PrepareSearchesAndGraph()
        {
            Graph = await GraphSingleton.GetGraph(graphRepository);
        }

        [Benchmark]
        [Arguments(0, 8)]
        [Arguments(0, 6)]
        [Arguments(0, 4)]
        [Arguments(4, 8)]
        [Arguments(4, 6)]
        public void SingleCriteriaDijkstra(
            int minimalTransferTime,
            int maximalTransferDistanceInMinutes)
        {
            var list = new List<FastestPath>();
            var parameters = CreateParameters(
                120, 15, minimalTransferTime, maximalTransferDistanceInMinutes);
            foreach (var search in Searches)
            {
                var searchInput = searchInputMapper.ToDomainFromGraph(search, parameters, Graph);
                var path = dijkstraSearchManager.PerformSearch(searchInput, Graph);
                list.Add(path); 
                dijkstraSearchManager.ClearVisitedVertices(Graph);
            }
            list.Consume(new Consumer());
        }

        [Benchmark]
        [Arguments(30,   3, 0, 8)]
        [Arguments(60,   3, 0, 8)]
        [Arguments(120,  3, 0, 8)]
        [Arguments(180,  3, 0, 8)]
        [Arguments(240,  3, 0, 8)]
        [Arguments(360,  3, 0, 8)]
        [Arguments(720,  3, 0, 8)]
        [Arguments(1440, 3, 0, 8)]

        [Arguments(30,   15, 0, 8)]
        [Arguments(60,   15, 0, 8)]
        [Arguments(120,  15, 0, 8)]
        [Arguments(180,  15, 0, 8)]
        [Arguments(240,  15, 0, 8)]
        [Arguments(360,  15, 0, 8)]
        [Arguments(720,  15, 0, 8)]
        [Arguments(1440, 15, 0, 8)]

        [Arguments(30,   30, 0, 8)]
        [Arguments(60,   30, 0, 8)]
        [Arguments(120,  30, 0, 8)]
        [Arguments(180,  30, 0, 8)]
        [Arguments(240,  30, 0, 8)]
        [Arguments(360,  30, 0, 8)]
        [Arguments(720,  30, 0, 8)]
        [Arguments(1440, 30, 0, 8)]

        [Arguments(30,   50, 0, 8)]
        [Arguments(60,   50, 0, 8)]
        [Arguments(120,  50, 0, 8)]
        [Arguments(180,  50, 0, 8)]
        [Arguments(240,  50, 0, 8)]
        [Arguments(360,  50, 0, 8)]
        [Arguments(720,  50, 0, 8)]
        [Arguments(1440, 50, 0, 8)]

        [Arguments(120, 3, 0, 8)]
        [Arguments(120, 3, 0, 6)]
        [Arguments(120, 3, 0, 4)]
        [Arguments(120, 3, 4, 8)]
        [Arguments(120, 3, 4, 6)]

        [Arguments(120, 15, 0, 8)]
        [Arguments(120, 15, 0, 6)]
        [Arguments(120, 15, 0, 4)]
        [Arguments(120, 15, 4, 8)]
        [Arguments(120, 15, 4, 6)]

        [Arguments(120, 30, 0, 8)]
        [Arguments(120, 30, 0, 6)]
        [Arguments(120, 30, 0, 4)]
        [Arguments(120, 30, 4, 8)]
        [Arguments(120, 30, 4, 6)]

        [Arguments(120, 50, 0, 8)]
        [Arguments(120, 50, 0, 6)]
        [Arguments(120, 50, 0, 4)]
        [Arguments(120, 50, 4, 8)]
        [Arguments(120, 50, 4, 6)]
        public void MultipleCriteriaDijkstra(
            int maxTimeAheadFetchingPossibleConnections, 
            int minimumPossibleConnectionsFetched, 
            int minimalTransferTime, 
            int maximalTransferDistanceInMinutes)
        {
            var list = new List<BestConnections>();
            var parameters = CreateParameters(
                maxTimeAheadFetchingPossibleConnections, minimumPossibleConnectionsFetched, minimalTransferTime, maximalTransferDistanceInMinutes);
            foreach (var search in Searches)
            {
                var searchInput = searchInputMapper.ToDomainFromGraph(search, parameters, Graph);
                var paths = multipleCriteriaSearchManager.PerformSearch(searchInput, Graph);
                list.Add(paths);
            }
            list.Consume(new Consumer());
        }

        private SearchParameters CreateParameters(
            int maxTimeAheadFetchingPossibleConnections,
            int minimumPossibleConnectionsFetched,
            int minimalTransferTime,
            int maximalTransferDistanceInMinutes)
        {
            return new SearchParameters()
            {
                MaxTimeAheadFetchingPossibleConnections = maxTimeAheadFetchingPossibleConnections,
                MinimumPossibleConnectionsFetched = minimumPossibleConnectionsFetched,
                MinimalTransferTime = minimalTransferTime,
                MaximalTransferDistanceInMinutes = maximalTransferDistanceInMinutes,
            };
        }
    }
}
