using Chilicki.Ptsa.Data.Entities;
using System;

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
            TimeSpan departureTime = TimeSpan.Zero;
            TimeSpan arrivalTime = TimeSpan.Zero;
            if (startStopTime != null)
            {
                trip = startStopTime.Trip;
                departureTime = startStopTime.DepartureTime;
                arrivalTime = startStopTime.DepartureTime;
            }
            return new Connection()
            {
                Graph = graph,
                Trip = trip,
                StartVertex = currentVertex,
                StartVertexId = currentVertex?.Id,
                EndVertex = nextVertex,
                EndVertexId = nextVertex?.Id,
                StartStopTime = startStopTime,
                DepartureTime = departureTime,
                EndStopTime = endStopTime,
                ArrivalTime = arrivalTime,
                IsTransfer = isTransfer,
            };
        }
    }
}
