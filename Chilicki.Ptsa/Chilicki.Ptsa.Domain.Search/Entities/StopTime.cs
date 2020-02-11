using Chilicki.Ptsa.Domain.Search.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chilicki.Ptsa.Domain.Search.Entities
{
    public class StopTime : BaseEntity
    {
        public Trip Trip { get; set; }
        public Stop Stop { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public int StopSequence { get; set; }
    }
}
