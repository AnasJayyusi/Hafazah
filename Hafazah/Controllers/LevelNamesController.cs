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
    public class LevelNamesController : BaseController
    {
        private HafazahDbContext db = new HafazahDbContext();

        // GET: LevelNames
        public ActionResult Index()
        {
            return View(db.LevelNames.ToList());
        }

        // GET: LevelNames/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LevelName levelName = db.LevelNames.Find(id);
            if (levelName == null)
            {
                return HttpNotFound();
            }
            return View(levelName);
        }

        // GET: LevelNames/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LevelNames/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] LevelName levelName)
        {
            if (ModelState.IsValid)
            {
                db.LevelNames.Add(levelName);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(levelName);
        }

        // GET: LevelNames/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LevelName levelName = db.LevelNames.Find(id);
            if (levelName == null)
            {
                return HttpNotFound();
            }
            return View(levelName);
        }

        // POST: LevelNames/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] LevelName levelName)
        {
            if (ModelState.IsValid)
            {
                db.Entry(levelName).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(levelName);
        }

        // GET: LevelNames/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LevelName levelName = db.LevelNames.Find(id);
            if (levelName == null)
            {
                return HttpNotFound();
            }
            return View(levelName);
        }

        // POST: LevelNames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LevelName levelName = db.LevelNames.Find(id);
            db.LevelNames.Remove(levelName);
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
