using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chilicki.Ptsa.Data.Configurations.DependencyInjection
{
    public class DataDependencyInjection
    {
        public void Configure(IServiceCollection services)
        {
            ConfigureRepositories(services);
            ConfigureDatabases(services);
        }

        private void ConfigureDatabases(IServiceCollection services)
        {
            
        }

        private void ConfigureRepositories(IServiceCollection services)
        {
            
        }
    }
}
