using Chilicki.Ptsa.Domain.Search.Aggregates;
using Chilicki.Ptsa.Domain.Search.Aggregates.Graphs;
using Chilicki.Ptsa.Domain.Search.Factories.StopConnections;
using System.Collections.Generic;
using System.Linq;

namespace Chilicki.Ptsa.Domain.Search.Services.Path
{
    public class FastestPathResolver
    {
        readonly FastestPathTransferService fastestPathTransferService;
        readonly StopConnectionCloner cloner;

        public FastestPathResolver(
            FastestPathTransferService fastestPathTransferService,
            StopConnectionCloner cloner)
        {
            this.fastestPathTransferService = fastestPathTransferService;
            this.cloner = cloner;
        }

        public FastestPath ResolveFastestPath
            (SearchInput search, IEnumerable<StopConnection> vertexFastestConnection)
        {
            var fastestPath = new List<StopConnection>();
            var currentConnection = vertexFastestConnection
                .First(p => p.EndStopVertex.Stop.Id == search.DestinationStop.Id);            
            fastestPath.Add(currentConnection);
            while (currentConnection.SourceStopVertex.Stop.Id != search.StartStop.Id)
            {
                var nextConnection = currentConnection;
                var sourceVertex = currentConnection.SourceStopVertex;
                currentConnection = vertexFastestConnection
                    .First(p => p.EndStopVertex.Stop.Id == sourceVertex.Stop.Id);
                if (!fastestPathTransferService.IsAlreadyTransfer(currentConnection) &&
                    !fastestPathTransferService.IsAlreadyTransfer(nextConnection) &&
                    fastestPathTransferService.ShouldBeTransfer(currentConnection, nextConnection))
                {
                    var transferBeetweenVertices = fastestPathTransferService
                        .GenerateTransferAsStopConnection(currentConnection, nextConnection);
                    fastestPath.Add(transferBeetweenVertices);
                }
                fastestPath.Add(currentConnection);
            }
            fastestPath.Reverse();
            return new FastestPath()
            {
                Path = fastestPath,
                FlattenPath = FlattenFastestPath(fastestPath),
            };
        } 

        private IEnumerable<StopConnection> FlattenFastestPath
            (IList<StopConnection> fastestPath)
        {
            var flattenPath = new List<StopConnection>();
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
                            lastAddedConnection.EndStopVertex = currentConnection.EndStopVertex;
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
