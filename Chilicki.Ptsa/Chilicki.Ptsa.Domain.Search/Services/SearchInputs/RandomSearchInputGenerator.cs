using Chilicki.Ptsa.Data.Repositories;
using Chilicki.Ptsa.Domain.Search.Dtos;
using Chilicki.Ptsa.Domain.Search.Helpers.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Chilicki.Ptsa.Domain.Search.Services.SearchInputs
{
    public class RandomSearchInputGenerator
    {
        readonly StopRepository stopRepository;

        public RandomSearchInputGenerator(
            StopRepository stopRepository)
        {
            this.stopRepository = stopRepository;
        }

        public async Task<IEnumerable<SearchInputDto>> Generate(int searchInputCount)
        {
            var stopIds = await stopRepository.GetStopIds();
            var stopIdsCount = stopIds.Count;
            var random = new Random();
            var searchInputs = new List<SearchInputDto>();
            for (int i = 0; i < searchInputCount; i++)
            {
                var startStopRandomIndex = random.Next(0, stopIdsCount);
                var startStopId = stopIds[startStopRandomIndex];
                var endStopRandomIndex = random.Next(0, stopIdsCount);
                var endStopId = stopIds[endStopRandomIndex];
                var startTime = random.NextTimeSpan();
                var searchInput = SearchInputDto.Create(startStopId, endStopId, startTime);
                searchInputs.Add(searchInput);
            }
            return searchInputs;
        }
    }
}
