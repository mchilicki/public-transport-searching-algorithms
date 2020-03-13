﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Chilicki.Ptsa.Data.Entities
{
    public class SimilarVertex 
    {
        public Guid VertexId { get; set; }
        public virtual Vertex Vertex { get; set; }
        public Guid SimilarId { get; set; }
        public virtual Vertex Similar { get; set; }
    }
}