using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GoogleAnalyticsTracker.MVC5;
using MyWebsite.DAL;
using MyWebsite.Models;

namespace MyWebsite.Controllers
{
    public class ProductsController : Controller
    {
        private MyAppDbContext db = new MyAppDbContext();

        // GET: Products
        [ActionTracking("UA-128209449-1")]
        [Route("Produkty")]
        public ActionResult Products()
        {
            return View(db.Products.ToList());
        }
        [HttpPost]
        public ActionResult UploadImage()
        {
            if (Request.Files.AllKeys.Any())
            {
                var httpPostedFile = Request.Files["file"];
                if (httpPostedFile != null)
                {
                    var Filename = System.Guid.NewGuid().ToString() + "." + httpPostedFile.FileName.Split('.')[1].ToString();
                    httpPostedFile.SaveAs(Server.MapPath("~/Images/Product/Gallery/") + Filename);
                    return Json(Filename);
                }
            }
            return Json("Error");
        }
        [HttpPost]
        public ActionResult RemoveImage(string name)
        {

            if ((System.IO.File.Exists(HttpContext.Server.MapPath("~/Images/Product/Gallery/") + name)))
            {
                System.IO.File.Delete(HttpContext.Server.MapPath("~/Images/Product/Gallery/") + name);
                return Json("PDeleted");
            }
            return Json("Error");
        }
        // GET: Products
        [Route("Lista-Produktów")]
        [Authorize]
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        [ActionTracking("UA-128209449-1")]
        [Route("Produkt-{id}/{titl}")]
        public ActionResult Product(string titl,int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        [HttpPost]
        [Route("Zamowienie")]
        public ActionResult Order([Bind(Include = "Id,info,Email,Name,Phone,Street,City,ProductId")] Purchase p)
        {
            Product product = db.Products.Find(p.ProductId);
            p.Product = product;
            UpdateModel(p);
            db.Purchases.Add(p);
            db.SaveChanges();
            return Json("Sended");
        }

        // GET: Products/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.stringofphoto = "";
            ViewBag.categorieslist = new SelectList(db.ProductsCategory.ToList(), "Id", "CategoryName");
            return View();
        }

        // POST: Products/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Prine,Demo,Description")] Product product, string stringofphoto,int CatId= 0)
        {
            try
            {
                product.Photos = new List<Photo>();
                HttpPostedFileBase uploadedFile = Request.Files["Thumbs"];
                if (uploadedFile.ContentLength > 0)
                {
                    var Filename = System.Guid.NewGuid().ToString() + "." + uploadedFile.FileName.Split('.')[1].ToString();
                    product.Thumb = Filename;
                    uploadedFile.SaveAs(HttpContext.Server.MapPath("~/Images/Product/") + Filename);

                }
                if (stringofphoto != null)
                {
                    var array = stringofphoto.Split('|');
                    for (int i = 0; i < array.Length; i++)
                    {
                        if (array[i] != "")
                        {
                            Photo p = new Photo()
                            {
                                Path = array[i]
                            };
                            product.Photos.Add(p);
                            db.SaveChanges();
                        }
                    }
                }
                if (CatId != 0)
                {
                    product.Category = db.ProductsCategory.Find(CatId);
                }
                else
                {
                    product.Category = null;
                }
                UpdateModel(product);
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception exc)
            {
                ViewBag.categorieslist = new SelectList(db.ProductsCategory.ToList(), "Id", "CategoryName");
                return View(product);
            }
        }

        // GET: Products/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product.Category == null)
            {
                ViewBag.categories = new SelectList(db.ProductsCategory.ToList(), "Id", "CategoryName");
            }
            else
            {
                ViewBag.categories = new SelectList(db.ProductsCategory.ToList(), "Id", "CategoryName", product.Category.Id);
            }
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Prine,Demo,Description")] Product product, string stringofphoto, int CatId = 0)
        {
            try
            {
                MyAppDbContext ctxt = new MyAppDbContext();
                Product t = ctxt.Products.Single(p => p.Id == product.Id);
                HttpPostedFileBase uploadedFile = Request.Files["Thumbs"];
                if (uploadedFile.ContentLength > 0)
                {
                    if ((System.IO.File.Exists(HttpContext.Server.MapPath("~/Images/Product/") + t.Thumb)))
                    {
                        System.IO.File.Delete(HttpContext.Server.MapPath("~/Images/Product/") + t.Thumb);
                    }
                    var Filename = System.Guid.NewGuid().ToString() + "." + uploadedFile.FileName.Split('.')[1].ToString();
                    product.Thumb = Filename;
                    uploadedFile.SaveAs(HttpContext.Server.MapPath("~/Images/Product/") + Filename);

                }
                if (stringofphoto != null)
                {
                    var array = stringofphoto.Split('|');
                    for (int i = 0; i < array.Length; i++)
                    {
                        if (array[i] != "")
                        {
                            Photo p = new Photo()
                            {
                                Path = array[i]
                            };
                            t.Photos.Add(p);
                            db.SaveChanges();
                        }
                    }
                }
                UpdateModel(t);
                ctxt.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception exc)
            {
                return View(product);
            }
        }

        // GET: Products/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return Json("Error");
            }
            for (int i = 0; i < product.Photos.Count; i++)
            {

                var p = db.Photos.Remove(product.Photos.ElementAt(i));
                if ((System.IO.File.Exists(HttpContext.Server.MapPath("~/Images/Product/Gallery/") + p.Path)))
                {
                    System.IO.File.Delete(HttpContext.Server.MapPath("~/Images/Product/Gallery/") + p.Path);
                }
            }
            if (product.Thumb != null)
            {
                if ((System.IO.File.Exists(HttpContext.Server.MapPath("~/Images/Product/") + product.Thumb)))
                {
                    System.IO.File.Delete(HttpContext.Server.MapPath("~/Images/Product/") + product.Thumb);
                }
            }
            
            db.Products.Remove(product);
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
