using Chilicki.Ptsa.Domain.Search.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chilicki.Ptsa.Domain.Search.Services.Measures
{
    public class ShortMeasureLogger
    {
        readonly string dateTimeNowFormat = "yyyy-MM-dd-HH-mm-ss";
        readonly string logFolder = $"C:\\Users\\Marcin Chilicki\\Desktop\\Measures";
        readonly string fileExtension = ".txt";
        private readonly MeasureLogger measureLogger;

        public ShortMeasureLogger(
            MeasureLogger measureLogger)
        {
            this.measureLogger = measureLogger;
        }

        public async Task Log(PerformanceMeasure measure, string algorithm)
        {
            var measures = new List<PerformanceMeasure>() { measure };
            await Log(measures, algorithm);
        }

        public async Task Log(IEnumerable<PerformanceMeasure> measures, string algorithm)
        {
            if (measures == null)
                return;
            var nowString = DateTime.Now.ToString(dateTimeNowFormat);
            var fileName = $"{logFolder}/{algorithm}-{nowString}{fileExtension}";
            var sb = new StringBuilder();
            measureLogger.MakeTitle(sb, nowString);
            measureLogger.MakeSeparation(sb);
            measureLogger.MakeMeasures(sb, measures);
            MakeShortSummary(sb, measures);
            var log = sb.ToString();
            await measureLogger.SaveLogFile(fileName, log);
        }

        private void MakeShortSummary(StringBuilder sb, IEnumerable<PerformanceMeasure> measures)
        {
            var fastestPaths = measures.Select(p => p.FastestPaths);

            int correctWays = fastestPaths.Count(p => p.Any());

            double averagePaths = fastestPaths.Average(p => p.Count);
            int minPaths = fastestPaths.Min(p => p.Count);
            int maxPaths = fastestPaths.Max(p => p.Count);

            sb.AppendLine($"Correctly found ways: {correctWays}");

            sb.AppendLine($"Average paths found per way: {averagePaths}");
            sb.AppendLine($"Min paths found per way: {minPaths}");
            sb.AppendLine($"Max paths found per way: {maxPaths}");
        }
    }
}
