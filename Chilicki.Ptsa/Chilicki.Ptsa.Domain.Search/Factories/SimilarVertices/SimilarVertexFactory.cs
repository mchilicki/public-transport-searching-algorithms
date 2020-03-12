using Chilicki.Ptsa.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chilicki.Ptsa.Domain.Search.Factories.SimilarVertices
{
    public class SimilarVertexFactory
    {
        public SimilarVertex Create(Vertex vertex, Vertex similar)
        {
            return new SimilarVertex()
            {
                Vertex = vertex,
                Similar = similar,
            };
        }
    }
}
