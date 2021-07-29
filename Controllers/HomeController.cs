using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Hobbies.Models;

namespace Hobbies.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;
        public HomeController(MyContext context)
        {
            dbContext = context;
        }
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("register")]
        public IActionResult Register(User user)
        {
            if(ModelState.IsValid)
            {
                if(dbContext.Users.Any(o => o.Email == user.Email))
                {
                    ModelState.AddModelError("Email", "Email already in use");
                    return View("Index");
                }   
                PasswordHasher<User> hasher = new PasswordHasher<User>();
                user.Password = hasher.HashPassword(user, user.Password);   
                var newUser = dbContext.Users.Add(user).Entity;
                dbContext.SaveChanges();    
                HttpContext.Session.SetInt32("UserId", newUser.UserId); 
                return RedirectToAction("Index", "Hobbies");
            }

            return View("Index");
        }

        [HttpPost("login")]
        public IActionResult Login(LoginUser user)
        {
            var test = dbContext.Users.ToList();
            if(ModelState.IsValid)
            {
                User toLogin = dbContext.Users.FirstOrDefault(u => u.Email == user.Email);
                if(toLogin == null)
                {
                    ModelState.AddModelError("Email", "Invalid Email/Password");
                    return View("Index");
                }
                PasswordHasher<LoginUser> hasher = new PasswordHasher<LoginUser>();
                var result = hasher.VerifyHashedPassword(user, toLogin.Password, user.Password);
                if(result == PasswordVerificationResult.Failed)
                {
                    ModelState.AddModelError("Email", "Invalid Email/Password");
                    return View("Index");
                }
                // Log user into session
                HttpContext.Session.SetInt32("UserId", toLogin.UserId);
                return RedirectToAction("Index", "Hobbies");
            }
            return View("Index");
        }
        [HttpGet("logout")]
        public RedirectToActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("");
        }
    }
}
