using System.Collections.Generic;
using System.Linq;
using ProjectHub.Data;
using ProjectHub.Models.User;

namespace ProjectHub.Services.UserKinds
{
    public class UserKindService : IUserKindService
    {
        private readonly ProjectHubDbContext data;

        public UserKindService(ProjectHubDbContext data)
        {
            this.data = data;
        }

        public IEnumerable<UserKindRegisterFormModel> GetAllUserKinds()
            => this.data
                   .UserKinds
                   .Select(ut => new UserKindRegisterFormModel
                   {
                       Id = ut.Id,
                       Name = ut.Name
                   })
                   .ToList();
    }
}
