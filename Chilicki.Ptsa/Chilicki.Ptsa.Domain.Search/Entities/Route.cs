using Chilicki.Ptsa.Domain.Search.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chilicki.Ptsa.Domain.Search.Entities
{
    public class Route : BaseGtfsEntity
    {
        public Agency Agency { get; set; } 
        public string ShortName { get; set; }
        public string Name { get; set; }
    }
}
