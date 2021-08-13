using ProjectHub.Data.Models;
using ProjectHub.Data.Models.Users;
using ProjectHub.Models.Projects;
using ProjectHub.Models.User;
using System.Collections.Generic;

namespace ProjectHub.Models.Contractor
{
    public class ContractorProfileViewModel
    {      
        public AppUserProfileViewModel User { get; set; }

        public ICollection<Activity> Activities { get; set; }

        public ICollection<ProjectListingViewModel> Projects { get; set; }
    }
}
