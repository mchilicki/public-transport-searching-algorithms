using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Data.Repositories;
using Chilicki.Ptsa.Domain.Search.Aggregates;
using Chilicki.Ptsa.Domain.Search.Aggregates.MultipleCriterion;
using Chilicki.Ptsa.Domain.Search.Dtos;
using Chilicki.Ptsa.Domain.Search.Helpers.Exceptions;
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
        readonly FastestPathResolver fastestPathResolver;
        readonly SearchValidator searchValidator;
        readonly SearchInputMapper mapper;
        readonly GraphRepository graphRepository;
        readonly RandomSearchInputGenerator searchInputGenerator;
        readonly MeasureLogger measureLogger;

        public MultipleCriteriaSearchManager(
            MultipleCriterionDijkstraSearch searchEngine,
            FastestPathResolver fastestPathResolver,
            SearchValidator searchValidator,
            SearchInputMapper mapper,
            GraphRepository graphRepository,
            RandomSearchInputGenerator searchInputGenerator,
            MeasureLogger measureLogger)
        {
            this.searchEngine = searchEngine;
            this.searchValidator = searchValidator;
            this.mapper = mapper;
            this.fastestPathResolver = fastestPathResolver;
            this.graphRepository = graphRepository;
            this.searchInputGenerator = searchInputGenerator;
            this.measureLogger = measureLogger;
        }

        public async Task SearchBestConnections(SearchInputDto searchInputDto)
        {
            await searchValidator.Validate(searchInputDto);
            var search = await mapper.ToDomain(searchInputDto);
            var graph = await graphRepository.GetGraph();
            PerformSearch(search, graph);
        }

        private IEnumerable<FastestPath> PerformSearch(SearchInput search, Graph graph)
        {
            try
            {
                var bestConnections = searchEngine.SearchConnections(search, graph);
                return null;
            }
            catch (DijkstraNoFastestPathExistsException)
            {
                return null;
            }
        }
    }
}
