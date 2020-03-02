using Chilicki.Ptsa.Domain.Search.Aggregates;
using Chilicki.Ptsa.Domain.Search.Aggregates.Graphs;
using Chilicki.Ptsa.Domain.Search.Services.GraphFactories;

namespace Chilicki.Ptsa.Domain.Search.Services.Path
{
    public class FastestPathTransferService
    {
        private readonly StopConnectionFactory stopConnectionFactory;

        public FastestPathTransferService(
            StopConnectionFactory stopConnectionFactory)
        {
            this.stopConnectionFactory = stopConnectionFactory;
        }
        
        public bool IsAlreadyTransfer(StopConnection currentConnection)
        {
            return currentConnection.IsTransfer;
        }

        public bool ShouldBeTransfer(
            StopConnection sourceConnection, StopConnection nextConnection)
        {
            return sourceConnection.Trip.Id != nextConnection.Trip.Id;
        }

        public StopConnection GenerateTransferAsStopConnection(
            StopConnection sourceConnection, StopConnection nextConnection)
        {
            return stopConnectionFactory.Create(
                currentVertex: sourceConnection.EndStopVertex,
                startStopTime: sourceConnection.StartStopTime,
                nextVertex: sourceConnection.EndStopVertex,
                endStopTime: nextConnection.EndStopTime,
                isTransfer: true);
        }
    }
}
