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
        private readonly IComments _comments;
        IWebHostEnvironment _appEnvironment;
        public AdminController(IUsers users, IRoles roles, IFrames frames, IWebHostEnvironment appEnvironment, ILocations locations, IComments comments)
        {
            _users = users;
            _roles = roles;
            _frames = frames;
            _appEnvironment = appEnvironment;
            _locations = locations;
            _comments = comments;
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
                PageChooseAdminModel model = new PageChooseAdminModel();
                model.users = new List<User>();
                List<User> AllUsers = _users.users.ToList();

                foreach(var u in AllUsers)
                {
                    u.existadminrole = _roles.getRole(u);
                    if(u.existadminrole.adminRole == false)
                    {
                        model.users.Add(u); 
                    }
                }
                model.locations = _locations.locations;

                return View(model);
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

        [HttpGet]
        public IActionResult DeleteLocation(int id)
        {
            if (AreYouAdmin())
            {
                Location location = _locations.getLocation(id);
                Frame frame = _frames.getFrame(location);

                PageAdminModel pageAdminModel = new PageAdminModel();
                pageAdminModel.location = location;
                pageAdminModel.frame = frame;
                ViewBag.fotoss = location.PathToPhotos.Split(",");
                return View(pageAdminModel);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public IActionResult DeleteLocation(string LocationTitle, string LocationDescription, int LocId, string PathToFotos, int FrId, string usedFrame)
        {
            Location location = new Location();
            location.Id = LocId;
            location.LocationDescription = LocationDescription;
            location.LocationTitle = LocationTitle;
            location.PathToPhotos = PathToFotos;


            Frame frame = Parse.returnFrame(usedFrame);
            frame.Id = FrId;
            frame.LocationId = location.Id;

            location.comments = _comments.comments(location).ToList();

            _comments.deleteComment(location.comments);
            
            _frames.removeFrame(location);
            _locations.removeLocation(location.Id);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult DeleteAccount(int id)
        {
            if (AreYouAdmin())
            {
                User user = _users.getUser(id);
                return View(user);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public IActionResult DeleteAccount(int UserId, string Name)
        {
            List<Comment> allComments = _comments.Allcomments.ToList();
            List<Comment> commentsOfUser = new List<Comment>();
            foreach (var com in allComments)
            {
                if(com.CommentatorId == UserId)
                {
                    commentsOfUser.Add(com);
                }
            }
            _comments.deleteComment(commentsOfUser);
            _users.removeUser(UserId);
            return RedirectToAction("ChooseEdit", "Admin");

        }
    }
}
