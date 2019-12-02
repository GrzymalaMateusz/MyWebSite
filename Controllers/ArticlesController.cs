using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using GoogleAnalyticsTracker.MVC5;
using MyWebsite.DAL;
using MyWebsite.Models;
using PagedList;

namespace MyWebsite.Controllers
{
    public class ArticlesController : Controller
    {
        private MyAppDbContext db = new MyAppDbContext();

        // GET: Articles
        [Authorize]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) ? "Date" : "";
            ViewBag.TitleSortParm = sortOrder == "Title" ? "Title_desc" : "Title";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            var ar = from a in db.Articles
                     select a;
            ar = ar.OrderBy(s => s.PublishDate);
            if (!String.IsNullOrEmpty(searchString))
            {
                ar = ar.Where(s => s.Title.Contains(searchString)
                                       || s.Tags.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Title_desc":
                    ar = ar.OrderByDescending(s => s.Title);
                    break;
                case "Title":
                    ar = ar.OrderBy(s => s.Title);
                    break;
                case "Date":
                    ar = ar.OrderBy(s => s.PublishDate);
                    break;
                default:
                    ar = ar.OrderByDescending(s => s.PublishDate);
                    break;
            }
            int pageSize = 15;
            int pageNumber = (page ?? 1);
            return View(ar.ToPagedList(pageNumber, pageSize));
        }
        [ActionTracking("UA-128209449-1")]
        [Route("Aktualności/{cat}/{page}", Name = "ArticlesCat")]
        [Route("Aktualności/{page}", Name = "Articles")]
        public ActionResult Articles(string cat, string search, int? page)
        {


            var ar = from a in db.Articles
                     select a;
            ar = ar.Where(p => p.PublishDate.CompareTo(DateTime.Now) == -1);
            ar = ar.OrderBy(s => s.PublishDate);
            if (!String.IsNullOrEmpty(cat))
            {
                ar = ar.Where(p => p.Category.CategoryName == cat);
            }
            if (!String.IsNullOrEmpty(search))
            {
                ar = ar.Where(s => s.Tags.Contains(search) || s.Title.Contains(search));
            }
            ar.OrderByDescending(s => s.PublishDate);
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            return View(ar.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult PopularArticles()
        {
            var ar = from a in db.Articles
                     select a;
            ar = ar.Where(p => p.PublishDate.CompareTo(DateTime.Now) == -1);
            ar = ar.OrderByDescending(s => s.Views);
            ar = ar.Take(5);
            return PartialView(ar);
        }

        // GET: Articles/Details/5
        [ActionTracking("UA-128209449-1")]
        [Route("Aktualności-{id}/{titl}")]
        public ActionResult Article(string titl, int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            article.Views += 1;
            UpdateModel(article);
            db.SaveChanges();
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }
        // GET: Articles/Details/5
        [HttpPost]
        public JsonResult Comment(int ida, string CommentName, string CommentText)
        {
            ArticleComment comment = new ArticleComment()
            {
                Text = CommentText,
                CreateDate = DateTime.Now,
                CreateIp = (Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ??
                Request.ServerVariables["REMOTE_ADDR"]).Split(',')[0].Trim(),
                ModDate = DateTime.Now,
                ModIp = (Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ??
                Request.ServerVariables["REMOTE_ADDR"]).Split(',')[0].Trim(),
                IsShow = true,
                Reported = false
            };
            Article article = db.Articles.Find(ida);
            if (Request.IsAuthenticated)
            {
                comment.User = db.Users.Single(p => p.Email == User.Identity.Name);
                comment.Name = null;
            }
            else
            {
                comment.Name = CommentName;
            }
            UpdateModel(comment);
            article.Comments.Add(db.ArticleComment.Add(comment));
            UpdateModel(article);
            db.SaveChanges();
            if (article == null)
            {
                return Json("Error");
            }
            return Json(comment);
        }
        [HttpPost]
        public JsonResult ReportComment(int id_c, string text_reason)
        {
            ArticleComment comment = db.ArticleComment.Find(id_c);
            if (comment == null)
            {
                return Json("Error");
            }
            comment.Reported = true;
            comment.Reported_reason = text_reason;
            comment.Reported_ip = (Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ??
                Request.ServerVariables["REMOTE_ADDR"]).Split(',')[0].Trim();
            Notification n = new Notification()
            {
                Date = DateTime.Now,
                Type = Notification_type.comment,
                Name = "Zgłoszono komentarz",
                Url = Url.Action("")

            };
            n = db.Notifications.Add(n);
            foreach (var item in db.Users.Where(p => p.UserType == Models.UserType.Admin || p.UserType == UserType.Mod).ToList())
            {
                item.User_Notification.Add(n);
            }
            db.SaveChanges();

            return Json("Reported");
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            UpdateModel(article);
            db.SaveChanges();
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
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
                    httpPostedFile.SaveAs(Server.MapPath("~/Images/Article/Gallery/") + Filename);
                    return Json(Filename);
                }
            }
            return Json("Error");
        }
        [HttpPost]
        public ActionResult RemoveImage(string name)
        {

            if ((System.IO.File.Exists(HttpContext.Server.MapPath("~/Images/Article/Gallery/") + name)))
            {
                System.IO.File.Delete(HttpContext.Server.MapPath("~/Images/Article/Gallery/") + name);
                return Json("PDeleted");
            }
            return Json("Error");
        }
        // GET: Articles/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.stringofphoto = "";
            ViewBag.categorieslist = new SelectList(db.ArticleCategories.ToList(), "Id", "CategoryName");
            return View();
        }

        // POST: Articles/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Text,Tags,PublishDate")] Article article, string stringofphoto, int CatId = 0)
        {


            try
            {
                article.Photos = new List<Photo>();
                article.Comments = new List<ArticleComment>();
                //Thumb
                HttpPostedFileBase uploadedFile = Request.Files["Thumbs"];
                if (uploadedFile.ContentLength > 0)
                {
                    var Filename = System.Guid.NewGuid().ToString() + "." + uploadedFile.FileName.Split('.')[1].ToString();
                    article.Thumb = Filename;
                    uploadedFile.SaveAs(HttpContext.Server.MapPath("~/Images/Article/") + Filename);

                }
                //Thumb end
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
                            article.Photos.Add(p);
                            db.SaveChanges();
                        }
                    }
                }
                article.Views = 0;
                article.User = db.Users.Single(p => p.Email == User.Identity.Name);
                article.CreateDate = DateTime.Now;
                if (article.PublishDate == null)
                {
                    article.PublishDate = DateTime.Now;
                }
                article.CreateIp = (Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ??
                    Request.ServerVariables["REMOTE_ADDR"]).Split(',')[0].Trim();
                article.ModDate = DateTime.Now;
                article.ModIp = (Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ??
                    Request.ServerVariables["REMOTE_ADDR"]).Split(',')[0].Trim();
                if (CatId != 0)
                {
                    article.Category = db.ArticleCategories.Find(CatId);
                }
                else
                {
                    article.Category = null;
                }
                UpdateModel(article);
                db.Articles.Add(article);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception exc)
            {
                ViewBag.stringofphoto = stringofphoto;
                ViewBag.categorieslist = new SelectList(db.ArticleCategories.ToList(), "Id", "CategoryName");
                return View(article);
            }
        }

        // GET: Articles/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article.Category == null)
            {
                ViewBag.categories = new SelectList(db.ArticleCategories.ToList(), "Id", "CategoryName");
            }
            else
            {
                ViewBag.categories = new SelectList(db.ArticleCategories.ToList(), "Id", "CategoryName", article.Category.Id);
            }
            if (article == null)
            {
                return HttpNotFound();
            }
            ViewBag.stringofphoto = "";
            return View(article);
        }

        // POST: Articles/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Text,Tags,PublishDate")] Article article, string stringofphoto, int CategoryId = 0)
        {
            try
            {
                Article t = db.Articles.Find(article.Id);
                HttpPostedFileBase uploadedFile = Request.Files["Thumbs"];
                if (uploadedFile.ContentLength > 0)
                {
                    if ((System.IO.File.Exists(HttpContext.Server.MapPath("~/Images/Article/") + t.Thumb)))
                    {
                        System.IO.File.Delete(HttpContext.Server.MapPath("~/Images/Article/") + t.Thumb);
                    }
                    var Filename = System.Guid.NewGuid().ToString() + "." + uploadedFile.FileName.Split('.')[1].ToString();
                    article.Thumb = Filename;
                    uploadedFile.SaveAs(HttpContext.Server.MapPath("~/Images/Article/") + Filename);

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
                t.ModDate = DateTime.Now;
                t.ModIp = (Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ??
                    Request.ServerVariables["REMOTE_ADDR"]).Split(',')[0].Trim();
                if (t.PublishDate == null)
                {
                    t.PublishDate = DateTime.Now;
                }
                if (CategoryId != 0)
                {
                    t.Category = db.ArticleCategories.Find(CategoryId);
                }
                else
                {
                    t.Category = null;
                }
                UpdateModel(t);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception exc)
            {
                if (article.Category == null)
                {
                    ViewBag.categories = new SelectList(db.ArticleCategories.ToList(), "Id", "CategoryName");
                }
                else
                {
                    ViewBag.categories = new SelectList(db.ArticleCategories.ToList(), "Id", "CategoryName", article.Category.Id);
                }
                ViewBag.stringofphoto = stringofphoto;
                return View(article);
            }
        }

        // GET: Articles/Delete/5
        [HttpPost]
        public JsonResult DeletePhoto(int? id, int ph)
        {
            Photo photo = db.Articles.Find(id).Photos.SingleOrDefault(s => s.Id == ph);
            if (photo == null)
            {
                return Json("Error");
            }
            db.Articles.Find(id).Photos.Remove(photo);
            if ((System.IO.File.Exists(HttpContext.Server.MapPath("~/Images/Article/Gallery/") + photo.Path)))
            {
                System.IO.File.Delete(HttpContext.Server.MapPath("~/Images/Article/Gallery/") + photo.Path);
            }
            db.SaveChanges();
            return Json("PDeleted");
        }
        [HttpPost]
        public JsonResult ShowComment(int? id)
        {
            ArticleComment ac = db.ArticleComment.Find(id);
            if (ac == null)
            {
                return Json("Error");
            }
            ac.IsShow = true;
            db.SaveChanges();
            return Json("Showed");
        }
        [HttpPost]
        public JsonResult HideComment(int? id)
        {
            ArticleComment ac = db.ArticleComment.Find(id);
            if (ac == null)
            {
                return Json("Error");
            }
            ac.IsShow = false;
            db.SaveChanges();
            return Json("Hided");
        }
        [HttpPost]
        public JsonResult UnreportComment(int? id)
        {
            ArticleComment ac = db.ArticleComment.Find(id);
            if (ac == null)
            {
                return Json("Error");
            }
            ac.Reported = false;
            db.SaveChanges();
            return Json("Unreport");
        }
        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        public JsonResult DeleteConfirmed(int id)
        {
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return Json("Error");
            }
            int count = article.Photos.Count-1;
            for (int i = count; i >= 0; i--)
            {

                var p = db.Photos.Remove(article.Photos.ElementAt(i));
                if ((System.IO.File.Exists(HttpContext.Server.MapPath("~/Images/Article/Gallery/") + p.Path)))
                {
                    System.IO.File.Delete(HttpContext.Server.MapPath("~/Images/Article/Gallery/") + p.Path);
                }
            }
            if (article.Thumb != null)
            {
                if ((System.IO.File.Exists(HttpContext.Server.MapPath("~/Images/Article/") + article.Thumb)))
                {
                    System.IO.File.Delete(HttpContext.Server.MapPath("~/Images/Article/") + article.Thumb);
                }
            }
            foreach (ArticleComment item in article.Comments)
            {
                db.ArticleComment.Remove(item);
            }
            db.Articles.Remove(article);
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
