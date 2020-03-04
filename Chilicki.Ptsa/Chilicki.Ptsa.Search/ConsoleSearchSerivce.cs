using Chilicki.Ptsa.Data.Configurations.ProjectConfiguration;
using Chilicki.Ptsa.Domain.Gtfs.Services;
using Chilicki.Ptsa.Domain.Search.Dtos;
using Chilicki.Ptsa.Domain.Search.Managers;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Chilicki.Ptsa.Search.Configurations.Startup
{
    public class ConsoleSearchSerivce
    {
        private readonly AppSettings appSettings;
        private readonly GtfsImportService importService;
        private readonly SearchManager searchManager;

        public ConsoleSearchSerivce(
            IOptions<AppSettings> appSettings,
            GtfsImportService importService,
            SearchManager searchManager)
        {
            this.appSettings = appSettings.Value;
            this.importService = importService;
            this.searchManager = searchManager;
        }

        public async Task Run()
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (environmentName == "GtfsImport" || environmentName == "GtfsImportPC")
                await ImportGtfs();
            if (environmentName == "DijkstraSearch" || environmentName == "DijkstraSearchPC")
                await SearchWithDijkstra();
            if (environmentName == "CreateGraph")
                await CreateGraph();
        }

        private async Task SearchWithDijkstra()
        {
            var searchInput = new SearchInputDto()
            {
                StartStopId = appSettings.StartStopId,
                DestinationStopId = appSettings.EndStopId,
                StartTime = appSettings.StartTime,
            };
            await searchManager.SearchFastestConnections(searchInput);
        }

        private async Task CreateGraph()
        {
            await searchManager.CreateGraph();
        }

        private async Task ImportGtfs()
        {
            var gtfsFolderPath = appSettings.ImportGtfsPath;
            await importService.ImportGtfs(gtfsFolderPath);
        }
    }
}
