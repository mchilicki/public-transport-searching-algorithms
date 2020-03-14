using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Domain.Search.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chilicki.Ptsa.Domain.Search.Services.Dijkstra
{
    public class DijkstraConnectionService
    {
        public Connection GetDestinationStopFastestConnection(
            VertexFastestConnections vertexFastestConnections,
            Connection connection)
        {
            return vertexFastestConnections.Get(connection.EndVertexId);
        }

        public Connection GetConnectionFromPreviousVertex(
            VertexFastestConnections vertexFastestConnections,
            Connection connection)
        {
            if (connection.StartVertex == null)
                return null;
            return vertexFastestConnections.Get(connection.StartVertexId);
        }

        public bool IsConnectionEmpty(Connection conn)
        {
            return conn == null ||
                conn.StartVertex == null ||
                conn.EndVertex == null ||
                conn.DepartureTime == TimeSpan.Zero ||
                conn.ArrivalTime == TimeSpan.Zero;

        }
    }
}
