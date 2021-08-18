using Microsoft.AspNetCore.Identity;
using Moq;
using ProjectHub.Data.Models;

namespace ProjectHub.Tests.Mocks
{
    public static class UserManagerMock
    {
        public static UserManager<ApplicationUser> Instance
        {
            get
            {
                var store = new Mock<IUserStore<ApplicationUser>>();
                var userManager = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);

                return userManager.Object;
            }
        }

    }
}
