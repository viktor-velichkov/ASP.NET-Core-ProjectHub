using Microsoft.AspNetCore.Identity;
using Moq;
using ProjectHub.Data.Models;
using System;
using System.Linq;
using System.Security.Claims;

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

                var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim("Id", "3") }));

                userManager
                    .Setup(um => um.GetUserId(user))
                    .Returns(user.Claims.FirstOrDefault(c=>c.Type=="Id").Value);

                return userManager.Object;
            }
        }

    }
}
