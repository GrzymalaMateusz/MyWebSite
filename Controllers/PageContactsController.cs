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
using PagedList;

namespace MyWebsite.Controllers
{
    public class PageContactsController : Controller
    {
        private MyAppDbContext db = new MyAppDbContext();

        // GET: PageContacts
        [Authorize]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) ? "Date" : "";
            ViewBag.NameSortParm = sortOrder == "Name" ? "Name_desc" : "Name";
            ViewBag.EmailSortParm = sortOrder == "Email" ? "Email_desc" : "Email";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var message = from m in db.PageContacts
                     select m;
            message = message.OrderBy(s => s.Date);
            if (!String.IsNullOrEmpty(searchString))
            {
                message = message.Where(s => s.Name.Contains(searchString)
                                       || s.Mail.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Name_desc":
                    message = message.OrderByDescending(s => s.Name);
                    break;
                case "Name":
                    message = message.OrderBy(s => s.Name);
                    break;
                case "Date":
                    message = message.OrderBy(s => s.Date);
                    break;
                case "Email":
                    message = message.OrderBy(s => s.Mail);
                    break;
                case "Email_desc":
                    message = message.OrderByDescending(s => s.Mail);
                    break;
                default:
                    message = message.OrderByDescending(s => s.Date);
                    break;
            }
            int pageSize = 15;
            int pageNumber = (page ?? 1);
            return View(message.ToPagedList(pageNumber, pageSize));
        }

        // GET: PageContacts/Details/5
        [Authorize]
        [HttpPost]
        public JsonResult Details(int? id)
        {
            if (id == null)
            {
                return Json("Error");
            }
            PageContact pageContact = db.PageContacts.Find(id);
            if (pageContact == null)
            {
                return Json("Error");
            }
            return Json(pageContact);
        }    

        // POST: PageContacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        public JsonResult DeleteConfirmed(int id)
        {
            PageContact pageContact = db.PageContacts.Find(id);
            if (pageContact == null)
            {
                return Json("Error");
            }
            db.PageContacts.Remove(pageContact);
            db.SaveChanges();
            return Json("Deleted");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
