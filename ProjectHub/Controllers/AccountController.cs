using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectHub.Data.Models;
using ProjectHub.Models.User;
using ProjectHub.Services.Account;
using ProjectHub.Services.DIscipline;
using ProjectHub.Services.UserKinds;
using System.Threading.Tasks;

namespace ProjectHub.Controllers
{
    public class AccountController : Controller
    {

        private readonly IAccountService accountService;
        private readonly IDisciplineService disciplineService;
        private readonly IUserKindService userKindService;

        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;

        public AccountController(
                         IAccountService accountService,
                         IDisciplineService disciplineService,
                         IUserKindService userKindService,
                         UserManager<ApplicationUser> userManager,
                         SignInManager<ApplicationUser> signInManager)
        {
            this.accountService = accountService;
            this.disciplineService = disciplineService;
            this.userKindService = userKindService;
            this.userManager = userManager;
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
            if (!this.accountService.ConfirmThatUserKindIsValid(user.UserKindId))
            {
                this.ModelState.AddModelError(nameof(user.UserKindId), ValidationErrorMessages.InvalidUserKindMessage);
            }

            if (this.accountService.GetUserKindId(nameof(Designer)).Equals(user.UserKindId)
                && !this.accountService.ConfirmThatDisciplineIsValid(user.DisciplineId))
            {
                this.ModelState.AddModelError(nameof(user.DisciplineId), ValidationErrorMessages.InvalidDisciplineMessage);
            }

            if (!ModelState.IsValid)
            {
                user.UserKinds = this.userKindService.GetAllUserKinds();

                user.Disciplines = this.disciplineService.GetAllDisciplines();

                return View(user);
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

            return RedirectToAction("Profile", "User");
        }
    }
}
