using ProjectHub.Models.Investor;
using ProjectHub.Models.Projects;
using System.Collections.Generic;

namespace ProjectHub.Models.Home
{
    public class IndexPageViewModel
    {
        public List<ProjectListingViewModel> Projects { get; set; }
        public List<InvestorListViewModel> Investors { get; set; }
    }
}
