using ProjectHub.Models.Message;
using ProjectHub.Models.Offer;
using ProjectHub.Models.Project;
using ProjectHub.Models.Review;
using System.Collections.Generic;

namespace ProjectHub.Models.User
{
    public class UserProfileViewModel
    {
        public string FullName { get; set; }

        public string UserType { get; set; }

        public string Discipline { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public int Recomendations { get; set; }

        public int Disapprovals { get; set; }

        public string FacebookPage { get; set; }

        public string LinkedinPage { get; set; }

        public string SkypeProfile { get; set; }

        public ICollection<ProjectGeneralViewModel> Projects { get; set; }
        public ICollection<ReviewViewModel> Reviews { get; set; }
        public ICollection<OfferViewModel> Offers { get; set; }
        public ICollection<MessageViewModel> Messages { get; set; }


    }
}
