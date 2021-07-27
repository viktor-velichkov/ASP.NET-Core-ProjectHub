using ProjectHub.Models.Discussion;
using ProjectHub.Models.Offer;
using ProjectHub.Models.Review;
using System.Collections.Generic;
using System.ComponentModel;

namespace ProjectHub.Models.User
{
    public class AppUserProfileViewModel
    {
        public int Id { get; set; }

        public string UserKindName { get; set; }

        public byte[] Image { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string WebSite { get; set; }

        public int Recomendations { get; set; }

        public int Disapprovals { get; set; }

        public string Description { get; set; }

        [DisplayName("Facebook Profile:")]
        public string FacebookPage { get; set; }

        [DisplayName("LinkedIn Profile:")]
        public string LinkedinPage { get; set; }

        [DisplayName("Skype Profile:")]
        public string SkypeProfile { get; set; }

        public List<ReviewViewModel> Reviews => new List<ReviewViewModel>();
        public List<OfferViewModel> Offers => new List<OfferViewModel>();
        public List<DiscussionViewModel> Discussions => new List<DiscussionViewModel>();

    }
}
