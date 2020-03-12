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
                DepartureTime = connection.DepartureTime,
                StartStopTime = connection.StartStopTime,
                EndVertex = connection.EndVertex,
                ArrivalTime = connection.ArrivalTime,
                EndStopTime = connection.EndStopTime,
                IsTransfer = connection.IsTransfer,
            };
        }
    }
}
