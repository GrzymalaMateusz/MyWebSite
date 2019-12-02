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
    public class ChatController : Controller
    {
        private MyAppDbContext db = new MyAppDbContext();

        // GET: Chat_Room
        public ActionResult Index()
        {
            var chat = from ch in db.Chat_Room
                       select ch;
            chat = chat.Where(s => s.Users.Any(p => p.User.Email == User.Identity.Name));
            return View(chat.ToList());
        }
        public ActionResult Meesages()
        {
            var chat = from ch in db.Chat_Room
                       select ch;
            chat = chat.Where(s => s.Users.Any(p => p.User.Email == User.Identity.Name));
            return PartialView(chat.ToList().OrderByDescending(x=>x.LastUpdate));
        }
        [HttpPost]
        public JsonResult AddFile()
        {
            string[] images = { "png", "jpg", "gif", "svg" };
            var mess = "";
            Files f= new Files();
            HttpPostedFileBase uploadedFile = Request.Files["File"];
            var fileName1 = uploadedFile.FileName;
            if (uploadedFile.ContentLength > 0)
            {
                fileName1 = System.Guid.NewGuid().ToString() + "." + uploadedFile.FileName.Split('.')[1].ToString();
                f.Path ="/Files/Chat/" + fileName1;
                f.Name = uploadedFile.FileName;
                db.Files.Add(f);
                db.SaveChanges();
                uploadedFile.SaveAs(Server.MapPath("~/Files/Chat/" + fileName1));
            }
            else
            {
                return Json("Error");
            }
            if (images.Contains(uploadedFile.FileName.Split('.')[1].ToString()))
            {
                mess = "<a href=\""+f.Path+"\"><div class=\"chat-message-image\" style=\"background-image:url('"+f.Path+"')\"></div></a>";
            }
            else
            {
                mess = "<a class='chat-message-file-"+ uploadedFile.FileName.Split('.')[1].ToString() + "' href='" + f.Path + "'>" + f.Name + "</a>";
            }


            return Json(mess);
        }
        // GET: Chat_Room/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chat_Room chat_Room = db.Chat_Room.Find(id);
            Chat_Users user = chat_Room.Users.Single(s => s.User.Email == User.Identity.Name);
            user.LastView = DateTime.Now;
            db.SaveChanges();
            if (chat_Room == null)
            {
                return HttpNotFound();
            }
            return View(chat_Room);
        }
        [HttpPost]
        public ActionResult Details(int idr, string Text)
        {
            Chat_Room chat_Room = db.Chat_Room.Find(idr);
            Chat_Users user = chat_Room.Users.Single(s => s.User.Email == User.Identity.Name);
            Chat_Message message = new Chat_Message()
            {
                Text = Text,
                User = user
            };
            message = db.Chat_Message.Add(message);
            chat_Room.Messages.Add(message);
            var date = DateTime.Now;
            user.LastView = date;
            chat_Room.LastUpdate = date;
            db.SaveChanges();
            return View(chat_Room);
        }

        // GET: Chat_Room/Create
        public ActionResult Create(int id)
        {
            var chat = from ch in db.Chat_Room
                       select ch;
            var user = db.Users.Single(p => p.Email == User.Identity.Name);
            var user1 = db.Users.Single(p => p.Id == id);
            chat = chat.Where(s => s.Users.Any(p => p.User.Id == user1.Id));
            chat = chat.Where(s => s.Users.Any(p => p.User.Id == user.Id));
            chat = chat.Where(s => s.Users.Count == 2);
            if (chat.ToList().Count == 1)
            {
                return View("Details", chat.First());
            }
            else
            {
                var date = DateTime.Now;
                Chat_Users u1 = new Chat_Users()
                {
                    User = user,
                    LastView = date
                };

                Chat_Users u2 = new Chat_Users()
                {
                    User = user1,
                    LastView = date
                };
                Chat_Room room = new Chat_Room()
                {
                    LastUpdate = date,
                    Messages = new List<Chat_Message>(),
                    Users = new List<Chat_Users>()
                };
                u1 = db.Chat_Users.Add(u1);
                u2 = db.Chat_Users.Add(u2);
                room = db.Chat_Room.Add(room);
                room.Users.Add(u1);
                room.Users.Add(u2);
                UpdateModel(room);
                db.SaveChanges();
                return Redirect(Url.Action("Details", new { id = chat.First().ID }));

            }
        }

        // GET: Chat_Room/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chat_Room chat_Room = db.Chat_Room.Find(id);
            if (chat_Room == null)
            {
                return HttpNotFound();
            }
            return View(chat_Room);
        }

        // POST: Chat_Room/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,LastUpdate")] Chat_Room chat_Room)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chat_Room).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(chat_Room);
        }

        // GET: Chat_Room/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chat_Room chat_Room = db.Chat_Room.Find(id);
            if (chat_Room == null)
            {
                return HttpNotFound();
            }
            return View(chat_Room);
        }

        // POST: Chat_Room/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Chat_Room chat_Room = db.Chat_Room.Find(id);
            db.Chat_Room.Remove(chat_Room);
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
