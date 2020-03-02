using System;

namespace Chilicki.Ptsa.Domain.Search.Dtos
{
    public class SearchInputDto
    {
        public Guid StartStopId { get; set; }
        public Guid DestinationStopId { get; set; }
        public TimeSpan StartTime { get; set; }
    }
}
