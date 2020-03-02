using Chilicki.Ptsa.Domain.Search.Dtos;
using Chilicki.Ptsa.Domain.Search.Resources;
using Chilicki.Ptsa.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chilicki.Ptsa.Domain.Search.Validators
{
    public class SearchValidator
    {
        readonly StopRepository stopRepository;

        public SearchValidator(StopRepository stopRepository)
        {
            this.stopRepository = stopRepository;
        }

        public async Task<bool> Validate(SearchInputDto search)
        {
            if (search == null)
                throw new ArgumentNullException(nameof(search));
            if (search.StartStopId == Guid.Empty)
                throw new ArgumentException(SearchValidationResources.StartStopIsEmpty);
            if (search.DestinationStopId == Guid.Empty)
                throw new ArgumentException(SearchValidationResources.EndStopIsEmpty);
            if (!await stopRepository.DoesStopWithIdExist(search.StartStopId))
                throw new ArgumentException($"{SearchValidationResources.StopWithIdDoesNotExist} {search.StartStopId}");
            if (!await stopRepository.DoesStopWithIdExist(search.DestinationStopId))
                throw new ArgumentException($"{SearchValidationResources.StopWithIdDoesNotExist} {search.DestinationStopId}");
            if (search.StartTime.Equals(TimeSpan.Zero))
                throw new ArgumentException(SearchValidationResources.DateIsEmpty);
            return true;
        }

        public async Task<bool> Validate(IEnumerable<SearchInputDto> searches)
        {
            foreach (var search in searches)
            {
                await Validate(search);
            }
            return true;
        }
    }
}
