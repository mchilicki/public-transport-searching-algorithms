using Chilicki.Ptsa.Domain.Search.Aggregates;
using Chilicki.Ptsa.Domain.Search.Aggregates.Graphs;

namespace Chilicki.Ptsa.Domain.Search.Services.Dijkstra
{
    public class DijkstraFastestConnectionReplacer
    {
        readonly DijkstraStopConnectionsService _dijkstraStopConnectionsService;

        public DijkstraFastestConnectionReplacer(
            DijkstraStopConnectionsService dijkstraStopConnectionsService)
        {
            _dijkstraStopConnectionsService = dijkstraStopConnectionsService;
        }

        public bool ShouldConnectionBeReplaced(
            SearchInput searchInput,
            StopConnection stopConnectionFromPreviousVertex,
            StopConnection destinationStopCurrentFastestConnection,
            StopConnection maybeNewFastestConnection)
        {
            bool isDestinationVertexMarkedAsVisited = destinationStopCurrentFastestConnection
                .EndStopVertex.IsVisited;
            bool isCurrentFastestConnectionEmpty = _dijkstraStopConnectionsService
                    .IsConnectionEmpty(destinationStopCurrentFastestConnection);
            bool isPreviousVertexFastestConnectionEmpty = _dijkstraStopConnectionsService
                    .IsConnectionEmpty(stopConnectionFromPreviousVertex);
            bool canMaybeNewFastestConnectionExist =
                searchInput.StartTime <= maybeNewFastestConnection.StartStopTime.DepartureTime &&
                    (isPreviousVertexFastestConnectionEmpty ||
                    stopConnectionFromPreviousVertex.EndStopTime.DepartureTime <= maybeNewFastestConnection.StartStopTime.DepartureTime);
            bool isMaybeNewFastestConnectionFaster =
                maybeNewFastestConnection.EndStopTime.DepartureTime < destinationStopCurrentFastestConnection.EndStopTime.DepartureTime;
            if (isDestinationVertexMarkedAsVisited)
                return false;
            return canMaybeNewFastestConnectionExist &&
                (isCurrentFastestConnectionEmpty ||
                isMaybeNewFastestConnectionFaster);
        }

        public void ReplaceWithNewFastestConnection(
            StopConnection currentFastestStopConnection, 
            StopConnection newFastestConnection)
        {
            currentFastestStopConnection.Trip = newFastestConnection.Trip;
            currentFastestStopConnection.StartStopTime = newFastestConnection.StartStopTime;
            currentFastestStopConnection.SourceStopVertex = newFastestConnection.SourceStopVertex;
            currentFastestStopConnection.EndStopTime = newFastestConnection.EndStopTime;
        }
    }
}
