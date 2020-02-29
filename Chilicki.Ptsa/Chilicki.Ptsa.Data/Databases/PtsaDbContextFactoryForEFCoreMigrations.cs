﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Chilicki.Ptsa.Data.Databases
{
    public class PtsaDbContextFactoryForEFCoreMigrations : IDesignTimeDbContextFactory<PtsaDbContext>
    {
        public PtsaDbContext CreateDbContext(string[] args)
        {
            var configuration = GetConfiguration();
            var databaseConnectionString = configuration.GetConnectionString("PtsaDatabase");
            var optionsBuilder = new DbContextOptionsBuilder<PtsaDbContext>();
            optionsBuilder.UseSqlServer(
                databaseConnectionString,
                b => b.MigrationsAssembly(typeof(PtsaDbContext).Assembly.GetName().Name)
            );
            return new PtsaDbContext(optionsBuilder.Options);
        }

        private IConfigurationRoot GetConfiguration()
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            var configuration = builder.Build();
            return configuration;
        }
    }
}
