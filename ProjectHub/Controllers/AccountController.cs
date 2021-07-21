using System.Collections.Generic;
using System.Linq;
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
        private readonly PasswordHasher<ApplicationUser> passwordHasher;

        public AccountController(ProjectHubDbContext data, PasswordHasher<ApplicationUser> passwordHasher)
        {
            this.data = data;
            this.passwordHasher = passwordHasher;
        }

        [HttpGet]
        public IActionResult Register()             
        {
            var userTypes = GetUserTypes();
            return View(new UserRegisterFormModel
            {
                UserTypes = userTypes
            });
        }

        [HttpPost]
        public IActionResult Register(UserRegisterFormModel user)
        {
            if (!this.data.UserTypes.Any(ut=>ut.Id.Equals(user.UserTypeId)))
            {
                this.ModelState.AddModelError(nameof(user.UserTypeId),ValidationErrorMessages.InvalidUserTypeMessage);
            }

            if (!ModelState.IsValid)
            {
                user.UserTypes = GetUserTypes();

                //var selectedUserType = this.data.UserTypes.FirstOrDefault(ut => ut.Id.Equals(user.UserTypeId));

                //user.UserTypes.ToList().Add(new UserTypeRegisterFormModel
                //{
                //    Id = selectedUserType.Id,
                //    Name = selectedUserType.Name
                //});

                return View(user);
            }

            var newUser = new ApplicationUser
            {
                Email = user.Email,
                UserTypeId = user.UserTypeId
            };

            newUser.PasswordHash = this.passwordHasher.HashPassword(newUser, user.Password);

            this.data.Users.Add(newUser);

            this.data.SaveChanges();

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
