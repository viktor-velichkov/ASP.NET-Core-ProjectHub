using ProjectHub.Data.Models;
using ProjectHub.Data.Models.Projects;
using ProjectHub.Models.Projects;
using System.Collections.Generic;

namespace ProjectHub.Services.Projects
{
    public interface IProjectService
    {
        public void AddProject(ProjectAddViewModel model, int investorId);
        public void RemoveProject(int projectId);

        public byte[] GetProjectImage(int id);        

        public List<Project> GetLatestThreeProjects();

        public Project GetProjectById(int id);

        public Project GetProjectWithItsParticipantsById(int id);

        public List<ProjectDesigner> GetProjectDesignersByProjectId(int projectId);

        public List<Offer> GetProjectOffersWithAuthorByProjectId(int id);

        public string GetDesignerDisciplineName(int id);

        public void AddDesignerToProject(int projectId, int designerId);

        public void AddUserToProjectPosition(int projectId, int userId, string projectPosition);

        public bool CheckIfProjectAlreadyHasSuchASpecialist(int projectId, string position);

        public bool ConfirmThatInvestorIsOwnerOfTheProject(int investorId, int projectId);
    }
}
