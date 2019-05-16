using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using TemplateName.Core.Configuration;
using TemplateName.Core.Web;

namespace TemplateName.Data.EntityFrameworkCore
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>();

            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder(), environment);

            // TODO: config db connection.
            // builder.UseMySql(configuration.GetConnectionString(Core.AppConsts.ConnectionStringName));

            return new AppDbContext(builder.Options);
        }
    }
}
