using Chilicki.Ptsa.Data.Configurations.ProjectConfiguration;
using Chilicki.Ptsa.Domain.Gtfs.Services;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Chilicki.Ptsa.Search.Configurations.Startup
{
    public class ConsoleSearchSerivce
    {
        private AppSettings appSettings;
        private GtfsImportService importService;

        public ConsoleSearchSerivce(
            IOptions<AppSettings> appSettings,
            GtfsImportService importService)
        {
            this.appSettings = appSettings.Value;
            this.importService = importService;
        }

        public async Task Run()
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (environmentName == "GtfsImport")
                await ImportGtfs();
        }

        public async Task ImportGtfs()
        {
            var gtfsFolderPath = appSettings.ImportGtfsPath1;
            await importService.ImportGtfs(gtfsFolderPath);
        }
    }
}
