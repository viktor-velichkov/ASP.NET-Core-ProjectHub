using AutoMapper;
using ProjectHub.Data;
using ProjectHub.Models.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectHub.Data.Models.Projects;
using ProjectHub.Data.Models;

namespace ProjectHub.Services.Projects
{
    public class ProjectService : IProjectService
    {
        private readonly ProjectHubDbContext data;
        private readonly IMapper mapper;

        public ProjectService(ProjectHubDbContext data,
                             IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public void AddProject(ProjectAddViewModel model, int investorId)
        {
            this.data.Projects.Add(new Project
            {
                InvestorId = investorId,
                Name = model.Name,
                City = model.City,
                Address = model.Address,
                Deadline = model.Deadline,
                Description = model.Description
            });

            this.data.SaveChanges();
        }
    }
}
