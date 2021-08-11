using ProjectHub.Models.User;
using System;
using System.Collections.Generic;

namespace ProjectHub.Models.Projects
{
    public class ProjectDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string Deadline { get; set; }

        public string Description { get; set; }

        public string Investor { get; set; }

        public string Manager { get; set; }
        public ICollection<DesignerProjectDetailsViewModel> Designers => new HashSet<DesignerProjectDetailsViewModel>();

        public string Contractor { get; set; }

        public bool IsLoggedUserPositionFree { get; set; }

        public bool IsLoggedUserAlreadySentAnOffer { get; set; }
    }
}
