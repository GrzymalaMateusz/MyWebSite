using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using MyWebsite.DAL;
using MyWebsite.Models;

namespace MyWebsite.Controllers
{
    public class NewslettersController : Controller
    {
        private MyAppDbContext db = new MyAppDbContext();

        // GET: Newsletters
        public ActionResult Index()
        {
            return View(db.Newsletters.ToList().OrderByDescending(x=>x.Date));
        }


        // GET: Newsletters/Create
        public ActionResult Create()
        {
            return View();
        }
        
        // POST: Newsletters/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Text")] Newsletter newsletter, bool sendNow)
        {
            if (ModelState.IsValid)
            {
                newsletter.Date = DateTime.Now;
                string tmp = newsletter.Text;
                var item=db.Newsletters.Add(newsletter);
                db.SaveChanges();
                if (sendNow == true)
                {
                     item.Send();
                }
                return RedirectToAction("Index");
            }

            return View(newsletter);
        }
        // POST: Newsletters/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public JsonResult Join([Bind(Include = "Id,Email")] Newsletter_Subscribers subscribers)
        {
            if (db.Newsletters_Subscribers.Any(p => p.Email == subscribers.Email))
            {
                return Json("Exist");
            }
            if (ModelState.IsValid)
            {
                subscribers.Date = DateTime.Now;
                subscribers.Token = SHA1.Create(subscribers.Date.ToString()).ToString();
                db.Newsletters_Subscribers.Add(subscribers);
                db.SaveChanges();
                return Json("Added");
            }

            return Json("Error");
        }
        [HttpPost]
        public ActionResult Unsubscribe(String hash)
        {
            if (db.Newsletters_Subscribers.Any(p => p.Token == hash))
            {

                db.Newsletters_Subscribers.Remove(db.Newsletters_Subscribers.SingleOrDefault(p => p.Token == hash));
                return View();
            }
            else
            {  
                return View();
            }
        }
        // GET: Newsletters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Newsletter newsletter = db.Newsletters.Find(id);
            if (newsletter == null)
            {
                return HttpNotFound();
            }
            return View(newsletter);
        }

        // POST: Newsletters/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email")] Newsletter newsletter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(newsletter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(newsletter);
        }

        // GET: Newsletters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Newsletter newsletter = db.Newsletters.Find(id);
            if (newsletter == null)
            {
                return HttpNotFound();
            }
            return View(newsletter);
        }

        // POST: Newsletters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Newsletter newsletter = db.Newsletters.Find(id);
            db.Newsletters.Remove(newsletter);
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
