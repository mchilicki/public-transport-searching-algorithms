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
        readonly IGraphFactory<Graph> graphFactory;
        readonly FastestPathResolver fastestPathResolver;
        readonly SearchValidator searchValidator;
        readonly SearchInputManualMapper searchInputManualMapper;
        readonly StopRepository stopRepository;
        readonly GraphRepository graphRepository;
        readonly IUnitOfWork unitOfWork;

        public SearchManager(
            IConnectionSearchEngine connectionSearchEngine,
            IGraphFactory<Graph> graphFactory,
            FastestPathResolver fastestPathResolver,
            SearchValidator searchValidator,
            SearchInputManualMapper searchInputManualMapper,
            StopRepository stopRepository,
            GraphRepository graphRepository,
            IUnitOfWork unitOfWork)
        {
            this.connectionSearchEngine = connectionSearchEngine;
            this.graphFactory = graphFactory;
            this.searchValidator = searchValidator;
            this.searchInputManualMapper = searchInputManualMapper;
            this.stopRepository = stopRepository;
            this.fastestPathResolver = fastestPathResolver;
            this.graphRepository = graphRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<FastestPath> SearchFastestConnections(SearchInputDto searchInputDto)
        {
            await searchValidator.Validate(searchInputDto);
            var searchInput = await searchInputManualMapper.ToDomain(searchInputDto);
            var graph = await graphRepository.GetGraph(searchInput.StartTime);
            var fastestConnections = connectionSearchEngine.SearchConnections(searchInput, graph);
            var fastestPath = fastestPathResolver.ResolveFastestPath(searchInput, fastestConnections);
            return fastestPath;
        }

        public async Task CreateGraph()
        {
            var stops = await stopRepository.GetAllAsync();
            var graph = graphFactory.CreateGraph(stops, TimeSpan.Zero);
            await graphRepository.AddAsync(graph);
            await unitOfWork.SaveAsync();
            graph = await graphRepository.GetWholeGraph();
            graphFactory.FillVerticesWithSimilarVertices(graph, stops);
            await unitOfWork.SaveAsync();
        }
    }
}
