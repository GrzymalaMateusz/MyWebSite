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
    public class Newsletter_SubscribersController : Controller
    {
        private MyAppDbContext db = new MyAppDbContext();

        // GET: Newsletter_Subscribers
        public ActionResult Index()
        {
            return View(db.Newsletters_Subscribers.ToList());
        }

        // GET: Newsletter_Subscribers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Newsletter_Subscribers newsletter_Subscribers = db.Newsletters_Subscribers.Find(id);
            if (newsletter_Subscribers == null)
            {
                return HttpNotFound();
            }
            return View(newsletter_Subscribers);
        }

        // GET: Newsletter_Subscribers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Newsletter_Subscribers/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Email,Date")] Newsletter_Subscribers newsletter_Subscribers)
        {
            if (ModelState.IsValid)
            {
                db.Newsletters_Subscribers.Add(newsletter_Subscribers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(newsletter_Subscribers);
        }

        // GET: Newsletter_Subscribers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Newsletter_Subscribers newsletter_Subscribers = db.Newsletters_Subscribers.Find(id);
            if (newsletter_Subscribers == null)
            {
                return HttpNotFound();
            }
            return View(newsletter_Subscribers);
        }

        // POST: Newsletter_Subscribers/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,Date")] Newsletter_Subscribers newsletter_Subscribers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(newsletter_Subscribers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(newsletter_Subscribers);
        }

        // GET: Newsletter_Subscribers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Newsletter_Subscribers newsletter_Subscribers = db.Newsletters_Subscribers.Find(id);
            if (newsletter_Subscribers == null)
            {
                return HttpNotFound();
            }
            return View(newsletter_Subscribers);
        }

        // POST: Newsletter_Subscribers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Newsletter_Subscribers newsletter_Subscribers = db.Newsletters_Subscribers.Find(id);
            db.Newsletters_Subscribers.Remove(newsletter_Subscribers);
            db.SaveChanges();
            return RedirectToAction("Index");
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
