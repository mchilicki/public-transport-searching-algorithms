using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using Chilicki.Ptsa.Benchmarks;
using Chilicki.Ptsa.Data.Configurations.ProjectConfiguration;
using Chilicki.Ptsa.Domain.Gtfs.Services;
using Chilicki.Ptsa.Domain.Search.Dtos;
using Chilicki.Ptsa.Domain.Search.Managers;
using Chilicki.Ptsa.Domain.Search.Services.SearchInputs;
using Chilicki.Ptsa.Domain.Search.Services.Summary;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Chilicki.Ptsa.Search.Configurations.Startup
{
    public class ConsoleSearchService
    {
        private readonly SearchSettings searchSettings;
        private readonly PathSettings pathSettings;
        private readonly ModuleType moduleType;
        private readonly DatabaseType databaseType;
        private readonly SummarySettings summarySettings;
        private readonly GtfsImportService importService;
        private readonly SearchManager searchManager;
        private readonly GraphManager graphManager;
        private readonly MultipleCriteriaSearchManager multipleCriteriaSearchManager;
        private readonly RandomSearchInputGenerator searchInputGenerator;
        private readonly DataSummaryService dataSummaryService;

        public ConsoleSearchService(
            IOptions<SearchSettings> searchSettingsOptions,
            GtfsImportService importService,
            SearchManager searchManager,
            GraphManager graphManager,
            MultipleCriteriaSearchManager multipleCriteriaSearchManager,
            RandomSearchInputGenerator searchInputGenerator,
            DataSummaryService dataSummaryService,
            IOptions<PathSettings> pathSettingsOptions,
            IOptions<ModuleTypes> moduleTypeOptions,
            IOptions<ConnectionStrings> connectionStringsOptions,
            IOptions<SummarySettings> summarySettingsOptions)
        {
            this.searchSettings = searchSettingsOptions.Value;
            this.importService = importService;
            this.searchManager = searchManager;
            this.graphManager = graphManager;
            this.multipleCriteriaSearchManager = multipleCriteriaSearchManager;
            this.searchInputGenerator = searchInputGenerator;
            this.dataSummaryService = dataSummaryService;
            this.pathSettings = pathSettingsOptions.Value;
            this.moduleType = moduleTypeOptions.Value.ModuleType;
            this.databaseType = connectionStringsOptions.Value.DatabaseType;
            this.summarySettings = summarySettingsOptions.Value;
        }

        public async Task Run()
        {
            try
            {
                switch (moduleType)
                {
                    case ModuleType.ImportGtfs:
                        await ImportGtfs();
                        break;
                    case ModuleType.ConvertGtfsToGraph:
                        await CreateGraph();
                        break;
                    case ModuleType.GenerateRandomSearchInputs:
                        await GenerateRandomSearchInputs();
                        break;
                    case ModuleType.SearchWithDijkstra:
                        await SearchWithDijkstra();
                        break;
                    case ModuleType.SearchWithMultipleCriterionDijkstra:
                        await SearchWithMultipleCriteriaDijkstra();
                        break;
                    case ModuleType.ExecuteBenchmark:
                        PerformFullBenchmarks();
                        break;
                    case ModuleType.CreateDataSummary:
                        await PerformDataSummary();
                        break;
                    case ModuleType.CreateDataSummaryFromCityCenter:
                        await PerformNearCenterDataSummary();
                        break;
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.ReadKey();
            }            
        }

        private async Task PerformDataSummary()
        {
            Console.WriteLine($"Calculating data summary for {databaseType}");
            await dataSummaryService.Summarize();
        }

        private async Task PerformNearCenterDataSummary()
        {
            Console.WriteLine($"Calculating data summary from city center only for {databaseType}");
            await dataSummaryService.SummarizeNearCenter();
        }

        private async Task GenerateRandomSearchInputs()
        {
            string path = pathSettings.CurrentSearchInputsFile(databaseType);
            var searches = await searchInputGenerator.Generate(summarySettings.SearchInputCount);
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.WriteAllText(path, JsonConvert.SerializeObject(searches));
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

        private async Task SearchWithDijkstra()
        {
            var search = SearchInputDto.Create(searchSettings);
            await searchManager.SearchFastestConnections(search);
        }

        private async Task SearchWithMultipleCriteriaDijkstra()
        {
            var search = SearchInputDto.Create(searchSettings);
            await multipleCriteriaSearchManager.SearchBestConnections(search);
        }

        private async Task CreateGraph()
        {
            await graphManager.CreateGraph();
        }

        private async Task ImportGtfs()
        {
            var gtfsFolderPath = pathSettings.GtfsImportFolder;
            await importService.ImportGtfs(gtfsFolderPath);
        }
    }
}
