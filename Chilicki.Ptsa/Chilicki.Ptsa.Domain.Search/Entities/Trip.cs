using Chilicki.Ptsa.Domain.Search.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chilicki.Ptsa.Domain.Search.Entities
{
    public class Trip : BaseGtfsEntity
    {
        public Route Route { get; set; }
        public string HeadSign { get; set; }
    }
}
