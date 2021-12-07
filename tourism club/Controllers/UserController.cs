using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tourism_club.Domain;
using tourism_club.Domain.Interfaces;
using tourism_club.Models;
using tourism_club.Controllers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace tourism_club.Controllers
{
    public class UserController : Controller
    {
        private readonly IUsers _users;
        private readonly IRoles _roles;
        public UserController(IUsers users, IRoles roles)
        {
            _users = users;
            _roles = roles;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string Name, string password)
        {
            List<User> users = _users.users.ToList();
            if (CorrectDatas(users, Name, password))
            {
                await Authenticate(Name);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Дані не вірні");
                return View();
            }
        }
        bool CorrectDatas(List<User> users, string name, string pass)
        {
            foreach(var us in users)
            {
                if(us.Name == name && us.password == pass)
                {
                    return true;
                }
            }
            return false;
        }
        [HttpGet]
        public IActionResult Registration()
        {
            User user = new User();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(string Name, string mail, string password)
        {
            string cond = @"(\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)";
            List<User> users = _users.users.ToList();
            User user = new User();

            user.Name = Name;
            user.mail = mail;
            user.password = password;
            if (!UserExist(users, Name, mail, password))
            {
                if(Regex.IsMatch(mail, cond))
                {
                    _users.addUser(user);

                    Role roles = new Role();
                    roles.Id = default;
                    roles.UserId = user.Id;
                    roles.adminRole = false;
                    _roles.addRole(roles);

                    await Authenticate(user.Name);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View(user);
                }
                
            }
            else
            {
                return View(user);
            }
            //return View(user);
        }
        bool UserExist(List<User> users, string name, string mail, string password)
        {
            foreach(var u in users)
            {
                if(u.Name == name || u.mail == mail)
                {
                    return true;
                }
            }
            return false;
        }
        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}