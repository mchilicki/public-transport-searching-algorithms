using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Domain.Search.Aggregates;
using System;

namespace Chilicki.Ptsa.Domain.Search.Services.GraphFactories
{
    public class ConnectionFactory
    {
        public Connection CreateConnection(
            Graph graph,
            Guid? tripId,
            Vertex startVertex,
            TimeSpan departureTime,
            Vertex endVertex,
            TimeSpan arrivalTime)
        {
            var conn = new Connection();
            var isTransfer = false;
            return FillIn(
                conn, graph, tripId, startVertex, departureTime, 
                endVertex, arrivalTime, isTransfer);
        }

        public Connection CreateTransfer(
            Vertex startVertex, Vertex endVertex, TimeSpan startTime, TimeSpan endTime)
        {
            var conn = new Connection();
            Graph graph = null;
            Guid? tripId = null;
            return FillIn(
                conn, graph, tripId, startVertex, startTime, endVertex, endTime);
        }

        public Connection FillInZeroCostTransfer(
            Connection conn, Vertex startVertex, Vertex endVertex, TimeSpan time)
        {
            Graph graph = null;
            Guid? tripId = null;           
            var isTransfer = true;
            var departureTime = time;
            var arrivalTime = time;
            return FillIn(
                conn, graph, tripId, startVertex, departureTime,
                endVertex, arrivalTime, isTransfer);
        }

        public Connection CreateStartingConnection(
            Graph graph, Vertex startVertex, SearchInput search)
        {
            var conn = new Connection();
            Guid? tripId = null;
            var isTransfer = false;
            var endVertex = startVertex;
            var departureTime = search.StartTime;
            var arrivalTime = search.StartTime;
            return FillIn(
                conn, graph, tripId, startVertex, departureTime,
                endVertex, arrivalTime, isTransfer);
        }

        public Connection CloneFrom(Connection c)
        {
            var conn = new Connection();
            return FillIn(
                conn, c.Graph, c.TripId, c.StartVertex,
                c.DepartureTime, c.EndVertex, c.ArrivalTime, 
                c.IsTransfer);
        }

        public Connection CreateEmptyConnection(
            Graph graph, Vertex endVertex)
        {
            var conn = new Connection();
            Guid? tripId = null;
            Vertex startVertex = null;
            var isTransfer = false;
            var departureTime = TimeSpan.Zero;
            var arrivalTime = TimeSpan.Zero;
            return FillIn(
                conn, graph, tripId, startVertex, departureTime,
                endVertex, arrivalTime, isTransfer);
        }

        public Connection FillIn(
            Connection conn,
            Graph graph,
            Guid? tripId,
            Vertex startVertex,
            TimeSpan departureTime,
            Vertex endVertex,
            TimeSpan arrivalTime,
            bool isTransfer = false)
        {
            conn.Graph = graph;
            conn.TripId = tripId;
            conn.StartVertex = startVertex;
            conn.StartVertexId = startVertex?.Id;
            conn.EndVertex = endVertex;
            conn.EndVertexId = endVertex?.Id;
            conn.DepartureTime = departureTime;
            conn.ArrivalTime = arrivalTime;
            conn.IsTransfer = isTransfer;
            return conn;
        }
    }
}
