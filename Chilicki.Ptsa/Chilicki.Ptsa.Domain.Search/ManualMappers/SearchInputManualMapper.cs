using Chilicki.Ptsa.Domain.Search.Dtos;
using Chilicki.Ptsa.Domain.Search.Aggregates;
using Chilicki.Ptsa.Data.Repositories;
using System.Threading.Tasks;

namespace Chilicki.Ptsa.Domain.Search.ManualMappers
{
    public class SearchInputManualMapper 
    {
        readonly StopRepository stopRepository;
        readonly VertexRepository vertexRepository;

        public SearchInputManualMapper(           
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
    }
}
