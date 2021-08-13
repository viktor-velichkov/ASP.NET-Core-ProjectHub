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

            SeedDisciplines(data);

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

        private static void SeedDisciplines(ProjectHubDbContext data)
        {
            if (data.Disciplines.Any())
            {
                return;
            }

            data.Disciplines.AddRange(new[]
            {
                new Discipline { Id = 1, Name = "Architecture"},
                new Discipline { Id = 2, Name = "Structure"},
                new Discipline { Id = 3, Name = "Electro"},
                new Discipline { Id = 4, Name = "WS&S"},
                new Discipline { Id = 5, Name = "HVAC"},
                new Discipline { Id = 6, Name = "Geodesy"},
                new Discipline { Id = 7, Name = "Landscaping"},
            });

            data.SaveChanges();
        }


    }
}
