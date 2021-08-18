using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProjectHub.Controllers;
using ProjectHub.Models.Projects;
using ProjectHub.Models.Review;
using ProjectHub.Models.User;
using ProjectHub.Services.Files;
using ProjectHub.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using Xunit;

namespace ProjectHub.Tests.Controllers
{
    public class UserControllerTests
    {

        //Profile Tests
        [Fact]
        public void ProfileMethodShouldReturnViewResultWithUserProfileViewModel()
        {
            //Arrange
            var userController = this.Create();

            SetUserToController(userController);

            //Act
            var result = userController.Profile(1);


            //Assert
            Assert.NotNull(result);
            var resultType = Assert.IsType<ViewResult>(result);
            Assert.IsType<Tuple<UserProfileViewModel, int>>(resultType.Model);
        }

        [Fact]
        public void ProfileMethodShouldReturnNotFoundResponseIfUserIsNotValid()
        {
            //Arrange
            var userController = this.Create();

            //Act
            var result = userController.Profile(-1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void ProfileMethodShouldReturnNotFoundResponseIfUserIsAdministrator()
        {
            //Arrange
            var userController = this.Create();

            //Act
            var result = userController.Profile(2);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }


        //EditUserProfile Tests

        [Fact]
        public void EditUserProfileMethodShouldReturnBadRequestResultIfUserIdIsInvalid()
        {
            //Assert
            var userController = this.Create();

            //Act            
            var result = userController.EditUserProfile(-1, "Investor");

            //Assert
            Assert.NotNull(result);
            var resultType = Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void EditUserProfileMethodShouldReturnBadRequestResultIfUserKindIsInvalid()
        {
            //Assert
            var userController = this.Create();

            //Act            
            var result = userController.EditUserProfile(1, "UserKind");

            //Assert
            Assert.NotNull(result);
            var resultType = Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void EditUserProfileMethodShouldHaveAuthorizeAttribute()
        {
            //Assert
            var userController = this.Create();

            //Act
            var hasAuthorizeAttr = userController.GetType()
                                                 .GetMethods()
                                                 .Where(m => m.Name.Equals("EditUserProfile"))
                                                 .Select(m => m.GetCustomAttributes(true))
                                                 .All(attr => attr.Any(x => x.GetType().Name.Equals(nameof(AuthorizeAttribute))));

            //Assert
            Assert.True(hasAuthorizeAttr);
        }

        [Fact]
        public void EditUserProfileMethodShouldReturnViewResultWithUserEditProfileViewModel()
        {
            //Arrange
            var userController = this.Create();
            SetUserToController(userController);

            //Act
            var result = userController.EditUserProfile(1, "Investor");

            //Assert
            Assert.NotNull(result);
            var resultType = Assert.IsType<ViewResult>(result);
            Assert.IsType<UserEditProfileViewModel>(resultType.Model);
        }

        [Theory]
        [InlineData("User", "Profile", "id", 1)]
        public void EditUserProfileMethodShouldReturnRedirectToActionResultWithGivenRouteData(
            string expectedController,
            string expectedAction,
            string routeValueKey,
            int id)
        {
            //Arrange
            var userController = this.Create();

            //Act
            var formFile = new FormFile(new MemoryStream(), 0, 0, null, null);
            var result = userController.EditUserProfile(
                new UserEditProfileViewModel { User = new AppUserEditProfileViewModel { Id = id, ImageUpload = formFile } });

            //Assert
            Assert.NotNull(result);
            var resultType = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(expectedController, resultType.ControllerName);
            Assert.Equal(expectedAction, resultType.ActionName);
            Assert.True(resultType.RouteValues.ContainsKey(routeValueKey));
            Assert.Equal(1, resultType.RouteValues[routeValueKey]);
        }

        [Fact]
        public void EditUserProfileMethodShouldReturnViewResultIfUploadedImageIsBiggerThanTwoMegaBytes()
        {
            //Assert
            var userController = this.Create();

            //Act
            var formFile = new FormFile(new MemoryStream(), 0, 2100000, null, null);
            var result = userController.EditUserProfile(
                new UserEditProfileViewModel { User = new AppUserEditProfileViewModel { ImageUpload = formFile } });

            //Assert
            Assert.NotNull(result);
            var resultType = Assert.IsType<ViewResult>(result);
            Assert.IsType<UserEditProfileViewModel>(resultType.Model);
            Assert.False(resultType.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void EditUserProfileMethodShouldHaveHttpPostAttribure()
        {
            //Assert
            var userController = this.Create();

            //Act
            var method = userController.GetType()
                    .GetMethod("EditUserProfile", new Type[] { typeof(UserEditProfileViewModel) });

            var hasHttpPostAttr = method.GetCustomAttributes(true)
                                        .Any(attr => attr.GetType()
                                        .Name
                                        .Equals(nameof(HttpPostAttribute)));
            //Assert
            Assert.True(hasHttpPostAttr);
        }


        //Projects Tests

        [Fact]
        public void ProjectsMethodShouldReturnBadRequestResultIfUserIdIsInvalid()
        {
            //Assert
            var userController = this.Create();

            //Act            
            var result = userController.Projects(-1, "Investor");

            //Assert
            Assert.NotNull(result);
            var resultType = Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void ProjectsMethodShouldReturnBadRequestResultIfUserKindIsInvalid()
        {
            //Assert
            var userController = this.Create();

            //Act            
            var result = userController.Projects(1, "UserKind");

            //Assert
            Assert.NotNull(result);
            var resultType = Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void ProjectsMethodShouldReturnPartialViewResultWithTupleViewModel()
        {
            //Arrange
            var userController = this.Create();
            SetUserToController(userController);

            //Act
            var result = userController.Projects(1, "Investor");

            //Assert
            Assert.NotNull(result);
            var resultType = Assert.IsType<PartialViewResult>(result);
            Assert.Equal("UserProjectsPartial", resultType.ViewName);
            Assert.IsType<Tuple<List<ProjectListingViewModel>, UserProjectsListingViewModel>>(resultType.Model);
        }


        //Reviews Tests

        [Fact]
        public void ReviewsMethodShouldReturnBadRequestResultIfUserIdIsInvalid()
        {
            //Assert
            var userController = this.Create();

            //Act            
            var result = userController.Reviews(-1, "Investor");

            //Assert
            Assert.NotNull(result);
            var resultType = Assert.IsType<BadRequestResult>(result);
        }        

        [Fact]
        public void ReviewsMethodShouldReturnPartialViewResultWithTupleViewModel()
        {
            //Arrange
            var userController = this.Create();
            SetUserToController(userController);

            //Act
            var result = userController.Reviews(1, "Investor");

            //Assert
            Assert.NotNull(result);
            var resultType = Assert.IsType<PartialViewResult>(result);
            Assert.Equal("UserReviewsPartial", resultType.ViewName);
            Assert.IsType<Tuple<List<ReviewListingViewModel>, UserReviewsListViewModel>>(resultType.Model);
        }



        private UserController Create()
        {
            var userService = UserServiceMock.Instance;
            var userManager = UserManagerMock.Instance;
            var filesService = Mock.Of<FilesService>();
            var userKindService = UserKindServiceMock.Instance;

            return new UserController(userManager, userService, userKindService, filesService);
        }

        private void SetUserToController(UserController controller)
        {
            var user = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new Claim[] { new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", "1") }));

            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext { User = user };
        }
    }
}
