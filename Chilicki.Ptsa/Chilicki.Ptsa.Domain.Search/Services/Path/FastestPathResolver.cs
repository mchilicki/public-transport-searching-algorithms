using Chilicki.Ptsa.Domain.Search.Aggregates;
using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Domain.Search.Factories.StopConnections;
using System.Collections.Generic;
using System.Linq;

namespace Chilicki.Ptsa.Domain.Search.Services.Path
{
    public class FastestPathResolver
    {
        readonly FastestPathTransferService transferService;
        readonly ConnectionCloner cloner;

        public FastestPathResolver(
            FastestPathTransferService trasferService,
            ConnectionCloner cloner)
        {
            this.transferService = trasferService;
            this.cloner = cloner;
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
            while (currentConnection.StartVertex.StopId != search.StartStop.Id)
            {
                var nextConnection = currentConnection;
                var sourceVertexId = currentConnection.StartVertexId;

                // Here is long time
                vertexFastestConnections.Dictionary.TryGetValue(sourceVertexId.Value, out currentConnection);

                if (!transferService.IsAlreadyTransfer(currentConnection) &&
                    !transferService.IsAlreadyTransfer(nextConnection) &&
                    transferService.ShouldBeTransfer(currentConnection, nextConnection))
                {
                    var transferBeetweenVertices = transferService
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
