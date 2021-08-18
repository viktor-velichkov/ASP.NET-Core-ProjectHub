using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectHub.Areas.Admin;
using ProjectHub.Data.Models;
using ProjectHub.Models.User;
using ProjectHub.Services.Account;
using ProjectHub.Services.Disciplines;
using ProjectHub.Services.User;
using ProjectHub.Services.UserKinds;

namespace ProjectHub.Controllers
{
    public class AccountController : Controller
    {

        private readonly IAccountService accountService;
        private readonly IDisciplineService disciplineService;
        private readonly IUserKindService userKindService;
        private readonly IUserService userService;

        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole<int>> roleManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(
                         IAccountService accountService,
                         IDisciplineService disciplineService,
                         IUserKindService userKindService,
                         IUserService userService,
                         UserManager<ApplicationUser> userManager,
                         RoleManager<IdentityRole<int>> roleManager,
                         SignInManager<ApplicationUser> signInManager)
        {
            this.accountService = accountService;
            this.disciplineService = disciplineService;
            this.userKindService = userKindService;
            this.userService = userService;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
        }

        public IActionResult Register()
        {
            var userKinds = this.userKindService.GetAllUserKinds();
            var designerDisciplines = this.disciplineService.GetAllDisciplines();
            return View(new UserRegisterFormModel
            {
                UserKinds = userKinds,
                Disciplines = designerDisciplines
            });
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterFormModel user)
        {
            var isUserNameExists = await this.userManager.FindByNameAsync(user.Email) != null;
            if (isUserNameExists)
            {
                this.ModelState
                    .AddModelError(nameof(user.Email), ValidationErrorMessages.UserAlreadyExists);
            }
            var isUserKindValid = this.accountService.ConfirmThatUserKindIsValid(user.UserKindId);

            if (!isUserKindValid)
            {
                this.ModelState
                    .AddModelError(nameof(user.UserKindId), ValidationErrorMessages.InvalidUserKindMessage);
            }

            var userKindId = this.accountService.GetUserKindId(nameof(Designer));

            var isDisciplineValid = this.accountService
                                        .ConfirmThatDisciplineIsValid(user.DisciplineId);

            if (userKindId.Equals(user.UserKindId) && !isDisciplineValid)
            {
                this.ModelState
                    .AddModelError(nameof(user.DisciplineId), ValidationErrorMessages.InvalidDisciplineMessage);
            }
            
            if (!ModelState.IsValid)
            {
                user.UserKinds = this.userKindService.GetAllUserKinds();

                user.Disciplines = this.disciplineService.GetAllDisciplines();

                return View(user);
            }

            if (!await roleManager.RoleExistsAsync(ControllerConstants.UserIdentityRole))
            {
                var role = new IdentityRole<int> { Name = ControllerConstants.UserIdentityRole };

                await this.roleManager.CreateAsync(role);
            }

            var newUser = new ApplicationUser
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.Email,
                Email = user.Email,
                UserKindId = user.UserKindId
            };

            await userManager.CreateAsync(newUser, user.Password);

            await userManager.AddToRoleAsync(newUser, ControllerConstants.UserIdentityRole);

            this.accountService.CreateUserKindEntityRecord(user.UserKindId, newUser.Id, user.DisciplineId);

            return RedirectToAction("Login", "Account");
        }

        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginViewModel user)
        {
            var userDb = this.userManager.FindByEmailAsync(user.Email).Result;

            if (userDb == null)
            {
                this.ModelState.AddModelError(nameof(user), ValidationErrorMessages.UserInvalidCredentialsGivenMessage);
            }
            else
            {
                var passwordMatch = ((int)this.userManager.PasswordHasher.VerifyHashedPassword(userDb, userDb.PasswordHash, user.Password));// != 0;

                if (passwordMatch == 0)
                {
                    this.ModelState.AddModelError(nameof(user), ValidationErrorMessages.UserInvalidCredentialsGivenMessage);
                }
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


            if (this.userService.IsInRole(userDb.Id, AdminConstants.AdministratorRole))
            {
                return Redirect("/Admin/Home/Index");
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
