using System.Collections.Generic;

namespace ProjectHub.Models.Projects
{
    public class AllProjectsViewModel
    {
        public AllProjectsViewModel()
        {
            this.Projects = new List<ProjectCardViewModel>();
        }

        public const int ProjectsPerPage = 3;
        public int CurrentPage { get; set; } = 1;
        public string SearchTerm { get; set; }
        public string CityFilter { get; set; }
        public List<ProjectCardViewModel> Projects { get; set; }
        public List<string> Cities { get; set; }
    }
}
