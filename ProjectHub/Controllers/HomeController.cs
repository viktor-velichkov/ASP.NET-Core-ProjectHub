using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectHub.Areas.Admin;
using ProjectHub.Data.Models;
using ProjectHub.Models;
using ProjectHub.Models.Contractor;
using ProjectHub.Models.Designer;
using ProjectHub.Models.Home;
using ProjectHub.Models.Investor;
using ProjectHub.Models.Manager;
using ProjectHub.Models.Projects;
using ProjectHub.Services.Projects;
using ProjectHub.Services.User;
using System.Collections.Generic;
using System.Diagnostics;

namespace ProjectHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IProjectService projectService;
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public HomeController(
                              ILogger<HomeController> logger,
                              UserManager<ApplicationUser> userManager,
                              IProjectService projectService,
                              IUserService userService,
                              IMapper mapper)
        {
            _logger = logger;
            this.userManager = userManager;
            this.projectService = projectService;
            this.userService = userService;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return View();
            }

            var loggedUserId = int.Parse(this.userManager.GetUserId(this.User));

            if (this.userService.IsInRole(loggedUserId,AdminConstants.AdministratorRole))
            {
                return Redirect("/Admin/Home/Index");
            }


            var latestThreeProjects = this.projectService
                .GetLatestThreeProjects();

            var modelProjects = this.mapper
                .Map<List<Project>, List<ProjectListingViewModel>>(latestThreeProjects);

            var topThreeInvestors = this.userService
                .GetTopThreeInvestors();

            var modeInvestors = this.mapper
                .Map<List<Investor>, List<InvestorListViewModel>>(topThreeInvestors);

            var topThreeManagers = this.userService
                .GetTopThreeManagers();

            var modelManagers = this.mapper
                .Map<List<Manager>, List<ManagerListViewModel>>(topThreeManagers);

            var topThreeDesigners = this.userService
                .GetTopThreeDesigners();

            var modelDesigners = this.mapper
                .Map<List<Designer>, List<DesignerListViewModel>>(topThreeDesigners);

            var topThreeContractor = this.userService
                .GetTopThreeContractors();

            var modelContractors = this.mapper
                .Map<List<Contractor>, List<ContractorListViewModel>>(topThreeContractor);

            var model = new IndexPageViewModel
            {
                Projects = modelProjects,
                Investors = modeInvestors,
                Managers = modelManagers,
                Designers = modelDesigners,
                Contractors = modelContractors
            };

            return View("~/Views/Home/AuthorizedIndex.cshtml", model);
        }        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
