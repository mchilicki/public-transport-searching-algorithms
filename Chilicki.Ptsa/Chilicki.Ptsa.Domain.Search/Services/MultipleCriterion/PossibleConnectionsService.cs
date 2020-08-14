using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Domain.Search.Aggregates;
using Chilicki.Ptsa.Domain.Search.Configurations.Options;
using Microsoft.Extensions.Options;
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
            Vertex vertex, TimeSpan earliestTime, bool isPreviousConnTransfer, SearchInput search)
        {
            var connections = graphService.GetPossibleConnections(vertex, search, earliestTime, isPreviousConnTransfer);
            var latestTime = earliestTime.Add(TimeSpan.FromMinutes(search.Parameters.MaxTimeAheadFetchingPossibleConnections));
            var possibleConnections = connections.Where(p => p.DepartureTime <= latestTime);
            if (possibleConnections.Count() >= search.Parameters.MinimumPossibleConnectionsFetched)
                return possibleConnections;
            return connections.Take(search.Parameters.MinimumPossibleConnectionsFetched);
        }
    }
}
