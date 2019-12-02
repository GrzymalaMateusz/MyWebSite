using GoogleAnalyticsTracker.MVC5;
using MyWebsite.DAL;
using MyWebsite.Models;
using MyWebsite.Models.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyWebsite.Controllers
{
    public class HomeController : Controller
    {
        private MyAppDbContext db = new MyAppDbContext();
        [ActionTracking("UA-128209449-1")]
        public ActionResult Index()
        {
            return View();
        }
        
        [ActionTracking("UA-128209449-1")]
        [Route("CV")]
        public ActionResult About()
        {
            int id = 1;
            CV cV = db.CV.Find(id);
            if (cV == null)
            {
                cV = new CV()
                {
                    Id = 1,
                    ForName = "",
                    SurName = "",
                    Photo = "",
                    Description = "",
                    Jobs = new List<CVJobs>(),
                    School = new List<CVSchool>(),
                    Skill = new List<CVSkills>()
                };
                db.CV.Add(cV);
                db.SaveChanges();
            }
            return View(cV);
        }
        [ActionTracking("UA-128209449-1")]
        [Route("Portfolio")]
        public ActionResult Portfolio ()
        {
            return View(db.Portfolios.ToList());
        }
        [ActionTracking("UA-128209449-1")]
        [Route("Kontakt")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public JsonResult PageContact([Bind(Include = "Id,Name,Mail,Phone,Text")] Models.PageContact pageContact)
        {
            if (ModelState.IsValid)
            {
                pageContact.Date = DateTime.Now;
                pageContact.Ip = (Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ??
                Request.ServerVariables["REMOTE_ADDR"]).Split(',')[0].Trim();
                UpdateModel(pageContact);
                Notification n = new Notification()
                {
                    Date = DateTime.Now,
                    Type = Notification_type.mail,
                    Url = Url.Action("Index","PageContacts"),
                    Name = "Nowa wiadomość w skrzyńce pocztowej",
                };
                db.Notifications.Add(n);
                foreach(var item in db.Users.Where(p => p.UserType == Models.UserType.Admin||p.UserType==UserType.Mod).ToList())
                {
                    item.User_Notification.Add(n);
                }
                db.PageContacts.Add(pageContact);
                db.SaveChanges();
                return Json("Sended");
            }

            return Json("Error");
        }

    }
}