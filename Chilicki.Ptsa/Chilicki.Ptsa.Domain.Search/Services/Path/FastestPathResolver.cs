using Chilicki.Ptsa.Domain.Search.Aggregates;
using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Domain.Search.Factories.StopConnections;
using System.Collections.Generic;
using System.Linq;
using Chilicki.Ptsa.Domain.Search.Services.Dijkstra;

namespace Chilicki.Ptsa.Domain.Search.Services.Path
{
    public class FastestPathResolver
    {
        readonly FastestPathTransferService transferService;
        readonly ConnectionCloner cloner;
        readonly DijkstraContinueChecker continueChecker;

        public FastestPathResolver(
            FastestPathTransferService transferService,
            ConnectionCloner cloner,
            DijkstraContinueChecker continueChecker)
        {
            this.transferService = transferService;
            this.cloner = cloner;
            this.continueChecker = continueChecker;
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
            return new FastestPath()
            {
                Path = fastestPath,
                FlattenPath = FlattenFastestPath(fastestPath),
            };
        }

        private IEnumerable<Connection> FlattenFastestPath
            (IList<Connection> fastestPath)
        {
            var flattenPath = new List<Connection>();
            foreach (var currentConnection  in fastestPath)
            {
                if (flattenPath.Count() > 0)
                {
                    if (!currentConnection.IsTransfer && !flattenPath.Last().IsTransfer)
                    {
                        var currentLine = currentConnection.Trip;
                        var lastAddedLine = flattenPath.Last().Trip;
                        if (currentLine.Id == lastAddedLine.Id &&
                            currentLine.HeadSign == lastAddedLine.HeadSign)
                        {
                            var lastAddedConnection = flattenPath.Last();
                            lastAddedConnection.EndVertex = currentConnection.EndVertex;
                            lastAddedConnection.EndStopTime = currentConnection.EndStopTime;
                        }
                        else
                        {
                            flattenPath.Add(cloner.CloneFrom(currentConnection));
                        }                        
                    }
                    else
                    {
                        flattenPath.Add(cloner.CloneFrom(currentConnection));
                    }
                }
                else
                {
                    flattenPath.Add(cloner.CloneFrom(currentConnection));
                }
            }
            return flattenPath;
        }
    }
}
