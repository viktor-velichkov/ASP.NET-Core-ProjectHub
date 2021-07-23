using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProjectHub.Controllers
{
    public class UserController : Controller
    {
       
        public IActionResult Profile()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            
            return View();
        }
    }
}
