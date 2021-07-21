using ProjectHub.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectHub.Models.User
{
    public class UserRegisterFormModel
    {
        public UserRegisterFormModel()
        {
            this.UserTypes = new List<UserTypeRegisterFormModel>();
        }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(DataConstants.PasswordMaxLength, 
                      ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", 
                      MinimumLength = DataConstants.PasswordMinLength)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Choose User Type")]
        public int UserTypeId { get; set; }

        public IEnumerable<UserTypeRegisterFormModel> UserTypes { get; set; }
    }
}
