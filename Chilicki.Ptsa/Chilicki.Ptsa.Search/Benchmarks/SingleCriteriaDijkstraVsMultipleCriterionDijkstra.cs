﻿using BenchmarkDotNet.Attributes;
using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Data.Repositories;
using Chilicki.Ptsa.Domain.Search.Aggregates;
using Chilicki.Ptsa.Domain.Search.Dtos;
using Chilicki.Ptsa.Domain.Search.Managers;
using Chilicki.Ptsa.Domain.Search.Mappers;
using Chilicki.Ptsa.Domain.Search.Services;
using Chilicki.Ptsa.Domain.Search.Services.SearchInputs;
using Chilicki.Ptsa.Search.Configurations.Startup;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chilicki.Ptsa.Benchmarks
{
    public class SingleCriteriaDijkstraVsMultipleCriterionDijkstra
    {
        private readonly RandomSearchInputGenerator searchInputGenerator;
        private readonly SearchInputMapper searchInputMapper;
        private readonly GraphRepository graphRepository;
        private readonly SearchManager dijkstraSearchManager;
        private readonly MultipleCriteriaSearchManager multipleCriteriaSearchManager;

        public Graph Graph { get; set; }
        public IEnumerable<SearchInputDto> Searches { get; set; } = new List<SearchInputDto>()
        {
            new SearchInputDto() { StartStopId = new Guid("8fda5d8f-e959-4aad-9d64-8e857beb313e"),DestinationStopId = new Guid("87e7a7eb-7bf1-4df0-8ed3-62b845bd535d"), StartTime = TimeSpan.Parse("09:33:00") },
            //new SearchInputDto() { StartStopId = new Guid("f88dc322-e038-45c4-8af2-4a6be7f0664b"),DestinationStopId = new Guid("1f8dd008-3940-4630-96d5-b48889feefb5"), StartTime = TimeSpan.Parse("17:09:00") },
            //new SearchInputDto() { StartStopId = new Guid("666d08c1-42f8-424c-9e78-df929600f4b4"),DestinationStopId = new Guid("7e8363b2-4e6a-45bc-b276-72bfc2279cf7"), StartTime = TimeSpan.Parse("16:34:00") },
            //new SearchInputDto() { StartStopId = new Guid("50d19f91-7223-4603-9c6a-500847b42e30"),DestinationStopId = new Guid("3817248e-ffd1-48d8-818b-0ed9ca3bde47"), StartTime = TimeSpan.Parse("18:12:00") },
            //new SearchInputDto() { StartStopId = new Guid("c9835234-011e-4719-b5da-8070864a54ff"),DestinationStopId = new Guid("568c9b9e-2739-4c86-a57c-3ad44a8bcea5"), StartTime = TimeSpan.Parse("13:11:00") },
        };

        public SingleCriteriaDijkstraVsMultipleCriterionDijkstra()
        {
            Console.WriteLine("Constructor - Started");
            var startupService = new StartupService();            
            IServiceProvider serviceProvider = startupService.Configure();
            this.searchInputGenerator = serviceProvider.GetRequiredService<RandomSearchInputGenerator>();
            this.searchInputMapper = serviceProvider.GetRequiredService<SearchInputMapper>();
            this.graphRepository = serviceProvider.GetRequiredService<GraphRepository>();
            this.dijkstraSearchManager = serviceProvider.GetRequiredService<SearchManager>();
            this.multipleCriteriaSearchManager = serviceProvider.GetRequiredService<MultipleCriteriaSearchManager>();
            Console.WriteLine("Constructor - Done");
        }

        [GlobalSetup]
        public async Task PrepareSearchesAndGraph()
        {
            Console.WriteLine("FetchingGraphFromDatabase - Started");
            Graph = await graphRepository.GetGraph();
            Console.WriteLine("FetchingGraphFromDatabase - Done");
        }

        [Benchmark]
        [ArgumentsSource(nameof(Searches))]
        public void SingleCriteriaDijkstra(SearchInputDto search)
        {
            var searchInput = searchInputMapper.ToDomainFromGraph(search, Graph);
            dijkstraSearchManager.PerformSearch(searchInput, Graph);
            dijkstraSearchManager.ClearVisitedVertices(Graph);
        }

        [Benchmark]
        [ArgumentsSource(nameof(Searches))]
        public void MultipleCriteriaDijkstra(SearchInputDto search)
        {
            var searchInput = searchInputMapper.ToDomainFromGraph(search, Graph);
            multipleCriteriaSearchManager.PerformSearch(searchInput, Graph);
        }
    }
}