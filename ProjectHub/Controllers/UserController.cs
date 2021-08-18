using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectHub.Areas.Admin;
using ProjectHub.Data.Models;
using ProjectHub.Models.Discussion;
using ProjectHub.Models.Projects;
using ProjectHub.Models.Review;
using ProjectHub.Models.User;
using ProjectHub.Services.Files;
using ProjectHub.Services.User;
using ProjectHub.Services.UserKinds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace ProjectHub.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserService userService;
        private readonly IUserKindService userKindService;
        private readonly IFilesService filesService;

        public UserController(UserManager<ApplicationUser> userManager,
                              IUserService userService,
                              IUserKindService userKindService,
                              IFilesService filesService)
        {
            this.userManager = userManager;
            this.userService = userService;
            this.userKindService = userKindService;
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

            var loggedUserId = int.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier).Value);

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
            if (!this.userService.IsUserExists(userId) 
                || !this.userKindService.IsValid(userKind))
            {
                return BadRequest();
            }

            var userModel = this.userService.GetUserEditProfileViewModel(userId, userKind);

            var loggedUserId = int.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (loggedUserId == userId)
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
            if (!this.userService.IsUserExists(id)
                || !this.userKindService.IsValid(userKind))
            {
                return BadRequest();
            }

            var projects = this.userService.GetUserProjects(id, userKind).ToList();

            var loggedUserId = int.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var userModel = new UserProjectsListingViewModel
            {
                Id = id,
                UserKind = userKind,
                IsLoggedUser = loggedUserId.Equals(id)
            };

            var tuple = new Tuple<List<ProjectListingViewModel>, UserProjectsListingViewModel>(projects, userModel);

            return PartialView("UserProjectsPartial", tuple);
        }

        [HttpGet]
        public IActionResult Reviews(int id, string userKind)
        {
            if (!this.userService.IsUserExists(id))
            {
                return BadRequest();
            }

            var userReviews = this.userService.GetUserReviews(id).ToList();

            var loggedUserId = int.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            UserReviewsListViewModel user = new UserReviewsListViewModel
            {

                LoggedUserId = loggedUserId,
                Id = id,
                UserKind = userKind,
                IsLoggedUser = loggedUserId.Equals(id),
                AlreadyIsReviewedByTheLoggedUser = 
                    this.userService.ReviewAlreadyExists(id, loggedUserId)
            };

            var tuple = new Tuple<List<ReviewListingViewModel>, UserReviewsListViewModel>(userReviews, user);

            return PartialView("UserReviewsPartial", tuple);
        }       

        public string Recommendations(int authorId, int recipientId)
            => this.userService.GetUserRecommendationsCount(authorId, recipientId);

        public string Disapprovals(int authorId, int recipientId)
            => this.userService.GetUserDisapprovalsCount(authorId, recipientId);

        public IActionResult Discussions(int id, string userKind)
            => PartialView("~/Views/User/UserDiscussionsPartial.cshtml", 
                new Tuple<List<DiscussionViewModel>,string>(this.userService.GetUserDiscussions(id).ToList(),userKind));
    }
}
