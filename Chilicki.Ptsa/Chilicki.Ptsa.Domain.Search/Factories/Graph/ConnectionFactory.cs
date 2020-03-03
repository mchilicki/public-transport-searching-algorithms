using Chilicki.Ptsa.Data.Entities;

namespace Chilicki.Ptsa.Domain.Search.Services.GraphFactories
{
    public class ConnectionFactory
    {
        public Connection Create(
            Vertex currentVertex,
            StopTime startStopTime,
            Vertex nextVertex,
            StopTime endStopTime,
            bool isTransfer = false)
        {
            return new Connection()
            {
                Trip = startStopTime.Trip,
                StartVertex = currentVertex,
                EndVertex = nextVertex,
                StartStopTime = startStopTime,
                EndStopTime = endStopTime,
                IsTransfer = isTransfer,
            };
        }
    }
}
