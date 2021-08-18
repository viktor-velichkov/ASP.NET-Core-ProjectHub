using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectHub.Data;
using ProjectHub.Data.Models;
using ProjectHub.Models.Projects;
using ProjectHub.Services.Disciplines;
using ProjectHub.Services.Files;
using ProjectHub.Services.Offers;
using ProjectHub.Services.Projects;
using ProjectHub.Services.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace ProjectHub.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {        
        private readonly IProjectService projectService;
        private readonly IOfferService offerService;
        private readonly IUserService userService;
        private readonly IFilesService filesService;
        private readonly IDisciplineService disciplineService;

        public ProjectsController(IProjectService projectService,
                                  IOfferService offerService,
                                  IUserService userService,
                                  IFilesService filesService,
                                  IDisciplineService disciplineService)
        {
            this.projectService = projectService;
            this.offerService = offerService;
            this.userService = userService;
            this.filesService = filesService;
            this.disciplineService = disciplineService;
        }
        public IActionResult All()
        {
            var modelProjects = this.projectService.GetAllProjectsOrderedByDateDescending();

            var modelCities = new List<string>() { "All" };

            modelCities.AddRange(this.projectService.GetAllProjectCities());

            var model = new AllProjectsViewModel { Projects = modelProjects, Cities = modelCities };

            return View(model);
        }

        public IActionResult City(string city)
        {
            var projectsModel = this.projectService.FilterByCity(city);

            return PartialView("~/Views/Projects/AllByCityPartial.cshtml", projectsModel);
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

            var investorId = int.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            this.projectService.AddProject(model, investorId);

            return RedirectToAction("Profile", "User", new { id = investorId });
        }

        public IActionResult List()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            if (!this.projectService.ProjectExists(id))
            {
                return NotFound();
            }

            var projectViewModel = this.projectService.GetProjectDetailsViewModel(id);

            var disciplines = this.disciplineService.GetAllDisciplines();

            projectViewModel.Designers = this.projectService
                                             .GetProjectDesignersByProjectId(id);

            var loggedUserId = int.Parse(this.User
                                             .FindFirst(ClaimTypes.NameIdentifier)
                                             .Value);

            var loggedUser = this.userService.GetUserById(loggedUserId);

            var loggedUserUserKind = loggedUser.UserKind.Name;

            var loggedUserDiscipline = loggedUserUserKind.Equals("Designer") ?
                                       this.projectService.GetDesignerDisciplineName(loggedUserId) : null;

            var projectViewType = projectViewModel.GetType();

            bool isPositionFree = false;

            bool isHiredForThisProject = false;

            if (!loggedUserUserKind.Equals("Designer"))
            {
                isPositionFree = projectViewModel.GetType()
                                                 .GetProperty(loggedUserUserKind) != null ?
                                                 String.IsNullOrWhiteSpace(projectViewType.GetProperty(loggedUserUserKind)
                                                                                          .GetValue(projectViewModel) as string) : true;
                
                isHiredForThisProject = Convert.ToInt32(typeof(ProjectDetailsViewModel)
                                               .GetProperty(loggedUserUserKind + "Id")
                                               .GetValue(projectViewModel)).Equals(loggedUserId);
                                               
            }
            else
            {
                isPositionFree = String.IsNullOrWhiteSpace(projectViewModel.Designers
                                                                           .Select(d => d.Discipline)
                                                                           .FirstOrDefault(d => d.Equals(loggedUserDiscipline)));

                isHiredForThisProject = projectViewModel.Designers
                                                        .Any(pd => pd.DesignerId.Equals(loggedUserId));
            }

            projectViewModel.IsLoggedUserPositionFree = isPositionFree;

            projectViewModel.IsLoggedUserAlreadySentAnOffer = this.offerService
                                                                  .IsOfferAlreadyExists(loggedUserId, id);

            var viewModel = new Tuple<ProjectDetailsViewModel, List<Discipline>>(projectViewModel, disciplines);

            return View(viewModel);
        }

        public IActionResult Offers(int id)
        {
            if (!this.projectService.ProjectExists(id))
            {
                return NotFound();
            }

            var projectModel = this.projectService.GetProjectWithOffersById(id);

            var loggedUserId = int.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (!projectModel.InvestorId.Equals(loggedUserId))
            {
                this.ModelState.AddModelError(nameof(Investor), ValidationErrorMessages.ProjectInvestorNotLoggedMessage);
            }

            if (!ModelState.IsValid)
            {
                return RedirectToAction("Profile", "User", new { id = loggedUserId });
            }

            projectModel.Disciplines = this.disciplineService.GetAllDisciplines();

            return View(projectModel);
        }

        public IActionResult Remove(int id)
        {
            if (!this.projectService.ProjectExists(id))
            {
                return NotFound();
            }

            int loggedUserId = int.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var loggedUser = this.userService.GetUserById(loggedUserId);

            if (loggedUser.UserKind.Name != nameof(Investor)
                || !this.projectService.IsOwnerOfTheProject(loggedUserId, id))
            {
                return Unauthorized();
            }

            this.projectService.RemoveProject(id);

            return RedirectToAction("Profile", "User", new { Id = loggedUserId });
        }

    }
}
