using Microsoft.AspNetCore.Mvc;

namespace ProjectHub.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Profile()
        {
            return View();
        }
    }
}
