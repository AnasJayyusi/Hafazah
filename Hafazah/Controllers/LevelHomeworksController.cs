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
using Hafazah.Model.Entities.Program;

namespace Hafazah.Controllers
{
    public class LevelHomeworksController : BaseController
    {
        private HafazahDbContext db = new HafazahDbContext();

        // GET: LevelHomeworks
        public ActionResult Index()
        {
            var levelHomeworks = db.LevelHomeworks.Include(l => l.Level);
            return View(levelHomeworks.ToList());
        }

        // GET: LevelHomeworks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LevelHomework levelHomework = db.LevelHomeworks.Find(id);
            if (levelHomework == null)
            {
                return HttpNotFound();
            }
            return View(levelHomework);
        }

        // GET: LevelHomeworks/Create
        public ActionResult Create()
        {
            ViewBag.LevelId = new SelectList(db.Levels, "Id", "SurahFrom");
            return View();
        }

        // POST: LevelHomeworks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RequiredMemorizedFrom,RequiredMemorizedTo,AbilityConnectFrom,AbilityConnectTo,ReviewFrom,ReviewTo,LevelId,CreatedDate,UpdateDate,CreatedBy,UpdatedBy,IsDeleted")] LevelHomework levelHomework)
        {
            if (ModelState.IsValid)
            {
                db.LevelHomeworks.Add(levelHomework);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LevelId = new SelectList(db.Levels, "Id", "SurahFrom", levelHomework.LevelId);
            return View(levelHomework);
        }

        // GET: LevelHomeworks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LevelHomework levelHomework = db.LevelHomeworks.Find(id);
            if (levelHomework == null)
            {
                return HttpNotFound();
            }
            ViewBag.LevelId = new SelectList(db.Levels, "Id", "SurahFrom", levelHomework.LevelId);
            return View(levelHomework);
        }

        // POST: LevelHomeworks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RequiredMemorizedFrom,RequiredMemorizedTo,AbilityConnectFrom,AbilityConnectTo,ReviewFrom,ReviewTo,LevelId,CreatedDate,UpdateDate,CreatedBy,UpdatedBy,IsDeleted")] LevelHomework levelHomework)
        {
            if (ModelState.IsValid)
            {
                db.Entry(levelHomework).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LevelId = new SelectList(db.Levels, "Id", "SurahFrom", levelHomework.LevelId);
            return View(levelHomework);
        }

        // GET: LevelHomeworks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LevelHomework levelHomework = db.LevelHomeworks.Find(id);
            if (levelHomework == null)
            {
                return HttpNotFound();
            }
            return View(levelHomework);
        }

        // POST: LevelHomeworks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LevelHomework levelHomework = db.LevelHomeworks.Find(id);
            db.LevelHomeworks.Remove(levelHomework);
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
