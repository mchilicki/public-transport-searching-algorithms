using System;
using System.Collections.Generic;
using System.Text;

namespace Chilicki.Ptsa.Data.Configurations.ProjectConfiguration
{
    public class CityCenterSettings
    {
        public double MaxDistanceFromCenterInKm { get; set; }
        public CityCenter PoznanCenter { get; set; }
        public CityCenter LahtiCenter { get; set; }
        public CityCenter StavangerCenter { get; set; }
    }
}
