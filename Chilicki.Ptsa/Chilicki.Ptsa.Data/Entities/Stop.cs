using Chilicki.Ptsa.Data.Entities.Base;
using System.Collections.Generic;

namespace Chilicki.Ptsa.Data.Entities
{
    public class Stop : BaseGtfsEntity
    {
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public virtual ICollection<StopTime> StopTimes { get; set; }

        public override string ToString()
        {
            return $"Id: {Id} Name: {Name} Latitude: {Latitude} Longitude: {Longitude}";
        }
    }
}
