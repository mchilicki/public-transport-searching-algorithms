using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Domain.Search.Aggregates;
using Chilicki.Ptsa.Domain.Search.Services.SimilarVertices;
using System;

namespace Chilicki.Ptsa.Domain.Search.Services.Dijkstra
{
    public class DijkstraContinueChecker
    {
        private readonly SimilarVerticesService similarVerticesService;

        public DijkstraContinueChecker(
            SimilarVerticesService similarVerticesService)
        {
            this.similarVerticesService = similarVerticesService;
        }

        public bool ShouldContinue(Guid stopId, Vertex currentVertex, SearchInput search)
        {
            bool isCurrentVertexDestinationStop = currentVertex != null && currentVertex.StopId == stopId;
            if (isCurrentVertexDestinationStop == true)
                return false;
            var possibleSimilarVertices = similarVerticesService.GetPossibleSimilarVertices(currentVertex.SimilarVertices, search);
            foreach (var similarVertex in possibleSimilarVertices)
            {
                if (similarVertex.Similar.StopId == stopId)
                    return false;
            }
            return true;
        }
    }
}
