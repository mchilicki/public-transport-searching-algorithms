using Chilicki.Ptsa.Domain.Search.Dtos;
using Chilicki.Ptsa.Domain.Search.Aggregates;
using Chilicki.Ptsa.Data.Repositories;
using System.Threading.Tasks;
using System.Collections.Generic;

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
