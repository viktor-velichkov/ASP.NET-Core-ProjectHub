using Moq;
using ProjectHub.Data.Models;
using ProjectHub.Models.Projects;
using ProjectHub.Models.Review;
using ProjectHub.Models.User;
using ProjectHub.Services.User;
using System.Linq;

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
                    .Setup(us => us.GetUserById(-1))
                    .Returns<ApplicationUser>(null);

                userService
                    .Setup(us => us.GetUserById(1))
                    .Returns(new ApplicationUser { Id = 1, UserKind = new UserKind { Name = "UserKind" } });

                userService
                    .Setup(us => us.GetUserById(3))
                    .Returns(new ApplicationUser { Id = 3, UserKind = new UserKind { Name = "Designer" } });

                userService
                    .Setup(us => us.GetUserById(4))
                    .Returns(new ApplicationUser { Id = 4, UserKind = new UserKind { Name = "Investor" } });

                userService
                    .Setup(us => us.GetUserById(2))
                    .Returns(new ApplicationUser { Id = 2, UserKind = new UserKind { Name = "Administrator" } });

                userService
                    .Setup(us => us.IsUserExists(-1))
                    .Returns(false);

                userService
                    .Setup(us => us.IsUserExists(1))
                    .Returns(true);

                userService
                    .Setup(us => us.IsInRole(1, "UserKind"))
                    .Returns(false);

                userService
                    .Setup(us => us.IsInRole(2, "Administrator"))
                    .Returns(true);

                userService
                    .Setup(us => us.GetUserProfileViewModel(1, "UserKind"))
                    .Returns(new UserProfileViewModel());

                userService
                    .Setup(us => us.GetUserEditProfileViewModel(1, "Investor"))
                    .Returns(new UserEditProfileViewModel { });

                userService
                    .Setup(us => us.GetUserProjects(1, "Investor"))
                    .Returns(Enumerable.Range(0, 10).Select(x => new ProjectListingViewModel { }));

                userService
                    .Setup(us => us.GetUserReviews(1))
                    .Returns(Enumerable.Range(0, 10).Select(x => new ReviewListingViewModel { }));

                return userService.Object;
            }
        }
    }
}
