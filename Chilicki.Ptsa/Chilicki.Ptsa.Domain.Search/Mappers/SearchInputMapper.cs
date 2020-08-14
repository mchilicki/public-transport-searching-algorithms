using Chilicki.Ptsa.Domain.Search.Dtos;
using Chilicki.Ptsa.Domain.Search.Aggregates;
using Chilicki.Ptsa.Data.Repositories;
using System.Threading.Tasks;
using System.Collections.Generic;
using Chilicki.Ptsa.Data.Entities;
using System.Linq;
using Chilicki.Ptsa.Domain.Search.Configurations.Options;

namespace Chilicki.Ptsa.Domain.Search.Mappers
{
    public class SearchInputMapper 
    {
        readonly StopRepository stopRepository;
        readonly VertexRepository vertexRepository;

        public SearchInputMapper(           
            StopRepository stopRepository,
            VertexRepository vertexRepository)
        {
            this.stopRepository = stopRepository;
            this.vertexRepository = vertexRepository;
        }

        public async Task<SearchInput> ToDomain(SearchInputDto searchInput)
        {            
            return new SearchInput()
            {
                StartStop = await stopRepository.FindAsync(searchInput.StartStopId),
                DestinationStop = await stopRepository.FindAsync(searchInput.DestinationStopId),
                StartVertex = await vertexRepository.GetByStopId(searchInput.StartStopId),
                DestinationVertex = await vertexRepository.GetByStopId(searchInput.DestinationStopId),
                StartTime = searchInput.StartTime,
                Parameters = new SearchParameters()
                {
                    MaxTimeAheadFetchingPossibleConnections = 120,
                    MaximalTransferDistanceInMinutes = 20,
                    MinimalTransferTime = 0,
                    MinimumPossibleConnectionsFetched = 3,
                }
            };
        }

        public SearchInput ToDomainFromGraph(SearchInputDto searchInputDto, SearchParameters parameters, Graph graph)
        {
            var startVertex = graph.Vertices.FirstOrDefault(p => p.StopId == searchInputDto.StartStopId);
            var destinationVertex = graph.Vertices.FirstOrDefault(p => p.StopId == searchInputDto.DestinationStopId);
            return new SearchInput()
            {
                StartStop = startVertex.Stop,
                DestinationStop = destinationVertex.Stop,
                StartVertex = startVertex,
                DestinationVertex = destinationVertex,
                StartTime = searchInputDto.StartTime,
                Parameters = parameters,
            };
        }

        public async Task<IEnumerable<SearchInput>> ToDomain(IEnumerable<SearchInputDto> searchInputDtos)
        {
            var searchInputs = new List<SearchInput>();
            foreach (var searchInputDto in searchInputDtos)
            {
                var searchInput = await ToDomain(searchInputDto);
                searchInputs.Add(searchInput);
            }
            return searchInputs;
        }
    }
}
