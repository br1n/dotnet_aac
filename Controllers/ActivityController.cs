using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ActivityCenter.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace ActivityCenter.Controllers
{
    public class ActivityController : Controller
    {
        private MyContext dbContext;
        public ActivityController(MyContext context)
        {
            dbContext = context;
        }

        public MainUser ActiveUser
        {
            get { return dbContext.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("user_Id")); }
        }
        //All activities
        [HttpGet("Dashboard")]
        public IActionResult Dashboard()
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Register", "Home");
            }

            //create query for activities
            var activityList = dbContext.Activities
            .Include(a => a.Coordinator)
            .Include(a => a.JoinActivities)
            .ThenInclude(j => j.User)
            .OrderByDescending(w => w.CreatedAt).ToList();

            ViewBag.User = ActiveUser;

            return View("Dashboard", activityList);
        }
        //Show activity 
        [HttpGet]
        [Route("activity/{ActivityId}")]
        public IActionResult Show(int activityId)
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Register", "Home");
            }

            MainUser User = ActiveUser;
            ViewBag.activity_Info = dbContext.Activities
            .Include(a => a.Coordinator)
            .Include(a => a.JoinActivities)
            .ThenInclude(j => j.User)
            .FirstOrDefault(a => a.ActivityId == activityId);


            var thisActivity = dbContext.Activities
            .Include(a => a.JoinActivities)
            .ThenInclude(j => j.User)
            .FirstOrDefault(a => a.ActivityId == activityId);

            return View(thisActivity);
        }


        [HttpGet("New")]
        public IActionResult New()
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Register", "Home");
            }

            ViewBag.User = ActiveUser;
            return View();
        }

        [HttpPost("create_activity")]
        public IActionResult CreateActivity(Models.Activity newActivity)
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Register", "Home");
            }

            if(ModelState.IsValid)
            {
                dbContext.Activities.Add(newActivity);
                dbContext.SaveChanges();
                return Redirect("Dashboard");
            }
            ViewBag.User = ActiveUser;
            return View("New");
        }

        // delete Activity
        [HttpGet("/delete/{ActivityId}")]
        public IActionResult DeleteActivity(int activityId)
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Register", "Home");
            }

            Models.Activity DeleteActivity = dbContext.Activities
            .FirstOrDefault(a => a.ActivityId == activityId);

            dbContext.Activities.Remove(DeleteActivity);
            dbContext.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        // Join Activity
        [HttpGet("/join/{ActivityId}/")]
        public IActionResult Join(int activityId)
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Register", "Home");
            }

            JoinActivity newJoin = new JoinActivity
            {
                UserId = ActiveUser.UserId,
                ActivityId = activityId
            };
            dbContext.Add(newJoin);
            dbContext.SaveChanges();

            return RedirectToAction("Dashboard");
        }
        //leave activity
        [HttpGet("/leave/{ActivityId}/")]
        public IActionResult Leave(int activityId)
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Register", "Home");
            }

            JoinActivity remove = dbContext.JoinActivities
            .FirstOrDefault(j => j.ActivityId == activityId && j.UserId == (int)ActiveUser.UserId);

            dbContext.Remove(remove);
            dbContext.SaveChanges();

            return RedirectToAction("Dashboard");
        }
    }
}
