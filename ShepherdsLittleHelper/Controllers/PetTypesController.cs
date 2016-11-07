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
    public class PetTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PetTypes
        public ActionResult Index()
        {
            return View(db.PetTypes.ToList());
        }

        // GET: PetTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PetType petType = db.PetTypes.Find(id);
            if (petType == null)
            {
                return HttpNotFound();
            }
            return View(petType);
        }

        // GET: PetTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PetTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PetTypeID,PetTypeDescription")] PetType petType)
        {
            if (ModelState.IsValid)
            {
                db.PetTypes.Add(petType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(petType);
        }

        // GET: PetTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PetType petType = db.PetTypes.Find(id);
            if (petType == null)
            {
                return HttpNotFound();
            }
            return View(petType);
        }

        // POST: PetTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PetTypeID,PetTypeDescription")] PetType petType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(petType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(petType);
        }

        // GET: PetTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PetType petType = db.PetTypes.Find(id);
            if (petType == null)
            {
                return HttpNotFound();
            }
            return View(petType);
        }

        // POST: PetTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PetType petType = db.PetTypes.Find(id);
            db.PetTypes.Remove(petType);
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
