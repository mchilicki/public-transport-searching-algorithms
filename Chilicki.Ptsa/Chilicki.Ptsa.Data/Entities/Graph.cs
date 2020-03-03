using Chilicki.Ptsa.Data.Entities.Base;
using System.Collections.Generic;

namespace Chilicki.Ptsa.Data.Entities
{
    public class Graph : BaseEntity
    {
        public virtual IEnumerable<Vertex> Vertices { get; set; }
    }
}
