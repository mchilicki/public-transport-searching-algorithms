using Chilicki.Ptsa.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chilicki.Ptsa.Data.Entities
{
    public class Trip : BaseGtfsEntity
    {
        public Route Route { get; set; }
        public string HeadSign { get; set; }
        public ICollection<StopTime> StopTimes { get; set; }
    }
}
