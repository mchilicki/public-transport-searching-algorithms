using System;
using System.Collections.Generic;
using System.Text;

namespace Chilicki.Ptsa.Data.Configurations.ProjectConfiguration
{
    public class SearchSettings
    {
        public Guid StartStopId { get; set; }
        public Guid EndStopId { get; set; }
        public TimeSpan StartTime { get; set; }
        public SearchParameters Parameters { get; set; }
    }
}
