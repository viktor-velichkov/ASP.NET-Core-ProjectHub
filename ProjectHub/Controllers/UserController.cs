using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectHub.Data;
using ProjectHub.Data.Models;
using ProjectHub.Models.Project;
using ProjectHub.Models.User;
using ProjectHub.Services.User;

namespace ProjectHub.Controllers
{
    public class UserController : Controller
    {
        private readonly ProjectHubDbContext data;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;
        private readonly IUserService userService;

        public UserController(ProjectHubDbContext data,
                              UserManager<ApplicationUser> userManager,
                              IMapper mapper,
                              IUserService userService)
        {
            this.data = data;
            this.userManager = userManager;
            this.mapper = mapper;
            this.userService = userService;
        }

        public IActionResult Profile()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            var userId = this.Request.RouteValues.ContainsKey("id") ?
                         int.Parse(this.Request.RouteValues["id"].ToString()) :
                         int.Parse(this.userManager.GetUserId(this.User));

            var userDb = this.data
                           .Users
                           .Include(u => u.UserKind)
                           .FirstOrDefault(u => u.Id.Equals(userId));

            if (userDb == null)
            {
                return NotFound();
            }

            var userTypeName = userDb.UserKind.Name;

            var userTypeEntity = this.userService.GetUserKindEntityByUserId(userTypeName, userId);

            var userViewModel = this.mapper.Map<object, UserProfileViewModel>(userTypeEntity);

            if (int.Parse(this.userManager.GetUserId(this.User)) == userId)
            {
                userViewModel.IsLoggedUser = true;
            }

            return View(userViewModel);
        }


        public IActionResult EditUserProfile(int userId, string userKind)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            var userDb = this.userService.GetUserKindEntityByUserId(userKind, userId);

            var currentUser = this.mapper.Map<object, UserEditProfileViewModel>(userDb);

            if (int.Parse(this.userManager.GetUserId(this.User)) == userId)
            {
                currentUser.IsLoggedUser = true;
            }

            return View(currentUser);
        }

        [HttpPost]
        public IActionResult EditUserProfile(UserEditProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            this.userService.EditUser(model);

            return RedirectToAction("Profile", "User", new { id = model.User.Id });
        }

        public IActionResult UserProjectsPartial(int id, string userKind)
        {
            var projects = this.userService.GetUserProjects(id, userKind).ToList();

            Tuple<List<ProjectGeneralViewModel>, string> tuple = new Tuple<List<ProjectGeneralViewModel>, string>(projects, userKind);

            return PartialView(tuple);
        }

    }
}
