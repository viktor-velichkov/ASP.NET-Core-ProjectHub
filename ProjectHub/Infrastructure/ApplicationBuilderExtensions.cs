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

            SeedUserKinds(data);

            return app;
        }

        private static void SeedUserKinds(ProjectHubDbContext data)
        {
            if (data.UserKinds.Any())
            {
                return;
            }

            data.UserKinds.AddRange(new[] 
            {
                new UserKind { Name = "Investor"},
                new UserKind { Name = "Manager"},
                new UserKind { Name = "Designer"},
                new UserKind { Name = "Contractor"},
            });

            data.SaveChanges();
        }
    }
}
