using Chilicki.Ptsa.Data.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Chilicki.Ptsa.Domain.Gtfs.Services
{
    public class GtfsImportService
    {
        private readonly string AGENCY = "agency.txt";
        private readonly string ROUTES = "routes.txt";
        private readonly string STOP_TIMES = "stop_times.txt";
        private readonly string STOPS = "stops.txt";
        private readonly string TRIPS = "trips.txt";

        public void ImportGtfs(string gtfsFolderPath)
        {
            var agency = ReadAgency($"{gtfsFolderPath}{AGENCY}");
            var stops = ReadStops($"{gtfsFolderPath}{STOPS}");
            var routes = ReadRoutes($"{gtfsFolderPath}{ROUTES}", agency);
            var trips = ReadTrips($"{gtfsFolderPath}{TRIPS}", routes);            
            var stopTimes = ReadStopTimes($"{gtfsFolderPath}{STOP_TIMES}", trips, stops);
        }

        private Agency ReadAgency(string path)
        {
            var agency = new Agency();
            using (FileStream fs = File.Open(path, FileMode.Open))
            using (BufferedStream bs = new BufferedStream(fs))
            using (StreamReader sr = new StreamReader(bs))
            {
                string line = sr.ReadLine();
                line = sr.ReadLine();
                var splittedLine = line.Split(",");
                agency = new Agency()
                {
                    Name = splittedLine[1].Replace("\"", string.Empty),
                };
            }
            return agency;
        }

        private IEnumerable<Route> ReadRoutes(string path, Agency agency)
        {
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
                    var route = new Route()
                    {
                        GtfsId = splittedLine[0],
                        Agency = agency,
                        ShortName = splittedLine[2].Replace("\"", string.Empty),
                        Name = splittedLine[3].Replace("\"", string.Empty),
                    };
                    routes.Add(route);
                }
            }
            return routes;
        }

        private IEnumerable<Stop> ReadStops(string path)
        {
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
                    var stop = new Stop()
                    {
                        GtfsId = splittedLine[0],
                        Name = splittedLine[2].Replace("\"", string.Empty),
                        Latitude = double.Parse(splittedLine[3]),
                        Longitude = double.Parse(splittedLine[4]),
                    };
                    stops.Add(stop);
                }
            }
            return stops;
        }

        private IEnumerable<Trip> ReadTrips(string path, IEnumerable<Route> routes)
        {
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
                    if (!isFirstLine && serviceId == null)
                    {
                        serviceId = splittedLine[1];
                    }
                    if (isFirstLine)
                    {
                        isFirstLine = false;
                        continue;
                    }    
                    if (serviceId == splittedLine[1])
                    {
                        var route = routes.FirstOrDefault(p => p.GtfsId == splittedLine[0]);
                        if (route != null)
                        {
                            var trip = new Trip()
                            {
                                GtfsId = splittedLine[2].Replace("\"", string.Empty),
                                HeadSign = splittedLine[3].Replace("\"", string.Empty),
                                Route = route,
                            };                            
                            trips.Add(trip);                            
                        }                                             
                    }                    
                }
            }
            return trips;
        }

        private IEnumerable<StopTime> ReadStopTimes(string path, IEnumerable<Trip> trips, IEnumerable<Stop> stops)
        {
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
                    var timespan = new TimeSpan(
                        int.Parse(splittedLine[2].Split(':')[0]),    // hours
                        int.Parse(splittedLine[2].Split(':')[1]),    // minutes
                        int.Parse(splittedLine[2].Split(':')[2]));   // seconds
                    var trip = trips.FirstOrDefault(p => p.GtfsId == splittedLine[0].Replace("\"", string.Empty));
                    if (trip != null)
                    {
                        var stop = stops.FirstOrDefault(p => p.GtfsId == splittedLine[3].Replace("\"", string.Empty));
                        if (stop != null)
                        {
                            var stopTime = new StopTime()
                            {
                                DepartureTime = timespan,
                                StopSequence = int.Parse(splittedLine[4]),
                                Trip = trip,
                                Stop = stop,
                            };
                            stopTimes.Add(stopTime);
                        }
                    }                                         
                }
            }
            return stopTimes;
        }
    }
}
