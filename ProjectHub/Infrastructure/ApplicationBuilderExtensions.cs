using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjectHub.Areas.Admin;
using ProjectHub.Data;
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

            SeedAdministrator(scopedServices.ServiceProvider);

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
                new UserKind { Id = 1, Name = nameof(Investor)},
                new UserKind { Id = 2, Name = nameof(Manager)},
                new UserKind { Id = 3, Name = nameof(Designer)},
                new UserKind { Id = 4, Name = nameof(Contractor)}                
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

        private static void SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole<int>>>();

            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync(AdminConstants.AdministratorRole))
                    {
                        return;
                    }

                    var role = new IdentityRole<int> { Name = AdminConstants.AdministratorRole };

                    await roleManager.CreateAsync(role);

                    const string adminEmail = "admin@projecthub.com";
                    const string adminPassword = "admin123";

                    var user = new ApplicationUser
                    {
                        Email = adminEmail,
                        UserName = adminEmail,
                        FirstName = "Admin",
                        LastName = "Admin"
                    };

                    await userManager.CreateAsync(user, adminPassword);

                    await userManager.AddToRoleAsync(user, AdminConstants.AdministratorRole);
                })
                .GetAwaiter()
                .GetResult();
        }


    }
}
