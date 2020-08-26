using Chilicki.Ptsa.Data.Configurations.ProjectConfiguration;
using System;

namespace Chilicki.Ptsa.Domain.Search.Dtos
{
    public class SearchInputDto
    {
        public Guid StartStopId { get; set; }
        public Guid DestinationStopId { get; set; }
        public TimeSpan StartTime { get; set; }
        public SearchParameters Parameters { get; set; }

        public static SearchInputDto Create(            
            Guid startStopId, Guid destinationStopId, TimeSpan startTime)
        {
            return new SearchInputDto()
            {
                StartStopId = startStopId,
                DestinationStopId = destinationStopId,
                StartTime = startTime,
            };
        }

        public static SearchInputDto Create(SearchSettings settings)
        {
            return new SearchInputDto
            {
                StartStopId = settings.StartStopId,
                DestinationStopId = settings.EndStopId,
                StartTime = settings.StartTime,
                Parameters = settings.Parameters,
            };            
        }

        public override string ToString()
        {
            return $"{StartStopId} {DestinationStopId} {StartTime}";
        }
    }
}
