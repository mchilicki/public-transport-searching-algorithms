using Chilicki.Ptsa.Domain.Search.Aggregates;
using Chilicki.Ptsa.Data.Entities;

namespace Chilicki.Ptsa.Domain.Search.Services.Dijkstra
{
    public class DijkstraFastestConnectionReplacer
    {
        readonly DijkstraConnectionService dijkstraConnectionsService;

        public DijkstraFastestConnectionReplacer(
            DijkstraConnectionService dijkstraConnectionsService)
        {
            this.dijkstraConnectionsService = dijkstraConnectionsService;
        }

        public bool ShouldConnectionBeReplaced(
            SearchInput searchInput,
            Connection connectionFromPreviousVertex,
            Connection destinationStopCurrentFastestConnection,
            Connection maybeNewFastestConnection)
        {
            bool isDestinationVertexMarkedAsVisited = destinationStopCurrentFastestConnection
                .EndVertex.IsVisited;
            bool isCurrentFastestConnectionEmpty = dijkstraConnectionsService
                    .IsConnectionEmpty(destinationStopCurrentFastestConnection);
            bool isPreviousVertexFastestConnectionEmpty = dijkstraConnectionsService
                    .IsConnectionEmpty(connectionFromPreviousVertex);
            bool canMaybeNewFastestConnectionExist =
                searchInput.StartTime <= maybeNewFastestConnection.DepartureTime &&
                    (isPreviousVertexFastestConnectionEmpty ||
                    connectionFromPreviousVertex.ArrivalTime <= maybeNewFastestConnection.DepartureTime);
            bool isMaybeNewFastestConnectionFaster = !isCurrentFastestConnectionEmpty &&
                maybeNewFastestConnection.ArrivalTime < destinationStopCurrentFastestConnection.ArrivalTime;
            if (isDestinationVertexMarkedAsVisited)
                return false;
            return canMaybeNewFastestConnectionExist &&
                (isCurrentFastestConnectionEmpty ||
                isMaybeNewFastestConnectionFaster);
        }

        public void ReplaceWithNewFastestConnection(
            Connection currentFastestConnection, 
            Connection newFastestConnection)
        {
            currentFastestConnection.Trip = newFastestConnection.Trip;
            currentFastestConnection.StartStopTime = newFastestConnection.StartStopTime;
            currentFastestConnection.EndStopTime = newFastestConnection.EndStopTime;
            currentFastestConnection.ArrivalTime = newFastestConnection.ArrivalTime;
            currentFastestConnection.DepartureTime = newFastestConnection.DepartureTime;            
            currentFastestConnection.StartVertex = newFastestConnection.StartVertex;
            currentFastestConnection.StartVertexId = newFastestConnection.StartVertexId;
            currentFastestConnection.EndVertex = newFastestConnection.EndVertex;
            currentFastestConnection.EndVertexId = newFastestConnection.EndVertexId;
            currentFastestConnection.Graph = newFastestConnection.Graph;
        } 
    }
}
