using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chilicki.Ptsa.Domain.Search.Configurations.DependencyInjection
{
    public class DomainSearchDependencyInjection
    {
        public void Configure(IServiceCollection services)
        {
            ConfigureServices(services);
        }

        private void ConfigureServices(IServiceCollection services)
        {
            
        }
    }
}
