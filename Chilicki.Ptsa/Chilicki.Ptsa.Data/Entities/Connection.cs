using Chilicki.Ptsa.Data.Entities.Base;

namespace Chilicki.Ptsa.Data.Entities
{
    public class Connection : BaseEntity
    {
        public virtual Graph Graph { get; set; }
        public virtual Trip Trip { get; set; }
        public virtual Vertex StartVertex { get; set; }
        public virtual Vertex EndVertex { get; set; }
        public virtual StopTime StartStopTime { get; set; }
        public virtual StopTime EndStopTime { get; set; }
        public bool IsTransfer { get; set; }
    }
}
