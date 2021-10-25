using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tourism_club.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.SRC = "https://www.google.com/maps/embed?pb=!1m22!1m8!1m3!1d10162.914183152026!2d30.155543573622708!3d50.44615672566546!3m2!1i1024!2i768!4f13.1!4m11!3e2!4m3!3m2!1d50.436034799999995!2d30.0976875!4m5!1s0x472b35cf170e946d%3A0x2a308ff1a1e4f50d!2z0KHQv9C-0YDRgtC40LLQvdCw0Y8sIDIwLCDQttC40LvQvtC5INC00L7QvA!3m2!1d50.444531299999994!2d30.165438899999998!5e0!3m2!1sru!2sua!4v1635166620715!5m2!1sru!2sua";
            ViewBag.Width = "600";
            ViewBag.Height = "450";
            ViewBag.Style = "border:0;";
            ViewBag.screen = "";
            ViewBag.loading = "lazy";
 
            return View();
        }
    }
}
