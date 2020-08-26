using Chilicki.Ptsa.Domain.Search.Services.Base;
using Chilicki.Ptsa.Domain.Search.Validators;
using Chilicki.Ptsa.Domain.Search.Dtos;
using Chilicki.Ptsa.Domain.Search.Mappers;
using Chilicki.Ptsa.Data.Repositories;
using Chilicki.Ptsa.Domain.Search.Services.Path;
using Chilicki.Ptsa.Domain.Search.Aggregates;
using System.Threading.Tasks;
using Chilicki.Ptsa.Domain.Search.Services.SearchInputs;
using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Domain.Search.Helpers.Exceptions;
using System.Collections.Generic;
using System.Diagnostics;
using Chilicki.Ptsa.Domain.Search.Services.Measures;
using System;
using Chilicki.Ptsa.Domain.InputSearches;
using Chilicki.Ptsa.Domain.Search.InputSearches;

namespace Chilicki.Ptsa.Domain.Search.Managers
{
    public class SearchManager
    {
        readonly IConnectionSearchEngine connectionSearchEngine;
        readonly FastestPathResolver fastestPathResolver;
        readonly SearchValidator searchValidator;
        readonly SearchInputMapper mapper;
        readonly GraphRepository graphRepository;
        readonly ShortMeasureLogger measureLogger;
        private readonly string algorithmName = "SingleDijkstra";

        public SearchManager(
            IConnectionSearchEngine connectionSearchEngine,
            FastestPathResolver fastestPathResolver,
            SearchValidator searchValidator,
            SearchInputMapper mapper,
            GraphRepository graphRepository,
            ShortMeasureLogger measureLogger)
        {
            this.connectionSearchEngine = connectionSearchEngine;
            this.searchValidator = searchValidator;
            this.mapper = mapper;
            this.fastestPathResolver = fastestPathResolver;
            this.graphRepository = graphRepository;
            this.measureLogger = measureLogger;
        }

        public async Task SearchFastestConnections(SearchInputDto searchInputDto)
        {            
            await searchValidator.Validate(searchInputDto);
            var search = await mapper.ToDomain(searchInputDto);
            Console.WriteLine("Searching with time-dependent Dijkstra algorithm");
            Console.WriteLine($"From {search.StartStop.Name} to {search.DestinationStop.Name} at {search.StartTime}");
            Console.WriteLine("Fetching graph from database");
            var graph = await graphRepository.GetGraph();
            Console.WriteLine("Graph fetched, starting searching");
            await PerformSearchWithLog(search, graph);
        }

        private async Task PerformSearchWithLog(SearchInput search, Graph graph)
        {
            var stopwatch = Stopwatch.StartNew();
            var path = PerformSearch(search, graph);
            stopwatch.Stop();
            var measure = PerformanceMeasure.Create(path, stopwatch.Elapsed);
            await measureLogger.Log(measure, algorithmName);
        }

        public FastestPath PerformSearch(SearchInput search, Graph graph)
        {
            try
            {
                var fastestConnections = connectionSearchEngine.SearchConnections(search, graph);
                return fastestPathResolver.ResolveFastestPath(search, fastestConnections);
            }
            catch (NoFastestPathExistsException)
            {
                var previousDayStartTime = search.StartTime;
                search.StartTime = TimeSpan.FromMinutes(1);
                return PerformSearchNextDay(search, graph, previousDayStartTime);
            }            
        }

        private FastestPath PerformSearchNextDay(SearchInput search, Graph graph, TimeSpan previousDayStartTime)
        {
            try
            {
                var fastestConnections = connectionSearchEngine.SearchConnections(search, graph);
                search.StartTime = previousDayStartTime;
                return fastestPathResolver.ResolveFastestPath(search, fastestConnections);
            }
            catch (NoFastestPathExistsException)
            {                
                return fastestPathResolver.CreateNotFoundPath(search);
            }
        }

        public void ClearVisitedVertices(Graph graph)
        {
            foreach (var vertex in graph.Vertices)
            {
                vertex.IsVisited = false;
            }
        }
    }
}
