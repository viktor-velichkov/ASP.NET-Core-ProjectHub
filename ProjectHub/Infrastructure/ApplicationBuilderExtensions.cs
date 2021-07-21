using Microsoft.AspNetCore.Builder;
using ProjectHub.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ProjectHub.Data.Models;

namespace ProjectHub.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<ProjectHubDbContext>();

            data.Database.Migrate();

            SeedUserTypes(data);

            return app;
        }

        private static void SeedUserTypes(ProjectHubDbContext data)
        {
            if (data.UserTypes.Any())
            {
                return;
            }

            data.UserTypes.AddRange(new[] 
            {
                new UserType { Name = "Investor"},
                new UserType { Name = "Manager"},
                new UserType { Name = "Designer"},
                new UserType { Name = "Contractor"},
            });

            data.SaveChanges();
        }
    }
}
