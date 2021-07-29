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
using Microsoft.Extensions.DependencyInjection;
using Hobbies.Models;

namespace Hobbies.Controllers
{
    [Route("association")]
    public class AssociationController : Controller
    {
        private User loggedInUser
        {
            //Not retreiving the correct UserId
            get { return dbContext.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UserId")); }
        }
        private MyContext dbContext;
        public AssociationController(MyContext context)
        {
            dbContext = context;
        }
        [HttpGet("hobby/{hobbyId}/{status}")]
        public IActionResult Create(int hobbyId, string status)
        {
            if(status == "add")
            {
                Association newAssoc = new Association()
                {
                HobbyId = hobbyId,
                UserId = loggedInUser.UserId
                };
                dbContext.Associations.Add(newAssoc);
                dbContext.SaveChanges();
            }
            else
                return RedirectToAction("Index", "Hobbies");
            return RedirectToAction("Index", "Hobbies");
        }
    }
}