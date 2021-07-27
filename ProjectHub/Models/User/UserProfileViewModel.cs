using ProjectHub.Data.Models;

namespace ProjectHub.Models.User
{
    public class UserProfileViewModel
    {
        public bool IsLoggedUser { get; set; }
        public AppUserProfileViewModel User { get; set; }
        public Discipline Discipline { get; set; }
        public int? WorkExperience { get; set; }        
    }
}
