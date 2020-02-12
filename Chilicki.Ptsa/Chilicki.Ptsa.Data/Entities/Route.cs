﻿using Chilicki.Ptsa.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chilicki.Ptsa.Data.Entities
{
    public class Route : BaseGtfsEntity
    {
        public virtual Agency Agency { get; set; } 
        public string ShortName { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Trip> Trips { get; set; }
    }
}
