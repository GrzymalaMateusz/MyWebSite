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
    public class PortfoliosController : Controller
    {
        private MyAppDbContext db = new MyAppDbContext();

        // GET: Portfolios
        public ActionResult Index()
        {
            return View(db.Portfolios.ToList());
        }

        // GET: Portfolios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Portfolio portfolio = db.Portfolios.Find(id);
            if (portfolio == null)
            {
                return HttpNotFound();
            }
            return View(portfolio);
        }

        // GET: Portfolios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Portfolios/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,WorkRange")] Portfolio portfolio)
        {
            try
            {
                HttpPostedFileBase uploadedFile = Request.Files["PhotoP"];
                if (uploadedFile.ContentLength > 0)
                {
                    var Filename = System.Guid.NewGuid().ToString() + "." + uploadedFile.FileName.Split('.')[1].ToString();
                    portfolio.Photo = Filename;
                    uploadedFile.SaveAs(HttpContext.Server.MapPath("~/Images/Portfolio/") + Filename);

                }
                db.Portfolios.Add(portfolio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception exc)
            {
                return View(portfolio);
            }
        }

        // GET: Portfolios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Portfolio portfolio = db.Portfolios.Find(id);
            if (portfolio == null)
            {
                return HttpNotFound();
            }
            return View(portfolio);
        }

        // POST: Portfolios/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,WorkRange")] Portfolio portfolio)
        {
            try
            {
                Portfolio p = db.Portfolios.Find(portfolio.Id);
                HttpPostedFileBase uploadedFile = Request.Files["PhotoP"];
                if (uploadedFile.ContentLength > 0)
                {
                    if ((System.IO.File.Exists(HttpContext.Server.MapPath("~/Images/Portfolio/") + p.Photo)))
                    {
                        System.IO.File.Delete(HttpContext.Server.MapPath("~/Images/Portfolio/") + p.Photo);
                    }
                    var Filename = System.Guid.NewGuid().ToString() + "." + uploadedFile.FileName.Split('.')[1].ToString();
                    p.Photo = Filename;
                    uploadedFile.SaveAs(HttpContext.Server.MapPath("~/Images/Portfolio/") + Filename);

                }
                UpdateModel(p);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception exc)
            {
                return View(portfolio);
            }
        }

        // POST: Portfolios/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {

            Portfolio portfolio = db.Portfolios.Find(id);
            if (portfolio == null)
            {
                return Json("Error");
            }
            if (portfolio.Photo != null)
            {
                if ((System.IO.File.Exists(HttpContext.Server.MapPath("~/Images/Portfolio/") + portfolio.Photo)))
                {
                    System.IO.File.Delete(HttpContext.Server.MapPath("~/Images/Portfolio/") + portfolio.Photo);
                }
            }
            db.Portfolios.Remove(portfolio);
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
