﻿using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Domain.Search.Aggregates;
using System;

namespace Chilicki.Ptsa.Domain.Search.Services.Dijkstra
{
    public class DijkstraConnectionService
    {
        public Connection GetCurrentConnection(
            FastestConnections fastestConnections,
            Connection conn)
        {
            return fastestConnections.Get(conn.EndVertexId);
        }

        public Connection GetPreviousVertexConnection(
            FastestConnections fastestConnections,
            Connection conn)
        {
            if (conn.StartVertex == null)
                return null;
            return fastestConnections.Get(conn.StartVertexId);
        }

        public bool IsConnectionEmpty(Connection conn)
        {
            return conn == null || conn.StartVertex == null;
        }
    }
}
