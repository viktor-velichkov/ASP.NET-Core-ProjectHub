using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectHub.Data.Models;
using ProjectHub.Models.Review;
using ProjectHub.Services.Reviews;
using ProjectHub.Services.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace ProjectHub.Controllers
{
    [Authorize]
    public class ReviewsController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;
        private readonly IUserService userService;
        private readonly IReviewService reviewService;


        public ReviewsController(UserManager<ApplicationUser> userManager,
                                 IMapper mapper,
                                 IUserService userService,
                                 IReviewService reviewService)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.userService = userService;
            this.reviewService = reviewService;

        }

        public IActionResult Add(int recipientId, string userKind)
        {
            if (!this.userService.IsUserExists(recipientId))
            {
                return NotFound();
            }

            var loggedUserId = int.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (recipientId.Equals(loggedUserId))
            {
                return BadRequest();
            }

            var viewModel = new ReviewAddViewModel
            {
                AuthorId = loggedUserId,
                RecipientId = recipientId,
                Date = DateTime.UtcNow,
                RecipientUserKind = userKind
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Add(int authorId, int recipientId, string content)
        {
            if (!this.userService.IsUserExists(authorId)
                || !this.userService.IsUserExists(recipientId))
            {
                return NotFound();
            }

            if (recipientId.Equals(authorId))
            {
                return BadRequest();
            }

            this.reviewService.AddReview(authorId, recipientId, content);

            var recipientReviews = this.reviewService.GetRecipientReviews(recipientId);

            var reviews = recipientReviews.Select(r => this.mapper.Map<Review, ReviewListingViewModel>(r)).ToList();

            var loggedUserId = int.Parse(this.userManager.GetUserId(this.User));

            var viewModel = new Tuple<List<ReviewListingViewModel>, int>(reviews, loggedUserId);

            return View("~/Views/Reviews/List.cshtml", viewModel);
        }

        [HttpPost]
        public IActionResult Edit(int recipientId, int authorId, string content)
        {
            if (!this.userService.IsUserExists(authorId)
                || !this.userService.IsUserExists(recipientId))
            {
                return NotFound();
            }

            if (recipientId.Equals(authorId))
            {
                return BadRequest();
            }

            this.reviewService.EditReview(authorId, recipientId, content);

            var userKind = this.GetUserKindByUserId(recipientId);

            return Redirect($"/User/Reviews?id={recipientId}&userKind={userKind}");
        }

        [HttpPost]
        public IActionResult Remove(int recipientId, int authorId)
        {
            if (!this.reviewService.ReviewExists(authorId,recipientId))
            {
                return NotFound();
            }

            this.reviewService.RemoveReview(recipientId, authorId);

            var userKind = this.GetUserKindByUserId(recipientId);

            return Redirect($"/User/Reviews?id={recipientId}&userKind={userKind}");
        }

        private string GetUserKindByUserId(int id)
        {
            var query = this.userManager.Users.Include(u => u.UserKind);

            var user = query.FirstOrDefault(u => u.Id.Equals(id));

            return user.UserKind.Name;
        }
    }
}
