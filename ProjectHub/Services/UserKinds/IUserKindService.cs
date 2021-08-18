using System.Collections.Generic;
using ProjectHub.Models.User;

namespace ProjectHub.Services.UserKinds
{
    public interface IUserKindService
    {
        public bool IsValid(string userKind);
        public IEnumerable<UserKindRegisterFormModel> GetAllUserKinds();
    }
}
