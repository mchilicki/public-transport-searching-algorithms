using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Domain.Search.Services.GraphFactories;

namespace Chilicki.Ptsa.Domain.Search.Services.Path
{
    public class FastestPathTransferService
    {
        private readonly ConnectionFactory factory;

        public FastestPathTransferService(
            ConnectionFactory factory)
        {
            this.factory = factory;
        }
        
        public bool IsAlreadyTransfer(Connection currentConn)
        {
            return currentConn.IsTransfer;
        }

        public bool ShouldBeTransfer(
            Connection sourceConn, Connection nextConn)
        {
            if (IsAlreadyTransfer(sourceConn))
                return false;
            if (IsAlreadyTransfer(nextConn))
                return false;
            return sourceConn.TripId != nextConn.TripId;
        }

        public Connection GenerateTransferAsStopConnection(
            Connection sourceConnection, Connection nextConnection)
        {
            // TODO Check if second parameter should be sourceConnection.EndVertex or nextConnection.EndVertex
            return factory.CreateZeroCostTransfer(
                sourceConnection.EndVertex,
                nextConnection.EndVertex, 
                sourceConnection.ArrivalTime);
        }
    }
}
