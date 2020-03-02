using Chilicki.Ptsa.Domain.Search.Aggregates.Graphs;
using System.Collections.Generic;
using System.Linq;

namespace Chilicki.Ptsa.Domain.Search.Services.Dijkstra
{
    public class DijkstraStopConnectionsService
    {
        public StopConnection GetDestinationStopFastestConnection(
            IEnumerable<StopConnection> vertexFastestConnections,
            StopConnection stopConnection)
        {
            return vertexFastestConnections
                .First(p => p.EndStopVertex.Stop.Id == 
                    stopConnection.EndStopVertex.Stop.Id);
        }

        public StopConnection GetStopConnectionFromPreviousVertex(
            IEnumerable<StopConnection> vertexFastestConnections,
            StopConnection stopConnection)
        {
            if (stopConnection.SourceStopVertex == null)
                return null;
            return vertexFastestConnections
                .FirstOrDefault(p => p.EndStopVertex != null &&
                    p.EndStopVertex.Stop.Id == stopConnection.SourceStopVertex.Stop.Id);
        }

        public bool IsConnectionEmpty(StopConnection stopConnection)
        {
            return stopConnection == null ||
                stopConnection.SourceStopVertex == null;
        }
    }
}
