using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static ProjectHub.Areas.Admin.AdminConstants;

namespace ProjectHub.Areas.Admin.Controllers
{
    [Area(AdministratorArea)]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
