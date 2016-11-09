using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShepherdsLittleHelper.Models;

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
                var locations = db.Locations.Include(l => l.Group);
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
                return View(location);
            }
            return RedirectToAction("/Index");
        }

        // GET: Locations/Create
        public ActionResult Create()
        {
            if (Request.IsAuthenticated)
            {
                ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "GroupName");
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

                ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "GroupName", location.GroupID);
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
                ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "GroupName", location.GroupID);
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
                ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "GroupName", location.GroupID);
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
