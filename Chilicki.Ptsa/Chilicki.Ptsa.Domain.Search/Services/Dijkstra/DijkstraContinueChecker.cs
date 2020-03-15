using Chilicki.Ptsa.Data.Entities;
using System;

namespace Chilicki.Ptsa.Domain.Search.Services.Dijkstra
{
    public class DijkstraContinueChecker
    {
        public bool ShouldContinue(Guid stopId, Vertex currentVertex)
        {
            bool isCurrentVertexDestinationStop = currentVertex != null &&
                currentVertex.StopId == stopId;
            if (isCurrentVertexDestinationStop == true)
                return false;
            foreach (var similarVertex in currentVertex.SimilarVertices)
            {
                if (similarVertex.Similar.StopId == stopId)
                    return false;
            }
            return true;
        }
    }
}
