using ProjectHub.Data.Models;
using ProjectHub.Models.Projects;
using System.Collections.Generic;

namespace ProjectHub.Services.Projects
{
    public interface IProjectService
    {
        public void AddProject(ProjectAddViewModel model, int investorId);

        public Project GetProjectById(int id);

        public Project GetProjectWithItsParticipantsById(int id);

        public List<Offer> GetProjectOffersWithAuthorByProjectId(int id);

        public List<Discipline> GetAllDisciplines();

        public string GetDesignerDisciplineName(int id);
    }
}
