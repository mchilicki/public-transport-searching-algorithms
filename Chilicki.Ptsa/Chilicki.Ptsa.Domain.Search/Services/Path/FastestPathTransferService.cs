using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Domain.Search.Services.GraphFactories;
using System;

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
       

        public bool ShouldBeTransfer(
            Connection sourceConn, Connection nextConn)
        {
            if (sourceConn.IsTransfer)
                return false;
            if (nextConn.IsTransfer)
                return false;
            return sourceConn.TripId != nextConn.TripId;
        }

        public Connection CreateTranfer(
            Connection sourceConnection, Connection nextConnection)
        {
            return factory.CreateTransfer(
                sourceConnection.EndVertex,
                nextConnection.StartVertex,                
                sourceConnection.ArrivalTime,
                nextConnection.DepartureTime);
        }

        public bool ShouldExtendAlreadyTransfer(Connection currentConn)
        {
            return currentConn.IsTransfer;
        }

        public void ExtendAlreadyTransfer(Connection currentConn, Connection nextConn)
        {
            currentConn.ArrivalTime = nextConn.DepartureTime;
        }
    }
}
