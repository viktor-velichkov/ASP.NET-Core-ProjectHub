using ProjectHub.Data.Models;
using ProjectHub.Models.Projects;
using ProjectHub.Models.User;
using System.Collections.Generic;

namespace ProjectHub.Services.Projects
{
    public interface IProjectService
    {
        public bool ProjectExists(int id);
        public List<ProjectCardViewModel> GetAllProjectsOrderedByDateDescending();

        public List<ProjectCardViewModel> FilterByCity(string city);

        public List<string> GetAllProjectCities();

        public void AddProject(ProjectAddViewModel model, int investorId);

        public void RemoveProject(int projectId);

        public byte[] GetProjectImage(int id);

        public List<Project> GetLatestThreeProjects();

        public ProjectOffersListViewModel GetProjectWithOffersById(int id);

        public ProjectDetailsViewModel GetProjectDetailsViewModel(int id);        

        public List<DesignerProjectDetailsViewModel> GetProjectDesignersByProjectId(int projectId);

        public string GetDesignerDisciplineName(int id);

        public void AddDesignerToProject(int projectId, int designerId);

        public void AddUserToProject(int projectId, int userId, string projectPosition);

        public bool AlreadyHasSuchASpecialist(int projectId, string position);

        public bool IsOwnerOfTheProject(int investorId, int projectId);
        public Project GetProject(int projectId);
    }
}
