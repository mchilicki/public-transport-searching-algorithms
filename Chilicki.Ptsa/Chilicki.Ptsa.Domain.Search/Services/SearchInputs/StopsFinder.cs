using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Data.Repositories;
using Chilicki.Ptsa.Domain.Search.Services.Calculations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chilicki.Ptsa.Domain.Search.Services.SearchInputs
{
    public class StopsFinder
    {
        // Poznan
        private readonly double latitude = 52.406397; 
        private readonly double longitude = 16.925207;

        // Lahti 
        //private readonly double latitude = 60.977406;
        //private readonly double longitude = 25.657927;

        // Stavanger 
        //private readonly double latitude = 58.962080; 
        //private readonly double longitude = 5.720296;

        private readonly double maxDistanceFromCenter = 3.5;
        private readonly StopRepository stopRepository;
        private readonly HaversineDistanceCalculator haversineDistanceCalculator;

        public StopsFinder(
            StopRepository stopRepository,
            HaversineDistanceCalculator haversineDistanceCalculator)
        {
            this.stopRepository = stopRepository;
            this.haversineDistanceCalculator = haversineDistanceCalculator;
        }

        public async Task<IList<Guid>> FindStopIdsNearCenter()
        {
            var stops = await stopRepository.GetAllAsync();
            var selectedStops = new List<Stop>();
            foreach (var stop in stops)
            {
                var distanceInKm = haversineDistanceCalculator.CalculateDistanceInKm(stop.Latitude, stop.Longitude, latitude, longitude);
                if (distanceInKm <= maxDistanceFromCenter)
                    selectedStops.Add(stop);
            }
            return selectedStops.Select(p => p.Id).ToList();
        }

        public async Task<IEnumerable<Vertex>> FindVerticesNearCenter(IEnumerable<Vertex> vertices)
        {
            var stopIdsNearCenter = await FindStopIdsNearCenter();
            var verticesNearCenter = vertices.Where(p =>
                stopIdsNearCenter.Contains(p.StopId));
            return verticesNearCenter;
        }
    }
}
