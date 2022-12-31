using Hafazah.DAL;
using Hafazah.Model.Dtos;
using Hafazah.Model.Entities.Program;
using Hafazah.Utility;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Member = Hafazah.Model.Member;
using System.Data.Entity;

namespace Hafazah.Services
{
    public class SharedServices
    {
        public HafazahDbContext _db;
        internal SharedServices()
        {
            _db = new HafazahDbContext();
        }
        internal void AddMember(Member member, out List<string> validations)
        {
            validations = GetValidationsErrors(member);
            if (!validations.Any())
            {
                member.BirthDate = DateTimeResolver.GetStringAsDateTime(member.BirthDateAsString);
                _db.Members.Add(member);
                _db.SaveChanges();
            }
        }

        internal bool UpdateMember(MemberProfileInfo memberProfileInfo)
        {
            var originalmemberdata = _db.Members.Find(memberProfileInfo.Id);

            if (originalmemberdata != null)
            {
                originalmemberdata.FirstName = memberProfileInfo.FirstName;
                originalmemberdata.SecondName = memberProfileInfo.SecondName;
                originalmemberdata.ThirdName = memberProfileInfo.ThirdName;
                originalmemberdata.LastName = memberProfileInfo.LastName;
                originalmemberdata.BirthDate = DateTimeResolver.GetStringAsDateTime(memberProfileInfo.BirthDateAsString);
                originalmemberdata.Address = memberProfileInfo.Address;
                originalmemberdata.JobTitle = memberProfileInfo.JobTitle;
                originalmemberdata.PhoneNumber = memberProfileInfo.PhoneNumber;
                originalmemberdata.Email = memberProfileInfo.Email;
                originalmemberdata.EducationLevelId = memberProfileInfo.EducationLevelId;
                originalmemberdata.QuranMemorizedId = memberProfileInfo.QuranMemorizedId;
                originalmemberdata.CountryId = memberProfileInfo.CountryId;

                _db.Members.AddOrUpdate(originalmemberdata);
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        internal void SetToken(string username, string token)
        {
            Member member = _db.Members.Where(x => x.Username.ToLower() == username.ToLower()).Single();
            member.NotificationToken = token;
            _db.SaveChanges();
        }

        internal string GetToken(string username)
        {
            return _db.Members.FirstOrDefault(x => x.Username == username).NotificationToken;
        }

        internal DropDownListOptions GetDDLsOptions()
        {
            DropDownListOptions dropDownListOptions = new DropDownListOptions()
            {
                Countries = _db.Countries.ToList(),
                EducationLevels = _db.EducationLevels.ToList(),
                LevelNames = _db.LevelNames.ToList(),
                QuranMemorized = _db.QuranMemorized.ToList(),
            };

            return dropDownListOptions;
        }

        internal List<Member> GettingLazyMemberWithCounter()
        {
            List<Member> membersAlreadyStarts = _db.Members.Where(x => x.LastSent.HasValue).ToList();
            List<Member> lazyMembers = new List<Member>();
            DateTime now = DateTime.Now.AddDays(-3);

            foreach (var member in membersAlreadyStarts)
            {
                if (member.LastSent.Value.Date < now)
                    lazyMembers.Add(member);

                if (member.WarningCounter < 0)
                    lazyMembers.Add(member);
            }
            return lazyMembers.DistinctBy(x => x.Username).ToList();
        }

        internal void UpdateCounter(string username)
        {
            _db.Members.Single(x => x.Username.ToLower() == username.ToLower()).WarningCounter += 1;
        }

        internal List<Level> GettingPaths()
        {
            return _db.Levels.ToList();
        }

        internal string SetPageNumberWithSentDate(int pageNumber, DateTime sendDate, string username)
        {
            if (string.IsNullOrEmpty(username))
                return "UnAuthorized";

            if (username.Equals("Administrator"))
                return "You Don't have access to a program";


            Member member = _db.Members.Single(x => x.Username == username);
            if (member.IsActive)
            {
                member.AccomplishedPages = pageNumber;
                member.LastSent = sendDate;
                _db.SaveChanges();
                return @"'isSucceeded':'true'";
            }
            else
                return "Your account is deactivated";
        }

        internal bool GetRegistrationStatus()
        {
            var obj = _db.GlobalValues.Single(x => x.Key == "ChangeRegistrationStatus");
            return obj.Value == "true";
        }

        internal string UpdateProfilePicture(string username, string imgBase64)
        {
            var user = _db.Members.SingleOrDefault(x => x.Username == username);
            if (user != null)
            {
                user.ProfilePictureBase64 = imgBase64;
                _db.SaveChanges();
                return "Uploaded Successfully";
            }
            return "User Not Exists";
        }

        internal bool IsUsernameToken(string username)
        {
            var user = _db.Users
                                .Where(x => x.UserName.ToLower() == username).FirstOrDefault();
            return user != null;
        }

        internal bool IsEmaiAlreadylExists(string email)
        {
            var user = _db.Users
                                .Where(x => x.Email.ToLower() == email).FirstOrDefault();
            return user != null;
        }

        internal bool IsPhoneNumberAlreadylExists(string email)
        {
            var user = _db.Users
                                .Where(x => x.PhoneNumber.ToLower() == email).FirstOrDefault();
            return user != null;
        }

        internal dynamic GetUserProgram(string username)
        {
            // Get User Path 
            var m = _db.Members.Single(x => x.Username.ToLower() == username.ToLower());
            int pId = m.PathId;

            // Get Stages / Level based on user 
            if (m.ProgramType == Model.Enums.ProgramType.Hafazah)
                return _db.Paths.Include(x => x.Phases).Where(x => x.Id == pId);
            else
                return _db.Paths.Include(x => x.Levels).Where(x => x.Id == pId);
        }

        internal dynamic GetHomeWorks(Model.Enums.ProgramType programType, int selectedNumber)
        {
            // Get Stages / Level based on user 
            if (programType == Model.Enums.ProgramType.Hafazah)
                return _db.PhaseHomeworks.Where(x => x.PhaseId == selectedNumber).OrderBy(x=>x.OrderNumber);
            else
                return _db.LevelHomeworks.Where(x => x.LevelId == selectedNumber).OrderBy(x => x.OrderNumber);
        }

        #region Helpers
        // Validations
        private List<string> GetValidationsErrors(Member m)
        {
            List<string> errors = new List<string>();

            if (string.IsNullOrEmpty(m.FirstName))
                errors.Add("Missing First Name");

            if (string.IsNullOrEmpty(m.LastName))
                errors.Add("Missing Last Name");

            if (string.IsNullOrEmpty(m.Username))
                errors.Add("Missing Username");

            if (string.IsNullOrEmpty(m.SuggestPassword))
                errors.Add("Missing Password");

            if (string.IsNullOrEmpty(m.PhoneNumber))
                errors.Add("Missing Phone Number");

            if (string.IsNullOrEmpty(m.Email))
                errors.Add("Missing Email");

            if (m.SuggestPassword.Length < 6)
                errors.Add("Password must have at least 6 non letter or digit character");

            if (IsUsernameToken(m.Username.ToLower()))
                errors.Add("User already token");


            if (IsEmaiAlreadylExists(m.Email.ToLower()))
                errors.Add("Email already exists");

            return errors;
        }




        #endregion

        #region Core 
        //public void GetUserProgram(string username)
        //{
        //    // Get 
        //    var member=_db.Members.Where(x=>x.Username == username).Single();


        //}
        #endregion
    }
}

