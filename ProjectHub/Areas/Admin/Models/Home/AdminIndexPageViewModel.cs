using ProjectHub.Areas.Admin.Models.Projects;
using ProjectHub.Areas.Admin.Models.User;

namespace ProjectHub.Areas.Admin.Models.Home
{
    public class AdminIndexPageViewModel
    {
        public UserFormViewModel User { get; set; }
        public ProjectFormViewModel Project { get; set; }
    }
}
