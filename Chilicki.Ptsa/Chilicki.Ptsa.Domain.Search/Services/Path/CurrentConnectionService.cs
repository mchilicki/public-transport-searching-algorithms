using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Domain.Search.Aggregates.MultipleCriterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chilicki.Ptsa.Domain.Search.Services.Path
{
    public class CurrentConnectionService
    {
        public IEnumerable<Connection> GetPossibleConnections(
            BestConnections bestConnections, Connection currentConn)
        {
            var vertexLabels = bestConnections.Get(currentConn.StartVertexId)
                .Where(p => p.Connection.ArrivalTime <= currentConn.DepartureTime)
                .OrderBy(p => p.Connection.ArrivalTime);
            return vertexLabels
                .Select(p => p.Connection);
        }
    }
}
