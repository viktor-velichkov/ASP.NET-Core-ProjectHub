using ProjectHub.Models.User;
using System.Collections.Generic;

namespace ProjectHub.Models.Projects
{
    public class ProjectDetailsViewModel
    {
        public ProjectDetailsViewModel()
        {
            this.Designers = new HashSet<DesignerProjectDetailsViewModel>();
        }
        public int Id { get; set; }

        public byte[] Image { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string Deadline { get; set; }

        public string Description { get; set; }

        public int InvestorId { get; set; }
        public string Investor { get; set; }

        public int ManagerId { get; set; }
        public string Manager { get; set; }

        public ICollection<DesignerProjectDetailsViewModel> Designers;

        public int ContractorId { get; set; }
        public string Contractor { get; set; }

        public bool IsLoggedUserPositionFree { get; set; }

        public bool IsLoggedUserAlreadySentAnOffer { get; set; }
        public bool IsLoggedUserAlreadyHiredForThisProject { get; set; }
    }
}
