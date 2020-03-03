using Chilicki.Ptsa.Data.Entities;

namespace Chilicki.Ptsa.Domain.Search.Factories.StopConnections
{
    public class ConnectionCloner
    {
        public Connection CloneFrom(Connection connection)
        {
            return new Connection
            {
                Trip = connection.Trip,
                StartVertex = connection.StartVertex,
                StartStopTime = connection.StartStopTime,
                EndVertex = connection.EndVertex,
                EndStopTime = connection.EndStopTime,
                IsTransfer = connection.IsTransfer,
            };
        }
    }
}
