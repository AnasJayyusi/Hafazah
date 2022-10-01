using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Hafazah.DAL;
using Hafazah.Model.Entities.DropDownListOptions;

namespace Hafazah.Controllers
{
    public class QuranMemorizedsController : Controller
    {
        private HafazahDbContext db = new HafazahDbContext();

        // GET: QuranMemorizeds
        public ActionResult Index()
        {
            return View(db.QuranMemorized.ToList());
        }

        // GET: QuranMemorizeds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuranMemorized quranMemorized = db.QuranMemorized.Find(id);
            if (quranMemorized == null)
            {
                return HttpNotFound();
            }
            return View(quranMemorized);
        }

        // GET: QuranMemorizeds/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QuranMemorizeds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] QuranMemorized quranMemorized)
        {
            if (ModelState.IsValid)
            {
                db.QuranMemorized.Add(quranMemorized);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(quranMemorized);
        }

        // GET: QuranMemorizeds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuranMemorized quranMemorized = db.QuranMemorized.Find(id);
            if (quranMemorized == null)
            {
                return HttpNotFound();
            }
            return View(quranMemorized);
        }

        // POST: QuranMemorizeds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] QuranMemorized quranMemorized)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quranMemorized).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(quranMemorized);
        }

        // GET: QuranMemorizeds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuranMemorized quranMemorized = db.QuranMemorized.Find(id);
            if (quranMemorized == null)
            {
                return HttpNotFound();
            }
            return View(quranMemorized);
        }

        // POST: QuranMemorizeds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QuranMemorized quranMemorized = db.QuranMemorized.Find(id);
            db.QuranMemorized.Remove(quranMemorized);
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
