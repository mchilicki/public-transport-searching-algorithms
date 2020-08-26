using Chilicki.Ptsa.Data.Configurations.ProjectConfiguration;
using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Data.Repositories;
using Chilicki.Ptsa.Domain.Search.Services.Calculations;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chilicki.Ptsa.Domain.Search.Services.SearchInputs
{
    public class StopsFinder
    {
        private readonly CityCenter cityCenter;
        private readonly double maxDistanceFromCenterInKm;
        private readonly StopRepository stopRepository;
        private readonly HaversineDistanceCalculator haversineDistanceCalculator;

        public StopsFinder(
            StopRepository stopRepository,
            HaversineDistanceCalculator haversineDistanceCalculator,
            IOptions<CityCenterSettings> cityCenterOptions,
            IOptions<ConnectionStrings> connectionStringsOptions)
        {
            this.stopRepository = stopRepository;
            this.haversineDistanceCalculator = haversineDistanceCalculator;
            var cityCenterSettings = cityCenterOptions.Value;
            maxDistanceFromCenterInKm = cityCenterOptions.Value.MaxDistanceFromCenterInKm;
            cityCenter = connectionStringsOptions.Value.DatabaseType switch
            {
                DatabaseType.Poznan => cityCenterSettings.PoznanCenter,
                DatabaseType.Lahti => cityCenterSettings.LahtiCenter,
                DatabaseType.Stavanger => cityCenterSettings.StavangerCenter,
                _ => cityCenterSettings.PoznanCenter,
            };
        }

        public async Task<IList<Guid>> FindStopIdsNearCenter()
        {
            var stops = await stopRepository.GetAllAsync();
            var selectedStops = new List<Stop>();
            foreach (var stop in stops)
            {
                var distanceInKm = haversineDistanceCalculator.CalculateDistanceInKm(
                    stop.Latitude, stop.Longitude, cityCenter.Latitude, cityCenter.Longitude);
                if (distanceInKm <= maxDistanceFromCenterInKm)
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
