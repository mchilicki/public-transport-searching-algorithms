using Chilicki.Ptsa.Data.Configurations.ProjectConfiguration;
using Chilicki.Ptsa.Data.Databases;
using Chilicki.Ptsa.Data.UnitsOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chilicki.Ptsa.Data.Configurations.DependencyInjection
{
    public class DataDependencyInjection
    {
        public void Configure(IServiceCollection services, ConnectionStrings connectionStrings)
        {
            ConfigureRepositories(services);
            ConfigureDatabases(services, connectionStrings);
            ConfigureServices(services);
        }

        private void ConfigureRepositories(IServiceCollection services)
        {

        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }

        private void ConfigureDatabases(IServiceCollection services, ConnectionStrings connectionStrings)
        {
            var databaseConnectionString = connectionStrings.PtsaDatabase;
            services.AddDbContext<DbContext, PtsaDbContext>(options => options
                .UseSqlServer(
                    databaseConnectionString,
                    b => b.MigrationsAssembly(typeof(PtsaDbContext).Assembly.GetName().Name
                ))
                .UseLazyLoadingProxies()
            );
        }        
    }
}
