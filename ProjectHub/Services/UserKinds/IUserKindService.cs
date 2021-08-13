using System.Collections.Generic;
using ProjectHub.Models.User;

namespace ProjectHub.Services.UserKinds
{
    public interface IUserKindService
    {
        public IEnumerable<UserKindRegisterFormModel> GetAllUserKinds();
    }
}
