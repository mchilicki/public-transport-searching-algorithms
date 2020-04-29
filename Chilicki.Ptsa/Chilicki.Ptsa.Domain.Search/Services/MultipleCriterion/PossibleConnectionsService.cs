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
        private readonly MultipleCriterionGraphService graphService;

        public PossibleConnectionsService(
            MultipleCriterionGraphService graphService)
        {
            this.graphService = graphService;
        }

        public IEnumerable<Connection> GetPossibleConnections(
            Vertex vertex, TimeSpan earliestTime)
        {
            var connections = graphService.GetPossibleConnections(vertex, earliestTime);
            var latestTime = earliestTime.Add(TimeSpan.FromHours(2));
            var possibleConnections = connections
                .Where(p =>
                    p.DepartureTime >= earliestTime &&
                    p.DepartureTime <= latestTime
                )
                .OrderBy(p => p.ArrivalTime)
                .Take(3);
            var bla = connections.OrderBy(p => p.DepartureTime);
            if (possibleConnections.Count() >= 3)                
                return possibleConnections;
            var firstPossibleConnections = connections
                .Where(p => p.DepartureTime >= earliestTime)
                .OrderBy(p => p.ArrivalTime)
                .Take(3);
            return firstPossibleConnections;
        }
    }
}
