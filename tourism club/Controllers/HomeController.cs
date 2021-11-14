using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tourism_club.Domain;
using tourism_club.Domain.Classes;
using tourism_club.Domain.Interfaces;
using tourism_club.Models;

namespace tourism_club.Controllers
{
    public class HomeController : Controller
    {
        readonly AppDBContent db;
        readonly ILocations loc;
        public HomeController(AppDBContent context)
        {
            db = context;
            loc = new EFLocations(context);
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

            Location location = loc.getLocation(id);
            return View();
        }
    }
}
