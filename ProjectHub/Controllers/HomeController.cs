using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectHub.Data.Models;
using ProjectHub.Models;
using ProjectHub.Models.Home;
using ProjectHub.Models.Investor;
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
        private readonly IProjectService projectService;
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public HomeController(
                              ILogger<HomeController> logger,
                              IProjectService projectService,
                              IUserService userService,
                              IMapper mapper)
        {
            _logger = logger;
            this.projectService = projectService;
            this.userService = userService;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var latestThreeProjects = this.projectService
                              .GetLatestThreeProjects();

            var modelProjects = this.mapper.Map<List<Project>, List<ProjectListingViewModel>>(latestThreeProjects);

            var topThreeInvestors = this.userService.GetTopThreeInvestors();

            var modeInvestors = this.mapper.Map<List<Investor>, List<InvestorListViewModel>>(topThreeInvestors);

            var model = new IndexPageViewModel { Projects = modelProjects, Investors = modeInvestors };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
