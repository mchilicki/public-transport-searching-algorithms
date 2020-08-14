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
    [SimpleJob(RunStrategy.Monitoring, warmupCount: 3)]
    [MinColumn, MaxColumn, MeanColumn, MedianColumn]
    [CsvExporter]
    [HtmlExporter]
    [KeepBenchmarkFiles]
    public class SingleCriteriaDijkstraVsMultipleCriterionDijkstra
    {
        private readonly SearchInputMapper searchInputMapper;
        private readonly GraphRepository graphRepository;
        private readonly SearchManager dijkstraSearchManager;
        private readonly MultipleCriteriaSearchManager multipleCriteriaSearchManager;

        public Graph Graph { get; set; }
        public IEnumerable<SearchInputDto> Searches { get; set; } = new List<SearchInputDto>()
        {
            new SearchInputDto() { StartStopId = new Guid("8fda5d8f-e959-4aad-9d64-8e857beb313e"), DestinationStopId = new Guid("87e7a7eb-7bf1-4df0-8ed3-62b845bd535d"), StartTime = TimeSpan.Parse("09:00:00") },
            //new SearchInputDto() { StartStopId = new Guid("f88dc322-e038-45c4-8af2-4a6be7f0664b"), DestinationStopId = new Guid("1f8dd008-3940-4630-96d5-b48889feefb5"), StartTime = TimeSpan.Parse("17:00:00") },
            //new SearchInputDto() { StartStopId = new Guid("666d08c1-42f8-424c-9e78-df929600f4b4"), DestinationStopId = new Guid("7e8363b2-4e6a-45bc-b276-72bfc2279cf7"), StartTime = TimeSpan.Parse("16:00:00") },
            //new SearchInputDto() { StartStopId = new Guid("50d19f91-7223-4603-9c6a-500847b42e30"), DestinationStopId = new Guid("3817248e-ffd1-48d8-818b-0ed9ca3bde47"), StartTime = TimeSpan.Parse("18:12:00") },
            //new SearchInputDto() { StartStopId = new Guid("c9835234-011e-4719-b5da-8070864a54ff"), DestinationStopId = new Guid("568c9b9e-2739-4c86-a57c-3ad44a8bcea5"), StartTime = TimeSpan.Parse("13:11:00") },
        };

        [Params(60, 120/*, 180, 240, 300, 360*/)]
        public int MaxTimeAheadFetchingPossibleConnections { get; set; }

        [Params(/*0, 1,*/ 3, 5/*, 10, 25, 50, 100*/)]
        public int MinimumPossibleConnectionsFetched { get; set; }

        [Params(2/*, 3, 4, 5*/)]
        public int MinimalTransferTimeOnTheSameStop { get; set; }

        [Params(5/*, 7, 10, 20*/)]
        public int MaximalTransferTimeBetweenTwoDifferentStops { get; set; }


        public SingleCriteriaDijkstraVsMultipleCriterionDijkstra()
        {
            Console.WriteLine("Constructor - Started");
            var startupService = new StartupService();            
            IServiceProvider serviceProvider = startupService.Configure();
            this.searchInputMapper = serviceProvider.GetRequiredService<SearchInputMapper>();
            this.graphRepository = serviceProvider.GetRequiredService<GraphRepository>();
            this.dijkstraSearchManager = serviceProvider.GetRequiredService<SearchManager>();
            this.multipleCriteriaSearchManager = serviceProvider.GetRequiredService<MultipleCriteriaSearchManager>();
            Console.WriteLine("Constructor - Done");
        }

        [GlobalSetup]
        public async Task PrepareSearchesAndGraph()
        {
            Graph = await GraphSingleton.GetGraph(this.graphRepository);
        }

        [Benchmark]
        public void SingleCriteriaDijkstra()
        {
            var list = new List<FastestPath>();
            var parameters = CreateParameters();
            foreach (var search in Searches)
            {
                var searchInput = searchInputMapper.ToDomainFromGraph(search, parameters, Graph);
                var path = dijkstraSearchManager.PerformSearch(searchInput, Graph);
                list.Add(path); 
                dijkstraSearchManager.ClearVisitedVertices(Graph);
            }
            list.Consume(new Consumer());
        }

        //[Benchmark]
        public void MultipleCriteriaDijkstra()
        {
            var list = new List<BestConnections>();
            var parameters = CreateParameters();
            foreach (var search in Searches)
            {
                var searchInput = searchInputMapper.ToDomainFromGraph(search, parameters, Graph);
                var paths = multipleCriteriaSearchManager.PerformSearch(searchInput, Graph);
                list.Add(paths);
            }
            list.Consume(new Consumer());
        }

        private SearchParameters CreateParameters()
        {
            return new SearchParameters()
            {
                MaxTimeAheadFetchingPossibleConnections = this.MaxTimeAheadFetchingPossibleConnections,
                MinimumPossibleConnectionsFetched = this.MinimumPossibleConnectionsFetched,
                MinimalTransferTime = this.MinimalTransferTimeOnTheSameStop,
                MaximalTransferTime = this.MaximalTransferTimeBetweenTwoDifferentStops,
            };
        }
    }
}
