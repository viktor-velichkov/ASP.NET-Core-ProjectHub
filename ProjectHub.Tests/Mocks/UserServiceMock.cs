using Moq;
using ProjectHub.Data.Models;
using ProjectHub.Models.User;
using ProjectHub.Services.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHub.Tests.Mocks
{
    public static class UserServiceMock
    {
        public static IUserService Instance
        {
            get
            {
                var userService = new Mock<IUserService>();

                userService
                    .Setup(us => us.GetUserById(1))
                    .Returns(new ApplicationUser { Id = 1, UserKind=new UserKind { Name = "UserKind" } });

                userService
                    .Setup(us => us.IsInRole(1, "Administrator"))
                    .Returns(false);

                userService
                    .Setup(us => us.GetUserProfileViewModel(1, "UserKind"))
                    .Returns(new UserProfileViewModel());

                return userService.Object;
            }
        }
    }
}
