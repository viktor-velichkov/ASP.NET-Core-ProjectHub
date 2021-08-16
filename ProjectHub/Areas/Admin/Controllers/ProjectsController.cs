using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static ProjectHub.Areas.Admin.AdminConstants;

namespace ProjectHub.Areas.Admin.Controllers
{
    [Area(AdministratorArea)]
    [Authorize(Roles = AdministratorRole)]
    public class ProjectsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
