using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectHub.Data;
using ProjectHub.Data.Models;
using ProjectHub.Data.Models.Projects;
using ProjectHub.Models.Projects;
using System.Collections.Generic;
using System.Linq;

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


        public List<Project> GetAllProjectsWithInvestor()
            => this.data
                   .Projects
                   .Include(p=>p.Investor)
                   .ThenInclude(i=>i.User)
                   .OrderByDescending(p=>p.Id)                   
                   .ToList();

        public List<Project> GetLatestThreeProjects()
            => this.data
                   .Projects
                   .Include(p => p.Investor)
                   .ThenInclude(i => i.User)
                   .OrderByDescending(p => p.Id)
                   .Take(3)
                   .ToList();

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

        public Project GetProjectById(int id)
            => this.data
                   .Projects
                   .FirstOrDefault(p => p.Id.Equals(id));

        public Project GetProjectWithItsParticipantsById(int id)
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

        public List<Offer> GetProjectOffersWithAuthorByProjectId(int id)
            => this.data
                   .Offers
                   .Include(offer => offer.Author)
                   .Where(offer => offer.ProjectId.Equals(id))
                   .ToList();

        public string GetDesignerDisciplineName(int id)
            => this.data
                   .Designers
                   .Include(d => d.Discipline)
                   .FirstOrDefault(d => d.Id.Equals(id))
                   .Discipline
                   .Name;

        public void AddDesignerToProject(int projectId, int designerId)
        {
            var projectDesigner = new ProjectDesigner { ProjectId = projectId, DesignerId = designerId };

            var designer = this.data.Designers.FirstOrDefault(d => d.Id.Equals(designerId));
            


            var asd = this.data.ProjectDesigners;

            asd.Add(projectDesigner);

            this.data.SaveChanges();
        }

        public void AddUserToProjectPosition(int projectId, int userId, string projectPosition)
        {
            var project = this.GetProjectWithItsParticipantsById(projectId);

            this.data.Projects.Add(project);

            typeof(Project).GetProperty(projectPosition + "Id").SetValue(project, userId);

            this.data.SaveChanges();
        }

        public bool CheckIfProjectAlreadyHasSuchASpecialist(int projectId, string position)
        {
            var project = this.GetProjectWithItsParticipantsById(projectId);

            var positionParts = position.Split(" - ").ToArray();

            var projectPosition = positionParts.First();

            if (projectPosition.Equals(nameof(Designer)))
            {
                var positionDiscipline = positionParts[1];

                return project.Designers.Any(pd => pd.Designer
                                                     .Discipline
                                                     .Name
                                                     .Equals(positionDiscipline));
            }
            else
            {
                return typeof(Project).GetProperty(projectPosition + "Id").GetValue(project) != null;
            }
        }

        
    }
}
