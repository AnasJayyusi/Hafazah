using Hafazah.Common;
using Hafazah.DAL;
using Hafazah.Model;
using Hafazah.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

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
        }

        [Route("Registration")]
        public ActionResult Create()
        {
            var obj = _db.GlobalValues.Single(x => x.Key == "ChangeRegistrationStatus");

            if (obj.Value == "true")
            {
                ViewBag.IsRegistrationOpen = true;
                FillingDDLs();
                SetDefautValuesForValdations();
            }
            else
                ViewBag.IsRegistrationOpen = false;


            return View();
        }

        private void FillingDDLs()
        {
            // Filling DDls
            ViewBag.EducationLevelId = new SelectList(_db.EducationLevels, "Id", "Name");
            ViewBag.QuranMemorizedId = new SelectList(_db.QuranMemorized, "Id", "Name");
            ViewBag.CountryId = new SelectList(_db.Countries, "Id", "Name");
        }

        private void SetDefautValuesForValdations()
        {
            // Validation
            ViewBag.FirstNameMissing = true;
            ViewBag.LastNameMissing = true;
            ViewBag.GenderNameMissing = true;
            ViewBag.CountryIdMissing = true;
            ViewBag.AddressMissing = true;
            ViewBag.BirthDateMissing = true;
            ViewBag.EducationLevelIdMissing = true;
            ViewBag.JobTitleMissing = true;
            ViewBag.KnownFromMissing = true;
            ViewBag.PhoneNumberMissing = true;
            ViewBag.EmailMissing = true;
            ViewBag.UsernameMissing = true;
            ViewBag.SuggestPasswordMissing = true;
            ViewBag.QuranMemorizedIdMissing = true;
            ViewBag.InterviewDateMissing = true;
            ViewBag.ProgramTypeMissing = true;
            ViewBag.PathMissing = true;

        }

        [HttpPost]
        [Route("Registration")]
        public ActionResult Submit(Member member)
        {
            SharedServices sharedServices = new SharedServices();
            if (ModelState.IsValid && !sharedServices.IsUsernameToken(member.Username) && !sharedServices.IsEmaiAlreadylExists(member.Email) && !sharedServices.IsPhoneNumberAlreadylExists(member.PhoneNumber))
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


        [Route("EditMember")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = _db.Members.Find(id);
            ViewBag.InstrcutorId = new SelectList(_db.Instructors, "Id", "LastName", member.InstrcutorId);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("EditMember")]
        public ActionResult Edit(
            [Bind(Include = "Id,FirstName,SecondName,ThirdName,LastName,Gender,BirthDate,EducationLevelId,JobTitle,PhoneNumber,Email,IsActive,InstrcutorId")] Member member)
        {
            ViewBag.InstrcutorId = new SelectList(_db.Instructors, "Id", "LastName");
            try
            {
                // Update Values 
                Member obj = _db.Members.Find(member.Id);
                obj.FirstName = member.FirstName;
                obj.SecondName = member.SecondName;
                obj.ThirdName = member.ThirdName;
                obj.LastName = member.LastName;
                obj.Gender = member.Gender;
                obj.BirthDate = member.BirthDate;
                obj.EducationLevelId = member.EducationLevelId;
                obj.JobTitle = member.JobTitle;
                obj.PhoneNumber = member.PhoneNumber;
                obj.IsActive = member.IsActive;
                obj.InstrcutorId = member.InstrcutorId;

                _db.Entry(obj).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (System.Exception)
            {
                return View(member);
            }
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

        // DDLs


        [HttpGet]
        public JsonResult GetHafazahProgram()
        {
            var programs = _db.Paths.Where(x => x.ProgramType == Model.Enums.ProgramType.Hafazah).ToList();
            return Json(programs, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetFursanProgram()
        {
            var programs = _db.Paths.Where(x => x.ProgramType == Model.Enums.ProgramType.Fursan).ToList();
            return Json(programs, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult IsUsernameToken(string username)
        {
            SharedServices sharedServices = new SharedServices();
            var res= sharedServices.IsUsernameToken(username);
            return Json(res);
        }

        [HttpPost]
        public JsonResult IsEmailAlreadyExists(string email)
        {
            SharedServices sharedServices = new SharedServices();
            var res = sharedServices.IsEmaiAlreadylExists(email);
            return Json(res);
        }


        [HttpPost]
        public JsonResult IsPhoneNumberAlreadyExists(string phonenumber)
        {
            SharedServices sharedServices = new SharedServices();
            var res = sharedServices.IsPhoneNumberAlreadylExists(phonenumber);
            return Json(res);
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