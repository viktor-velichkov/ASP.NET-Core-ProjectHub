using System.Collections.Generic;

namespace ProjectHub.Models.Projects
{
    public class AllProjectsViewModel
    {
        public const int ProjectsPerPage = 3;
        public int CurrentPage { get; set; } = 1;
        public List<ProjectListingViewModel> Projects { get; set; }
    }
}
