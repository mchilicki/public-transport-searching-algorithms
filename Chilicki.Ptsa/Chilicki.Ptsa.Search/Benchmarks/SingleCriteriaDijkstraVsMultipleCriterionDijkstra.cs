using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Data.Repositories;
using Chilicki.Ptsa.Domain.Search.Aggregates;
using Chilicki.Ptsa.Domain.Search.Aggregates.MultipleCriterion;
using Chilicki.Ptsa.Domain.Search.Configurations.Options;
using Chilicki.Ptsa.Domain.Search.Dtos;
using Chilicki.Ptsa.Domain.Search.Managers;
using Chilicki.Ptsa.Domain.Search.Mappers;
using Chilicki.Ptsa.Domain.Search.Services;
using Chilicki.Ptsa.Domain.Search.Services.SearchInputs;
using Chilicki.Ptsa.Search.Benchmarks.InputSearches;
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
    [HtmlExporter]
    public class SingleCriteriaDijkstraVsMultipleCriterionDijkstra
    {
        private readonly SearchInputMapper searchInputMapper;
        private readonly GraphRepository graphRepository;
        private readonly SearchManager dijkstraSearchManager;
        private readonly MultipleCriteriaSearchManager multipleCriteriaSearchManager;

        public Graph Graph { get; set; }
        public IEnumerable<SearchInputDto> Searches { get; set; } = BenchmarkInputSearches.Searches;

        [Params(120, 240, 360, 480)]
        public int MaxTimeAheadFetchingPossibleConnections { get; set; }

        [Params(3/*, 10, 50*/)]
        public int MinimumPossibleConnectionsFetched { get; set; }

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
        [Arguments(0, 5)]
        //[Arguments(0, 3)]
        //[Arguments(3, 5)]
        public void SingleCriteriaDijkstra(int minimalTransferTime, int maximalTransferDistanceInMinutes)
        {
            var list = new List<FastestPath>();
            var parameters = CreateParameters(minimalTransferTime, maximalTransferDistanceInMinutes);
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
        [Arguments(0, 5)]
        //[Arguments(0, 3)]
        //[Arguments(3, 5)]
        public void MultipleCriteriaDijkstra(int minimalTransferTime, int maximalTransferDistanceInMinutes)
        {
            var list = new List<BestConnections>();
            var parameters = CreateParameters(minimalTransferTime, maximalTransferDistanceInMinutes);
            foreach (var search in Searches)
            {
                var searchInput = searchInputMapper.ToDomainFromGraph(search, parameters, Graph);
                var paths = multipleCriteriaSearchManager.PerformSearch(searchInput, Graph);
                list.Add(paths);
            }
            list.Consume(new Consumer());
        }

        private SearchParameters CreateParameters(int minimalTransferTime, int maximalTransferDistanceInMinutes)
        {
            return new SearchParameters()
            {
                MaxTimeAheadFetchingPossibleConnections = MaxTimeAheadFetchingPossibleConnections,
                MinimumPossibleConnectionsFetched = MinimumPossibleConnectionsFetched,
                MinimalTransferTime = minimalTransferTime,
                MaximalTransferDistanceInMinutes = maximalTransferDistanceInMinutes,
            };
        }
    }
}
