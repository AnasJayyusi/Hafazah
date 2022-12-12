using Hafazah.Common;
using Hafazah.DAL;
using Hafazah.Model;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace Hafazah.Controllers
{
    public class MembersController : BaseController
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
        [Route("Dashboard")]
        public ActionResult Index(string filterMembers = "Registered Members", string phoneNumber = "")
        {
            if (IsLoggedIn())
            {
                if (User.IsInRole("Admin"))
                {
                    InitializationValues();
                    FillingDDL();
                    SetRegistrationStatus();
                    return View(GetFilteredList(filterMembers, phoneNumber));
                }
                else
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
            ViewBag.IsRegistrationOpen = false;
            ViewBag.IsAdmin = User.IsInRole("Admin");
        }
        private void FillingDDL()
        {
            var memberSearchedFilter = new List<string>()
            {
                 @Resources.GetResource.T("RegisteredMembers"),
                 @Resources.GetResource.T("PendingRequests")
            };

            ViewBag.MembersDDLFilter = memberSearchedFilter;
        }
        private void SetRegistrationStatus()
        {
            if (ViewBag.IsAdmin)
            {
                var obj = _db.GlobalValues.Single(x => x.Key == "ChangeRegistrationStatus");

                if (obj.Value == "true")
                    ViewBag.IsRegistrationOpen = true;
            }
        }

        [Route("UnAuthorized")]
        public ActionResult UnAuthorized()
        {
            return View();
        }
        public List<Member> GetFilteredList(string filterMembers, string phoneNumber)
        {
            IQueryable<Member> query = Enumerable.Empty<Member>().AsQueryable();

            if (filterMembers == "Registered Members" || filterMembers == Resources.GetResource.T("RegisteredMembers"))
            {
                query = _db.Members.Where(x => x.IsActive == true);
                ViewBag.ShowIsActiveColumn = false;
            }

            if (filterMembers == "Pending Requests" || filterMembers == Resources.GetResource.T("PendingRequests"))
            {
                query = _db.Members.Where(x => x.IsActive == false);
                ViewBag.ShowIsActiveColumn = true;
            }

            if (!string.IsNullOrEmpty(phoneNumber))
                query = _db.Members.Where(x => x.PhoneNumber.Contains(phoneNumber.Trim()));

            return query.ToList();
        }

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


            return RedirectToAction("ApproveNewMember", "Account", member);
            //using (var accountController = new AccountController())
            //    await accountController.AddNewMember(member.Username, member.Email, member.SuggestPassword="P@ssword");
        }

        [Route("Registration")]
        public ActionResult Create()
        {
            var obj = _db.GlobalValues.Single(x => x.Key == "ChangeRegistrationStatus");

            if (obj.Value == "true")
                ViewBag.IsRegistrationOpen = true;
            else
                ViewBag.IsRegistrationOpen = false;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Submit(Member member)
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


        [Route("EditMember")]
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