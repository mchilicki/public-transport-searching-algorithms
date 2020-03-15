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
        readonly ConnectionFactory factory;

        public FastestPathResolver(
            FastestPathTransferService transferService,
            DijkstraContinueChecker continueChecker,
            ConnectionFactory factory)
        {
            this.transferService = transferService;
            this.continueChecker = continueChecker;
            this.factory = factory;
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
                var nextConn = currentConn;
                currentConn = fastestConnections.Get(currentConn.StartVertexId);

                if (transferService.ShouldBeTransfer(currentConn, nextConn))
                {
                    var transfer = transferService
                        .GenerateTransferAsStopConnection(currentConn, nextConn);
                    fastestPath.Add(transfer);
                }
                fastestPath.Add(currentConn);
                iteration++;
            }
            fastestPath.Reverse();
            var flattenPath = FlattenFastestPath(fastestPath);
            return FastestPath.Create(search, fastestPath, flattenPath);
        }

        public FastestPath CreateNotFoundPath(SearchInput search)
        {
            return FastestPath.Create(search);
        }

        private IEnumerable<Connection> FlattenFastestPath
            (IList<Connection> fastestPath)
        {
            var flattenPath = new List<Connection>();
            foreach (var currentConn  in fastestPath)
            {
                if (flattenPath.Any() && 
                    !currentConn.IsTransfer && !flattenPath.Last().IsTransfer)
                {
                    var currentTripId = currentConn.TripId;
                    var lastAddedTripId = flattenPath.Last().TripId;
                    if (currentTripId == lastAddedTripId)
                    {
                        var lastAddedConnection = flattenPath.Last();
                        lastAddedConnection.EndVertex = currentConn.EndVertex;
                    }
                    else
                    {
                        flattenPath.Add(factory.CloneFrom(currentConn));
                    }
                }
                else
                {
                    flattenPath.Add(factory.CloneFrom(currentConn));
                }
            }
            return flattenPath;
        }
    }
}
