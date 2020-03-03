using Chilicki.Ptsa.Data.Entities.Base;
using System.Collections.Generic;

namespace Chilicki.Ptsa.Data.Entities
{
    public class Vertex : BaseEntity
    {
        public bool IsVisited { get; set; }
        public virtual Graph Graph { get; set; }
        public virtual Stop Stop { get; set; }
        public virtual IEnumerable<Connection> Connections { get; set; }        
        public virtual IEnumerable<Vertex> SimilarVertices { get; set; }
    }
}
