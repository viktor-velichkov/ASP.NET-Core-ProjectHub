using ProjectHub.Models.Projects;
using ProjectHub.Models.User;
using System.Collections.Generic;

namespace ProjectHub.Models.Manager
{
    public class ManagerProfileViewModel
    {
        public AppUserProfileViewModel User { get; set; }

        public ICollection<ProjectListingViewModel> Projects { get; set; }
    }
}
