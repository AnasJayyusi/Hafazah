using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Hafazah.DAL;
using Hafazah.Model;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

namespace Hafazah.Controllers
{
    public class MembersController : Controller
    {
        private HafazahDbContext _db = new HafazahDbContext();

        [HttpGet]
        public ActionResult ChangeRegistrationStatus()
        {
            var obj = _db.GlobalValues.Single(x => x.Key == "ChangeRegistrationStatus");
            if (obj.Value == "true")
                obj.Value = "false";
            else
                obj.Value = "true";
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Members
        public ActionResult Index()
        {
            ViewBag.IsRegistraionOpen = false;
            var strCurrentUserId = User.Identity.GetUserName();
            ViewBag.IsAdmin = User.IsInRole("Admin");

            if (ViewBag.IsAdmin)
            {
                var obj = _db.GlobalValues.Single(x => x.Key == "ChangeRegistrationStatus");

                if (obj.Value == "true")
                    ViewBag.IsRegistraionOpen = true;
            }
            return View(_db.Members.ToList());
        }

        // GET: Members/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = _db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();

            }
            return View(member);
        }

        public ActionResult Approve(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = _db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }


            return RedirectToAction("AddNewMember", "Account", member);
            //using (var accountController = new AccountController())
            //    await accountController.AddNewMember(member.Username, member.Email, member.SuggestPassword="P@ssword");
        }

        // GET: Members/Create
        public ActionResult Create()
        {
            var obj = _db.GlobalValues.Single(x => x.Key == "ChangeRegistrationStatus");

            if (obj.Value == "true")
                ViewBag.IsRegistraionOpen = true;
            else
                ViewBag.IsRegistraionOpen = false;
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,SecondName,ThirdName,LastName,Gender,BirthDate,Country,Address,EducationLevel,JobTitle,Username,PhoneNumber,Email,QuranMemorized,InterviewDate,KnownFrom,IsActive,CreatedDate,UpdateDate,CreatedBy,UpdatedBy,IsDeleted")] Member member)
        {
            if (ModelState.IsValid)
            {
                _db.Members.Add(member);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(member);
        }

       


        // GET: Members/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = _db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,SecondName,ThirdName,LastName,Gender,BirthDate,Country,Address,EducationLevel,JobTitle,Username,PhoneNumber,Email,QuranMemorized,InterviewDate,KnownFrom,IsActive,CreatedDate,UpdateDate,CreatedBy,UpdatedBy,IsDeleted")] Member member)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(member).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(member);
        }

        // GET: Members/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = _db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Member member = _db.Members.Find(id);
            _db.Members.Remove(member);
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
    }
}

public class Membertest
{
    public string Id { get; set; }
}