using ProjectHub.Models.Project;
using ProjectHub.Models.User;
using System.Collections.Generic;

namespace ProjectHub.Services.User
{
    public interface IUserService
    {
        object GetUserKindEntityByUserId(string userKind, int userId);

        public void EditUser(UserEditProfileViewModel model);

        public IEnumerable<ProjectGeneralViewModel> GetUserProjects(int id, string userKind);
    }
}
