﻿using System;

namespace Chilicki.Ptsa.Data.Entities
{
    public class SimilarVertex 
    {
        public Guid VertexId { get; set; }
        public virtual Vertex Vertex { get; set; }
        public Guid SimilarId { get; set; }
        public virtual Vertex Similar { get; set; }
        public int DistanceInMinutes { get; set; }

        public override string ToString()
        {
            return $"({Vertex.ToString()}) -> {DistanceInMinutes} minutes -> ({Similar.ToString()})";
        }
    }
}
