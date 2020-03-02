using Chilicki.Ptsa.Domain.Search.Dtos;
using Chilicki.Ptsa.Domain.Search.Validators;
using Chilicki.Ptsa.Domain.Search.Aggregates;
using Chilicki.Ptsa.Data.Repositories;
using System.Threading.Tasks;

namespace Chilicki.Ptsa.Domain.Search.ManualMappers
{
    public class SearchInputManualMapper 
    {
        readonly StopRepository _stopRepository;
        readonly SearchValidator _searchValidator;


        public SearchInputManualMapper(           
            StopRepository stopRepository,
            SearchValidator searchValidator)
        {
            _stopRepository = stopRepository;
            _searchValidator = searchValidator;
        }

        public async Task<SearchInput> ToDomain(SearchInputDto searchInput)
        {            
            return new SearchInput()
            {
                StartStop = await _stopRepository.FindAsync(searchInput.StartStopId),
                DestinationStop = await _stopRepository.FindAsync(searchInput.DestinationStopId),
                StartTime = searchInput.StartTime,
            };
        }
    }
}
