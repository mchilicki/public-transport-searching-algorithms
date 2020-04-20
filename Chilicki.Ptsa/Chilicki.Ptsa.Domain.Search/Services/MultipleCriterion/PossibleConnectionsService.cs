using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Domain.Search.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chilicki.Ptsa.Domain.Search.Services.MultipleCriterion
{
    public class PossibleConnectionsService
    {
        public IEnumerable<Connection> GetPossibleConnections(
            IEnumerable<Connection> connections, TimeSpan earliestTime)
        {
            var latestTime = earliestTime.Add(TimeSpan.FromHours(2));
            var possibleConnections = connections
                .Where(p =>
                    p.DepartureTime >= earliestTime &&
                    p.DepartureTime <= latestTime
                );
            var bla = connections.OrderBy(p => p.DepartureTime);
            if (possibleConnections.Count() >= 3)                
                return possibleConnections;
            possibleConnections = connections
                .Where(p => p.DepartureTime >= earliestTime)
                .OrderBy(p => p.DepartureTime)
                .Take(3);
            return possibleConnections;
        }
    }
}
