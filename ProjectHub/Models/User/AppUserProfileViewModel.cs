using ProjectHub.Models.Message;
using ProjectHub.Models.Offer;
using ProjectHub.Models.Review;
using System.Collections.Generic;

namespace ProjectHub.Models.User
{
    public class AppUserProfileViewModel
    {
        public string UserKindName { get; set; }

        public string ImageUrl { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string WebSite { get; set; }

        public int Recomendations { get; set; }

        public int Disapprovals { get; set; }

        public string Description { get; set; }

        public string FacebookPage { get; set; }

        public string LinkedinPage { get; set; }

        public string SkypeProfile { get; set; }

        public ICollection<ReviewViewModel> Reviews { get; set; }
        public ICollection<OfferViewModel> Offers { get; set; }
        public ICollection<DiscussionViewModel> Discussions { get; set; }

    }
}
