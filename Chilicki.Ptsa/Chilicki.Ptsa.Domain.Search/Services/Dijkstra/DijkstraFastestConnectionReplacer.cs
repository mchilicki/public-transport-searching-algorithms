using Chilicki.Ptsa.Domain.Search.Aggregates;
using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Domain.Search.Services.GraphFactories;

namespace Chilicki.Ptsa.Domain.Search.Services.Dijkstra
{
    public class DijkstraFastestConnectionReplacer
    {
        readonly DijkstraConnectionService dijkstraConnectionsService;
        readonly ConnectionFactory connectionFactory;

        public DijkstraFastestConnectionReplacer(
            DijkstraConnectionService dijkstraConnectionsService,
            ConnectionFactory connectionFactory)
        {
            this.dijkstraConnectionsService = dijkstraConnectionsService;
            this.connectionFactory = connectionFactory;
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
            connectionFactory.FillIn(
                currentFastestConnection,
                newFastestConnection.Graph,
                newFastestConnection.StartVertex,
                newFastestConnection.StartStopTime,
                newFastestConnection.EndVertex,
                newFastestConnection.EndStopTime);
        } 
    }
}
