using Chilicki.Ptsa.Data.Entities;

namespace Chilicki.Ptsa.Domain.Search.Factories.SimilarVertices
{
    public class SimilarVertexFactory
    {
        public SimilarVertex Create(Vertex vertex, Vertex similar, int distanceInMinutes)
        {
            return new SimilarVertex()
            {
                Vertex = vertex,
                Similar = similar,
                DistanceInMinutes = distanceInMinutes,
            };
        }
    }
}
