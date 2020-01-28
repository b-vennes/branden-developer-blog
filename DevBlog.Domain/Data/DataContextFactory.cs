using System;
using System.IO;
using DevBlog.Domain.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace DevBlog.Domain.Data
{
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(params string[] args)
        {
            var options = new DbContextOptionsBuilder<DataContext>();
            var config = GetAppConfiguration();

            options.UseSqlServer(StartupUtility.GetConnectionString(config));

            return new DataContext(options.Options);
        }

        IConfiguration GetAppConfiguration()
        {
            var environmentName =
                Environment.GetEnvironmentVariable(
                    "ASPNETCORE_ENVIRONMENT");

            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json");

            return builder.Build();
        }
    }
}