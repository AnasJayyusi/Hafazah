﻿using System;
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
    public class LevelsController : BaseController
    {
        private HafazahDbContext db = new HafazahDbContext();

        // GET: Levels
        public ActionResult Index()
        {
            var levels = db.Levels.Include(l => l.Path);
            return View(levels.ToList());
        }

        // GET: Levels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Level level = db.Levels.Find(id);
            if (level == null)
            {
                return HttpNotFound();
            }
            return View(level);
        }

        // GET: Levels/Create
        public ActionResult Create()
        {
            ViewBag.PathId = new SelectList(db.Paths, "Id", "Name");
            return View();
        }

        // POST: Levels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,LevelNumber,PathId,TotalPageNumber,PageFrom,PageTo,SurahFrom,SurahTo,QuranicVerseFrom,QuranicVerseTo,MaxNumberOfExcuses,Description,CreatedDate,UpdateDate,CreatedBy,UpdatedBy,IsDeleted")] Level level)
        {
            if (ModelState.IsValid)
            {
                db.Levels.Add(level);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PathId = new SelectList(db.Paths, "Id", "Name", level.PathId);
            return View(level);
        }

        // GET: Levels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Level level = db.Levels.Find(id);
            if (level == null)
            {
                return HttpNotFound();
            }
            ViewBag.PathId = new SelectList(db.Paths, "Id", "Name", level.PathId);
            return View(level);
        }

        // POST: Levels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,LevelNumber,PathId,TotalPageNumber,PageFrom,PageTo,SurahFrom,SurahTo,QuranicVerseFrom,QuranicVerseTo,MaxNumberOfExcuses,Description,CreatedDate,UpdateDate,CreatedBy,UpdatedBy,IsDeleted")] Level level)
        {
            if (ModelState.IsValid)
            {
                db.Entry(level).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PathId = new SelectList(db.Paths, "Id", "Name", level.PathId);
            return View(level);
        }

        // GET: Levels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Level level = db.Levels.Find(id);
            if (level == null)
            {
                return HttpNotFound();
            }
            return View(level);
        }

        // POST: Levels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Level level = db.Levels.Find(id);
            db.Levels.Remove(level);
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
