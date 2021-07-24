using ProjectHub.Data.Models;
using ProjectHub.Models.Message;
using ProjectHub.Models.Offer;
using ProjectHub.Models.Project;
using ProjectHub.Models.Review;
using System.Collections.Generic;

namespace ProjectHub.Models.User
{
    public class UserProfileViewModel
    {
        public bool IsLoggedUser { get; set; }
        public AppUserProfileViewModel User { get; set; }
        public string UserKindName { get; set; }

        public Discipline Discipline { get; set; }

        public int? WorkExperience { get; set; }

        public ICollection<ProjectGeneralViewModel> Projects { get; set; }

        public ICollection<Activity> Activities { get; set; }
    }
}
