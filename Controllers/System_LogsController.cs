using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyWebsite.DAL;
using MyWebsite.Models;

namespace MyWebsite.Controllers
{
    public class System_LogsController : Controller
    {
        private MyAppDbContext db = new MyAppDbContext();

        // GET: System_Logs
        public ActionResult Index()
        {
            return View(db.System_Logs.ToList());
        }

        // GET: System_Logs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            System_Logs system_Logs = db.System_Logs.Find(id);
            if (system_Logs == null)
            {
                return HttpNotFound();
            }
            return View(system_Logs);
        }
    }
}
