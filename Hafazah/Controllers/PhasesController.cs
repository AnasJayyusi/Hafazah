using Hafazah.DAL;
using Hafazah.Model.Entities.Program;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Hafazah.Controllers
{
    public class PhasesController : Controller
    {
        private HafazahDbContext db = new HafazahDbContext();

        // GET: Phases
        public ActionResult Index()
        {
            var phases = db.Phases.Include(p => p.Path).Where(p => p.Path.ProgramType == Model.Enums.ProgramType.Hafazah);
            return View(phases.ToList());
        }

        // GET: Phases/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phase phase = db.Phases.Find(id);
            if (phase == null)
            {
                return HttpNotFound();
            }
            return View(phase);
        }

        // GET: Phases/Create
        public ActionResult Create()
        {
            ViewBag.PathId = new SelectList(db.Paths.Where(p => p.ProgramType == Model.Enums.ProgramType.Hafazah), "Id", "Name");
            return View();
        }

        // POST: Phases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PhaseNumber,PathId,TotalPageNumber,PageFrom,PageTo,SurahFrom,SurahTo,QuranicVerseFrom,QuranicVerseTo,MaxNumberOfExcuses,Description,CreatedDate,UpdateDate,CreatedBy,UpdatedBy,IsDeleted")] Phase phase)
        {
            if (ModelState.IsValid)
            {
                db.Phases.Add(phase);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PathId = new SelectList(db.Paths, "Id", "Name", phase.PathId);
            return View(phase);
        }

        // GET: Phases/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phase phase = db.Phases.Find(id);
            if (phase == null)
            {
                return HttpNotFound();
            }
            ViewBag.PathId = new SelectList(db.Paths, "Id", "Name", phase.PathId);
            return View(phase);
        }

        // POST: Phases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PhaseNumber,PathId,TotalPageNumber,PageFrom,PageTo,SurahFrom,SurahTo,QuranicVerseFrom,QuranicVerseTo,MaxNumberOfExcuses,Description,CreatedDate,UpdateDate,CreatedBy,UpdatedBy,IsDeleted")] Phase phase)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phase).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PathId = new SelectList(db.Paths, "Id", "Name", phase.PathId);
            return View(phase);
        }

        // GET: Phases/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phase phase = db.Phases.Find(id);
            if (phase == null)
            {
                return HttpNotFound();
            }
            return View(phase);
        }


        // POST: Phases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Phase phase = db.Phases.Find(id);
            db.Phases.Remove(phase);
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


        #region Homeworks Section
        public ActionResult Homeworks(int? id)
        {
            ViewBag.BindPhaseId = id;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var homeworks = db.PhaseHomeworks.Where(h => h.PhaseId == id).ToList();
            if (homeworks == null)
            {
                return HttpNotFound();
            }
            return View(homeworks);
        }

        [HttpGet]
        public JsonResult GetHomeworks(int phaseId)
        {
            var homeworks = db.PhaseHomeworks.Where(l => l.PhaseId == phaseId).ToList();
            return Json(homeworks, JsonRequestBehavior.AllowGet);
        }

        public JsonResult InsertHomeWorks(List<PhaseHomework> homeworks)
        {
            //Truncate Table to delete all old records.
            db.Database.ExecuteSqlCommand("DELETE FROM [PhaseHomeworks] WHERE PhaseId =" + homeworks[0].PhaseId);
            db.SaveChanges();
            //Check for NULL.
            if (homeworks == null)
            {
                homeworks = new List<PhaseHomework>();
            }

            //Loop and insert records.
            foreach (var homework in homeworks)
            {
                db.PhaseHomeworks.Add(homework);
            }
            int insertedRecords = db.SaveChanges();
            return Json(insertedRecords);
        }
        #endregion
    }
}
