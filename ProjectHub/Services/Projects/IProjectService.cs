using ProjectHub.Models.Project;

namespace ProjectHub.Services.Projects
{
    public interface IProjectService
    {
        public void AddProject(ProjectAddViewModel model, int investorId);
    }
}
