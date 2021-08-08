using AutoMapper;
using ProjectHub.Data;
using ProjectHub.Models.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectHub.Data.Models.Projects;
using ProjectHub.Data.Models;
using Microsoft.EntityFrameworkCore;

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

        public List<Discipline> GetAllDisciplines()
            => this.data.Disciplines.ToList();

        public Project GetProjectById(int id)
            => this.data
                   .Projects
                   .Include(p => p.Investor)
                   .ThenInclude(i => i.User)
                   .Include(p => p.Manager)
                   .ThenInclude(i => i.User)
                   .Include(p => p.Contractor)
                   .ThenInclude(i => i.User)
                   .Include(p => p.Designers)
                   .FirstOrDefault(p => p.Id.Equals(id));

        public ApplicationUser GetUserById(int id)
            => this.data
                   .Users
                   .Include(u=>u.UserKind)
                   .FirstOrDefault(u => u.Id.Equals(id));

        public string GetDesignerDisciplineName(int id)
            => this.data
                   .Designers
                   .Include(d => d.Discipline)
                   .FirstOrDefault(d => d.Id.Equals(id))
                   .Discipline
                   .Name;
    }
}
