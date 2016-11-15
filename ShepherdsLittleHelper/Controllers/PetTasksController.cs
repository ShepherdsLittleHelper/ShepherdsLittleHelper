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
    public class PetTasksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PetTasks
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                UserManager<User> UserManager = new UserManager<User>(new UserStore<User>(db));
                User currentUser = UserManager.FindById(User.Identity.GetUserId());
                var petTasks = UserTasks(currentUser);
                return View(petTasks.ToList());
            }
            return Redirect("/Home/Index");
        }

        // GET: PetTasks/Details/5
        public ActionResult Details(int? id)
        {
            if (Request.IsAuthenticated)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                PetTask petTask = db.PetTasks.Find(id);
                if (petTask == null)
                {
                    return HttpNotFound();
                }
                return View(petTask);
            }
            return RedirectToAction("/Index");
        }

        // GET: PetTasks/Create
        public ActionResult Create()
        {
            if (Request.IsAuthenticated)
            {
                ViewBag.ApplicationUserID = new SelectList(db.Users, "Id", "Email");
                UserManager<User> UserManager = new UserManager<User>(new UserStore<User>(db));
                User currentUser = UserManager.FindById(User.Identity.GetUserId());
                var groupIds = currentUser.Groups.Select(g => g.GroupID);
                IEnumerable<Location> locations = db.Locations.Where(l => groupIds.Contains(l.GroupID)).AsEnumerable();
                ViewBag.LocationID = new SelectList(locations, "LocationID", "LocationName");
                IEnumerable<Pet> pets = db.Pets.Where(p => groupIds.Contains(p.Location.GroupID)).AsEnumerable();
                ViewBag.PetID = new SelectList(pets, "PetID", "PetName");
                ViewBag.TaskTypeID = new SelectList(db.TaskTypes, "TaskID", "TaskTypeName");
                return View();
            }
            return RedirectToAction("/Index");
        }

        // POST: PetTasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TaskID,TaskDescription,Frequency,Deadline,PetID,TaskTypeID,ApplicationUserID")] PetTask petTask)
        {
            if (Request.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    db.PetTasks.Add(petTask);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.ApplicationUserID = new SelectList(db.Users, "Id", "Email", petTask.ApplicationUserID);
                UserManager<User> UserManager = new UserManager<User>(new UserStore<User>(db));
                User currentUser = UserManager.FindById(User.Identity.GetUserId());
                var groupIds = currentUser.Groups.Select(g => g.GroupID);
                IEnumerable<Location> locations = db.Locations.Where(l => groupIds.Contains(l.GroupID)).AsEnumerable();
                ViewBag.LocationID = new SelectList(locations, "LocationID", "LocationName");
                IEnumerable<Pet> pets = db.Pets.Where(p => groupIds.Contains(p.Location.GroupID)).AsEnumerable();
                ViewBag.PetID = new SelectList(pets, "PetID", "PetName");
                ViewBag.TaskTypeID = new SelectList(db.TaskTypes, "TaskID", "TaskTypeName", petTask.TaskTypeID);
                return View(petTask);
            }
            return RedirectToAction("/Index");
        }

        // GET: PetTasks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Request.IsAuthenticated)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                PetTask petTask = db.PetTasks.Find(id);
                if (petTask == null)
                {
                    return HttpNotFound();
                }
                ViewBag.ApplicationUserID = new SelectList(db.Users, "Id", "Email", petTask.ApplicationUserID);
                UserManager<User> UserManager = new UserManager<User>(new UserStore<User>(db));
                User currentUser = UserManager.FindById(User.Identity.GetUserId());
                var groupIds = currentUser.Groups.Select(g => g.GroupID);
                IEnumerable<Location> locations = db.Locations.Where(l => groupIds.Contains(l.GroupID)).AsEnumerable();
                ViewBag.LocationID = new SelectList(locations, "LocationID", "LocationName");
                IEnumerable<Pet> pets = db.Pets.Where(p => groupIds.Contains(p.Location.GroupID)).AsEnumerable();
                ViewBag.PetID = new SelectList(pets, "PetID", "PetName");
                ViewBag.TaskTypeID = new SelectList(db.TaskTypes, "TaskID", "TaskTypeName", petTask.TaskTypeID);
                return View(petTask);
            }
            return RedirectToAction("/Index");
        }

        // POST: PetTasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TaskID,TaskDescription,Frequency,Deadline,PetID,TaskTypeID,ApplicationUserID")] PetTask petTask)
        {
            if (Request.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(petTask).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.ApplicationUserID = new SelectList(db.Users, "Id", "Email", petTask.ApplicationUserID);
                UserManager<User> UserManager = new UserManager<User>(new UserStore<User>(db));
                User currentUser = UserManager.FindById(User.Identity.GetUserId());
                var groupIds = currentUser.Groups.Select(g => g.GroupID);
                IEnumerable<Location> locations = db.Locations.Where(l => groupIds.Contains(l.GroupID)).AsEnumerable();
                ViewBag.LocationID = new SelectList(locations, "LocationID", "LocationName");
                IEnumerable<Pet> pets = db.Pets.Where(p => groupIds.Contains(p.Location.GroupID)).AsEnumerable();
                ViewBag.PetID = new SelectList(pets, "PetID", "PetName");
                ViewBag.TaskTypeID = new SelectList(db.TaskTypes, "TaskID", "TaskTypeName", petTask.TaskTypeID);
                return View(petTask);
            }
            return RedirectToAction("/Index");
        }

        // GET: PetTasks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Request.IsAuthenticated)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                PetTask petTask = db.PetTasks.Find(id);
                if (petTask == null)
                {
                    return HttpNotFound();
                }
                return View(petTask);
            }
            return RedirectToAction("/Index");
        }

        // POST: PetTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Request.IsAuthenticated)
            {
                PetTask petTask = db.PetTasks.Find(id);
                db.PetTasks.Remove(petTask);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("/Index");
        }

        public IEnumerable<PetTask> UserTasks(User currentUser)
        {
            var groupIds = currentUser.Groups.Select(g => g.GroupID);
            var pets = db.Pets.Where(p => groupIds.Contains(p.Location.Group.GroupID));
            var petIDs = pets.Select(p => p.PetID);
            var petTasks = db.PetTasks.Where(t => petIDs.Contains(t.PetID));
            IEnumerable<PetTask> userTasks = petTasks.AsEnumerable();
            return userTasks;
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
