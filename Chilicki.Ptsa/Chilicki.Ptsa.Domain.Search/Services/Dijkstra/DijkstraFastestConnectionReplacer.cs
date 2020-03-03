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
                searchInput.StartTime <= maybeNewFastestConnection.StartStopTime.DepartureTime &&
                    (isPreviousVertexFastestConnectionEmpty ||
                    connectionFromPreviousVertex.EndStopTime.DepartureTime <= maybeNewFastestConnection.StartStopTime.DepartureTime);
            bool isMaybeNewFastestConnectionFaster =
                maybeNewFastestConnection.EndStopTime.DepartureTime < destinationStopCurrentFastestConnection.EndStopTime.DepartureTime;
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
            currentFastestConnection.StartVertex = newFastestConnection.StartVertex;
            currentFastestConnection.EndStopTime = newFastestConnection.EndStopTime;
        }
    }
}
