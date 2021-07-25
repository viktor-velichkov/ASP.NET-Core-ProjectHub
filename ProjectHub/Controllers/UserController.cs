using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectHub.Data;
using ProjectHub.Data.Models;
using ProjectHub.Models.Contractor;
using ProjectHub.Models.Designer;
using ProjectHub.Models.Investor;
using ProjectHub.Models.Manager;
using ProjectHub.Models.User;

namespace ProjectHub.Controllers
{
    public class UserController : Controller
    {
        private readonly ProjectHubDbContext data;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public UserController(ProjectHubDbContext data,
                              UserManager<ApplicationUser> userManager,
                              IMapper mapper)
        {
            this.data = data;
            this.userManager = userManager;
            this.mapper = mapper;
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

            var userTypeEntity = GetUserKindEntityByUserId(userTypeName, userId);

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

            var userDb = GetUserKindEntityByUserId(userKind, userId);

            var currentUser = this.mapper.Map<object, UserEditProfileViewModel>(userDb);

            if (int.Parse(this.userManager.GetUserId(this.User)) == userId)
            {
                currentUser.IsLoggedUser = true;
            }

            return View(currentUser);
        }

        [HttpPost]
        public async Task<IActionResult> EditUserProfile(UserEditProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userDbToUpdate = GetUserKindEntityByUserId(model.UserKindName, model.User.Id);

            
            return RedirectToAction("Profile", "User");
        }

        private object GetUserKindEntityByUserId(string userKind, int userId)
        {
            object result = null;

            switch (userKind)
            {
                case "Investor":
                    result = this.data
                                 .Investors
                                 .Include(i => i.User)
                                 .Include(i => i.User.UserKind)
                                 .FirstOrDefault(i => i.UserId.Equals(userId));
                    break;
                case "Manager":
                    result = this.data
                                 .Managers
                                 .Include(m => m.User)
                                 .Include(m => m.User.UserKind)
                                 .FirstOrDefault(i => i.UserId.Equals(userId));
                    break;
                case "Designer":
                    result = this.data
                                 .Designers
                                 .Include(d => d.User)
                                 .Include(d => d.User.UserKind)
                                 .FirstOrDefault(i => i.UserId.Equals(userId));
                    break;
                case "Contractor":
                    result = this.data
                                 .Contractors
                                 .Include(c => c.User)
                                 .Include(c => c.User.UserKind)
                                 .FirstOrDefault(i => i.UserId.Equals(userId));
                    break;
                default:
                    break;
            }

            return result;
        }

        private UserProfileViewModel MapToViewModel(string userTypeName, object obj)
        {
            UserProfileViewModel result = null;

            switch (userTypeName)
            {
                case "Investor":
                    result = this.mapper.Map<Investor, UserProfileViewModel>(obj as Investor);
                    break;
                case "Manager":
                    result = this.mapper.Map<Manager, UserProfileViewModel>(obj as Manager);
                    break;
                case "Designer":
                    result = this.mapper.Map<Designer, UserProfileViewModel>(obj as Designer);
                    break;
                case "Constractor":
                    result = this.mapper.Map<Contractor, UserProfileViewModel>(obj as Contractor);
                    break;
                default:
                    break;
            }

            return result;
        }




    }
}
