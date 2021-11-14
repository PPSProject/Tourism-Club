using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tourism_club.Domain;
using tourism_club.Domain.Classes;
using tourism_club.Models;

namespace tourism_club.Controllers
{
    public class HomeController : Controller
    {
        AppDBContent db;

        public HomeController(AppDBContent context)
        {
            db = context;
        }
        public IActionResult Index()
        {

            return View(db.locations.ToList());
        }


        [HttpGet]
        public IActionResult Location(int id)
        {
            if (id == null)
                return RedirectToAction("Index");

            //Location location = db.locations.

            //ViewBag.Locationdesc = location.LocationDescription;



            return View();
        }
    }
}
