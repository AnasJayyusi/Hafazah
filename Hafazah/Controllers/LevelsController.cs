using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
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
            var levels = db.Levels.Include(l => l.Path).Where(l=>l.Path.ProgramType == Model.Enums.ProgramType.Fursan);
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
            ViewBag.PathId = new SelectList(db.Paths.Where(p=>p.ProgramType == Model.Enums.ProgramType.Fursan), "Id", "Description");
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


        #region Homeworks Section
        public ActionResult Homeworks(int? id)
        {
            ViewBag.BindLevelId = id;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var homeworks = db.LevelHomeworks.Where(h => h.LevelId == id).ToList();
            if (homeworks == null)
            {
                return HttpNotFound();
            }
            return View(homeworks);
        }

        [HttpGet]
        public JsonResult GetHomeworks(int levelId)
        {
            var homeworks = db.LevelHomeworks.Where(l => l.LevelId == levelId).ToList();
            return Json(homeworks, JsonRequestBehavior.AllowGet);
        }

        public JsonResult InsertHomeWorks(List<LevelHomework> homeworks)
        {
            //Truncate Table to delete all old records.
            db.Database.ExecuteSqlCommand("DELETE FROM [LevelHomeworks] WHERE LevelId =" + homeworks[0].LevelId);
            db.SaveChanges();
            //Check for NULL.
            if (homeworks == null)
            {
                homeworks = new List<LevelHomework>();
            }

            //Loop and insert records.
            foreach (var homework in homeworks)
            {
                db.LevelHomeworks.Add(homework);
            }
            int insertedRecords = db.SaveChanges();
            return Json(insertedRecords);
        }

        public ActionResult EditHomeWork(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var homework = db.LevelHomeworks.Include(p => p.Level).SingleOrDefault(x => x.Id == id);
            if (homework == null)
            {
                return HttpNotFound();
            }
            return View(homework);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditHomeWork(LevelHomework homework)
        {
            if (ModelState.IsValid)
            {
              
                db.Entry(homework).State = EntityState.Modified;
                db.SaveChanges();
                TempData["msg"] = TempData["msg"] = "<span class=\"label label-success\"> Updated Successfully </span>";
            }
            return View(homework);
        }
        #endregion
    }
}
