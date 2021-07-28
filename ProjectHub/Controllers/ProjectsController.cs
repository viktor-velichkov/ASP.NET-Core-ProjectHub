using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectHub.Controllers
{
    public class ProjectsController : Controller
    {
        public IActionResult Add()
        {
            return View();
        }

        public IActionResult List()
        {
            return View();
        }
        public IActionResult Details()
        {
            return View();
        }

    }
}
