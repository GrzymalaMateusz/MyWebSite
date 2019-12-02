using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyWebsite.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        [Route("panel-administratora")]
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        // GET: Admin
        [Route("Admin")]
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Menu()
        {
            DAL.MyAppDbContext db = new DAL.MyAppDbContext();
            Models.User u = db.Users.ToList().Find(x=>x.Email==User.Identity.Name);
            return PartialView("_AdminMenu", u);
        }
    }
}