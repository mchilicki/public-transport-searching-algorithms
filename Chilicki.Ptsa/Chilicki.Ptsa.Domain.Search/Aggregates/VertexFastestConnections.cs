using Chilicki.Ptsa.Data.Entities;
using System;
using System.Collections.Generic;

namespace Chilicki.Ptsa.Domain.Search.Aggregates
{
    public class VertexFastestConnections
    {
        public IDictionary<Guid, Connection> Dictionary { get; } = new Dictionary<Guid, Connection>();

        public void Add(Guid vertexId, Connection connection)
        {
            Dictionary.Add(vertexId, connection);
        }

        public Connection Find(Guid vertexId)
        {
            Dictionary.TryGetValue(vertexId, out Connection connection);
            return connection;
        }

        public Connection Get(Guid? vertexId)
        {
            if (!vertexId.HasValue)
            {
                throw new VertexFastestConnectionsException("Null vertexId in dictionary Find");
            }
            return Find(vertexId.Value);
        }
    }

    public class VertexFastestConnectionsException : Exception
    {
        public VertexFastestConnectionsException(string message) : base(message)
        {
        }
    }
}
