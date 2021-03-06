using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectHub.Data.Models;
using ProjectHub.Models.Offer;
using ProjectHub.Models.Projects;
using ProjectHub.Services.Offers;
using ProjectHub.Services.Projects;
using ProjectHub.Services.User;
using System.Collections.Generic;
using System.Linq;

namespace ProjectHub.Controllers
{
    [Authorize]
    public class OffersController : Controller
    {
        private readonly IProjectService projectService;
        private readonly IUserService userService;
        private readonly IOfferService offerService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public OffersController(IProjectService projectService,
                                IUserService userService,
                                IOfferService offerService,
                                UserManager<ApplicationUser> userManager,
                                IMapper mapper)
        {
            this.projectService = projectService;
            this.userService = userService;
            this.offerService = offerService;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public IActionResult Add(int id)
        {
            var project = this.projectService.GetProject(id);

            var projectModel = this.mapper.Map<Project, ProjectOfferAddViewModel>(project);

            var authorId = int.Parse(this.userManager.GetUserId(this.User));

            var position = this.userService.GetPositionThatUserAppliesFor(authorId);

            var offerModel = new OfferAddVIewModel { AuthorId = authorId, Project = projectModel, Position = position };

            return View(offerModel);
        }

        [HttpPost]
        public IActionResult Add(OfferAddVIewModel model)
        {
            if (this.offerService.IsOfferAlreadyExists(model.AuthorId, model.ProjectId))
            {
                this.ModelState.AddModelError(nameof(Offer), "This user already have sent an offer for this project.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            this.offerService.AddOffer(model);

            return RedirectToAction("Details", "Projects", new { id = model.ProjectId });
        }

        public IActionResult Filter(int projectId, string position)
        {
            var offers = this.offerService.GetProjectOffersByPosition(projectId, position);

            var offersModel = this.mapper.Map<List<Offer>, List<OfferListViewModel>>(offers);

            return PartialView("~/Views/Projects/OffersPartial.cshtml", offersModel);
        }

        public IActionResult Accept(int projectId, int authorId, string position)
        {

            if (this.projectService.AlreadyHasSuchASpecialist(projectId, position))
            {
                return BadRequest();
            }

            if (position.StartsWith(nameof(Designer)))
            {
                this.projectService.AddDesignerToProject(projectId, authorId);
            }
            else
            {
                var projectPosition = position.Split(" - ").ToArray().First();

                this.projectService.AddUserToProject(projectId, authorId, projectPosition);
            }

            this.offerService.RemoveOffersForThisPosition(projectId, position);

            return RedirectToAction("Details", "Projects", new { id = projectId });
        }
    }
}
