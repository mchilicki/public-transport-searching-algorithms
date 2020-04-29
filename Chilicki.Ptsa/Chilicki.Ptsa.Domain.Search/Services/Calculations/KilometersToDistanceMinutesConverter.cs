using Chilicki.Ptsa.Data.Configurations.ProjectConfiguration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chilicki.Ptsa.Domain.Search.Services.Calculations
{
    public class KilometersToDistanceMinutesConverter
    {
        private const double MINUTES_PER_KM = 10;
        private const int MINIMUM_DISTANCE_IN_MINUTES = 2;        

        public int ConvertToDistanceInMinutes(double distanceInKm)
        {
            var distanceInKmRounded = Math.Round(distanceInKm, 1);
            var distanceInMinutes = (int)(MINUTES_PER_KM * distanceInKmRounded);
            if (distanceInMinutes <= 2)
                return MINIMUM_DISTANCE_IN_MINUTES;
            return distanceInMinutes;
        }
    }
}
