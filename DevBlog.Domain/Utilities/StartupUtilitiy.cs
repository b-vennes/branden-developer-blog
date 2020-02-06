using System;
using Microsoft.Extensions.Configuration;

namespace DevBlog.Domain.Utilities
{
    public class StartupUtility
    {
        public static string GetConnectionString(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var databaseServerName = Environment.GetEnvironmentVariable("ASPNET_DBSERVER");
            var databaseUserId = Environment.GetEnvironmentVariable("ASPNET_DBUSERID");
            var databasePassword = Environment.GetEnvironmentVariable("ASPNET_DBPASSWORD");

            return connectionString.Replace("{DBSERVER}", databaseServerName)
                .Replace("{DBUSERID}", databaseUserId)
                .Replace("{DBPASSWORD}", databasePassword);
        }
    }
}