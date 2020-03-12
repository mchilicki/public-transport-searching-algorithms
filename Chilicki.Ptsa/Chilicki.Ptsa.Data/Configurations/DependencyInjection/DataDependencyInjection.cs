using Chilicki.Ptsa.Data.Configurations.ProjectConfiguration;
using Chilicki.Ptsa.Data.Databases;
using Chilicki.Ptsa.Data.Repositories;
using Chilicki.Ptsa.Data.UnitsOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddTransient<StopRepository>();
            services.AddTransient<GraphRepository>();
            services.AddTransient<ISimilarVertexRepository, SimilarVertexRepository>();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }

        private void ConfigureDatabases(IServiceCollection services, ConnectionStrings connectionStrings)
        {
            var databaseConnectionString = connectionStrings.PtsaDatabase;            
            services.AddDbContext<DbContext, PtsaDbContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer(
                        databaseConnectionString,
                        b => b.MigrationsAssembly(typeof(PtsaDbContext).Assembly.GetName().Name)
                    )
                    .UseLazyLoadingProxies();
                optionsBuilder.EnableSensitiveDataLogging(true);
            });            
        }        
    }
}
