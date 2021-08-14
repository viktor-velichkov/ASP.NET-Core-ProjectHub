using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectHub.Data.Models
{

    public class ApplicationUser : IdentityUser<int>
    {
        public ApplicationUser()
        {
            this.RatesReceived = new HashSet<Rate>();
            this.RatesSent = new HashSet<Rate>();
            this.Offers = new HashSet<Offer>();

            this.ReviewsReceived = new HashSet<Review>();
            this.ReviewsSent = new HashSet<Review>();

            this.Discussions = new HashSet<Discussion>();
            this.Messages = new HashSet<Message>();
        }






        [ForeignKey(nameof(UserKind))]
        public int UserKindId { get; set; }
        public UserKind UserKind { get; set; }

        public byte[] Image { get; set; }

        [Required]
        [MaxLength(DataConstants.UserFirstNameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(DataConstants.UserLastNameMaxLength)]
        public string LastName { get; set; }

        public string FullName => this.FirstName + " " + this.LastName;


        [MaxLength(DataConstants.DescriptionMaxLength)]
        public string Description { get; set; }


        [MaxLength(DataConstants.WebSiteAddresMaxLength)]
        public string WebSite { get; set; }

        [InverseProperty(nameof(Rate.Recipient))]
        public ICollection<Rate> RatesReceived { get; set; }

        [InverseProperty(nameof(Rate.Author))]
        public ICollection<Rate> RatesSent { get; set; }

        [Url]
        public string FacebookPage { get; set; }

        [Url]
        public string LinkedinPage { get; set; }

        [Url]
        public string SkypeProfile { get; set; }


        public ICollection<Offer> Offers { get; set; }

        [InverseProperty(nameof(Review.Recipient))]
        public ICollection<Review> ReviewsReceived { get; set; }

        [InverseProperty(nameof(Review.Author))]
        public ICollection<Review> ReviewsSent { get; set; }

        public ICollection<Discussion> Discussions { get; set; }

        public ICollection<Message> Messages { get; set; }

    }
}
