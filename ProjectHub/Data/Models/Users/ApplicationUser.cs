using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectHub.Data.Models
{

    public class ApplicationUser : IdentityUser<int>
    {
        [ForeignKey(nameof(UserKind))]
        public int UserKindId { get; set; }
        public UserKind UserKind { get; set; }

        
        public string ImageUrl { get; set; }

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


        public ICollection<Offer> Offers => new HashSet<Offer>();

        [InverseProperty(nameof(Review.Recipient))]
        public ICollection<Review> ReviewsReceived => new HashSet<Review>();

        [InverseProperty(nameof(Review.Author))]
        public ICollection<Review> ReviewsSent => new HashSet<Review>();

        public ICollection<Discussion> Discussions => new HashSet<Discussion>();

        public ICollection<Message> Messages => new HashSet<Message>();

    }
}
