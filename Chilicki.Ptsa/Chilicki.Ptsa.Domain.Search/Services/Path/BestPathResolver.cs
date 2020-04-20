using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Domain.Search.Aggregates;
using Chilicki.Ptsa.Domain.Search.Aggregates.MultipleCriterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chilicki.Ptsa.Domain.Search.Services.Path
{
    public class BestPathResolver
    {
        private readonly FastestPathTransferService transferService;
        private readonly FastestPathFlattener flattener;
        private readonly CurrentConnectionService currentConnectionService;

        public BestPathResolver(
            FastestPathTransferService transferService,
            FastestPathFlattener flattener,
            CurrentConnectionService currentConnectionService)
        {
            this.transferService = transferService;
            this.flattener = flattener;
            this.currentConnectionService = currentConnectionService;
        }

        public ICollection<FastestPath> ResolveBestPaths(
            SearchInput search, BestConnections bestConnections)
        {
            var paths = new List<FastestPath>();
            var labels = bestConnections.Find(search.DestinationVertex.Id);
            foreach (var label in labels)
            {
                var path = ResolveBestPath(search, bestConnections, label);
                paths.Add(path);
            }
            return paths;
        }

        public FastestPath ResolveBestPath(
            SearchInput search, BestConnections bestConnections, Label label)
        {
            var connPath = new List<Connection>();
            var currentConn = label.Connection;
            connPath.Add(currentConn);
            int iteration = 0;
            while (search.StartStop.Id != currentConn.StartVertex.Stop.Id)
            {
                currentConn = IterateReversedFastestPath(bestConnections, connPath, currentConn);
                iteration++;
            }
            connPath.Reverse();
            var flattenPath = flattener.FlattenFastestPath(connPath);
            return FastestPath.Create(search, connPath, flattenPath);
        }

        private Connection IterateReversedFastestPath(
            BestConnections bestConnections, ICollection<Connection> connPath, Connection currentConn)
        {
            var nextConn = currentConn;
            currentConn = currentConnectionService.GetCurrentConnection(bestConnections, currentConn);
            if (transferService.ShouldBeTransfer(currentConn, nextConn))
            {
                var transfer = transferService
                    .CreateTranfer(currentConn, nextConn);
                connPath.Add(transfer);
            }
            else if (transferService.ShouldExtendAlreadyTransfer(currentConn))
            {
                ExtendAlreadyTransfer(currentConn, nextConn);
            }
            connPath.Add(currentConn);
            return currentConn;
        }

        private void ExtendAlreadyTransfer(Connection currentConn, Connection nextConn)
        {
            currentConn.ArrivalTime = nextConn.DepartureTime;
        }

        public FastestPath CreateNotFoundPath(SearchInput search)
        {
            return FastestPath.Create(search);
        }
    }
}
