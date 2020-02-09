using Chilicki.Ptsa.Search.Configurations.DependencyInjection;
using Chilicki.Ptsa.Search.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Chilicki.Ptsa.Gtfs
{
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }

        public IConfiguration Configuration { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            Configuration = builder.Build();

            var serviceCollection = new ServiceCollection();
            var gtfsDependencyInjection = new GtfsDependencyInjection();
            gtfsDependencyInjection.Configure(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }        
    }
}
