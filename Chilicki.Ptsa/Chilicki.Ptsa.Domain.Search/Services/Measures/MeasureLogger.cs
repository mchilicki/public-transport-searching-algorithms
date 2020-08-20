using Chilicki.Ptsa.Data.Entities;
using Chilicki.Ptsa.Data.Repositories;
using Chilicki.Ptsa.Domain.Search.Aggregates;
using Chilicki.Ptsa.Domain.Search.Helpers.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chilicki.Ptsa.Domain.Search.Services.Measures
{
    public class MeasureLogger
    {
        readonly TripRepository tripRepository;

        public MeasureLogger(
            TripRepository tripRepository)
        {
            this.tripRepository = tripRepository;
        }

        readonly string dateTimeNowFormat = "yyyy-MM-dd-HH-mm-ss";
        readonly string fileStart = "Dijkstra-";
        readonly string logFolder = "Measures";
        readonly string fileExtension = ".txt";
        readonly string title = "Dijkstra measurements report";

        public async Task Log(PerformanceMeasure measure)
        {
            var measures = new List<PerformanceMeasure>() { measure };
            await Log(measures);
        }

        public async Task Log(IEnumerable<PerformanceMeasure> measures)
        {
            if (measures == null)
                return;
            var nowString = DateTime.Now.ToString(dateTimeNowFormat);
            var fileName = $"{logFolder}/{fileStart}{nowString}{fileExtension}";
            var sb = new StringBuilder();
            MakeTitle(sb, nowString);
            MakeSeparation(sb);
            MakeMeasures(sb, measures);
            MakeSeparation(sb);
            await MakePathsMeasures(sb, measures);
            var log = sb.ToString();
            await SaveLogFile(fileName, log);
        }        

        public void MakeTitle(StringBuilder sb, string nowString)
        {
            sb.AppendLine($"{title} {nowString}");
        }

        public void MakeSeparation(StringBuilder sb)
        {
            sb.AppendLine();
        }

        public void MakeMeasures(StringBuilder sb, IEnumerable<PerformanceMeasure> measures)
        {
            MakeMeasuresTitle(sb);
            MakeAverageTime(sb, measures);
            MakeMinimumTime(sb, measures);
            MakeMaximumTime(sb, measures);
            MakeStdTime(sb, measures);
        }

        private void MakeMeasuresTitle(StringBuilder sb)
        {
            sb.AppendLine($"Measures");
        }

        private void MakeAverageTime(StringBuilder sb, IEnumerable<PerformanceMeasure> measures)
        {
            var ticks = (long)measures.Average(p => p.Time.Ticks);
            var time = TimeSpan.FromTicks(ticks).ToString();
            sb.AppendLine($"Average time: {time}");
        }

        private void MakeMinimumTime(StringBuilder sb, IEnumerable<PerformanceMeasure> measures)
        {
            var ticks = measures.Min(p => p.Time.Ticks);
            var time = TimeSpan.FromTicks(ticks).ToString();
            sb.AppendLine($"Minimum time: {time}");
        }

        private void MakeMaximumTime(StringBuilder sb, IEnumerable<PerformanceMeasure> measures)
        {
            var ticks = measures.Max(p => p.Time.Ticks);
            var time = TimeSpan.FromTicks(ticks).ToString();
            sb.AppendLine($"Maximum time: {time}");
        }

        private void MakeStdTime(StringBuilder sb, IEnumerable<PerformanceMeasure> measures)
        {
            var ticks = (long)measures.StdDev(p => p.Time.Ticks);
            var time = TimeSpan.FromTicks(ticks).ToString();
            sb.AppendLine($"Standard deviation time: {time}");
        }

        private async Task MakePathsMeasures(StringBuilder sb, IEnumerable<PerformanceMeasure> measures)
        {
            MakePathsTitle(sb);
            foreach (var measure in measures)
            {
                MakeSeparation(sb);
                foreach (var path in measure.FastestPaths)
                {
                    await MakePath(sb, path);
                    MakeSeparation(sb);
                }                
            }
        }

        private void MakePathsTitle(StringBuilder sb)
        {
            sb.AppendLine($"Paths");
        }

        private async Task MakePath(StringBuilder sb, FastestPath path)
        {
            MakeInputStops(sb, path);
            MakeSeparation(sb);            
            if (path.FlattenPath != null)
            {
                MakePathTitle(sb);
                await MakePath(sb, path.FlattenPath);
            }
            else
            {
                MakeNoPathFound(sb);
            }
        }

        private void MakePathTitle(StringBuilder sb)
        {
            sb.AppendLine($"Path found");
        }

        private void MakeInputStops(StringBuilder sb, FastestPath path)
        {
            var startStop = path.StartStop.ToString();
            sb.AppendLine($"Input");
            sb.AppendLine($"Start stop");
            sb.AppendLine(startStop);
            var destinationStop = path.DestinationStop.ToString();
            sb.AppendLine($"Destination stop");
            sb.AppendLine(destinationStop);
            sb.AppendLine($"Start time {path.StartTime}");
        }

        private async Task MakePath(StringBuilder sb, IEnumerable<Connection> pathConnections)
        {
            foreach (var conn in pathConnections)
            {                
                if (conn.IsTransfer || !conn.TripId.HasValue)
                {
                    MakeTransfer(sb, conn);
                }
                else
                {
                    MakeSeparation(sb);
                    await MakeConnection(sb, conn);
                }                
            }
        }
        
        private async Task MakeConnection(StringBuilder sb, Connection conn)
        {            
            var trip = await tripRepository.FindWithRouteAsync(conn.TripId.Value);
            var routeName = trip.Route.ShortName;
            var headSign = trip.HeadSign;
            sb.AppendLine($"({routeName}) - ({conn.StartVertex.StopName} - " +
                $"{conn.EndVertex.StopName}) -> {headSign}");
            sb.AppendLine($"Time {conn.DepartureTime} -> {conn.ArrivalTime}");
        }

        private void MakeTransfer(StringBuilder sb, Connection conn)
        {
            sb.AppendLine($"Transfer - {conn.StartVertex.StopName} -> {conn.EndVertex.StopName}");
            sb.AppendLine($"Time {conn.DepartureTime} -> {conn.ArrivalTime}");
        }

        private void MakeNoPathFound(StringBuilder sb)
        {
            sb.AppendLine($"No path was found between these two stops at this hour");
        }

        public async Task SaveLogFile(string fileName, string log)
        {
            Directory.CreateDirectory(logFolder);
            await File.WriteAllTextAsync(fileName, log);
        }
    }
}
