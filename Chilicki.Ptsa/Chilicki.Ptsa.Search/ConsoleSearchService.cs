using Chilicki.Ptsa.Data.Configurations.ProjectConfiguration;
using Chilicki.Ptsa.Domain.Gtfs.Services;
using Chilicki.Ptsa.Domain.Search.Dtos;
using Chilicki.Ptsa.Domain.Search.Managers;
using Microsoft.Extensions.Options;
using System;
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

        public ConsoleSearchService(
            IOptions<AppSettings> appSettings,
            GtfsImportService importService,
            SearchManager searchManager,
            GraphManager graphManager,
            MultipleCriteriaSearchManager multipleCriteriaSearchManager)
        {
            this.appSettings = appSettings.Value;
            this.importService = importService;
            this.searchManager = searchManager;
            this.graphManager = graphManager;
            this.multipleCriteriaSearchManager = multipleCriteriaSearchManager;
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.ReadKey();
            }            
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
