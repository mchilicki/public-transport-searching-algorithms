using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Domain.Search.Helpers.Exceptions;
using System;
using System.Collections.Generic;

namespace Chilicki.Ptsa.Domain.Search.Aggregates
{
    public class FastestConnections
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
                throw new FastestConnectionsException("Null vertexId in dictionary Find");
            }
            return Find(vertexId.Value);
        }
    }    
}
