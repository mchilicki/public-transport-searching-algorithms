using Chilicki.Ptsa.Data.Configurations.ProjectConfiguration;
using System;

namespace Chilicki.Ptsa.Domain.Search.Dtos
{
    public class SearchInputDto
    {
        public Guid StartStopId { get; set; }
        public Guid DestinationStopId { get; set; }
        public TimeSpan StartTime { get; set; }

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

        public static SearchInputDto Create(AppSettings settings)
        {
            return new SearchInputDto
            {
                StartStopId = settings.StartStopId,
                DestinationStopId = settings.EndStopId,
                StartTime = settings.StartTime,
            };            
        }
    }
}
