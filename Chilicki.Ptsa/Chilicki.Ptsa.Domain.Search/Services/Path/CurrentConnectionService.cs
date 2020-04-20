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
        public Connection GetCurrentConnection(
            BestConnections bestConnections, Connection currentConn)
        {
            var vertexLabels = bestConnections.Get(currentConn.StartVertexId)
                .OrderBy(p => p.Connection.ArrivalTime);
            var label = vertexLabels
                .FirstOrDefault(p => p.Connection.ArrivalTime <= currentConn.ArrivalTime);
            currentConn = label.Connection;
            return currentConn;
        }
    }
}
