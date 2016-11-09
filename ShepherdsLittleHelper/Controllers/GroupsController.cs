using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShepherdsLittleHelper.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ShepherdsLittleHelper.Controllers
{
    public class GroupsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Groups
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                return View(db.Groups.ToList());
            }
            return Redirect("/Home/Index");
        }

        // GET: Groups/Details/5
        public ActionResult Details(int? id)
        {
            if (Request.IsAuthenticated)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Group group = db.Groups.Find(id);
                if (group == null)
                {
                    return HttpNotFound();
                }
                return View(group);
            }
            return RedirectToAction("/Index");
        }

        // GET: Groups/Create
        public ActionResult Create()
        {
            if (Request.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("/Index");
        }

        // POST: Groups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GroupID,GroupName")] Group group)
        {
            if (Request.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    UserManager<User> UserManager = new UserManager<User>(new UserStore<User>(db));
                    User currentUser = UserManager.FindById(User.Identity.GetUserId());
                    db.Groups.Add(group);
                    currentUser.Groups.Add(group);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(group);
            }
            return RedirectToAction("/Index");
        }

        // GET: Groups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Request.IsAuthenticated)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Group group = db.Groups.Find(id);
                if (group == null)
                {
                    return HttpNotFound();
                }
                return View(group);
            }
            return RedirectToAction("/Index");
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GroupID,GroupName")] Group group)
        {
            if (Request.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(group).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(group);
            }
            return RedirectToAction("/Index");
        }

        // GET: Groups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Request.IsAuthenticated)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Group group = db.Groups.Find(id);
                if (group == null)
                {
                    return HttpNotFound();
                }
                return View(group);
            }
            return RedirectToAction("/Index");
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Request.IsAuthenticated)
            {
                Group group = db.Groups.Find(id);
                db.Groups.Remove(group);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("/Index");
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
