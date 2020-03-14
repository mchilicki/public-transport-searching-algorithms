using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Domain.Search.Services.GraphFactories;

namespace Chilicki.Ptsa.Domain.Search.Services.Path
{
    public class FastestPathTransferService
    {
        private readonly ConnectionFactory connectionFactory;

        public FastestPathTransferService(
            ConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        
        public bool IsAlreadyTransfer(Connection currentConnection)
        {
            return currentConnection.IsTransfer;
        }

        public bool ShouldBeTransfer(
            Connection sourceConnection, Connection nextConnection)
        {
            return sourceConnection.TripId != nextConnection.TripId;
        }

        public Connection GenerateTransferAsStopConnection(
            Connection sourceConnection, Connection nextConnection)
        {
            // TODO Check if second parameter should be sourceConnection.EndVertex or nextConnection.EndVertex
            return connectionFactory.CreateZeroCostTransfer(
                sourceConnection.EndVertex,
                nextConnection.EndVertex, 
                sourceConnection.ArrivalTime);
        }
    }
}
