using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectHub.Models.Offer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectHub.Controllers
{
    public class OffersController : Controller
    {
        [Authorize]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(OfferAddVIewModel model)
        {
            return View();
        }
    }
}
