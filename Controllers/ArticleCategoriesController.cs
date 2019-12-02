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
    public class ArticleCategoriesController : Controller
    {
        private MyAppDbContext db = new MyAppDbContext();

        // GET: ArticleCategories
        public ActionResult Index()
        {
            return View(db.ArticleCategories.ToList());
        }
        public ActionResult Categories()
        {
            return PartialView(db.ArticleCategories.ToList());
        }
  

        // POST: ArticleCategories/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,CategoryName")] ArticleCategory articleCategory)
        {
            if (ModelState.IsValid)
            {
                ArticleCategory ar = db.ArticleCategories.Add(articleCategory);
                db.SaveChanges();
                return Json(new { text = "Added", Id = ar.Id });
            }

            return Json("Error");
        }


        // POST: ArticleCategories/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,CategoryName")] ArticleCategory articleCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(articleCategory).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { text = "Added", Id = articleCategory.Id, Name = articleCategory.CategoryName });
            }
            return Json("Error");
        }


        // POST: ArticleCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ArticleCategory articleCategory = db.ArticleCategories.Find(id);
            if (articleCategory == null)
            {
                return Json("Error");
            }
            db.ArticleCategories.Remove(articleCategory);
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
