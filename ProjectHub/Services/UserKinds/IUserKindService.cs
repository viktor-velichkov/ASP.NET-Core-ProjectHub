using System.Collections.Generic;
using ProjectHub.Data.Models;
using ProjectHub.Models.User;

namespace ProjectHub.Services.UserKinds
{
    public interface IUserKindService
    {
        public IEnumerable<UserKindRegisterFormModel> GetAllUserKinds();
    }
}
