using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Data.Repositories;
using Chilicki.Ptsa.Domain.InputSearches;
using Chilicki.Ptsa.Domain.Search.Aggregates;
using Chilicki.Ptsa.Domain.Search.Aggregates.MultipleCriterion;
using Chilicki.Ptsa.Domain.Search.Dtos;
using Chilicki.Ptsa.Domain.Search.Helpers.Exceptions;
using Chilicki.Ptsa.Domain.Search.InputSearches;
using Chilicki.Ptsa.Domain.Search.Mappers;
using Chilicki.Ptsa.Domain.Search.Services;
using Chilicki.Ptsa.Domain.Search.Services.Measures;
using Chilicki.Ptsa.Domain.Search.Services.Path;
using Chilicki.Ptsa.Domain.Search.Services.SearchInputs;
using Chilicki.Ptsa.Domain.Search.Validators;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Chilicki.Ptsa.Domain.Search.Managers
{
    public class MultipleCriteriaSearchManager
    {
        readonly MultipleCriterionDijkstraSearch searchEngine;
        readonly BestPathResolver bestPathResolver;
        readonly SearchValidator searchValidator;
        readonly SearchInputMapper mapper;
        readonly GraphRepository graphRepository;
        readonly RandomSearchInputGenerator searchInputGenerator;
        readonly ShortMeasureLogger measureLogger;
        private readonly string algorithmName = "MultiDijkstra";

        public MultipleCriteriaSearchManager(
            MultipleCriterionDijkstraSearch searchEngine,
            BestPathResolver bestPathResolver,
            SearchValidator searchValidator,
            SearchInputMapper mapper,
            GraphRepository graphRepository,
            RandomSearchInputGenerator searchInputGenerator,
            ShortMeasureLogger measureLogger)
        {
            this.searchEngine = searchEngine;
            this.searchValidator = searchValidator;
            this.mapper = mapper;
            this.bestPathResolver = bestPathResolver;
            this.graphRepository = graphRepository;
            this.searchInputGenerator = searchInputGenerator;
            this.measureLogger = measureLogger;
        }

        public async Task SearchBestConnections(SearchInputDto searchInputDto)
        {
            await searchValidator.Validate(searchInputDto);
            var search = await mapper.ToDomain(searchInputDto);
            var graph = await graphRepository.GetGraph();
            await PerformSearchWithLog(search, graph);
        }

        private async Task PerformSearchWithLog(SearchInput search, Graph graph)
        {
            var stopwatch = Stopwatch.StartNew();
            var bestConnections = PerformSearch(search, graph);
            stopwatch.Stop();
            var paths = bestPathResolver.ResolveBestPaths(search, bestConnections);
            var measure = PerformanceMeasure.Create(paths, stopwatch.Elapsed);
            await measureLogger.Log(measure, algorithmName);
        }

        public BestConnections PerformSearch(SearchInput search, Graph graph)
        {
            try
            {
                return searchEngine.SearchConnections(search, graph);                
            }
            catch (NoFastestPathExistsException)
            {
                return null;
            }
        }
    }
}
