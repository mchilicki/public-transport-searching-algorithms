using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Domain.Search.Services.GraphFactories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chilicki.Ptsa.Domain.Search.Services.Path
{
    public class FastestPathFlattener
    {
        readonly ConnectionFactory factory;

        public FastestPathFlattener(
            ConnectionFactory factory)
        {
            this.factory = factory;
        }

        public IEnumerable<Connection> FlattenFastestPath
           (IList<Connection> fastestPath)
        {
            var flattenPath = new List<Connection>();
            foreach (var currentConn in fastestPath)
            {
                if (ShouldAddNormalConnection(flattenPath, currentConn))
                {
                    AddConnection(flattenPath, currentConn);
                }
                else if (ShouldAddFirstConnection(flattenPath))
                {
                    AddFirstConnection(flattenPath, currentConn);
                }
                else
                {
                    AddTransfer(flattenPath, currentConn);
                }
            }
            return flattenPath;
        }

        private static bool ShouldAddFirstConnection(List<Connection> flattenPath)
        {
            return !flattenPath.Any();
        }

        private static bool ShouldAddNormalConnection(List<Connection> flattenPath, Connection currentConn)
        {
            return flattenPath.Any() && 
                !currentConn.IsTransfer && 
                !flattenPath.Last().IsTransfer;
        }

        private void AddConnection(List<Connection> flattenPath, Connection currentConn)
        {
            var previousConn = flattenPath.Last();
            var currentTripId = currentConn.TripId;
            var lastAddedTripId = previousConn.TripId;
            if (currentTripId == lastAddedTripId)
            {
                ExtendPreviousConnection(currentConn, previousConn);
            }
            else
            {
                AddConnectionAfterTransfer(flattenPath, currentConn, previousConn);
            }
        }

        private void ExtendPreviousConnection(Connection currentConn, Connection previousConn)
        {
            previousConn.EndVertex = currentConn.EndVertex;
            previousConn.ArrivalTime = currentConn.ArrivalTime;
        }

        private void AddConnectionAfterTransfer(
            ICollection<Connection> flattenPath, Connection currentConn, Connection previousConn)
        {
            flattenPath.Add(factory.CloneFrom(currentConn));
            if (previousConn.IsTransfer)
                previousConn.ArrivalTime = currentConn.DepartureTime;
        }

        private void AddFirstConnection(List<Connection> flattenPath, Connection currentConn)
        {
            var firstConn = factory.CloneFrom(currentConn);
            flattenPath.Add(firstConn);
        }

        private void AddTransfer(List<Connection> flattenPath, Connection currentConn)
        {
            var transferConn = factory.CloneFrom(currentConn);
            transferConn.DepartureTime = flattenPath.Last().ArrivalTime;
            flattenPath.Add(transferConn);
        }
    }
}
