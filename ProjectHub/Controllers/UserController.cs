using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using ProjectHub.Data.Models;
using ProjectHub.Models.Discussion;
using ProjectHub.Models.Projects;
using ProjectHub.Models.Review;
using ProjectHub.Models.User;
using ProjectHub.Services.User;
using Microsoft.AspNetCore.Authorization;
using ProjectHub.Services.Files;
using ProjectHub.Areas.Admin;

namespace ProjectHub.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserService userService;
        private readonly IFilesService filesService;

        public UserController(UserManager<ApplicationUser> userManager,
                              IUserService userService,
                              IFilesService filesService)
        {
            this.userManager = userManager;
            this.userService = userService;
            this.filesService = filesService;
        }


        public IActionResult Profile(int id)
        {
            var userDb = this.userService.GetUserById(id);

            if (userDb == null || this.userService.IsInRole(id, AdminConstants.AdministratorRole))
            {
                return NotFound();
            }

            var userKindName = userDb.UserKind.Name;

            var userViewModel = this.userService.GetUserProfileViewModel(id, userKindName);

            var asd = this.userManager.GetUserId(this.User);

            var loggedUserId = int.Parse(asd);

            if (loggedUserId == id)
            {
                userViewModel.IsLoggedUser = true;
            }

            Tuple<UserProfileViewModel, int> tuple = new Tuple<UserProfileViewModel, int>(userViewModel, loggedUserId);

            return View(tuple);
        }

        [Authorize]
        public IActionResult EditUserProfile(int userId, string userKind)
        {
            var userModel = this.userService.GetUserEditProfileViewModel(userId, userKind);

            if (int.Parse(this.userManager.GetUserId(this.User)) == userId)
            {
                userModel.IsLoggedUser = true;
            }

            return View(userModel);
        }

        [Authorize]
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
                model.User.Image = this.filesService.ProcessUploadedFile(uploadedImage);
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

            var userModel = new UserProjectsListingViewModel
            {
                Id = id,
                UserKind = userKind,
                IsLoggedUser = int.Parse(this.userManager.GetUserId(this.User)).Equals(id)
            };

            var tuple = new Tuple<List<ProjectListingViewModel>, UserProjectsListingViewModel>(projects, userModel);

            return PartialView("UserProjectsPartial", tuple);
        }

        [HttpGet]
        public IActionResult Reviews(int id, string userKind)
        {
            var userReviews = this.userService.GetUserReviews(id).ToList();

            var loggedUserId = int.Parse(this.userManager.GetUserId(this.User));

            UserReviewsListViewModel user = new UserReviewsListViewModel
            {

                LoggedUserId = loggedUserId,
                Id = id,
                UserKind = userKind,
                IsLoggedUser = loggedUserId.Equals(id),
                AlreadyIsReviewedByTheLoggedUser = this.userService.CheckIfUserIsAlreadyReviewedByTheLoggedUser(id, loggedUserId)
            };

            Tuple<List<ReviewListingViewModel>, UserReviewsListViewModel> tuple =
                new Tuple<List<ReviewListingViewModel>, UserReviewsListViewModel>(userReviews, user);

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
