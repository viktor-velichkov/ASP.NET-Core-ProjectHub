using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProjectHub.Controllers;
using ProjectHub.Data.Models;
using ProjectHub.Models.Projects;
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
    public class ProjectsControllerTests
    {
        [Fact]
        public void ProjectControllerShouldHaveAuthorizeAttribute()
        {
            //Arrange
            var projectController = Create();

            //Act
            bool hasAuthorizeAttr = projectController.GetType()
                                                     .GetCustomAttributes(false)
                                                     .Any(attr => attr.GetType().Name.Equals(nameof(AuthorizeAttribute)));

            //Assert
            Assert.True(hasAuthorizeAttr);
        }

        [Fact]
        public void AllMethodShouldReturnViewResultWithAllProjectsViewModelType()
        {
            //Assert
            var projectsController = Create();

            //Act
            var result = projectsController.All();

            //Assert
            Assert.NotNull(result);
            var resultType = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<AllProjectsViewModel>(resultType.Model);
            Assert.Equal(10, model.Projects.Count());
            Assert.Equal(6, model.Cities.Count());
        }

        [Fact]
        public void CityMethodShouldReturnPartialViewWithListOfProjectCardViewModels()
        {
            //Arrange
            var projectController = Create();

            //Act
            var result = projectController.City("Sofia");

            //Assert
            Assert.NotNull(result);
            var resultType = Assert.IsType<PartialViewResult>(result);
            var model = Assert.IsType<List<ProjectCardViewModel>>(resultType.Model);
            Assert.Equal("~/Views/Projects/AllByCityPartial.cshtml", resultType.ViewName);
            Assert.Equal(5, model.Select(m => m.City.Equals("Sofia")).Count());
        }

        //Add Tests

        [Fact]
        public void AddMethodShouldReturnViewResult()
        {
            //Arrange
            var projectController = Create();

            //Act
            var result = projectController.Add();

            //Assert
            Assert.NotNull(result);
            var resultType = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void AddMethodShouldReturnViewResultIfUploadedImageIsBiggerThanTwoMegaBytes()
        {
            //Assert
            var projectController = this.Create();

            //Ac
            var formFile = new FormFile(new MemoryStream(), 0, 2100000, null, null);
            var result = projectController.Add(new ProjectAddViewModel { ImageUpload = formFile });

            //Assert
            Assert.NotNull(result);
            var resultType = Assert.IsType<ViewResult>(result);
            Assert.IsType<ProjectAddViewModel>(resultType.Model);
            Assert.False(resultType.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void AddMethodShouldReturnRedirectionViewResultIfGivenModelIsValid()
        {
            //Assert
            var projectController = this.Create();
            SetUserToController(projectController, "1");

            //Ac
            var formFile = new FormFile(new MemoryStream(), 0, 0, null, null);
            var result = projectController.Add(new ProjectAddViewModel { ImageUpload = formFile });

            //Assert
            Assert.NotNull(result);
            var resultType = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("User", resultType.ControllerName);
            Assert.Equal("Profile", resultType.ActionName);
            Assert.True(resultType.RouteValues.ContainsKey("id"));
            Assert.Equal(1, resultType.RouteValues["id"]);
        }

        [Fact]
        public void AddMethodAttributeShouldHaveHttpPostAttribute()
        {
            //Arrange
            var projectController = Create();

            //Act
            bool hasHttpPostAttribute = projectController.GetType()
                                                         .GetMethod("Add", 0, new Type[] { typeof(ProjectAddViewModel) })
                                                         .GetCustomAttributes(true)
                                                         .Any(attr => attr.GetType().Name
                                                                                    .Equals(nameof(HttpPostAttribute)));

            //Assert            
            Assert.True(hasHttpPostAttribute);
        }

        //List Tests

        [Fact]
        public void ListMethodShouldReturnViewResult()
        {
            //Arrange
            var projectController = Create();

            //Act
            var result = projectController.List();

            //Assert
            Assert.NotNull(result);
            var resultType = Assert.IsType<ViewResult>(result);
        }

        //Details Tests

        [Fact]
        public void DetailsMethodShouldReturnViewResultWithTuple()
        {
            //Arrange
            var projectController = Create();
            SetUserToController(projectController, "3");

            //Act
            var result = projectController.Details(1);

            //Assert
            Assert.NotNull(result);
            var resultType = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Tuple<ProjectDetailsViewModel, List<Discipline>>>(resultType.Model);
            Assert.Single(model.Item2);
        }

        [Fact]
        public void DetailsMethodShouldReturnNotFoundResultIfProjectDoesNotExist()
        {
            //Arrange
            var projectsController = Create();

            //Act
            var result = projectsController.Details(-1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
            Assert.True(projectsController.ModelState.IsValid);
        }

        //Offers Tests

        [Fact]
        public void OffersMethodShouldReturnViewResultWithProjectOffersListViewModel()
        {
            //Arrange
            var projectsController = Create();
            SetUserToController(projectsController, "3");

            //Act
            var result = projectsController.Offers(1);

            //Assert
            Assert.NotNull(result);
            var resultType = Assert.IsType<ViewResult>(result);
            Assert.True(resultType.ViewData.ModelState.IsValid);
            var model = Assert.IsType<ProjectOffersListViewModel>(resultType.Model);

        }

        [Fact]
        public void OffersMethodShouldReturnRedirectToActionResultIfLoggedUserIsNotTheProjectInvestor()
        {
            //Arrange
            var projectsController = Create();
            SetUserToController(projectsController, "1");

            //Act
            var result = projectsController.Offers(1);

            //Assert
            Assert.NotNull(result);
            var resultType = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("User", resultType.ControllerName);
            Assert.Equal("Profile", resultType.ActionName);
            Assert.True(resultType.RouteValues.ContainsKey("id"));
            Assert.Equal(1, resultType.RouteValues["id"]);
            Assert.False(projectsController.ModelState.IsValid);
        }

        [Fact]
        public void OffersMethodShouldReturnNotFoundResultIfProjectDoesNotExist()
        {
            //Arrange
            var projectController = Create();

            //Act
            var result = projectController.Offers(-1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }

        //Remove Tests

        [Fact]
        public void RemoveMethodShouldReturnNotFoundResultIfProjectDoesNotExist()
        {
            //Arrange
            var projectController = Create();

            //Act
            var result = projectController.Remove(-1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void RemoveMethodShouldReturnUnauthorizedResultIfLoggedUserIsNotInvestor()
        {
            //Arrange
            var projectController = Create();
            SetUserToController(projectController, "3");

            //Act
            var result = projectController.Remove(1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<UnauthorizedResult>(result);
        }

        [Fact]
        public void RemoveMethodShouldReturnUnauthorizedResultIfLoggedUserIsInvestorButHeIsNotTheOnwerOfTheProject()
        {
            //Arrange
            var projectController = Create();
            SetUserToController(projectController, "4");

            //Act
            var result = projectController.Remove(1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<UnauthorizedResult>(result);
        }

        [Fact]
        public void RemoveMethodShouldReturnRedirectToActionResultIfGivenDataIsValid()
        {
            //Arrange
            var projectController = Create();
            SetUserToController(projectController, "4");

            //Act
            var result = projectController.Remove(2);

            //Assert
            Assert.NotNull(result);
            var resultType = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("User",resultType.ControllerName);
            Assert.Equal("Profile",resultType.ActionName);
            Assert.True(resultType.RouteValues.ContainsKey("id"));
            Assert.Equal(4,resultType.RouteValues["id"]);
            Assert.True(projectController.ModelState.IsValid);
        }



        private ProjectsController Create()
        => new ProjectsController(ProjectServiceMock.Instance,
                                  OfferServiceMock.Instance,
                                  UserServiceMock.Instance,
                                  Mock.Of<IFilesService>(),
                                  DisciplineServiceMock.Instance);

        private void SetUserToController(ProjectsController controller, string id)
        {
            var user = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new Claim[] { new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", id) }));

            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext { User = user };
        }
    }
}
