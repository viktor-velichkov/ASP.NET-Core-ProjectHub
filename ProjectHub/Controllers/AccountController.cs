using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectHub.Data;
using ProjectHub.Data.Factories;
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
            var userTypes = GetUserKinds();
            return View(new UserRegisterFormModel
            {
                UserKinds = userTypes
            });
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterFormModel user)
        {
            if (!this.data.UserKinds.Any(ut => ut.Id.Equals(user.UserKindId)))
            {
                this.ModelState.AddModelError(nameof(user.UserKindId), ValidationErrorMessages.InvalidUserKindMessage);
            }

            if (!ModelState.IsValid)
            {
                user.UserKinds = GetUserKinds();

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

            CreateUserKindEntityRecord(this.data.UserKinds.FirstOrDefault(ut => ut.Id.Equals(user.UserKindId)), newUser.Id);

            this.data.SaveChanges();

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


        private IEnumerable<UserKindRegisterFormModel> GetUserKinds()
             => this.data
                    .UserKinds
                    .Select(ut => new UserKindRegisterFormModel
                    {
                        Id = ut.Id,
                        Name = ut.Name
                    })
                    .ToList();

        private void CreateUserKindEntityRecord(UserKind userType, int userId)
        {
            switch (userType.Name)
            {
                case "Investor":
                    this.data.Investors.Add(new Investor { Id = userId, UserId = userId });
                    break;
                case "Designer":
                    this.data.Designers.Add(new Designer { Id = userId, UserId = userId });
                    break;
                case "Manager":
                    this.data.Managers.Add(new Manager { Id = userId, UserId = userId });
                    break;
                case "Contractor":
                    this.data.Contractors.Add(new Contractor { Id = userId, UserId = userId });
                    break;
                default:
                    throw new ArgumentException(ValidationErrorMessages.InvalidUserKindMessage);
            }
        }
    }
}
