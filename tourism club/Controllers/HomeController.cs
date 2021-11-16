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
        readonly IFrames frames;
        public HomeController(AppDBContent context)
        {
            db = context;
            loc = new EFLocations(context);
            frames = new EFFrames(context);
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
            string[] arr = location.PathToPhotos.Split(",");
            Frame frame = frames.getFrame(location);
            ViewBag.Title = location.LocationTitle;
            ViewBag.Locationdesc = location.LocationDescription;
            ViewBag.FrameId = frame.Id;
            ViewBag.Fotos = arr;

            return View(db.frames.ToList());
        }
    }
}
