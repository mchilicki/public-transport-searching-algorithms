using Chilicki.Ptsa.Domain.Search.Aggregates.Graphs;

namespace Chilicki.Ptsa.Domain.Search.Factories.StopConnections
{
    public class StopConnectionCloner
    {
        public StopConnection CloneFrom(StopConnection stopConnection)
        {
            return new StopConnection
            {
                Trip = stopConnection.Trip,
                SourceStopVertex = stopConnection.SourceStopVertex,
                StartStopTime = stopConnection.StartStopTime,
                EndStopVertex = stopConnection.EndStopVertex,
                EndStopTime = stopConnection.EndStopTime,
                IsTransfer = stopConnection.IsTransfer,
            };
        }
    }
}
