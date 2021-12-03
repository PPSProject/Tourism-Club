using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using tourism_club.Domain;
using tourism_club.Domain.Classes;
using tourism_club.Domain.Interfaces;
using tourism_club.Functions;
using tourism_club.Models;


namespace tourism_club.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ILocations _locations;
        private readonly IUsers _users;
        private readonly IRoles _roles;
        private readonly IFrames _frames;
        IWebHostEnvironment _appEnvironment;
        public AdminController(IUsers users, IRoles roles, IFrames frames, IWebHostEnvironment appEnvironment, ILocations locations)
        {
            _users = users;
            _roles = roles;
            _frames = frames;
            _appEnvironment = appEnvironment;
            _locations = locations;
        }
        bool AreYouAdmin()
        {
            User user = _users.getUserbyName(User.Identity.Name);
            user.existadminrole = _roles.getRole(user);
            if (user.existadminrole.adminRole)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        [HttpGet]
        public IActionResult ChooseEdit()
        {
            
            if (AreYouAdmin())
            {
                return View(_locations.locations);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }
        

        [HttpGet]
        public IActionResult AddLocation()
        {
            if (AreYouAdmin())
            {
                Location location = new Location();
                return View(location);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddLocation(string LocationTitle, string LocationDescription,  string usedFrame,IFormFileCollection fotos)
        {
            Location location = new Location();
            Frame frame = new Frame();

            location.LocationTitle = LocationTitle;
            location.LocationDescription = LocationDescription;

            if (usedFrame == "" || usedFrame == null|| LocationTitle == null)
            {
                return View(location);
            }
            else
            {
                foreach (var u in fotos)
                {
                    string path = "/images/" + u.FileName;
                    location.PathToPhotos += path + ",";
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await u.CopyToAsync(fileStream);
                    }
                }

                frame = Parse.returnFrame(usedFrame);
                frame.Id = default;
                

                _locations.addLocation(location);
                frame.LocationId = location.Id;
                _frames.addFrame(frame);

            }

            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        public IActionResult EditLocation(int id)
        {
            if (AreYouAdmin())
            {
                Location location = _locations.getLocation(id);
                Frame frame = _frames.getFrame(location);

                PageAdminModel pageAdminModel = new PageAdminModel();
                pageAdminModel.location = location;
                pageAdminModel.frame = frame;
                return View(pageAdminModel);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditLocation(string LocationTitle, string LocationDescription, int LocId, string PathToFotos, IFormFileCollection fotos, int FrId, string usedFrame)
        {

            Location location = new Location();
            location.Id = LocId;
            location.LocationDescription = LocationDescription;
            location.LocationTitle = LocationTitle;
            location.PathToPhotos = PathToFotos;


            Frame frame = Parse.returnFrame(usedFrame);
            frame.Id = FrId;
            frame.LocationId = location.Id;

            PageAdminModel p = new PageAdminModel();
            p.frame = frame;
            p.location = location;
            if (usedFrame == "" || usedFrame == null || LocationTitle == null)
            {
                return View(p);
            }
            if (fotos.Count() != 0)
            {
                location.PathToPhotos = null;
                foreach (var u in fotos)
                {
                    string path = "/images/" + u.FileName;
                    location.PathToPhotos += path + ",";

                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await u.CopyToAsync(fileStream);
                    }
                }

                _locations.addLocation(location);
                _frames.addFrame(frame);
            }
            else
            {
                _locations.addLocation(location);
                _frames.addFrame(frame);
            }

            return RedirectToAction("Index", "Home");
        }
        
    }
}
