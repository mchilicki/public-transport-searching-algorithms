using Chilicki.Ptsa.Domain.Search.Aggregates;
using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Domain.Search.Services.GraphFactories;
using System.Linq;
using System;

namespace Chilicki.Ptsa.Domain.Search.Services.Dijkstra
{
    public class DijkstraFastestConnectionReplacer
    {
        private readonly DijkstraConnectionService service;
        private readonly ConnectionFactory factory;
        private readonly DijkstraConnectionService connectionService;

        public DijkstraFastestConnectionReplacer(
            DijkstraConnectionService service,
            ConnectionFactory factory,
            DijkstraConnectionService connectionService)
        {
            this.service = service;
            this.factory = factory;
            this.connectionService = connectionService;
        }

        public bool ShouldConnectionBeReplaced(
            SearchInput search,
            FastestConnections fastestConnections,
            Connection currentConn,
            Connection possibleConn)
        {
            var previousVertexConn = connectionService.GetPreviousVertexConnection(
                fastestConnections, possibleConn);
            var isVertexVisited = currentConn.EndVertex.IsVisited;
            if (isVertexVisited)
                return false;
            var isCurrentConnEmpty = service.IsConnectionEmpty(currentConn);
            var isPreviousVertexConnEmpty = service.IsConnectionEmpty(previousVertexConn);
            var canPossibleConnExist = CanPossibleConnExist(search, previousVertexConn,
                possibleConn, isPreviousVertexConnEmpty);
            var isPossibleConnFaster = IsPossibleConnFaster(currentConn, possibleConn);
            return ShouldConnBeReplaced(isCurrentConnEmpty, canPossibleConnExist, isPossibleConnFaster);
        }

        private bool CanPossibleConnExist(
            SearchInput search,
            Connection previousVertexConn,
            Connection possibleConn,
            bool isPreviousVertexConnEmpty)
        {
            var isPossibleConnAfterInput = search.StartTime <= possibleConn.DepartureTime;
            var latestTime = previousVertexConn.ArrivalTime.Add(TimeSpan.FromMinutes(search.Parameters.MaxTimeAheadFetchingPossibleConnections));
            var isPossibleConnBeforeMaxTimeParameter = latestTime >= possibleConn.DepartureTime;
            if (!isPossibleConnAfterInput)
                return false;
            if (!isPossibleConnBeforeMaxTimeParameter)
                return false;
            var isPossibleConnAfterPrevConn = previousVertexConn.ArrivalTime <= possibleConn.DepartureTime;
            return isPossibleConnAfterPrevConn || isPreviousVertexConnEmpty;
        }

        private bool IsPossibleConnFaster(
            Connection currentConn, Connection possibleConn)
        {
            return possibleConn.ArrivalTime < currentConn.ArrivalTime;
        }

        private bool ShouldConnBeReplaced(
            bool isCurrentConnEmpty, bool canPossibleConnExist, bool isPossibleConnFaster)
        {
            if (!canPossibleConnExist)
                return false;
            return isCurrentConnEmpty || isPossibleConnFaster;
        }

        public void ReplaceWithNewFastestConnection(
            Connection currentConn, 
            Connection newConn)
        {
            factory.FillIn(currentConn, newConn.Graph, newConn.TripId,
                newConn.StartVertex, newConn.DepartureTime, newConn.EndVertex, newConn.ArrivalTime);
        } 
    }
}
