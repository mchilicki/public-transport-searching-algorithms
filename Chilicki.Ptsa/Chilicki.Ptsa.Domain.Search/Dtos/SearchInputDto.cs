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
}
}
