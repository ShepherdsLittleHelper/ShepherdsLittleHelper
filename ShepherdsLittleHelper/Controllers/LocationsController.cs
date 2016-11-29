﻿using System;
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
    public class LocationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Locations
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                UserManager<User> UserManager = new UserManager<User>(new UserStore<User>(db));
                User currentUser = UserManager.FindById(User.Identity.GetUserId());
                var locations = UserLocations(currentUser);
                return View(locations.ToList());
            }
            return Redirect("/Home/Index");
        }

        // GET: Locations/Details/5
        public ActionResult Details(int? id)
        {
            if (Request.IsAuthenticated)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Location location = db.Locations.Find(id);
                if (location == null)
                {
                    return HttpNotFound();
                }
                IEnumerable<Pet> pets = db.Pets.Where(p => p.LocationID == location.LocationID).AsEnumerable();
                ViewBag.pets = pets;
                ViewBag.currentOccupancy = pets.Count();
                ViewBag.location = location;
                return View(location);
            }
            return RedirectToAction("/Index");
        }

        // GET: Locations/Create
        public ActionResult Create()
        {
            if (Request.IsAuthenticated)
            {
                UserManager<User> UserManager = new UserManager<User>(new UserStore<User>(db));
                User currentUser = UserManager.FindById(User.Identity.GetUserId());
                var groupIds = currentUser.Groups.Select(g => g.GroupID);
                IEnumerable<Group> groups = db.Groups.Where(g => groupIds.Contains(g.GroupID)).AsEnumerable();
                ViewBag.GroupID = new SelectList(groups, "GroupID", "GroupName");
                return View();
            }
            return RedirectToAction("/Index");
        }

        // POST: Locations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LocationID,LocationName,MaxOccupancy,LocationNotes,GroupID")] Location location)
        {
            if (Request.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    db.Locations.Add(location);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                UserManager<User> UserManager = new UserManager<User>(new UserStore<User>(db));
                User currentUser = UserManager.FindById(User.Identity.GetUserId());
                var groupIds = currentUser.Groups.Select(g => g.GroupID);
                IEnumerable<Group> groups = db.Groups.Where(g => groupIds.Contains(g.GroupID)).AsEnumerable();
                ViewBag.GroupID = new SelectList(groups, "GroupID", "GroupName", location.GroupID);
                return View(location);
            }
            return RedirectToAction("/Index");
        }

        // GET: Locations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Request.IsAuthenticated)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Location location = db.Locations.Find(id);
                if (location == null)
                {
                    return HttpNotFound();
                }
                UserManager<User> UserManager = new UserManager<User>(new UserStore<User>(db));
                User currentUser = UserManager.FindById(User.Identity.GetUserId());
                var groupIds = currentUser.Groups.Select(g => g.GroupID);
                IEnumerable<Group> groups = db.Groups.Where(g => groupIds.Contains(g.GroupID)).AsEnumerable();
                ViewBag.GroupID = new SelectList(groups, "GroupID", "GroupName", location.GroupID);
                return View(location);
            }
            return RedirectToAction("/Index");
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LocationID,LocationName,MaxOccupancy,LocationNotes,GroupID")] Location location)
        {
            if (Request.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(location).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                UserManager<User> UserManager = new UserManager<User>(new UserStore<User>(db));
                User currentUser = UserManager.FindById(User.Identity.GetUserId());
                var groupIds = currentUser.Groups.Select(g => g.GroupID);
                IEnumerable<Group> groups = db.Groups.Where(g => groupIds.Contains(g.GroupID)).AsEnumerable();
                ViewBag.GroupID = new SelectList(groups, "GroupID", "GroupName", location.GroupID);
                return View(location);
            }
            return RedirectToAction("/Index");
        }

        // GET: Locations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Request.IsAuthenticated)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Location location = db.Locations.Find(id);
                if (location == null)
                {
                    return HttpNotFound();
                }
                return View(location);
            }
            return RedirectToAction("/Index");
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Request.IsAuthenticated)
            {
                Location location = db.Locations.Find(id);
                db.Locations.Remove(location);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("/Index");
        }

        public IEnumerable<Location> UserLocations(User currentUser)
        {
            var groupIds = currentUser.Groups.Select(g => g.GroupID);
            IEnumerable<Location> locations = db.Locations.Where(l => groupIds.Contains(l.Group.GroupID)).AsEnumerable();
            return locations;
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
