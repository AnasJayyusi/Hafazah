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
    public class PhaseHomeworksController : BaseController
    {
        private HafazahDbContext db = new HafazahDbContext();

        // GET: PhaseHomeworks
        public ActionResult Index()
        {
            var phaseHomeworks = db.PhaseHomeworks.Include(p => p.Phase);
            return View(phaseHomeworks.ToList());
        }

        // GET: PhaseHomeworks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhaseHomework phaseHomework = db.PhaseHomeworks.Find(id);
            if (phaseHomework == null)
            {
                return HttpNotFound();
            }
            return View(phaseHomework);
        }

        // GET: PhaseHomeworks/Create
        public ActionResult Create()
        {
            ViewBag.PhaseId = new SelectList(db.Phases, "Id", "SurahFrom");
            return View();
        }

        // POST: PhaseHomeworks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RequiredMemorizedFrom,RequiredMemorizedTo,AbilityConnectFrom,AbilityConnectTo,ReviewFrom,ReviewTo,PhaseId,CreatedDate,UpdateDate,CreatedBy,UpdatedBy,IsDeleted")] PhaseHomework phaseHomework)
        {
            if (ModelState.IsValid)
            {
                db.PhaseHomeworks.Add(phaseHomework);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PhaseId = new SelectList(db.Phases, "Id", "SurahFrom", phaseHomework.PhaseId);
            return View(phaseHomework);
        }

        // GET: PhaseHomeworks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhaseHomework phaseHomework = db.PhaseHomeworks.Find(id);
            if (phaseHomework == null)
            {
                return HttpNotFound();
            }
            ViewBag.PhaseId = new SelectList(db.Phases, "Id", "SurahFrom", phaseHomework.PhaseId);
            return View(phaseHomework);
        }

        // POST: PhaseHomeworks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RequiredMemorizedFrom,RequiredMemorizedTo,AbilityConnectFrom,AbilityConnectTo,ReviewFrom,ReviewTo,PhaseId,CreatedDate,UpdateDate,CreatedBy,UpdatedBy,IsDeleted")] PhaseHomework phaseHomework)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phaseHomework).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PhaseId = new SelectList(db.Phases, "Id", "SurahFrom", phaseHomework.PhaseId);
            return View(phaseHomework);
        }

        // GET: PhaseHomeworks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhaseHomework phaseHomework = db.PhaseHomeworks.Find(id);
            if (phaseHomework == null)
            {
                return HttpNotFound();
            }
            return View(phaseHomework);
        }

        // POST: PhaseHomeworks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PhaseHomework phaseHomework = db.PhaseHomeworks.Find(id);
            db.PhaseHomeworks.Remove(phaseHomework);
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
