using Chilicki.Ptsa.Domain.Search.Aggregates;
using Chilicki.Ptsa.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using Chilicki.Ptsa.Domain.Search.Services.Dijkstra;
using Chilicki.Ptsa.Domain.Search.Services.GraphFactories;

namespace Chilicki.Ptsa.Domain.Search.Services.Path
{
    public class FastestPathResolver
    {
        readonly FastestPathTransferService transferService;
        readonly DijkstraContinueChecker continueChecker;
        readonly FastestPathFlattener flattener;

        public FastestPathResolver(
            FastestPathTransferService transferService,
            DijkstraContinueChecker continueChecker,
            FastestPathFlattener flattener)
        {
            this.transferService = transferService;
            this.continueChecker = continueChecker;
            this.flattener = flattener;
        }

        public FastestPath ResolveFastestPath(
            SearchInput search, FastestConnections fastestConnections)
        {
            var fastestPath = new List<Connection>();
            var currentConn = fastestConnections.Dictionary
                .First(p => p.Value.EndVertex.StopId == search.DestinationStop.Id)
                .Value;            
            fastestPath.Add(currentConn);
            int iteration = 0;
            while (continueChecker.ShouldContinue(search.StartStop.Id, currentConn.StartVertex))
            {
                currentConn = IterateReversedFastestPath(fastestConnections, fastestPath, currentConn);
                iteration++;
            }
            fastestPath.Reverse();
            var flattenPath = flattener.FlattenFastestPath(fastestPath);
            return FastestPath.Create(search, fastestPath, flattenPath);
        }

        private Connection IterateReversedFastestPath(
            FastestConnections fastestConnections, ICollection<Connection> fastestPath, Connection currentConn)
        {
            var nextConn = currentConn;
            currentConn = fastestConnections.Get(currentConn.StartVertexId);
            if (transferService.ShouldBeTransfer(currentConn, nextConn))
            {
                var transfer = transferService
                    .CreateTranfer(currentConn, nextConn);
                fastestPath.Add(transfer);
            }
            else if (transferService.ShouldExtendAlreadyTransfer(currentConn))
            {
                ExtendAlreadyTransfer(currentConn, nextConn);
            }
            fastestPath.Add(currentConn);
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
