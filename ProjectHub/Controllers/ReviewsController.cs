using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectHub.Data;
using ProjectHub.Data.Models;
using ProjectHub.Models.Review;
using System;
using System.Linq;

namespace ProjectHub.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly ProjectHubDbContext data;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public ReviewsController(ProjectHubDbContext data,
                                 UserManager<ApplicationUser> userManager,
                                 IMapper mapper)
        {
            this.data = data;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public IActionResult Add(int recipientId)
        {
            var viewModel = new ReviewAddViewModel
            {
                AuthorId = int.Parse(this.userManager.GetUserId(this.User)),
                RecipientId = recipientId,
                Date = DateTime.UtcNow
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Add(int authorId, int recipientId, string content)
        {
            this.data.Reviews.Add(new Review
            {
                AuthorId = authorId,
                RecipientId = recipientId,
                Date = DateTime.UtcNow,
                Content = content
            });

            this.data.SaveChanges();

            var reviews = this.data
                              .Reviews
                              .Include(r => r.Author)
                              .Where(r => r.RecipientId.Equals(recipientId))
                              .Select(r => this.mapper.Map<Review, ReviewListingViewModel>(r))
                              .ToList();

            return View("~/Views/Reviews/List.cshtml", reviews);
        }
    }
}
