using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectHub.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace ProjectHub.Models.User
{
    public class AppUserEditProfileViewModel
    {
        public int Id { get; set; }

        public string UserKindName { get; set; }

        public byte[] Image { get; set; }
        
        public IFormFile ImageUpload { get; set; }

        [Required]
        [StringLength(DataConstants.UserFirstNameMaxLength,
                      ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
                      MinimumLength = DataConstants.UserFirstNameMinLength)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(DataConstants.UserLastNameMaxLength,
                      ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
                      MinimumLength = DataConstants.UserLastNameMinLength)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [StringLength(DataConstants.WebSiteAddresMaxLength,
                      ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
                      MinimumLength = DataConstants.WebSiteAddresMinLength)]
        public string WebSite { get; set; }

        [StringLength(DataConstants.DescriptionMaxLength,
                      ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
                      MinimumLength = DataConstants.DescriptionMinLength)]
        public string Description { get; set; }

        [Url]
        [DisplayName("Facebook Profile:")]
        public string FacebookPage { get; set; }

        [Url]
        [DisplayName("LinkedIn Profile:")]
        public string LinkedinPage { get; set; }

        [Url]
        [DisplayName("Skype Profile:")]
        public string SkypeProfile { get; set; }
    }
}
