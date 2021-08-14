using ProjectHub.Models.Contractor;
using ProjectHub.Models.Designer;
using ProjectHub.Models.Investor;
using ProjectHub.Models.Manager;
using ProjectHub.Models.Projects;
using System.Collections.Generic;

namespace ProjectHub.Models.Home
{
    public class IndexPageViewModel
    {
        public List<ProjectListingViewModel> Projects { get; set; }
        public List<InvestorListViewModel> Investors { get; set; }
        public List<ManagerListViewModel> Managers { get; set; }
        public List<DesignerListViewModel> Designers { get; set; }
        public List<ContractorListViewModel> Contractors { get; set; }
    }
}
