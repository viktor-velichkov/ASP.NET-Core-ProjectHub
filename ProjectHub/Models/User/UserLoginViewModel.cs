using System.ComponentModel.DataAnnotations;

namespace ProjectHub.Models.User
{
    public class UserLoginViewModel
    {

        public string UserName => this.Email; 

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
