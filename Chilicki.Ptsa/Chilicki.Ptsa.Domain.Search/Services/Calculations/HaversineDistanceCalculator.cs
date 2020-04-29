using Chilicki.Ptsa.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chilicki.Ptsa.Domain.Search.Services.Calculations
{
    public class HaversineDistanceCalculator
    {
        public double CalculateDistance(Vertex vertex1, Vertex vertex2)
        {
            return CalculateDistance(vertex1.Stop, vertex2.Stop);
        }

        private double CalculateDistance(Stop stop1, Stop stop2)
        {
            return CalculateDistance(stop1.Latitude, stop1.Longitude, stop2.Latitude, stop2.Longitude);
        }

        public double CalculateDistance(
            double latitude1, double longitude1, double latitude2, double longitude2)
        {
            var piDividedBy180 = 0.017453292519943295;
            var doubledR = 12742;
            var a = 0.5 - Math.Cos((latitude2 - latitude1) * piDividedBy180) / 2 +
                    Math.Cos(latitude1 * piDividedBy180) * Math.Cos(latitude2 * piDividedBy180) *
                    (1 - Math.Cos((longitude2 - longitude1) * piDividedBy180)) / 2;
            return doubledR * Math.Asin(Math.Sqrt(a));
        }
    }
}
