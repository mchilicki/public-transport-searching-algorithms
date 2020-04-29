using Chilicki.Ptsa.Data.Entities.Base;
using System;

namespace Chilicki.Ptsa.Data.Entities
{
    public class Connection : BaseEntity
    {
        public virtual Graph Graph { get; set; }

        public Guid? TripId { get; set; }

        public Guid? StartVertexId { get; set; }
        public virtual Vertex StartVertex { get; set; }

        public Guid? EndVertexId { get; set; }
        public virtual Vertex EndVertex { get; set; }

        public TimeSpan DepartureTime { get; set; }
        public TimeSpan ArrivalTime { get; set; }
        
        public bool IsTransfer { get; set; }

        public override string ToString()
        {
            var startVertexName = "EMPTY";
            if (StartVertex != null)
                startVertexName = StartVertex.ToString();
            return $"({startVertexName}) {DepartureTime} -> " +
                $"({EndVertex.ToString()}) {ArrivalTime}";
        }
    }
}
