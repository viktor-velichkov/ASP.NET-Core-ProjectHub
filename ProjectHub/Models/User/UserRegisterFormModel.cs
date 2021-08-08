using ProjectHub.Data;
using ProjectHub.Data.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectHub.Models.User
{
    public class UserRegisterFormModel
    {
        public UserRegisterFormModel()
        {
            this.UserKinds = new List<UserKindRegisterFormModel>();
        }

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

        [Display(Name = "Choose User Kind")]
        public int UserKindId { get; set; }

        [Display(Name = "Choose Discipline")]
        public int DisciplineId { get; set; }

        public IEnumerable<UserKindRegisterFormModel> UserKinds { get; set; }
        public IEnumerable<Discipline> Disciplines { get; set; }
    }
}
