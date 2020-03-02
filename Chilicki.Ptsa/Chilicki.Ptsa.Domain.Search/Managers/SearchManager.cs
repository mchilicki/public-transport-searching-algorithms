using Chilicki.Ptsa.Domain.Search.Services.Base;
using Chilicki.Ptsa.Domain.Search.Validators;
using Chilicki.Ptsa.Domain.Search.Dtos;
using Chilicki.Ptsa.Domain.Search.ManualMappers;
using Chilicki.Ptsa.Domain.Search.Services.GraphFactories.Base;
using Chilicki.Ptsa.Domain.Search.Aggregates.Graphs;
using Chilicki.Ptsa.Data.Repositories;
using Chilicki.Ptsa.Domain.Search.Services.Path;
using Chilicki.Ptsa.Domain.Search.Aggregates;
using System.Threading.Tasks;

namespace Chilicki.Ptsa.Domain.Search.Managers
{
    public class SearchManager
    {
        readonly IConnectionSearchEngine _connectionSearchEngine;
        readonly IGraphFactory<StopGraph> _graphGenerator;
        readonly FastestPathResolver _fastestPathResolver;
        readonly SearchValidator _searchValidator;
        readonly SearchInputManualMapper _searchInputManualMapper;
        readonly StopRepository _stopRepository;

        public SearchManager(
            IConnectionSearchEngine connectionSearchEngine,
            IGraphFactory<StopGraph> graphGenerator,
            FastestPathResolver fastestPathResolver,
            SearchValidator searchValidator,
            SearchInputManualMapper searchInputManualMapper,
            StopRepository stopRepository)
        {
            _connectionSearchEngine = connectionSearchEngine;
            _graphGenerator = graphGenerator;
            _searchValidator = searchValidator;
            _searchInputManualMapper = searchInputManualMapper;
            _stopRepository = stopRepository;
            _fastestPathResolver = fastestPathResolver;
        }

        public async Task<FastestPath> SearchFastestConnections(SearchInputDto searchInputDTO)
        {
            await _searchValidator.Validate(searchInputDTO);
            var searchInput = await _searchInputManualMapper.ToDomain(searchInputDTO);
            var stops = await _stopRepository.GetAllAsync();
            var stopGraph = _graphGenerator.CreateGraph(stops, searchInput.StartTime);
            var fastestConnections = _connectionSearchEngine.SearchConnections(searchInput, stopGraph);
            var fastestPath = _fastestPathResolver.ResolveFastestPath(searchInput, fastestConnections);
            return fastestPath;
        }
    }
}
