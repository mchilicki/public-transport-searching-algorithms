using Chilicki.Ptsa.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chilicki.Ptsa.Data.Entities
{
    public class Agency : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<Route> Routes { get; set; }
    }
}
