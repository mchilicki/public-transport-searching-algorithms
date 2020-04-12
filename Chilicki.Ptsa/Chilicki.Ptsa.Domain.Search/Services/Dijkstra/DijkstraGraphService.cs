using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Domain.Search.Aggregates;
using Chilicki.Ptsa.Domain.Search.Services.Base;
using Chilicki.Ptsa.Domain.Search.Services.GraphFactories;
using System.Collections.Generic;
using System.Linq;

namespace Chilicki.Ptsa.Domain.Search.Services.Dijkstra
{
    public class DijkstraGraphService : GraphService
    {
        readonly ConnectionFactory factory;

        public DijkstraGraphService(
            ConnectionFactory factory)
        {
            this.factory = factory;
        }        

        public Vertex MarkVertexAsVisited(Vertex stopVertex)
        {
            stopVertex.IsVisited = true;
            foreach (var similarStopVertex in stopVertex.SimilarVertices)
            {
                similarStopVertex.Similar.IsVisited = true;
            }
            return stopVertex;
        }

        public FastestConnections SetTransferConnectionsToSimilarVertices (
            FastestConnections fastestConnections, 
            Vertex vertex, 
            IEnumerable<SimilarVertex> similarVertices)
        {
            var connectionToVertex = fastestConnections.Find(vertex.Id);
            foreach (var similarVertex in similarVertices)
            {
                var similar = fastestConnections.Find(similarVertex.SimilarId);
                factory.FillInZeroCostTransfer(
                    similar, vertex, similarVertex.Similar, 
                    connectionToVertex.DepartureTime);
            }
            return fastestConnections;
        }
    }
}
