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
    public class PetsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Pets
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                UserManager<User> UserManager = new UserManager<User>(new UserStore<User>(db));
                User currentUser = UserManager.FindById(User.Identity.GetUserId());
                var pets = UserPets(currentUser);
                return View(pets.ToList());
            }
            return Redirect("/Home/Index");
        }

        // GET: Pets/Details/5
        public ActionResult Details(int? id)
        {
            if (Request.IsAuthenticated)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Pet pet = db.Pets.Find(id);
                if (pet == null)
                {
                    return HttpNotFound();
                }
                return View(pet);
            }
            return RedirectToAction("/Index");
        }

        // GET: Pets/Create
        public ActionResult Create()
        {
            if (Request.IsAuthenticated)
            {
                ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "LocationName");
                ViewBag.PetTypeID = new SelectList(db.PetTypes, "PetTypeID", "PetTypeDescription");
                return View();
            }
            return RedirectToAction("/Index");
        }

        // POST: Pets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PetID,PetName,Birthday,Weight,PetNotes,ImageURL,LocationID,PetTypeID")] Pet pet)
        {
            if (Request.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    db.Pets.Add(pet);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "LocationName", pet.LocationID);
                ViewBag.PetTypeID = new SelectList(db.PetTypes, "PetTypeID", "PetTypeDescription", pet.PetTypeID);
                return View(pet);
            }
            return RedirectToAction("/Index");
        }

        // GET: Pets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Request.IsAuthenticated)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Pet pet = db.Pets.Find(id);
                if (pet == null)
                {
                    return HttpNotFound();
                }
                ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "LocationName", pet.LocationID);
                ViewBag.PetTypeID = new SelectList(db.PetTypes, "PetTypeID", "PetTypeDescription", pet.PetTypeID);
                return View(pet);
            }
            return RedirectToAction("/Index");
        }

        // POST: Pets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PetID,PetName,Birthday,Weight,PetNotes,ImageURL,LocationID,PetTypeID")] Pet pet)
        {
            if (Request.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(pet).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "LocationName", pet.LocationID);
                ViewBag.PetTypeID = new SelectList(db.PetTypes, "PetTypeID", "PetTypeDescription", pet.PetTypeID);
                return View(pet);
            }
            return RedirectToAction("/Index");
        }

        // GET: Pets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Request.IsAuthenticated)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Pet pet = db.Pets.Find(id);
                if (pet == null)
                {
                    return HttpNotFound();
                }
                return View(pet);
            }
            return RedirectToAction("/Index");
        }
        // POST: Pets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Request.IsAuthenticated)
            {
                Pet pet = db.Pets.Find(id);
                db.Pets.Remove(pet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("/Index");
        }

        public IEnumerable<Pet> UserPets(User currentUser)
        {
            var groupIds = currentUser.Groups.Select(g => g.GroupID);
            IEnumerable<Pet> pets = db.Pets.Where(p => groupIds.Contains(p.Location.Group.GroupID)).AsEnumerable();
            return pets;
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
