using System;
using System.Collections.Generic;
using System.Diagnostics;

using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Hobbies.Models;

namespace Hobbies.Controllers
{
    [Route("hobbies")]
    public class HobbiesController : Controller
    {
        private User loggedInUser
        {
            //This should work but after running the debugger it says UserId = null, and will display my @@@@@@@@@ error instead of redirecting you.
            get { return dbContext.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UserId")); }
            //I also tried this one, but it did not work either.
            //get{ return HttpContext.Session.GetInt32("UserId"); }
        }
        private MyContext dbContext;
        public HobbiesController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            if(loggedInUser == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(dbContext.Hobbies.OrderByDescending(d => d.CreatedAt));
        }

        [HttpGet("{hobbyId}")]
        public IActionResult Show(int hobbyId)
        {
            if(loggedInUser == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.UserInSess = dbContext.Users.FirstOrDefault(u=>u.UserId == HttpContext.Session.GetInt32("UserId"));
            Hobby viewModel = dbContext.Hobbies
                .Include(a=>a.Associations)
                .ThenInclude(u=>u.User)
                .FirstOrDefault(a=>a.HobbyId == hobbyId);
            return View(viewModel);
        }
        [HttpGet("new")]
        public IActionResult New()
        {
            if(loggedInUser == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
        [HttpPost("create")]
        public IActionResult Create(Hobby newHobby)
        {
            if(loggedInUser == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if(ModelState.IsValid)
            {
                dbContext.Hobbies.Add(newHobby);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("New");
        }
    }
}
