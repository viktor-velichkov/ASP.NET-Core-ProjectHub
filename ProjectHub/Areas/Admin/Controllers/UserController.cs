using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectHub.Areas.Admin.Models.User;
using ProjectHub.Controllers;
using ProjectHub.Services.User;
using static ProjectHub.Areas.Admin.AdminConstants;

namespace ProjectHub.Areas.Admin.Controllers
{
    [Area(AdministratorArea)]
    [Authorize(Roles = AdministratorRole)]
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        public IActionResult Remove(UserFormViewModel model)
        {

            var user = this.userService.GetUserById(model.Id);

            if (user == null)
            {
                this.ModelState.AddModelError(nameof(model.Id), ValidationErrorMessages.InvalidUser);
            }

            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }

            this.userService.RemoveUser(model.UserName);

            return RedirectToAction("Index", "Home");
        }
    }
}
