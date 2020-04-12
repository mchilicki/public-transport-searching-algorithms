using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Domain.Search.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chilicki.Ptsa.Domain.Search.Services.Base
{
    public abstract class GraphService
    {
        public Vertex GetStopVertexByStop(Graph graph, Stop stop)
        {
            return graph.Vertices
                .First(p => p.StopId == stop.Id);
        }

        public IEnumerable<Connection> GetPossibleConnections(Vertex vertex, SearchInput search)
        {
            var connections = new List<Connection>();
            connections.AddRange(
                GetValidConnections(vertex.Connections, search));
            foreach (var similar in vertex.SimilarVertices)
            {
                connections.AddRange(GetValidConnections(similar.Similar.Connections, search));
            }
            return connections;
        }

        private IEnumerable<Connection> GetValidConnections(
            ICollection<Connection> connections, SearchInput search)
        {
            return connections
                .Where(p => p.DepartureTime >= search.StartTime);
        }
    }
}
