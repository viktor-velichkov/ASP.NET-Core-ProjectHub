using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectHub.Data;
using ProjectHub.Data.Models;
using ProjectHub.Models.Discussion;
using ProjectHub.Models.Project;
using ProjectHub.Models.Review;
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

            var userDb = this.userService.GetUserById(userId);
                        
            if (userDb == null)
            {
                return NotFound();
            }

            var userTypeName = userDb.UserKind.Name;

            var userTypeEntity = this.userService.GetUserKindEntityByUserId(userTypeName, userId);            

            var userViewModel = this.mapper.Map<object, UserProfileViewModel>(userTypeEntity);            

            var loggedUserId = int.Parse(this.userManager.GetUserId(this.User));

            if (loggedUserId == userId)
            {
                userViewModel.IsLoggedUser = true;
            }

            Tuple<UserProfileViewModel, int> tuple = new Tuple<UserProfileViewModel, int>(userViewModel, loggedUserId);

            return View(tuple);
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
            var uploadedImage = model.User.ImageUpload;

            if (uploadedImage != null && uploadedImage.Length > 2097152)
            {
                ModelState.AddModelError(nameof(uploadedImage), "The file is too large");

            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (uploadedImage != null)
            {
                model.User.Image = this.userService.ProcessUploadedFile(uploadedImage);
            }
            else
            {
                model.User.Image = this.userService.GetUserImage(model.User.Id);
            }

            this.userService.EditUser(model);

            return RedirectToAction("Profile", "User", new { id = model.User.Id });
        }


        public IActionResult Projects(int id, string userKind)
        {
            var projects = this.userService.GetUserProjects(id, userKind).ToList();

            Tuple<List<ProjectListingViewModel>, string> tuple = new Tuple<List<ProjectListingViewModel>, string>(projects, userKind);

            return PartialView("UserProjectsPartial", tuple);
        }

        [HttpGet]
        public IActionResult Reviews(int id, string userKind)
        {
            var userReviews = this.userService.GetUserReviews(id).ToList();

            Tuple<List<ReviewViewModel>, string> tuple =
                new Tuple<List<ReviewViewModel>, string>(userReviews, userKind);

            return PartialView("UserReviewsPartial", tuple);
        }

        public IActionResult Discussions(int id, string userKind)
        {
            var userDiscussions = this.userService.GetUserDiscussions(id).ToList();

            Tuple<List<DiscussionViewModel>, string> tuple =
                new Tuple<List<DiscussionViewModel>, string>(userDiscussions, userKind);

            return PartialView("UserDiscussionsPartial", tuple);
        }

        public string Recommendations(int authorId, int recipientId)
            => this.userService.GetUserRecommendationsCount(authorId, recipientId);

        public string Disapprovals(int authorId, int recipientId)
            => this.userService.GetUserDisapprovalsCount(authorId, recipientId);
    }
}
