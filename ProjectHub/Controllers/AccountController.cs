using Microsoft.AspNetCore.Mvc;
using ProjectHub.Data;
using ProjectHub.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectHub.Controllers
{
    public class AccountController : Controller
    {
        private readonly ProjectHubDbContext data;

        public AccountController(ProjectHubDbContext data)
        {
            this.data = data;
        }

        
        public IActionResult Register() => View(new UserRegisterFormModel
        {
            UserTypes = GetUserTypes()
        });

        [HttpPost]
        public IActionResult Register(UserRegisterFormModel user)
        {

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
