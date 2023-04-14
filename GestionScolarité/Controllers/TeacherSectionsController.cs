using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GestionScolarité.Data;
using GestionScolarité.Models;

namespace GestionScolarité.Controllers
{
    public class TeacherSectionsController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: TeacherSections
        public ActionResult Index()
        {
            var teacherSections = db.TeacherSections.Include(t => t.Section).Include(t => t.Teacher);
            return View(teacherSections.ToList());
        }

        // GET: TeacherSections/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeacherSection teacherSection = db.TeacherSections.Find(id);
            if (teacherSection == null)
            {
                return HttpNotFound();
            }
            return View(teacherSection);
        }

        // GET: TeacherSections/Create
        public ActionResult Create()
        {
            ViewBag.SectionId = new SelectList(db.Sections, "Id", "SectionName");
            ViewBag.TeacherId = new SelectList(db.Users, "Id", "FirstName");
            return View();
        }

        // POST: TeacherSections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TeacherId,SectionId")] TeacherSection teacherSection)
        {
            if (ModelState.IsValid)
            {
                db.TeacherSections.Add(teacherSection);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SectionId = new SelectList(db.Sections, "Id", "SectionName", teacherSection.SectionId);
            ViewBag.TeacherId = new SelectList(db.Users, "Id", "FirstName", teacherSection.TeacherId);
            return View(teacherSection);
        }

        // GET: TeacherSections/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeacherSection teacherSection = db.TeacherSections.Find(id);
            if (teacherSection == null)
            {
                return HttpNotFound();
            }
            ViewBag.SectionId = new SelectList(db.Sections, "Id", "SectionName", teacherSection.SectionId);
            ViewBag.TeacherId = new SelectList(db.Users, "Id", "FirstName", teacherSection.TeacherId);
            return View(teacherSection);
        }

        // POST: TeacherSections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TeacherId,SectionId")] TeacherSection teacherSection)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teacherSection).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SectionId = new SelectList(db.Sections, "Id", "SectionName", teacherSection.SectionId);
            ViewBag.TeacherId = new SelectList(db.Users, "Id", "FirstName", teacherSection.TeacherId);
            return View(teacherSection);
        }

        // GET: TeacherSections/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeacherSection teacherSection = db.TeacherSections.Find(id);
            if (teacherSection == null)
            {
                return HttpNotFound();
            }
            return View(teacherSection);
        }

        // POST: TeacherSections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TeacherSection teacherSection = db.TeacherSections.Find(id);
            db.TeacherSections.Remove(teacherSection);
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
