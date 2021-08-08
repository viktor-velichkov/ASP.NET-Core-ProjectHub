using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectHub.Data;
using ProjectHub.Data.Models;
using ProjectHub.Models.Projects;
using ProjectHub.Models.User;
using ProjectHub.Services.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectHub.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly ProjectHubDbContext data;
        private readonly IMapper mapper;
        private readonly IProjectService projectService;
        private readonly UserManager<ApplicationUser> userManager;

        public ProjectsController(ProjectHubDbContext data,
                                  IMapper mapper,
                                  IProjectService projectService,
                                  UserManager<ApplicationUser> userManager)
        {
            this.data = data;
            this.mapper = mapper;
            this.projectService = projectService;
            this.userManager = userManager;
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(ProjectAddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var investorId = int.Parse(this.userManager.GetUserId(this.User));

            this.projectService.AddProject(model, investorId);

            return RedirectToAction("Profile","User");
        }

        public IActionResult List()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            var project = this.projectService.GetProjectById(id);

            var disciplines = this.projectService.GetAllDisciplines();

            var projectViewModel = this.mapper.Map<Project, ProjectDetailsViewModel>(project);

            var loggedUserId = int.Parse(this.userManager.GetUserId(this.User));

            var loggedUser = this.projectService.GetUserById(loggedUserId);

            var loggedUserUserKind = loggedUser.UserKind.Name;

            var loggedUserDiscipline = loggedUserUserKind.Equals("Designer") ? 
                                       this.projectService.GetDesignerDisciplineName(loggedUserId) : null;

            var projectViewType = projectViewModel.GetType();

            bool isLoggedUserPositionFree = false;

            if (!loggedUserUserKind.Equals("Designer"))
            {
                isLoggedUserPositionFree = projectViewType.GetProperty(loggedUserUserKind) != null ?
                                           String.IsNullOrWhiteSpace(
                                               projectViewType.GetProperty(loggedUserUserKind).GetValue(projectViewModel) as string) :
                                           true;                                                                                    
            }
            else
            {
                isLoggedUserPositionFree = String.IsNullOrWhiteSpace(
                    projectViewModel.Designers.Select(d => d.Discipline).FirstOrDefault(d => d.Equals(loggedUserDiscipline)));
            }

            projectViewModel.IsLoggedUserPositionFree = isLoggedUserPositionFree;

            return View(new Tuple<ProjectDetailsViewModel, List<Discipline>>(projectViewModel,disciplines));
        }

    }
}
