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
    public class TaskTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TaskTypes
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                return View(db.TaskTypes.ToList());
            }
            return RedirectToAction("/Home/Index");
        }

        // GET: TaskTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (Request.IsAuthenticated)
            {


                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                TaskType taskType = db.TaskTypes.Find(id);
                if (taskType == null)
                {
                    return HttpNotFound();
                }
                return View(taskType);
            }
            return RedirectToAction("/Index");
        }

        // GET: TaskTypes/Create
        public ActionResult Create()
        {
            if (Request.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("/Index");
        }

        // POST: TaskTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TaskID,TaskTypeName,TaskTypeNotes")] TaskType taskType)
        {
            if (Request.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    db.TaskTypes.Add(taskType);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(taskType);
            }
            return RedirectToAction("/Index");
        }

        // GET: TaskTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Request.IsAuthenticated)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                TaskType taskType = db.TaskTypes.Find(id);
                if (taskType == null)
                {
                    return HttpNotFound();
                }
                return View(taskType);
            }
            return RedirectToAction("/Index");
        }

        // POST: TaskTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TaskID,TaskTypeName,TaskTypeNotes")] TaskType taskType)
        {
            if (Request.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(taskType).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(taskType);
            }
            return RedirectToAction("/Index");
        }

        // GET: TaskTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Request.IsAuthenticated)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                TaskType taskType = db.TaskTypes.Find(id);
                if (taskType == null)
                {
                    return HttpNotFound();
                }
                return View(taskType);
            }
            return RedirectToAction("/Index");
        }

        // POST: TaskTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Request.IsAuthenticated)
            {
                TaskType taskType = db.TaskTypes.Find(id);
                db.TaskTypes.Remove(taskType);
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
