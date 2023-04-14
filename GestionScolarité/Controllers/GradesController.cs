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
    public class GradesController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: Grades
        public ActionResult Index()
        {
            var grades = db.Grades.Include(g => g.Student).Include(g => g.Subject);
            return View(grades.ToList());
        }

        // GET: Grades/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grade grade = db.Grades.Find(id);
            if (grade == null)
            {
                return HttpNotFound();
            }
            return View(grade);
        }

        // GET: Grades/Create
        public ActionResult Create()
        {
            //IEnumerable<Student> students=
            IEnumerable<TeacherSubject> Tsubjects = db.TeacherSubjects.Where(item => item.TeacherId.ToString() == User.Identity.Name);
            
            var query = from subject in db.Subjects
                        join teacherSubjects in db.TeacherSubjects
                        on subject.Id equals teacherSubjects.SubjectId
                        
                        select new Subject
                        {
                            Id = subject.Id,
                            SubjectName = subject.SubjectName
                           
                        };
            /*db.Students.Join(db.Departments,s => s.DepartmentId,d => d.DepartmentId,
                (s, d) => new Student
                {
                    StudentId = s.StudentId,
                    Name = s.Name,
                    DepartmentId = d.DepartmentId,
                    Department = d
                })*/
            IEnumerable<Subject> Subject = db.Subjects.ToList().Join(db.TeacherSubjects,t=> t.Id,s=>s.SubjectId,(t,s)=>new Subject
            {
                Id = t.Id,
                SubjectName = t.SubjectName

            });
            IEnumerable<Student> students = db.Students.ToList().Join(db.TeacherSections, t => t.Id, s => s.SectionId, (t, s) => new Student
            {
                Id = t.Id,
                FirstName = t.FirstName,
                LastName = t.LastName
    
            });
            ViewBag.StudentId = new SelectList(students, "Id", "FirstName");
            ViewBag.SubjectId = new SelectList(Subject, "Id", "SubjectName");
            return View();
        }

        // POST: Grades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Mark,SubjectId,StudentId")] Grade grade)
        {
            if (ModelState.IsValid)
            {
                db.Grades.Add(grade);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StudentId = new SelectList(db.Users, "Id", "FirstName", grade.StudentId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "SubjectName", grade.SubjectId);
            return View(grade);
        }

        // GET: Grades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grade grade = db.Grades.Find(id);
            if (grade == null)
            {
                return HttpNotFound();
            }
            ViewBag.StudentId = new SelectList(db.Users, "Id", "FirstName", grade.StudentId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "SubjectName", grade.SubjectId);
            return View(grade);
        }

        // POST: Grades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Mark,SubjectId,StudentId")] Grade grade)
        {
            if (ModelState.IsValid)
            {
                db.Entry(grade).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StudentId = new SelectList(db.Users, "Id", "FirstName", grade.StudentId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "SubjectName", grade.SubjectId);
            return View(grade);
        }

        // GET: Grades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grade grade = db.Grades.Find(id);
            if (grade == null)
            {
                return HttpNotFound();
            }
            return View(grade);
        }

        // POST: Grades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Grade grade = db.Grades.Find(id);
            db.Grades.Remove(grade);
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
