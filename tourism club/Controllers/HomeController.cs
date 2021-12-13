using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        readonly IComments coms;
        readonly IUsers users;
        readonly IRoles roles;
        public HomeController(AppDBContent context)
        {
            db = context;
            loc = new EFLocations(context);
            frames = new EFFrames(context);
            coms = new EFComments(context);
            users = new EFUsers(context);
            roles = new EFRoles(context);
        }

        bool getRole()
        {
            User user = users.getUserbyName(User.Identity.Name);
            user.existadminrole = roles.getRole(user);
            return user.existadminrole.adminRole;
        }
        public  IActionResult Index()
        {
            ViewBag.boolAdmin = false;
            if (User.Identity.Name != null)
            {
                ViewBag.boolAdmin = getRole();
            }

            return View(db.locations.ToList());
        }
        
        [HttpGet]
        public IActionResult Location(int id)
        {
            ViewBag.boolAdmin = false;
            if (User.Identity.Name != null)
            {
                ViewBag.boolAdmin = getRole();
            }

            PageModel pageModel = new PageModel();
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Location location = loc.getLocation(id);
            Frame frame = frames.getFrame(location);

            pageModel.location = location;
            pageModel.frame = frame;
            pageModel.users = users.users.ToList();
            pageModel.comments = coms.comments(location).ToList();
            pageModel.pathToPhotos = location.PathToPhotos.Split(",");
            return View(pageModel);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Location(Comment comm, string action, string comment, int LocationId, int CommentatorId, int Id)
        {
            ViewBag.boolAdmin = false;
            if (User.Identity.Name != null)
            {
                ViewBag.boolAdmin = getRole();
            }

            Location location = loc.getLocation(LocationId);
            location.Id = LocationId;

            comm.Id = default;
            comm.comment = comment;
            comm.LocationId = LocationId;
            comm.CommentatorId = CommentatorId;
            comm.Location = location;
            if(action == "addComment")
            {
                if(comm.comment != null)
                {
                    coms.addComment(comm);
                   // db.comments.Add(comm);
                }
                
            }
            if(action == "deleteComment")
            {
                coms.deleteComment(Id);
            }
            
            Frame frame = frames.getFrame(location);
            PageModel pageModel = new PageModel();
            pageModel.location = location;
            pageModel.frame = frame;
            pageModel.users = users.users.ToList();
            pageModel.comments = coms.comments(location).ToList();
            pageModel.pathToPhotos = location.PathToPhotos.Split(",");

            return View(pageModel);
        }
       

    }
}
