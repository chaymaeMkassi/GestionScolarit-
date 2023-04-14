using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using GestionScolarité.Data;
using GestionScolarité.Models;

namespace GestionScolarité.Controllers
{
    [Authorize(Roles = "administrator")]
    public class UsersController : Controller
    {
        private MyDbContext db = new MyDbContext();

        [AllowAnonymous]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("index", "Home");
        }

        // GET: Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        // GET: Login
        public ActionResult Login(User user, string returnUrl)
        {
            User u = db.Users.FirstOrDefault(item => item.Email == user.Email && item.Password == user.Password);
            if (u != null)
            {
                FormsAuthentication.SetAuthCookie(u.Id.ToString(), false);
                /*if (returnUrl != null)
                    return Redirect(returnUrl);*/
                if (u.Role == Role.administrator)
                    return RedirectToAction("home", "Administrators");
                else if (u.Role == Role.student)
                {
                    Student s = db.Students.FirstOrDefault(item => item.Email == user.Email && item.Password == user.Password);
                    if(s != null)
                    {
                        if (s.Status == StudentStatus.Rejected)
                        {
                            return RedirectToAction("home2", "Students");
                        }
                        else if (s.Status == StudentStatus.Submitted)
                        {
                            return RedirectToAction("home", "Students");
                        }
                    }
                    
                    return RedirectToAction("index", "home");

                }
                else if (u.Role == Role.director)
                    return RedirectToAction("home", "Directors");
                else if (u.Role == null)
                    return RedirectToAction("home2", "Students");
                else if (u.Role == Role.teacher)
                    return RedirectToAction("home", "Teachers");
                else
                    return RedirectToAction("index", "Home");
            }
            else
            {
                ViewBag.msgerror = "Error, Username or password is incorrect!";
                return View();
            }
        }

        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        [AllowAnonymous]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Email,Password,Role")] User user)
        {
            if (ModelState.IsValid)
            {
                if(user.Role == Role.student)
                {
                    Student item = new Student();
                    item.FirstName = user.FirstName;
                    item.LastName = user.LastName;
                    item.Email = user.Email;
                    item.Password= user.Password;
                    item.Role= Role.student;
                    item.Id= user.Id;
                    item.Status = StudentStatus.Rejected;
                    item.Role = null;
                    db.Students.Add(item);
                }
                if (user.Role == Role.teacher)
                {
                    Teacher item = new Teacher();
                    item.FirstName = user.FirstName;
                    item.LastName = user.LastName;
                    item.Email = user.Email;
                    item.Password = user.Password;
                    item.Role = Role.teacher;
                    item.Id = user.Id;
                    db.Teachers.Add(item);
                }
                if (user.Role == Role.director)
                {
                    Director item = new Director();
                    item.FirstName = user.FirstName;
                    item.LastName = user.LastName;
                    item.Email = user.Email;
                    item.Password = user.Password;
                    item.Role = Role.director;
                    item.Id = user.Id;
                    db.Directors.Add(item);
                }
                if (user.Role == Role.administrator)
                {
                    Administrator item = new Administrator();
                    item.FirstName = user.FirstName;
                    item.LastName = user.LastName;
                    item.Email = user.Email;
                    item.Password = user.Password;
                    item.Role = Role.administrator;
                    item.Id = user.Id;
                    db.Administrators.Add(item);
                }
                //db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        [AllowAnonymous]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Email,Password,Role")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }


        // GET: Users/Delete/5
        [AllowAnonymous]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [AllowAnonymous]
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
