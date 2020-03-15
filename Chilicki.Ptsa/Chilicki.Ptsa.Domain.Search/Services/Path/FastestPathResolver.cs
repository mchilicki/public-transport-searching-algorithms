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
        readonly ConnectionFactory connectionFactory;

        public FastestPathResolver(
            FastestPathTransferService transferService,
            DijkstraContinueChecker continueChecker,
            ConnectionFactory connectionFactory)
        {
            this.transferService = transferService;
            this.continueChecker = continueChecker;
            this.connectionFactory = connectionFactory;
        }

        public FastestPath ResolveFastestPath(
            SearchInput search, VertexFastestConnections vertexFastestConnections)
        {
            var fastestPath = new List<Connection>();
            var currentConnection = vertexFastestConnections
                .Dictionary
                .First(p => p.Value.EndVertex.StopId == search.DestinationStop.Id)
                .Value;            
            fastestPath.Add(currentConnection);
            int iteration = 0;
            while (continueChecker.ShouldContinue(search.StartStop.Id, currentConnection.StartVertex))
            {
                var nextConnection = currentConnection;
                var sourceVertexId = currentConnection.StartVertexId;

                currentConnection = vertexFastestConnections.Get(sourceVertexId);

                if (!transferService.IsAlreadyTransfer(currentConnection) &&
                    !transferService.IsAlreadyTransfer(nextConnection) &&
                    transferService.ShouldBeTransfer(currentConnection, nextConnection))
                {
                    var transferBeetweenVertices = transferService
                        .GenerateTransferAsStopConnection(currentConnection, nextConnection);
                    fastestPath.Add(transferBeetweenVertices);
                }
                fastestPath.Add(currentConnection);
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
            foreach (var currentConnection  in fastestPath)
            {
                if (flattenPath.Any() && 
                    !currentConnection.IsTransfer && !flattenPath.Last().IsTransfer)
                {
                    var currentTripId = currentConnection.TripId;
                    var lastAddedTripId = flattenPath.Last().TripId;
                    if (currentTripId == lastAddedTripId)
                    {
                        var lastAddedConnection = flattenPath.Last();
                        lastAddedConnection.EndVertex = currentConnection.EndVertex;
                    }
                    else
                    {
                        flattenPath.Add(connectionFactory.CloneFrom(currentConnection));
                    }
                }
                else
                {
                    flattenPath.Add(connectionFactory.CloneFrom(currentConnection));
                }
            }
            return flattenPath;
        }
    }
}
