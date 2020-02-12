using Chilicki.Ptsa.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chilicki.Ptsa.Data.Entities
{
    public class StopTime : BaseEntity
    {
        public virtual Trip Trip { get; set; }
        public virtual Stop Stop { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public int StopSequence { get; set; }
    }
}
