using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Domain.Search.Aggregates.Graphs;
using System;
using System.Linq;

namespace Chilicki.Ptsa.Domain.Search.Services.GraphFactories
{
    public class StopConnectionFactory
    {
        public StopConnection Create(
            StopVertex currentVertex,
            StopTime startStopTime,
            StopVertex nextVertex,
            StopTime endStopTime,
            bool isTransfer = false)
        {
            return new StopConnection()
            {
                Trip = startStopTime.Trip,
                SourceStopVertex = currentVertex,
                EndStopVertex = nextVertex,
                StartStopTime = startStopTime,
                EndStopTime = endStopTime,
                IsTransfer = isTransfer,
            };
        }
    }
}
