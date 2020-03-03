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

namespace Chilicki.Ptsa.Domain.Search.Managers
{
    public class SearchManager
    {
        readonly IConnectionSearchEngine connectionSearchEngine;
        readonly IGraphFactory<Graph> graphGenerator;
        readonly FastestPathResolver fastestPathResolver;
        readonly SearchValidator searchValidator;
        readonly SearchInputManualMapper searchInputManualMapper;
        readonly StopRepository stopRepository;

        public SearchManager(
            IConnectionSearchEngine connectionSearchEngine,
            IGraphFactory<Graph> graphGenerator,
            FastestPathResolver fastestPathResolver,
            SearchValidator searchValidator,
            SearchInputManualMapper searchInputManualMapper,
            StopRepository stopRepository)
        {
            this.connectionSearchEngine = connectionSearchEngine;
            this.graphGenerator = graphGenerator;
            this.searchValidator = searchValidator;
            this.searchInputManualMapper = searchInputManualMapper;
            this.stopRepository = stopRepository;
            this.fastestPathResolver = fastestPathResolver;
        }

        public async Task<FastestPath> SearchFastestConnections(SearchInputDto searchInputDTO)
        {
            await searchValidator.Validate(searchInputDTO);
            var searchInput = await searchInputManualMapper.ToDomain(searchInputDTO);
            var stops = await stopRepository.GetAllAsync();
            var stopGraph = graphGenerator.CreateGraph(stops, searchInput.StartTime);
            var fastestConnections = connectionSearchEngine.SearchConnections(searchInput, stopGraph);
            var fastestPath = fastestPathResolver.ResolveFastestPath(searchInput, fastestConnections);
            return fastestPath;
        }
    }
}
