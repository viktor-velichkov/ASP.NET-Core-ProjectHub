using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using ProjectHub.Areas.Admin.Models.Home;
using ProjectHub.Areas.Admin.Models.Projects;
using ProjectHub.Areas.Admin.Models.User;
using System;
using static ProjectHub.Areas.Admin.AdminConstants;

namespace ProjectHub.Areas.Admin.Controllers
{
    [Area(AdministratorArea)]
    [Authorize(Roles = AdministratorRole)]

    public class HomeController : Controller
    {
        public IActionResult Index()
        {           

            //ProjectFormViewModel projectModel = TempData["ProjectModel"] as ProjectFormViewModel;
            
            return View();
        }
    }
}
