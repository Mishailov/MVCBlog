using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyCustomTestBlog.Data;
using MyCustomTestBlog.Models;
using CommonLib.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyCustomTestBlog.Controllers
{
    public class HomeController : Controller
    {
        private IRepository<User> db;

        public HomeController(IRepository<User> _db)
        {
            db = _db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
                return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(User model)
        {
            if (db.GetItems().Where(x => x.E_Mail == model.E_Mail).Count() == 0)
            {
                db.Create(model);
                await Authenticate(model.E_Mail);
                return RedirectToAction("Index", "Account");
            }

            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User model)
        {
            var newUser = db.GetItems().FirstOrDefault(x => x.E_Mail == model.E_Mail
                && x.Password == model.Password);
            if (newUser != null)
            {
                await Authenticate(model.E_Mail);
                return RedirectToAction("Index", "Account");
            }

            return View();
        }

        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Home");
        }
    }
}
