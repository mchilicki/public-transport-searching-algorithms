using Chilicki.Ptsa.Data.Entities;

namespace Chilicki.Ptsa.Domain.Search.Services.GraphFactories
{
    public class ConnectionFactory
    {
        public Connection Create(
            Graph graph,
            Vertex currentVertex,
            StopTime startStopTime,
            Vertex nextVertex,
            StopTime endStopTime,
            bool isTransfer = false)
        {
            Trip trip = null;
            if (startStopTime != null)
                trip = startStopTime.Trip;
            return new Connection()
            {
                Graph = graph,
                Trip = trip,
                StartVertex = currentVertex,
                EndVertex = nextVertex,
                StartStopTime = startStopTime,
                EndStopTime = endStopTime,
                IsTransfer = isTransfer,
            };
        }
    }
}
