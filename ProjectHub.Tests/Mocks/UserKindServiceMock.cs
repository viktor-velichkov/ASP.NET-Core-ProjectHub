using Moq;
using ProjectHub.Services.UserKinds;

namespace ProjectHub.Tests.Mocks
{
    public static class UserKindServiceMock
    {
        public static IUserKindService Instance
        {
            get
            {
                var userKindService = new Mock<IUserKindService>();

                userKindService
                    .Setup(uks => uks.IsValid("UserKind"))
                    .Returns(false);

                userKindService
                    .Setup(uks => uks.IsValid("Investor"))
                    .Returns(true);

                return userKindService.Object;
            }
        }
    }
}
