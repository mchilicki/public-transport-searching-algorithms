using Chilicki.Ptsa.Domain.Search.Services.Base;
using Chilicki.Ptsa.Domain.Search.Validators;
using Chilicki.Ptsa.Domain.Search.Dtos;
using Chilicki.Ptsa.Domain.Search.ManualMappers;
using Chilicki.Ptsa.Domain.Search.Services.GraphFactories.Base;
using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Data.Repositories;
using Chilicki.Ptsa.Domain.Search.Services.Path;
using Chilicki.Ptsa.Domain.Search.Aggregates;
using System.Threading.Tasks;
using System;
using Chilicki.Ptsa.Data.UnitsOfWork;

namespace Chilicki.Ptsa.Domain.Search.Managers
{
    public class SearchManager
    {
        readonly IConnectionSearchEngine connectionSearchEngine;
        readonly FastestPathResolver fastestPathResolver;
        readonly SearchValidator searchValidator;
        readonly SearchInputManualMapper searchInputManualMapper;
        readonly StopRepository stopRepository;
        readonly GraphRepository graphRepository;

        public SearchManager(
            IConnectionSearchEngine connectionSearchEngine,
            FastestPathResolver fastestPathResolver,
            SearchValidator searchValidator,
            SearchInputManualMapper searchInputManualMapper,
            StopRepository stopRepository,
            GraphRepository graphRepository)
        {
            this.connectionSearchEngine = connectionSearchEngine;
            this.searchValidator = searchValidator;
            this.searchInputManualMapper = searchInputManualMapper;
            this.stopRepository = stopRepository;
            this.fastestPathResolver = fastestPathResolver;
            this.graphRepository = graphRepository;
        }

        public async Task<FastestPath> SearchFastestConnections(SearchInputDto searchInputDto)
        {
            await searchValidator.Validate(searchInputDto);
            var searchInput = await searchInputManualMapper.ToDomain(searchInputDto);
            var graph = await graphRepository.GetGraph();
            var fastestConnections = connectionSearchEngine.SearchConnections(searchInput, graph);
            var fastestPath = fastestPathResolver.ResolveFastestPath(searchInput, fastestConnections);
            return fastestPath;
        }        
    }
}
