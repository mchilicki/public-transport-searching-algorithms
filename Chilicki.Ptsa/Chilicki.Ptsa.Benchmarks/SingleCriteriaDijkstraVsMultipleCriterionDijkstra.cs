using BenchmarkDotNet.Attributes;
using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Data.Repositories;
using Chilicki.Ptsa.Domain.Search.Aggregates;
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

        private Graph Graph { get; set; }
        private IEnumerable<SearchInput> Searches { get; set; }

        private const int SearchInputCount = 10;

        public SingleCriteriaDijkstraVsMultipleCriterionDijkstra()
        {
        }

        [GlobalSetup]
        public async Task PrepareGraph()
        {
            var searchInputDtos = await searchInputGenerator.Generate(SearchInputCount);
            Searches = await searchInputMapper.ToDomain(searchInputDtos);
            Graph = await graphRepository.GetGraph();
        }

        [Benchmark]
        public void SingleCriteriaDijkstra()
        {
            dijkstraSearchManager.PerformSearch(Searches.First(), Graph);
        }
    }
}
