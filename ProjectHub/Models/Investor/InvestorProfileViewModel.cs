using ProjectHub.Models.Project;
using ProjectHub.Models.User;
using System.Collections.Generic;

namespace ProjectHub.Models.Investor
{
    public class InvestorProfileViewModel
    {
        public AppUserProfileViewModel User { get; set; }

        public ICollection<ProjectGeneralViewModel> Projects { get; set; }
    }
}
