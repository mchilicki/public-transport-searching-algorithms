using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using Chilicki.Ptsa.Benchmarks;
using Chilicki.Ptsa.Data.Configurations.ProjectConfiguration;
using Chilicki.Ptsa.Domain.Gtfs.Services;
using Chilicki.Ptsa.Domain.Search.Dtos;
using Chilicki.Ptsa.Domain.Search.Managers;
using Chilicki.Ptsa.Domain.Search.Mappers;
using Chilicki.Ptsa.Domain.Search.Services.SearchInputs;
using Chilicki.Ptsa.Domain.Search.Services.Summary;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Chilicki.Ptsa.Search.Configurations.Startup
{
    public class ConsoleSearchService
    {
        readonly AppSettings appSettings;
        readonly GtfsImportService importService;
        readonly SearchManager searchManager;
        readonly GraphManager graphManager;
        readonly MultipleCriteriaSearchManager multipleCriteriaSearchManager;
        private readonly RandomSearchInputGenerator searchInputGenerator;
        private readonly DataSummaryService dataSummaryService;

        public ConsoleSearchService(
            IOptions<AppSettings> appSettings,
            GtfsImportService importService,
            SearchManager searchManager,
            GraphManager graphManager,
            MultipleCriteriaSearchManager multipleCriteriaSearchManager,
            RandomSearchInputGenerator searchInputGenerator,
            DataSummaryService dataSummaryService)
        {
            this.appSettings = appSettings.Value;
            this.importService = importService;
            this.searchManager = searchManager;
            this.graphManager = graphManager;
            this.multipleCriteriaSearchManager = multipleCriteriaSearchManager;
            this.searchInputGenerator = searchInputGenerator;
            this.dataSummaryService = dataSummaryService;
        }

        public async Task Run()
        {
            try
            {
                var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                if (environmentName == "GtfsImport" || environmentName == "GtfsImportPC")
                    await ImportGtfs();
                if (environmentName == "DijkstraSearch" || environmentName == "DijkstraSearchPC")
                    await SearchWithDijkstra();
                if (environmentName == "CreateGraph")
                    await CreateGraph();
                if (environmentName == "DijkstraBenchmark")
                    await PerformDijkstraBenchmark();
                if (environmentName == "MultipleDijkstraSearch")
                    await SearchWithMultipleCriteriaDijkstra();
                if (environmentName == "MultipleDijkstraBenchmark")
                    await PerformMultipleDijkstraBenchmark();
                if (environmentName == "GenerateRandomSearchInputs")
                    await GenerateRandomSearchInputs();
                if (environmentName == "Benchmarks")
                    PerformFullBenchmarks();
                if (environmentName == "DataSummary")
                    await PerformDataSummary();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.ReadKey();
            }            
        }

        private async Task PerformDataSummary()
        {
            await dataSummaryService.Summarize();
        }

        private async Task GenerateRandomSearchInputs()
        {
            string path = $"C:\\Users\\Marcin Chilicki\\Desktop\\RandomSearchInputs.txt";
            var searches = await searchInputGenerator.Generate(100);
            var sb = new StringBuilder();
            foreach (var search in searches)
            {
                sb.Append($"new SearchInputDto() {{ " +
                    $"StartStopId = new Guid(\"{search.StartStopId}\"), " +
                    $"DestinationStopId = new Guid(\"{search.DestinationStopId}\"), " +
                    $"StartTime = TimeSpan.Parse(\"{search.StartTime}\") }},{Environment.NewLine}");
            }
            string result = sb.ToString();
            File.WriteAllText(path, result);
        }

        private void PerformFullBenchmarks()
        {
            BenchmarkRunner
                .Run<SingleCriteriaDijkstraVsMultipleCriterionDijkstra>(
                ManualConfig
                    .Create(DefaultConfig.Instance)
                    .WithOptions(ConfigOptions.JoinSummary));
            Console.ReadKey();
        }

        private async Task PerformMultipleDijkstraBenchmark()
        {
            await multipleCriteriaSearchManager.PerformDijkstraBenchmark(
                appSettings.BenchmarkIterations);
        }

        private async Task PerformDijkstraBenchmark()
        {
            await searchManager.PerformDijkstraBenchmark(appSettings.BenchmarkIterations);
        }

        private async Task SearchWithDijkstra()
        {
            var search = SearchInputDto.Create(appSettings);
            await searchManager.SearchFastestConnections(search);
        }

        private async Task SearchWithMultipleCriteriaDijkstra()
        {
            var search = SearchInputDto.Create(appSettings);
            await multipleCriteriaSearchManager.SearchBestConnections(search);
        }

        private async Task CreateGraph()
        {
            await graphManager.CreateGraph();
        }

        private async Task ImportGtfs()
        {
            var gtfsFolderPath = appSettings.ImportGtfsPath;
            await importService.ImportGtfs(gtfsFolderPath);
        }
    }
}
