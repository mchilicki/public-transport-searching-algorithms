using Chilicki.Ptsa.Data.Configurations.DependencyInjection;
using Chilicki.Ptsa.Domain.Gtfs.Configurations.DependencyInjection;
using Chilicki.Ptsa.Domain.Search.Configurations.DependencyInjection;
using Chilicki.Ptsa.Search.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chilicki.Ptsa.Search.Configurations.DependencyInjection
{
    public class GtfsDependencyInjection
    {
        public void Configure(IServiceCollection services)
        {
            ConfigureViews(services);
            new DomainSearchDependencyInjection().Configure(services);
            new DomainGtfsDependencyInjection().Configure(services);
            new DataDependencyInjection().Configure(services);
        }

        private static void ConfigureViews(IServiceCollection services)
        {
            services.AddTransient<MainWindow>();
        }
    }
}
