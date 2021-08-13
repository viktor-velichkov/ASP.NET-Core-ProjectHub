using ProjectHub.Data.Models;
using ProjectHub.Models.Projects;
using System.Collections.Generic;

namespace ProjectHub.Services.Projects
{
    public interface IProjectService
    {
        public List<Project> GetAllProjectsWithInvestor();
        public List<Project> GetLatestThreeProjects();
        public void AddProject(ProjectAddViewModel model, int investorId);

        public Project GetProjectById(int id);

        public Project GetProjectWithItsParticipantsById(int id);

        public List<Offer> GetProjectOffersWithAuthorByProjectId(int id);

        public string GetDesignerDisciplineName(int id);

        public void AddDesignerToProject(int projectId, int designerId);

        public void AddUserToProjectPosition(int projectId, int userId, string projectPosition);

        public bool CheckIfProjectAlreadyHasSuchASpecialist(int projectId, string position);
    }
}
