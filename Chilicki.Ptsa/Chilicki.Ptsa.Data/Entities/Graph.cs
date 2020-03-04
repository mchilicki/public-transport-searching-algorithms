using Chilicki.Ptsa.Data.Entities.Base;
using System.Collections.Generic;

namespace Chilicki.Ptsa.Data.Entities
{
    public class Graph : BaseEntity
    {
        public virtual ICollection<Vertex> Vertices { get; set; }
        public virtual ICollection<Connection> Connections { get; set; }
    }
}
