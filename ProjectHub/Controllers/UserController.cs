using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectHub.Data;
using ProjectHub.Data.Models;
using ProjectHub.Models.User;
using System.Linq;

namespace ProjectHub.Controllers
{
    public class UserController : Controller
    {
        private readonly ProjectHubDbContext data;
        private readonly UserManager<ApplicationUser> userManager;

        public UserController(ProjectHubDbContext data,
                              UserManager<ApplicationUser> userManager)
        {
            this.data = data;
            this.userManager = userManager;
        }

        public IActionResult Profile()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            
            var userId = this.Request.Query.ContainsKey("id") ? 
                         this.Request.Query["id"].ToString() : 
                         this.userManager.GetUserId(this.User);

            var user = this.data
                           .Users
                           .Select(u => new UserProfileViewModel
                           {
                               FullName = u.FullName,
                           });

            return View();
        }
    }
}
