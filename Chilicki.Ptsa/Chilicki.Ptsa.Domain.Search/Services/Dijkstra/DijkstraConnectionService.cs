using Chilicki.Ptsa.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Chilicki.Ptsa.Domain.Search.Services.Dijkstra
{
    public class DijkstraConnectionService
    {
        public Connection GetDestinationStopFastestConnection(
            IEnumerable<Connection> vertexFastestConnections,
            Connection connection)
        {
            return vertexFastestConnections
                .First(p => p.EndVertex.Stop.Id == 
                    connection.EndVertex.Stop.Id);
        }

        public Connection GetConnectionFromPreviousVertex(
            IEnumerable<Connection> vertexFastestConnections,
            Connection onnection)
        {
            if (onnection.StartVertex == null)
                return null;
            return vertexFastestConnections
                .FirstOrDefault(p => p.EndVertex != null &&
                    p.EndVertex.Stop.Id == onnection.StartVertex.Stop.Id);
        }

        public bool IsConnectionEmpty(Connection stopConnection)
        {
            return stopConnection == null ||
                stopConnection.StartVertex == null;
        }
    }
}
