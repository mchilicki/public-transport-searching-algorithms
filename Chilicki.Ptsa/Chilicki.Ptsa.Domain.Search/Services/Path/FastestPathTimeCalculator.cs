using Chilicki.Ptsa.Domain.Search.Aggregates;
using Chilicki.Ptsa.Data.Entities;
using System;
using System.Linq;

namespace Chilicki.Ptsa.Domain.Search.Services.Path
{
    public class FastestPathTimeCalculator
    {
        public int CalculateTravelTime(FastestPath fastestPath)
        {
            var departureTime = fastestPath.Path
                .First(p => p.IsTransfer == false)
                .DepartureTime;
            var arrivalTime = fastestPath.Path
                .Last(p => p.IsTransfer == false)
                .ArrivalTime;
            return (int)Math.Abs((arrivalTime - departureTime).TotalMinutes);
        }

        public int CalculateConnectionTime(Connection waitingConnection)
        {
            var departureTime = waitingConnection.DepartureTime;
            var arrivalTime = waitingConnection.ArrivalTime;
            return (int)Math.Abs((arrivalTime - departureTime).TotalMinutes);
        }
    }
}
