using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectHub.Data.Models
{
    
    public class User : IdentityUser
    {
        [Required]
        public string ImageUrl { get; set; }

        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }

        public string FullName => this.FirstName + " " + this.LastName;


        [MaxLength(10000)]
        public string Description { get; set; }

        [Required]
        [MaxLength(50)]
        public string WebSite { get; set; }


        public ICollection<Rate> Rates { get; set; }

        public ICollection<UserProject> UserProjects => new HashSet<UserProject>();

        public ICollection<ProjectOffer> ProjectOffers => new HashSet<ProjectOffer>();

        public ICollection<Review> Reviews => new HashSet<Review>();

        public ICollection<Message> Messages => new HashSet<Message>();

    }
}
