using ProjectHub.Models.Project;
using ProjectHub.Models.User;
using System.Collections.Generic;

namespace ProjectHub.Models.Designer
{
    public class DesignerProfileViewModel
    {
        public AppUserProfileViewModel User { get; set; }

        public string Discipline { get; set; }

        public int? WorkExperience { get; set; }

        public ICollection<ProjectGeneralViewModel> Projects { get; set; }
    }
}
