using Chilicki.Ptsa.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chilicki.Ptsa.Data.Entities
{
    public class Trip : BaseGtfsEntity
    {
        public virtual Route Route { get; set; }
        public string HeadSign { get; set; }
        public virtual ICollection<StopTime> StopTimes { get; set; }
    }
}
