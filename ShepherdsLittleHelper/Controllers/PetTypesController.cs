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
            if (Request.IsAuthenticated)
            {
               return View(db.PetTypes.ToList());
            }
            return RedirectToAction("/Home/Index");
        }


        // GET: PetTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (Request.IsAuthenticated)
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
            return RedirectToAction("/Index");
        }

        // GET: PetTypes/Create
        public ActionResult Create()
        {
            if (Request.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("/Index");
        }

        // POST: PetTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PetTypeID,PetTypeDescription")] PetType petType)
        {
            if (Request.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    db.PetTypes.Add(petType);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(petType);
            }
            return RedirectToAction("/Index");
        }

        // GET: PetTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Request.IsAuthenticated)
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
            return RedirectToAction("/Index");
        }

        // POST: PetTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PetTypeID,PetTypeDescription")] PetType petType)
        {
            if (Request.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(petType).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(petType);
            }
            return RedirectToAction("/Index");
        }

        // GET: PetTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Request.IsAuthenticated)
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
            return RedirectToAction("/Index");
        }

        // POST: PetTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Request.IsAuthenticated)
            {
                PetType petType = db.PetTypes.Find(id);
                db.PetTypes.Remove(petType);
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
