using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectHub.Data;
using ProjectHub.Data.Models;
using ProjectHub.Models.User;

namespace ProjectHub.Controllers
{
    public class AccountController : Controller
    {
        private readonly ProjectHubDbContext data;

        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;



        public AccountController(ProjectHubDbContext data,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this.data = data;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult Register()
        {
            var userTypes = GetUserTypes();
            return View(new UserRegisterFormModel
            {
                UserTypes = userTypes
            });
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterFormModel user)
        {
            if (!this.data.UserTypes.Any(ut => ut.Id.Equals(user.UserTypeId)))
            {
                this.ModelState.AddModelError(nameof(user.UserTypeId), ValidationErrorMessages.InvalidUserTypeMessage);
            }

            if (!ModelState.IsValid)
            {
                user.UserTypes = GetUserTypes();
                               

                return View(user);
            }

            var newUser = new ApplicationUser
            {
                UserName = user.Email,
                Email = user.Email,
                UserTypeId = user.UserTypeId
            };

            await userManager.CreateAsync(newUser, user.Password);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginViewModel user)
        {
            var userDb = this.data.Users.FirstOrDefault(u => u.Email.Equals(user.Email));

            if (userDb == null)
            {
                this.ModelState.AddModelError(nameof(user), ValidationErrorMessages.UserInvalidEmailGivenMessage);
            }

            var passwordMatch = ((int)this.userManager.PasswordHasher.VerifyHashedPassword(userDb, userDb.PasswordHash, user.Password));// != 0;

            if (passwordMatch == 0)
            {
                this.ModelState.AddModelError(nameof(user), ValidationErrorMessages.UserInvalidPasswordGivenMessage);
            }

            if (!ModelState.IsValid)
            {
                return View(user);
            }

            if (userDb.UserName != user.Email)
            {
                return View(user);
            }

            await signInManager.SignInAsync(userDb, user.RememberMe);

            return RedirectToAction("Index", "Home");
        }


        private IEnumerable<UserTypeRegisterFormModel> GetUserTypes()
             => this.data
                    .UserTypes
                    .Select(ut => new UserTypeRegisterFormModel
                    {
                        Id = ut.Id,
                        Name = ut.Name
                    })
                    .ToList();
    }
}
