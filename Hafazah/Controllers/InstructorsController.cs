using Hafazah.Common;
using Hafazah.DAL;
using Hafazah.Model.Entities.Users;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Hafazah.Controllers
{
    public class InstructorsController : BaseController
    {
        private HafazahDbContext _db = new HafazahDbContext();

        // GET: Instructors
        [Route("Instructors")]
        public ActionResult Index()
        {
            return View(_db.Instructors.ToList());
        }

        // GET: Instructors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructor instructor = _db.Instructors.Find(id);
            if (instructor == null)
            {
                return HttpNotFound();
            }
            return View(instructor);
        }

        // GET: Instructors/Create

        public ActionResult Create()
        {
            ViewBag.EducationLevelId = new SelectList(_db.EducationLevels, "Id", "Name");
            ViewBag.QuranMemorizedId = new SelectList(_db.QuranMemorized, "Id", "Name");
            return View();
        }

        // POST: Instructors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,SecondName,ThirdName,LastName,Gender,BirthDate,PhoneNumber,OtherDetails,EducationLevelId,Username,SuggestPassword,Email,QuranMemorizedId,ProgramType,CreatedDate,UpdateDate,CreatedBy,UpdatedBy,IsDeleted")] Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                _db.Instructors.Add(instructor);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(instructor);
        }

        // GET: Instructors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructor instructor = _db.Instructors.Find(id);
            if (instructor == null)
            {
                return HttpNotFound();
            }
            return View(instructor);
        }

        // POST: Instructors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,SecondName,ThirdName,LastName,Gender,BirthDate,PhoneNumber,OtherDetails,EducationLevelId,Username,SuggestPassword,Email,QuranMemorizedId,ProgramType,CreatedDate,UpdateDate,CreatedBy,UpdatedBy,IsDeleted")] Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(instructor).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(instructor);
        }

        // GET: Instructors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructor instructor = _db.Instructors.Find(id);
            if (instructor == null)
            {
                return HttpNotFound();
            }
            return View(instructor);
        }

        // POST: Instructors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Instructor instructor = _db.Instructors.Find(id);
            _db.Instructors.Remove(instructor);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }


        #region Custom

        public ActionResult Approve(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructor member = _db.Instructors.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return RedirectToAction("ApproveNewInstructor", "Account", member);
        }

        #endregion
    }
}
