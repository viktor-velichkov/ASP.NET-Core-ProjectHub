using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectHub.Data;
using ProjectHub.Data.ExceptionMessages;
using ProjectHub.Data.Models;
using ProjectHub.Data.Models.Projects;
using ProjectHub.Models.Projects;
using System;
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

        public List<Project> GetAllProjectsOrderedByDateDescending()
             => this.data
                    .Projects
                    .Include(p => p.Investor)
                    .OrderByDescending(p => p.Id)
                    .ToList();

        public List<ProjectCardViewModel> FilterByCity(string city)
        {
            var query = this.data
                            .Projects
                            .Include(p => p.Investor)
                            .AsQueryable();

            if (!String.IsNullOrWhiteSpace(city)
                && city != "All")
            {
                query = query.Where(p => p.City.Equals(city));
            }

            var projects = query.OrderByDescending(p => p.Id)
                                .ToList();

            var projectsModel = this.mapper
                                    .Map<List<Project>, List<ProjectCardViewModel>>(projects);

            return projectsModel;
        }

        public List<string> GetAllProjectCities()
            => this.data
                   .Projects
                   .Select(p => p.City)
                   .Distinct()
                   .OrderBy(c => c)
                   .ToList();

        public void AddProject(ProjectAddViewModel model, int investorId)
        {
            this.data.Projects.Add(new Project
            {
                Image = model.Image,
                InvestorId = investorId,
                Name = model.Name,
                City = model.City,
                Address = model.Address,
                Deadline = model.Deadline,
                Description = model.Description
            });

            this.data.SaveChanges();
        }

        public void RemoveProject(int projectId)
        {
            var project = this.data
                              .Projects
                              .FirstOrDefault(p => p.Id.Equals(projectId));

            var projectOffers = this.data
                                    .Offers
                                    .Where(offer => offer.ProjectId.Equals(projectId))
                                    .ToList();

            this.data.Offers.RemoveRange(projectOffers);

            this.data.Projects.Remove(project);

            this.data.SaveChanges();
        }

        public byte[] GetProjectImage(int id)
            => this.data.Projects.FirstOrDefault(u => u.Id.Equals(id)).Image;

        public List<Project> GetLatestThreeProjects()
            => this.data
                   .Projects
                   .Include(p => p.Investor)
                   .ThenInclude(i => i.User)
                   .OrderByDescending(p => p.Id)
                   .Take(3)
                   .ToList();

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
                   .FirstOrDefault(p => p.Id.Equals(id));

        public List<ProjectDesigner> GetProjectDesignersByProjectId(int projectId)
            => this.data
                   .ProjectDesigners
                   .Include(pd => pd.Designer)
                   .ThenInclude(d => d.Discipline)
                   .Include(pd => pd.Designer)
                   .ThenInclude(d => d.User)
                   .Where(pd => pd.ProjectId.Equals(projectId))
                   .ToList();

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

            var project = this.data.Projects.FirstOrDefault(p => p.Id.Equals(projectId));

            var designer = this.data.Designers.FirstOrDefault(p => p.Id.Equals(designerId));

            if (project == null || designer == null)
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidData);
            }

            var projectDesigner = new ProjectDesigner { Project = project, Designer = designer };

            this.data.ProjectDesigners.Add(projectDesigner);

            this.data.SaveChanges();
        }

        public void AddUserToProjectPosition(int projectId, int userId, string projectPosition)
        {
            var project = this.GetProjectWithItsParticipantsById(projectId);

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

        public bool ConfirmThatInvestorIsOwnerOfTheProject(int investorId, int projectId)
            => this.data
                   .Projects
                   .FirstOrDefault(p => p.Id.Equals(projectId))
                   .InvestorId
                   .Equals(investorId);


    }
}
