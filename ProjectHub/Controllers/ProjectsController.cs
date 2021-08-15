using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectHub.Data;
using ProjectHub.Data.Models;
using ProjectHub.Data.Models.Projects;
using ProjectHub.Models.Offer;
using ProjectHub.Models.Projects;
using ProjectHub.Models.User;
using ProjectHub.Services.DIscipline;
using ProjectHub.Services.Files;
using ProjectHub.Services.Offers;
using ProjectHub.Services.Projects;
using ProjectHub.Services.User;

namespace ProjectHub.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly ProjectHubDbContext data;
        private readonly IMapper mapper;
        private readonly IProjectService projectService;
        private readonly IOfferService offerService;
        private readonly IUserService userService;
        private readonly IFilesService filesService;
        private readonly IDisciplineService disciplineService;
        private readonly UserManager<ApplicationUser> userManager;

        public ProjectsController(ProjectHubDbContext data,
                                  IMapper mapper,
                                  IProjectService projectService,
                                  IOfferService offerService,
                                  IUserService userService,
                                  IFilesService filesService,
                                  IDisciplineService disciplineService,
                                  UserManager<ApplicationUser> userManager)
        {
            this.data = data;
            this.mapper = mapper;
            this.projectService = projectService;
            this.offerService = offerService;
            this.userService = userService;
            this.filesService = filesService;
            this.disciplineService = disciplineService;
            this.userManager = userManager;
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(ProjectAddViewModel model)
        {
            var uploadedImage = model.ImageUpload;

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
                model.Image = this.filesService.ProcessUploadedFile(uploadedImage);
            }
            else
            {
                model.Image = this.projectService.GetProjectImage(model.Id);
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var investorId = int.Parse(this.userManager.GetUserId(this.User));

            this.projectService.AddProject(model, investorId);

            return RedirectToAction("Profile", "User");
        }

        public IActionResult List()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            var project = this.projectService.GetProjectWithItsParticipantsById(id);

            var disciplines = this.disciplineService.GetAllDisciplines();

            var projectViewModel = this.mapper.Map<Project, ProjectDetailsViewModel>(project);

            var projectDesigners = this.projectService.GetProjectDesignersByProjectId(id);

            projectViewModel.Designers = this.mapper
                .Map<List<ProjectDesigner>, List<DesignerProjectDetailsViewModel>>(projectDesigners);

            var loggedUserId = int.Parse(this.userManager.GetUserId(this.User));

            var loggedUser = this.userService.GetUserById(loggedUserId);

            var loggedUserUserKind = loggedUser.UserKind.Name;

            var loggedUserDiscipline = loggedUserUserKind.Equals("Designer") ?
                                       this.projectService.GetDesignerDisciplineName(loggedUserId) : null;

            var projectViewType = projectViewModel.GetType();

            bool isLoggedUserPositionFree = false;

            bool isLoggedUserHiredForThisProject = false;

            if (!loggedUserUserKind.Equals("Designer"))
            {
                isLoggedUserPositionFree = projectViewType.GetProperty(loggedUserUserKind) != null ?
                                           String.IsNullOrWhiteSpace(
                                               projectViewType.GetProperty(loggedUserUserKind).GetValue(projectViewModel) as string) :
                                           true;

                isLoggedUserHiredForThisProject = Convert.ToInt32(typeof(Project).GetProperty(loggedUserUserKind + "Id").GetValue(project)).Equals(loggedUserId);
            }
            else
            {
                isLoggedUserPositionFree = String.IsNullOrWhiteSpace(
                    projectViewModel.Designers.Select(d => d.Discipline).FirstOrDefault(d => d.Equals(loggedUserDiscipline)));

                isLoggedUserHiredForThisProject = projectDesigners.Any(pd => pd.Designer.User.Id.Equals(loggedUserId));
            }

            projectViewModel.IsLoggedUserPositionFree = isLoggedUserPositionFree;

            projectViewModel.IsLoggedUserAlreadySentAnOffer = this.offerService
                                                                  .IsLoggedUserAlreadySentAnOfferForThisProject(loggedUserId, id);

            return View(new Tuple<ProjectDetailsViewModel, List<Discipline>>(projectViewModel, disciplines));
        }

        public IActionResult Offers(int id)
        {
            var project = this.projectService.GetProjectById(id);

            var loggedUserId = int.Parse(this.userManager.GetUserId(this.User));
            if (!project.InvestorId.Equals(loggedUserId))
            {
                this.ModelState.AddModelError(nameof(project.Investor), ValidationErrorMessages.ProjectInvestorNotLoggedMessage);
            }

            if (!ModelState.IsValid)
            {
                return RedirectToAction("Profile", "User");
            }

            var offers = this.projectService.GetProjectOffersWithAuthorByProjectId(id);

            var projectModel = this.mapper.Map<Project, ProjectOffersListViewModel>(project);

            projectModel.Offers = this.mapper.Map<List<Offer>, List<OfferListViewModel>>(offers);

            projectModel.Disciplines = this.disciplineService.GetAllDisciplines();

            return View(projectModel);
        }

        public IActionResult Remove(int id)
        {
            int loggedUserId = int.Parse(this.userManager.GetUserId(this.User));

            var loggedUser = this.userService.GetUserById(loggedUserId);

            if (loggedUser.UserKind.Name != nameof(Investor)
                || !this.projectService.ConfirmThatInvestorIsOwnerOfTheProject(loggedUserId, id))
            {
                return Unauthorized();
            }

            this.projectService.RemoveProject(id);

            return RedirectToAction("Profile", "User", new { Id = loggedUserId });
        }

    }
}
