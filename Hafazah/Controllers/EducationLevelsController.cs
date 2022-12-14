using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Hafazah.Common;
using Hafazah.DAL;
using Hafazah.Model.Entities.DropDownListOptions;

namespace Hafazah.Controllers
{
    public class EducationLevelsController : BaseController
    {
        private HafazahDbContext db = new HafazahDbContext();

        // GET: EducationLevels
        public ActionResult Index()
        {
            return View(db.EducationLevels.ToList());
        }

        // GET: EducationLevels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EducationLevel educationLevel = db.EducationLevels.Find(id);
            if (educationLevel == null)
            {
                return HttpNotFound();
            }
            return View(educationLevel);
        }

        // GET: EducationLevels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EducationLevels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] EducationLevel educationLevel)
        {
            if (ModelState.IsValid)
            {
                db.EducationLevels.Add(educationLevel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(educationLevel);
        }

        // GET: EducationLevels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EducationLevel educationLevel = db.EducationLevels.Find(id);
            if (educationLevel == null)
            {
                return HttpNotFound();
            }
            return View(educationLevel);
        }

        // POST: EducationLevels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] EducationLevel educationLevel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(educationLevel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(educationLevel);
        }

        // GET: EducationLevels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EducationLevel educationLevel = db.EducationLevels.Find(id);
            if (educationLevel == null)
            {
                return HttpNotFound();
            }
            return View(educationLevel);
        }

        // POST: EducationLevels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EducationLevel educationLevel = db.EducationLevels.Find(id);
            db.EducationLevels.Remove(educationLevel);
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
