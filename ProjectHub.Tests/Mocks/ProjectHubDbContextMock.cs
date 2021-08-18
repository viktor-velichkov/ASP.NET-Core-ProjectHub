using Microsoft.EntityFrameworkCore;
using ProjectHub.Data;
using System;

namespace ProjectHub.Tests.Mocks
{
    public static class ProjectHubDbContextMock
    {
        public static ProjectHubDbContext Instance
        {
            get
            {
                var options = new DbContextOptionsBuilder<ProjectHubDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                    .Options;

                return new ProjectHubDbContext(options);
            }
        }
    }
}
