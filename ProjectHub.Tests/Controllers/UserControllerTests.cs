using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectHub.Controllers;
using ProjectHub.Models.User;
using ProjectHub.Tests.Mocks;
using System;
using System.Security.Claims;
using Xunit;

namespace ProjectHub.Tests.Controllers
{
    public class UserControllerTests
    {
        [Fact]
        public void ProfileMethodShouldReturnViewWithCorrectTypeAndModelType()
        {
            //Arrange
            var userService = UserServiceMock.Instance;
            var userManager = UserManagerMock.Instance;

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim("Id","3")}));

           //Type = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"



            var userController = new UserController(userManager, userService, null);

            userController.ControllerContext = new ControllerContext();
            userController.ControllerContext.HttpContext = new DefaultHttpContext { User = user };

            //Act
            var result = userController.Profile(1);


            //Assert
            Assert.NotNull(result);
            var resultType = Assert.IsType<ViewResult>(result);
            Assert.IsType<Tuple<UserProfileViewModel, int>>(resultType.Model);
        }
    }
}
