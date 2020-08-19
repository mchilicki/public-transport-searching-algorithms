using Chilicki.Ptsa.Data.Entities.Base;
using System;
using System.Collections.Generic;

namespace Chilicki.Ptsa.Data.Entities
{
    public class Vertex : BaseEntity
    {
        public bool IsVisited { get; set; }
        public virtual Graph Graph { get; set; }

        public Guid StopId { get; set; }
        public string StopName { get; set; }
        public virtual Stop Stop { get; set; }
        
        public virtual ICollection<Connection> Connections { get; set; }
        public virtual ICollection<SimilarVertex> SimilarVertices { get; set; }

        public override string ToString()
        {
            return StopName;
        }
    }
}
