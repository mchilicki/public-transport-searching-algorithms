using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Domain.Search.Aggregates;
using Chilicki.Ptsa.Domain.Search.Services.GraphFactories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chilicki.Ptsa.Domain.Search.Services.Base
{
    public abstract class GraphService
    {
        private readonly ConnectionFactory connectionFactory;

        public GraphService(
            ConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public Vertex GetVertexByStop(Graph graph, Stop stop)
        {
            return graph.Vertices
                .First(p => p.StopId == stop.Id);
        }

        

        

        

        protected IEnumerable<Connection> GetValidConnections(
            ICollection<Connection> connections, TimeSpan earliestTime)
        {
            return connections
                .Where(p => p.DepartureTime >= earliestTime);
        }
    }
}
