using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Domain.Search.Aggregates.MultipleCriterion;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chilicki.Ptsa.Domain.Search.Services.Calculations
{
    public class ConnectionTimeCalculator
    {
        public int CalculateConnectionTime(Connection connection)
        {
            return CalculateConnectionTime(connection.DepartureTime, connection.ArrivalTime);
        }

        public int CalculateConnectionTime(TimeSpan startTime, TimeSpan endTime)
        {
            var timeElapsed = endTime - startTime;
            return (int)Math.Ceiling(timeElapsed.TotalMinutes);
        }

        public int CalculateAllConnectionsTime(Label currentLabel, Connection connection)
        {
            var newConnectionTime = CalculateConnectionTime(
                currentLabel.Connection.ArrivalTime, connection.ArrivalTime);
            var allConnectionsTime = currentLabel.TimeCriterion.Value + newConnectionTime;
            return allConnectionsTime;
        }
    }
}
