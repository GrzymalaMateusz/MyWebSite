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
    public class ProductCategoriesController : Controller
    {
        private MyAppDbContext db = new MyAppDbContext();

        // GET: ArticleCategories
        public ActionResult Index()
        {
            return View(db.ProductsCategory.ToList());
        }
        public ActionResult Categories()
        {
            return PartialView(db.ProductsCategory.ToList());
        }


        // POST: ArticleCategories/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,CategoryName")] ProductsCategory productsCategory)
        {
            if (ModelState.IsValid)
            {
                ProductsCategory pc=db.ProductsCategory.Add(productsCategory);
                db.SaveChanges();
                return Json(new {text="Added",Id=pc.Id });
            }

            return Json("Error");
        }

        // POST: ArticleCategories/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,CategoryName")] ProductsCategory productsCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productsCategory).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { text = "Added", Id = productsCategory.Id,Name = productsCategory.CategoryName});
            }
            return Json("Error");
        }

        // POST: ArticleCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductsCategory productsCategory = db.ProductsCategory.Find(id);
            if (productsCategory == null)
            {
                return Json("Error");
            }
            db.ProductsCategory.Remove(productsCategory);
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
