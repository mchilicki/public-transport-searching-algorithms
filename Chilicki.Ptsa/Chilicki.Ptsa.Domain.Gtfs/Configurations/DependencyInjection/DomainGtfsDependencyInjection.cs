using Chilicki.Ptsa.Domain.Gtfs.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chilicki.Ptsa.Domain.Gtfs.Configurations.DependencyInjection
{
    public class DomainGtfsDependencyInjection
    {
        public void Configure(IServiceCollection services)
        {
            ConfigureServices(services);
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<GtfsImportService>();
        }
    }
}
