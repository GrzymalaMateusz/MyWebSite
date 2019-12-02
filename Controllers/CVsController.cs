using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyWebsite.DAL;
using MyWebsite.Models.CV;

namespace MyWebsite.Controllers
{
    public class CVsController : Controller
    {
        private MyAppDbContext db = new MyAppDbContext();

        [HttpPost]
        [Authorize]
        public JsonResult AddSchool([Bind(Include = "Id,Name,Profile,TitleOfThesis")] Models.CV.CVSchool School, string DateStart, string DateEnd)
        {
            if (ModelState.IsValid)
            {
                School.DateStart = DateTime.Parse(DateStart);
                School.DateEnd = DateTime.Parse(DateEnd);
                db.CV.Find(1).School.Add(School);
                var s=db.CVSchools.Add(School);
                db.SaveChanges();
                return Json(new { text = "Sended", id=s.Id});
            }

            return Json("Error");
        }
        [HttpPost]
        [Authorize]
        public JsonResult DeleteSchool(int id)
        {
            CVSchool school = db.CVSchools.Find(id);
            if (school == null)
            {
                return Json("Error");
            }
            db.CVSchools.Remove(school);
            db.SaveChanges();
            return Json("Removed");
        }
        [HttpPost]
        [Authorize]
        public JsonResult AddJob([Bind(Include = "Id,CompanyName,Stand")] Models.CV.CVJobs Job, string DataStart, string DateEnd)
        {
            if (ModelState.IsValid)
            {
                Job.DataStart = DateTime.Parse(DataStart);
                Job.DateEnd = DateTime.Parse(DateEnd);
                db.CV.Find(1).Jobs.Add(Job);
                var s=db.CVJobs.Add(Job);
                db.SaveChanges();
                return Json(new { text = "Sended", id = s.Id });
            }

            return Json("Error");
        }
        [HttpPost]
        [Authorize]
        public JsonResult DeleteJob(int id)
        {
            CVJobs job = db.CVJobs.Find(id);
            if (job == null)
            {
                return Json("Error");
            }
            db.CVJobs.Remove(job);
            db.SaveChanges();
            return Json("Removed");
        }
        [HttpPost]
        [Authorize]
        public JsonResult AddSkill([Bind(Include = "Id,Name")] Models.CV.CVSkills Skill)
        {
            if (ModelState.IsValid)
            {
                db.CV.Find(1).Skill.Add(Skill);
                var s = db.CVSkills.Add(Skill);
                db.SaveChanges();
                return Json(new { text = "Sended", id = s.Id });
            }

            return Json("Error");
        }
        [HttpPost]
        [Authorize]
        public JsonResult DeleteSkill(int id)
        {
            CVSkills skill = db.CVSkills.Find(id);
            if (skill == null)
            {
                return Json("Error");
            }
            db.CVSkills.Remove(skill);
            db.SaveChanges();
            return Json("Removed");
        }
        [HttpPost]
        [Authorize]
        public JsonResult GetSchool(int id)
        {
            var e=db.CVSchools.Find(id);
            if (e == null)
            {
                return Json("Error");
            }
            return Json(e);
        }
        [HttpPost]
        [Authorize]
        public JsonResult GetJob(int id)
        {
            var e = db.CVJobs.Find(id);
            if (e == null)
            {
                return Json("Error");
            }
            return Json(e);
        }
        [HttpPost]
        [Authorize]
        public JsonResult GetSkill(int id)
        {
            var e = db.CVSkills.Find(id);
            if (e == null)
            {
                return Json("Error");
            }
            return Json(e);
        }
        [HttpPost]
        [Authorize]
        public JsonResult EditSchool([Bind(Include = "Id,Name,Profile,TitleOfThesis")] Models.CV.CVSchool School, string DateStart, string DateEnd)
        {
            if (ModelState.IsValid)
            {
                School.DateStart = DateTime.Parse(DateStart);
                School.DateEnd = DateTime.Parse(DateEnd);
                db.CV.Find(1).School.Add(School);
                var s = db.CVSchools.Add(School);
                db.SaveChanges();
                return Json(new { text = "Sended", id = s.Id });
            }
            else
            {
                var errors = "";
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        errors=errors+" "+error.ErrorMessage;
                    }
                }
                return Json(errors);
            }
            //return Json("Błąd");
        }
       
        [HttpPost]
        [Authorize]
        public JsonResult EditJob([Bind(Include = "Id,CompanyName,Stand")] Models.CV.CVJobs Job, string DataStart, string DateEnd)
        {
            if (ModelState.IsValid)
            {
                Job.DataStart = DateTime.Parse(DataStart);
                Job.DateEnd = DateTime.Parse(DateEnd);
                db.CV.Find(1).Jobs.Add(Job);
                var s = db.CVJobs.Add(Job);
                db.SaveChanges();
                return Json(new { text = "Sended", id = s.Id });
            }

            return Json("Error");
        }
        [HttpPost]
        [Authorize]
        public JsonResult EditSkill([Bind(Include = "Id,Name")] Models.CV.CVSkills Skill)
        {
            if (ModelState.IsValid)
            {
                db.CV.Find(1).Skill.Add(Skill);
                var s = db.CVSkills.Add(Skill);
                db.SaveChanges();
                return Json(new { text = "Sended", id = s.Id });
            }

            return Json("Error");
        }


        // GET: CVs/Details
        [Authorize]
        public ActionResult Details()
        {
            int id = 1;
            CV cV = db.CV.Find(id);
            if (cV == null)
            {
                cV = new CV()
                {
                    Id = 1,
                    ForName = "",
                    SurName = "",
                    Photo = "",
                    Description = "",
                    Jobs = new List<CVJobs>(),
                    School = new List<CVSchool>(),
                    Skill = new List<CVSkills>()
                };
                db.CV.Add(cV);
                db.SaveChanges();
            }
            return View(cV);
        }


        // GET: CVs/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CV cV = db.CV.Find(id);
            if (cV == null)
            {
                return HttpNotFound();
            }
            return View(cV);
        }

        // POST: CVs/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ForName,SurName,Description")] CV cV)
        {
            try
            {
                CV c = db.CV.Single(p => p.Id == cV.Id);
                HttpPostedFileBase Photo = Request.Files["Photo1"];
                if (Photo != null && Photo.ContentLength > 0)
                {
                    if ((System.IO.File.Exists(HttpContext.Server.MapPath("~/Images/CV/") + c.Photo)))
                    {
                        System.IO.File.Delete(HttpContext.Server.MapPath("~/Images/CV/") + c.Photo);
                    }
                    var Filename = System.Guid.NewGuid().ToString() + ".jpg";
                    c.Photo = Filename;
                    Photo.SaveAs(HttpContext.Server.MapPath("~/Images/CV/") + Filename);
                }
                UpdateModel(c);
                db.SaveChanges();
                return RedirectToAction("Details");
            }
            catch (Exception exc)
            {
                return View(cV);
            }
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
