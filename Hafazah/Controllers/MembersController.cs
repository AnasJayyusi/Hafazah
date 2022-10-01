using System.Collections.Generic;
using System.Data.Entity;
using System.EnterpriseServices;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Web.Management;
using System.Web.Mvc;
using Hafazah.DAL;
using Hafazah.Model;
using Microsoft.AspNet.Identity;

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
        public ActionResult Index(string filterMembers = "Registered Members", string phoneNumber = "")
        {
            if (IsLoggedIn())
            {
                InitializationValues();
                FillingDDL();
                SetRegistrationStatus();
                return View(GetFilteredList(filterMembers, phoneNumber));
            }
            else if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("UnAuthorized");
            }
            else
                return RedirectToAction("Login", "Account");
        }

        private bool IsLoggedIn()
        {
            string currentUsername = User.Identity.GetUserName();
            return !string.IsNullOrEmpty(currentUsername);
        }
        private void InitializationValues()
        {
            ViewBag.IsRegistraionOpen = false;
            ViewBag.IsAdmin = User.IsInRole("Admin");
        }
        private void FillingDDL()
        {
            var memberSearchedFilter = new List<string>()
            {
                 "Registered Members",
                 "Pending Requests",
            };

            ViewBag.MembersDDLFilter = memberSearchedFilter;
        }
        private void SetRegistrationStatus()
        {
            if (ViewBag.IsAdmin)
            {
                var obj = _db.GlobalValues.Single(x => x.Key == "ChangeRegistrationStatus");

                if (obj.Value == "true")
                    ViewBag.IsRegistraionOpen = true;
            }
        }

        public ActionResult UnAuthorized()
        {
            return View();
        }
        public List<Member> GetFilteredList(string filterMembers, string phoneNumber)
        {
            IQueryable<Member> query = Enumerable.Empty<Member>().AsQueryable();

            if (filterMembers == "Registered Members")
            {
                query = _db.Members.Where(x => x.IsActive == true);
                ViewBag.ShowIsActiveColumn = false;
            }

            if (filterMembers == "Pending Requests")
            {
                query = _db.Members.Where(x => x.IsActive == false);
                ViewBag.ShowIsActiveColumn = true;
            }

            if (!string.IsNullOrEmpty(phoneNumber))
                query = _db.Members.Where(x => x.PhoneNumber.Contains(phoneNumber.Trim()));

            return query.ToList();
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
        public ActionResult Create(Member member)
        {
            if (ModelState.IsValid && !IsUserNameExists(member.Username) && !IsEmailExists(member.Email))
            {
                _db.Members.Add(member);
                _db.SaveChanges();
                return RedirectToAction("ThankYou");
            }
            return View();
        }

        public ActionResult ThankYou()
        {
            return View();
        }

        public ActionResult SomeErrorHappend()
        {
            return View();
        }

        private bool IsUserNameExists(string username)
        {
            var user = _db.Users.Where(x => x.UserName.ToLower() == username).FirstOrDefault();
            return user != null;
        }

        private bool IsEmailExists(string email)
        {
            var user = _db.Users.Where(x => x.Email.ToLower() == email).FirstOrDefault();
            return user != null;
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