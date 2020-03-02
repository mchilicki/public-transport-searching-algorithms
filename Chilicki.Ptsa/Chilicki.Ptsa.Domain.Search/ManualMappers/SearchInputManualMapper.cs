using Chilicki.Ptsa.Domain.Search.Dtos;
using Chilicki.Ptsa.Domain.Search.Validators;
using Chilicki.Ptsa.Domain.Search.Aggregates;
using Chilicki.Ptsa.Data.Repositories;
using System.Threading.Tasks;

namespace Chilicki.Ptsa.Domain.Search.ManualMappers
{
    public class SearchInputManualMapper 
    {
        readonly StopRepository stopRepository;


        public SearchInputManualMapper(           
            StopRepository stopRepository)
        {
            this.stopRepository = stopRepository;
        }

        public async Task<SearchInput> ToDomain(SearchInputDto searchInput)
        {            
            return new SearchInput()
            {
                StartStop = await stopRepository.FindAsync(searchInput.StartStopId),
                DestinationStop = await stopRepository.FindAsync(searchInput.DestinationStopId),
                StartTime = searchInput.StartTime,
            };
        }
    }
}
