using System;
using System.Collections.Generic;
using System.Text;

namespace Chilicki.Ptsa.Domain.Search.Aggregates
{
    public class PerformanceMeasure
    {
        public ICollection<FastestPath> FastestPaths { get; internal set; }
        public TimeSpan Time { get; internal set; }

        public static PerformanceMeasure Create(FastestPath fastestPath, TimeSpan time)
        {
            return new PerformanceMeasure()
            {
                FastestPaths = new List<FastestPath>() { fastestPath },
                Time = time,
            };
        }

        public static PerformanceMeasure Create(ICollection<FastestPath> fastestPaths, TimeSpan time)
        {
            return new PerformanceMeasure()
            {
                FastestPaths = fastestPaths,
                Time = time,
            };
        }
    }
}
