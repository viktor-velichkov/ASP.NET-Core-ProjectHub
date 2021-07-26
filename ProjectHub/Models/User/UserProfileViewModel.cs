using ProjectHub.Data.Models;
using ProjectHub.Models.Project;
using System.Collections.Generic;

namespace ProjectHub.Models.User
{
    public class UserProfileViewModel
    {
        public bool IsLoggedUser { get; set; }
        public AppUserProfileViewModel User { get; set; }
        public Discipline Discipline { get; set; }

        public int? WorkExperience { get; set; }

        public List<ProjectGeneralViewModel> Projects { get; set; }

        public List<Activity> Activities { get; set; }
    }
}
