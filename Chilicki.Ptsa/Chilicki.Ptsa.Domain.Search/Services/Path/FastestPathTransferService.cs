using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Domain.Search.Services.GraphFactories;

namespace Chilicki.Ptsa.Domain.Search.Services.Path
{
    public class FastestPathTransferService
    {
        private readonly ConnectionFactory stopConnectionFactory;

        public FastestPathTransferService(
            ConnectionFactory stopConnectionFactory)
        {
            this.stopConnectionFactory = stopConnectionFactory;
        }
        
        public bool IsAlreadyTransfer(Connection currentConnection)
        {
            return currentConnection.IsTransfer;
        }

        public bool ShouldBeTransfer(
            Connection sourceConnection, Connection nextConnection)
        {
            return sourceConnection.Trip.Id != nextConnection.Trip.Id;
        }

        public Connection GenerateTransferAsStopConnection(
            Connection sourceConnection, Connection nextConnection)
        {
            return stopConnectionFactory.Create(
                currentVertex: sourceConnection.EndVertex,
                startStopTime: sourceConnection.StartStopTime,
                nextVertex: sourceConnection.EndVertex,
                endStopTime: nextConnection.EndStopTime,
                isTransfer: true);
        }
    }
}
