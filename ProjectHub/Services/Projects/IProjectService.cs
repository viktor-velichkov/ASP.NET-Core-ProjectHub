using ProjectHub.Data.Models;
using ProjectHub.Models.Project;
using System.Collections.Generic;

namespace ProjectHub.Services.Projects
{
    public interface IProjectService
    {
        public void AddProject(ProjectAddViewModel model, int investorId);

        public Project GetProjectById(int id);
        public List<Discipline> GetAllDisciplines();

        public ApplicationUser GetUserById(int id);

        public string GetDesignerDisciplineName(int id);
    }
}
