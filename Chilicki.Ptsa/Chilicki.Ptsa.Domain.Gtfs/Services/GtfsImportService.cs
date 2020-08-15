using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Data.Repositories.Base;
using Chilicki.Ptsa.Data.UnitsOfWork;
using Chilicki.Ptsa.Domain.Gtfs.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Chilicki.Ptsa.Domain.Gtfs.Services
{
    public class GtfsImportService
    {
        private readonly string AGENCY = "agency.txt";
        private readonly string ROUTES = "routes.txt";
        private readonly string STOP_TIMES = "stop_times.txt";
        private readonly string STOPS = "stops.txt";
        private readonly string TRIPS = "trips.txt";

        private bool IsServiceCheckingTurnedOff = true;

        private readonly IBaseRepository<Agency> agencyRepository;
        private readonly IBaseRepository<Stop> stopRepository;
        private readonly IBaseRepository<StopTime> stopTimeRepository;
        private readonly IBaseRepository<Route> routeRepository;
        private readonly IBaseRepository<Trip> tripRepository;
        private readonly IUnitOfWork unitOfWork;

        public GtfsImportService(
            IBaseRepository<Agency> agencyRepository,
            IBaseRepository<Stop> stopRepository,
            IBaseRepository<StopTime> stopTimeRepository,
            IBaseRepository<Route> routeRepository,
            IBaseRepository<Trip> tripRepository,
            IUnitOfWork unitOfWork)
        {
            this.agencyRepository = agencyRepository;
            this.unitOfWork = unitOfWork;
            this.stopRepository = stopRepository;
            this.stopTimeRepository = stopTimeRepository;
            this.routeRepository = routeRepository;
            this.tripRepository = tripRepository;
        }

        public async Task ImportGtfs(string gtfsFolderPath)
        {
            var agency = ReadAgency($"{gtfsFolderPath}{AGENCY}");
            var stops = ReadStops($"{gtfsFolderPath}{STOPS}");
            var routes = ReadRoutes($"{gtfsFolderPath}{ROUTES}", agency);
            var trips = ReadTrips($"{gtfsFolderPath}{TRIPS}", routes);
            var stopTimes = ReadStopTimes($"{gtfsFolderPath}{STOP_TIMES}", trips, stops);
            Console.WriteLine($"Saving everything to database");
            await agencyRepository.AddAsync(agency);
            await stopRepository.AddRangeAsync(stops);
            await routeRepository.AddRangeAsync(routes);
            await tripRepository.AddRangeAsync(trips);
            await stopTimeRepository.AddRangeAsync(stopTimes);
            await unitOfWork.SaveAsync();
            Console.WriteLine($"Everything is saved");
        }

        private Agency ReadAgency(string path)
        {
            Console.WriteLine($"Adding agency");
            var agency = new Agency();
            using (FileStream fs = File.Open(path, FileMode.Open))
            using (BufferedStream bs = new BufferedStream(fs))
            using (StreamReader sr = new StreamReader(bs))
            {
                string line = sr.ReadLine();
                line = sr.ReadLine();
                var splittedLine = line.Split(",");
                var name = splittedLine[1].Clean();
                agency = new Agency()
                {
                    Name = name,
                };
            }
            Console.WriteLine($"Agency added: {agency.Name}");
            return agency;
        }

        private IEnumerable<Route> ReadRoutes(string path, Agency agency)
        {
            Console.WriteLine($"Adding routes");
            var routes = new List<Route>();
            using (FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (BufferedStream bs = new BufferedStream(fs))
            using (StreamReader sr = new StreamReader(bs))
            {
                string line;
                bool isFirstLine = true;
                while ((line = sr.ReadLine()) != null)
                {
                    if (isFirstLine)
                    {
                        isFirstLine = false;
                        continue;
                    }
                    var splittedLine = line.Split(",");
                    var gtfsId = splittedLine[0].Clean();
                    var shortName = splittedLine[2].Clean();
                    var name = splittedLine[3].Clean();

                    var route = new Route()
                    {
                        GtfsId = gtfsId,
                        Agency = agency,
                        ShortName = shortName,
                        Name = name,
                    };
                    routes.Add(route);
                }
            }
            Console.WriteLine($"Routes added: {routes.Count}");
            return routes;
        }

        private IEnumerable<Stop> ReadStops(string path)
        {
            Console.WriteLine($"Adding stops");
            var stops = new List<Stop>();
            using (FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (BufferedStream bs = new BufferedStream(fs))
            using (StreamReader sr = new StreamReader(bs))
            {
                string line;
                bool isFirstLine = true;
                while ((line = sr.ReadLine()) != null)
                {
                    if (isFirstLine)
                    {
                        isFirstLine = false;
                        continue;
                    }

                    var splittedLine = line.Split(",");
                    var gtfsId = splittedLine[0].Clean();
                    var name = splittedLine[2].Clean();
                    var latitude = splittedLine[3].Clean();
                    var longitude = splittedLine[4].Clean();
                    double parsedLatitude = default;
                    double parsedLongitude = default;

                    try
                    {
                        parsedLatitude = double.Parse(latitude);
                        parsedLongitude = double.Parse(longitude);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    
                    var stop = new Stop()
                    {
                        GtfsId = gtfsId,
                        Name = name,
                        Latitude = parsedLatitude,
                        Longitude = parsedLongitude,
                    };
                    stops.Add(stop);
                }
            }
            Console.WriteLine($"Stops added: {stops.Count}");
            return stops;
        }

        private IEnumerable<Trip> ReadTrips(string path, IEnumerable<Route> routes)
        {
            Console.WriteLine($"Adding trips");
            var trips = new List<Trip>();
            string serviceId = null;
            using (FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (BufferedStream bs = new BufferedStream(fs))
            using (StreamReader sr = new StreamReader(bs))
            {
                string line;
                bool isFirstLine = true;
                while ((line = sr.ReadLine()) != null)
                {
                    var splittedLine = line.Split(",");
                    var routeGtfsId = splittedLine[0].Clean();
                    var tripGtfsId = splittedLine[2].Clean();
                    var headSign = splittedLine[3].Clean();
                    if (string.IsNullOrWhiteSpace(headSign))
                        headSign = "Pusty kierunek jazdy";
                    if (!isFirstLine && serviceId == null)
                    {
                        serviceId = splittedLine[1].Clean();
                    }
                    if (isFirstLine)
                    {
                        isFirstLine = false;
                        continue;
                    }    
                    if (IsServiceCheckingTurnedOff || serviceId == splittedLine[1].Clean())
                    {
                        var route = routes.FirstOrDefault(p => p.GtfsId == routeGtfsId);
                        if (route != null)
                        {
                            var trip = new Trip()
                            {
                                GtfsId = tripGtfsId,
                                HeadSign = headSign,
                                Route = route,
                            };                            
                            trips.Add(trip);                            
                        }                                             
                    }
                    else
                    {
                        Console.WriteLine($"No service found {serviceId}");
                    }
                }
            }
            Console.WriteLine($"Trips added: {trips.Count}");
            return trips;
        }

        private IEnumerable<StopTime> ReadStopTimes(string path, IEnumerable<Trip> trips, IEnumerable<Stop> stops)
        {
            Console.WriteLine($"Adding stop times");
            var stopTimes = new List<StopTime>();
            using (FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (BufferedStream bs = new BufferedStream(fs))
            using (StreamReader sr = new StreamReader(bs))
            {
                string line;
                bool isFirstLine = true;
                while ((line = sr.ReadLine()) != null)
                {
                    if (isFirstLine)
                    {
                        isFirstLine = false;
                        continue;
                    }
                    var splittedLine = line.Split(",");
                    var tripId = splittedLine[0].Clean();
                    var arrivalTime = splittedLine[1].Clean();
                    var departureTime = splittedLine[2].Clean();
                    var stopId = splittedLine[3].Clean();
                    var stopSequence = splittedLine[4].Clean();
                    var splittedHour = departureTime.Split(':');
                    //if (splittedHour.Length != 3)
                    //    splittedHour = splittedLine[3].Clean().Split(':');
                    //if (splittedHour.Length != 3)
                    //    splittedHour = splittedLine[4].Clean().Split(':');
                    
                    bool isHourOk = int.TryParse(splittedHour[0].Clean(), out int hour);
                    bool isMinuteOk = int.TryParse(splittedHour[1].Clean(), out int minute);
                    bool isSecondOk = int.TryParse(splittedHour[2].Clean(), out int second);
                    if (!isHourOk || !isMinuteOk || !isSecondOk)
                        throw new InvalidOperationException(tripId + "," + arrivalTime + "," + departureTime);
                    var timespan = new TimeSpan(hour, minute, second);
                    
                    var trip = trips.FirstOrDefault(p => p.GtfsId == tripId);
                    if (trip != null)
                    {
                        var stop = stops.FirstOrDefault(p => p.GtfsId == stopId);
                        if (stop != null)
                        {
                            var stopTime = new StopTime()
                            {
                                DepartureTime = timespan,
                                StopSequence = int.Parse(stopSequence),
                                Trip = trip,
                                Stop = stop,
                            };
                            stopTimes.Add(stopTime);
                        }
                        else
                        {
                            Console.WriteLine($"No stop found {stopId}");
                        }
                    }  
                    else
                    {
                        Console.WriteLine($"No trip found {tripId}");
                    }
                }
            }
            Console.WriteLine($"StopTimes added: {stopTimes.Count}");
            return stopTimes;
        }
    }
}
