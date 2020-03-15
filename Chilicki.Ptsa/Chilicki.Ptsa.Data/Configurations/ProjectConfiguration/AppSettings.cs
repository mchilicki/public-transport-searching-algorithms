using System;

namespace Chilicki.Ptsa.Data.Configurations.ProjectConfiguration
{
    public class AppSettings
    {
        public string ImportGtfsPath { get; set; }
        public Guid StartStopId { get; set; }
        public Guid EndStopId { get; set; }
        public TimeSpan StartTime { get; set; }
        public int BenchmarkIterations { get; set; }
    }
}
