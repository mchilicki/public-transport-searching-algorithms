using Chilicki.Ptsa.Data.Entities;
using System;

namespace Chilicki.Ptsa.Domain.Search.Services.GraphFactories
{
    public class ConnectionFactory
    {
        public Connection Create(
            Graph graph,
            Vertex startVertex,
            StopTime startStopTime,
            Vertex endVertex,
            StopTime endStopTime,
            bool isTransfer = false)
        {
            var conn = new Connection();
            return FillIn(
                conn, graph, startVertex, startStopTime, 
                endVertex, endStopTime, isTransfer);
        }

        public Connection FillIn(
            Connection conn, 
            Graph graph, 
            Vertex startVertex, 
            StopTime startStopTime, 
            Vertex endVertex, 
            StopTime endStopTime, 
            bool isTransfer = false)
        {
            Trip trip = null;
            TimeSpan departureTime = TimeSpan.Zero;
            TimeSpan arrivalTime = TimeSpan.Zero;
            if (startStopTime != null)
            {
                if (!isTransfer)
                    trip = startStopTime.Trip;
                departureTime = startStopTime.DepartureTime;
                arrivalTime = startStopTime.DepartureTime;
            }
            conn.Graph = graph;
            conn.Trip = trip;
            conn.StartVertex = startVertex;
            conn.StartVertexId = startVertex?.Id;
            conn.EndVertex = endVertex;
            conn.EndVertexId = endVertex?.Id;
            conn.StartStopTime = startStopTime;
            conn.DepartureTime = departureTime;
            conn.EndStopTime = endStopTime;
            conn.ArrivalTime = arrivalTime;
            conn.IsTransfer = isTransfer;
            return conn;
        }
    }
}
