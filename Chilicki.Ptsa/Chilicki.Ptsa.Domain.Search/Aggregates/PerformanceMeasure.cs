using System;
using System.Collections.Generic;
using System.Text;

namespace Chilicki.Ptsa.Domain.Search.Aggregates
{
    public class PerformanceMeasure
    {
        public FastestPath FastestPath { get; internal set; }
        public TimeSpan Time { get; internal set; }

        public static PerformanceMeasure Create(FastestPath fastestPath, TimeSpan time)
        {
            return new PerformanceMeasure()
            {
                FastestPath = fastestPath,
                Time = time,
            };
        }
    }
}
