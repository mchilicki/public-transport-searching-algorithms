using Chilicki.Ptsa.Domain.Search.Aggregates;
using Chilicki.Ptsa.Domain.Search.Aggregates.Graphs;
using System;
using System.Linq;

namespace Chilicki.Ptsa.Domain.Search.Services.Path
{
    public class FastestPathTimeCalculator
    {
        public int CalculateTravelTime(FastestPath fastestPath)
        {
            var travelStartTime = fastestPath.Path
                .First(p => p.IsTransfer == false)
                .StartStopTime.DepartureTime;
            var travelEndTime = fastestPath.Path
                .Last(p => p.IsTransfer == false)
                .EndStopTime.DepartureTime;
            return (int)Math.Abs((travelEndTime - travelStartTime).TotalMinutes);
        }

        public int CalculateConnectionTime(StopConnection waitingConnection)
        {
            var travelStartTime = waitingConnection.StartStopTime.DepartureTime;
            var travelEndTime = waitingConnection.EndStopTime.DepartureTime;
            return (int)Math.Abs((travelEndTime - travelStartTime).TotalMinutes);
        }
    }
}
