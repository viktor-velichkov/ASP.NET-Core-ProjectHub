using ProjectHub.Models.Projects;
using ProjectHub.Models.User;
using System.Collections.Generic;

namespace ProjectHub.Models.Investor
{
    public class InvestorProfileViewModel
    {
        public AppUserProfileViewModel User { get; set; }

        public ICollection<ProjectListingViewModel> Projects { get; set; }
    }
}
