using Chilicki.Ptsa.Domain.Search.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chilicki.Ptsa.Domain.Search.Entities
{
    public class Stop : BaseGtfsEntity
    {
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
