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
    public class LocationTasksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: LocationTasks
        public ActionResult Index()
        {

            if (Request.IsAuthenticated)
            {
                UserManager<User> UserManager = new UserManager<User>(new UserStore<User>(db));
                User currentUser = UserManager.FindById(User.Identity.GetUserId());
                var locationTaskEnum = UserTasks(currentUser);
                return View(locationTaskEnum.ToList());
            }
            return Redirect("/Home/Index");
        }

        // GET: LocationTasks/Details/5
        public ActionResult Details(int? id)
        {
            if (Request.IsAuthenticated)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                LocationTask locationTask = db.LocationTasks.Find(id);
                if (locationTask == null)
                {
                    return HttpNotFound();
                }
                return View(locationTask);
            }
            return RedirectToAction("/Index");
        }

        // GET: LocationTasks/Create
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
                ViewBag.TaskTypeID = new SelectList(db.TaskTypes, "TaskID", "TaskTypeName");
                return View();
            }
            return RedirectToAction("/Index");
        }

        // POST: LocationTasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "L_TaskID,TaskDescription,Frequency,Deadline,IsDone,LocationID,TaskTypeID,ApplicationUserID")] LocationTask locationTask)
        {
            if (Request.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    db.LocationTasks.Add(locationTask);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.ApplicationUserID = new SelectList(db.Users, "Id", "Email", locationTask.ApplicationUserID);
                UserManager<User> UserManager = new UserManager<User>(new UserStore<User>(db));
                User currentUser = UserManager.FindById(User.Identity.GetUserId());
                var groupIds = currentUser.Groups.Select(g => g.GroupID);
                IEnumerable<Location> locations = db.Locations.Where(l => groupIds.Contains(l.GroupID)).AsEnumerable();
                ViewBag.LocationID = new SelectList(locations, "LocationID", "LocationName");
                ViewBag.TaskTypeID = new SelectList(db.TaskTypes, "TaskID", "TaskTypeName", locationTask.TaskTypeID);
                return View(locationTask);
            }
            return RedirectToAction("/Index");
        }

        // GET: Items/Edit
        public ActionResult ToggleDone(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LocationTask item = db.LocationTasks.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            if (item.IsDone)
            {
                item.ApplicationUser = null;
                item.IsDone = false;
            }
            else if (!item.IsDone)
            {
                UserManager<User> UserManager = new UserManager<User>(new UserStore<User>(db));
                User currentUser = UserManager.FindById(User.Identity.GetUserId());
                item.ApplicationUser = currentUser;
                item.IsDone = true;
            }
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: LocationTasks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Request.IsAuthenticated)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                LocationTask locationTask = db.LocationTasks.Find(id);
                if (locationTask == null)
                {
                    return HttpNotFound();
                }
                ViewBag.ApplicationUserID = new SelectList(db.Users, "Id", "Email", locationTask.ApplicationUserID);
                UserManager<User> UserManager = new UserManager<User>(new UserStore<User>(db));
                User currentUser = UserManager.FindById(User.Identity.GetUserId());
                var groupIds = currentUser.Groups.Select(g => g.GroupID);
                IEnumerable<Location> locations = db.Locations.Where(l => groupIds.Contains(l.GroupID)).AsEnumerable();
                ViewBag.LocationID = new SelectList(locations, "LocationID", "LocationName");
                ViewBag.TaskTypeID = new SelectList(db.TaskTypes, "TaskID", "TaskTypeName", locationTask.TaskTypeID);
                ViewBag.Deadline = locationTask.Deadline.Date;
                return View(locationTask);
            }
            return RedirectToAction("/Index");
        }

        // POST: LocationTasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "L_TaskID,TaskDescription,Frequency,Deadline,IsDone,LocationID,TaskTypeID,ApplicationUserID")] LocationTask locationTask)
        {
            if (Request.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(locationTask).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.ApplicationUserID = new SelectList(db.Users, "Id", "Email", locationTask.ApplicationUserID);
                UserManager<User> UserManager = new UserManager<User>(new UserStore<User>(db));
                User currentUser = UserManager.FindById(User.Identity.GetUserId());
                var groupIds = currentUser.Groups.Select(g => g.GroupID);
                IEnumerable<Location> locations = db.Locations.Where(l => groupIds.Contains(l.GroupID)).AsEnumerable();
                ViewBag.LocationID = new SelectList(locations, "LocationID", "LocationName"); ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "LocationName", locationTask.LocationID);
                ViewBag.TaskTypeID = new SelectList(db.TaskTypes, "TaskID", "TaskTypeName", locationTask.TaskTypeID);
                return View(locationTask);
            }
            return RedirectToAction("/Index");
        }

        // GET: LocationTasks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Request.IsAuthenticated)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                LocationTask locationTask = db.LocationTasks.Find(id);
                if (locationTask == null)
                {
                    return HttpNotFound();
                }
                return View(locationTask);
            }
            return RedirectToAction("/Index");
        }

        // POST: LocationTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Request.IsAuthenticated)
            {
                LocationTask locationTask = db.LocationTasks.Find(id);
                db.LocationTasks.Remove(locationTask);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("/Index");
        }

        public IEnumerable<LocationTask> UserTasks(User currentUser)
        {
            var groupIds = currentUser.Groups.Select(g => g.GroupID);
            var locations = db.Locations.Where(l => groupIds.Contains(l.Group.GroupID));
            var locationIDs = locations.Select(l => l.LocationID);
            var locationTasks = db.LocationTasks.Where(t => locationIDs.Contains(t.LocationID));
            IEnumerable<LocationTask> userTasks = locationTasks.AsEnumerable();
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
