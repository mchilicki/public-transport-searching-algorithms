﻿using Chilicki.Ptsa.Domain.Search.Services.Base;
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

namespace Chilicki.Ptsa.Domain.Search.Managers
{
    public class SearchManager
    {
        readonly IConnectionSearchEngine connectionSearchEngine;
        readonly FastestPathResolver fastestPathResolver;
        readonly SearchValidator searchValidator;
        readonly SearchInputMapper mapper;
        readonly GraphRepository graphRepository;
        readonly RandomSearchInputGenerator searchInputGenerator;
        readonly MeasureLogger measureLogger;

        public SearchManager(
            IConnectionSearchEngine connectionSearchEngine,
            FastestPathResolver fastestPathResolver,
            SearchValidator searchValidator,
            SearchInputMapper mapper,
            GraphRepository graphRepository,
            RandomSearchInputGenerator searchInputGenerator,
            MeasureLogger measureLogger)
        {
            this.connectionSearchEngine = connectionSearchEngine;
            this.searchValidator = searchValidator;
            this.mapper = mapper;
            this.fastestPathResolver = fastestPathResolver;
            this.graphRepository = graphRepository;
            this.searchInputGenerator = searchInputGenerator;
            this.measureLogger = measureLogger;
        }

        public async Task<FastestPath> SearchFastestConnections(SearchInputDto searchInputDto)
        {
            await searchValidator.Validate(searchInputDto);
            var searchInput = await mapper.ToDomain(searchInputDto);
            var graph = await graphRepository.GetGraph();
            return PerformSearch(searchInput, graph);
        }

        private FastestPath PerformSearch(SearchInput searchInput, Graph graph)
        {
            try
            {
                var fastestConnections = connectionSearchEngine.SearchConnections(searchInput, graph);
                return fastestPathResolver.ResolveFastestPath(searchInput, fastestConnections);
            }
            catch (DijkstraNoFastestPathExistsException)
            {
                return fastestPathResolver.CreateNotFoundPath(searchInput);
            }            
        }

        public async Task PerformDijkstraBenchmark(int searchInputCount)
        {
            var searchInputDtos = await searchInputGenerator.Generate(searchInputCount);
            var searchInputs = await mapper.ToDomain(searchInputDtos);
            var graph = await graphRepository.GetGraph();
            var measures = new List<PerformanceMeasure>();
            foreach (var searchInput in searchInputs)
            {
                var stopwatch = Stopwatch.StartNew();
                var path = PerformSearch(searchInput, graph);
                stopwatch.Stop();
                measures.Add(PerformanceMeasure.Create(path, stopwatch.Elapsed));
                ClearVisitedVertices(graph);
            }
            await measureLogger.Log(measures);
        }

        private void ClearVisitedVertices(Graph graph)
        {
            foreach (var vertex in graph.Vertices)
            {
                vertex.IsVisited = false;
            }
        }
    }
}
