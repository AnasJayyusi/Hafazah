using Hafazah.DAL;
using Hafazah.Model.Dtos;
using Hafazah.Model.Entities.Program;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using Member = Hafazah.Model.Member;

namespace Hafazah.Services
{
    public class SharedServices
    {
        public HafazahDbContext _db;
        internal SharedServices()
        {
            _db = new HafazahDbContext();
        }
        internal void AddNewMember(Member member, out List<string> validations)
        {
            validations = GetValidationsErrors(member);
            if (!validations.Any())
            {
                _db.Members.Add(member);
                _db.SaveChanges();
            }
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
                return "Your account is deactivated ";
        }

        internal bool GetRegistrationStatus()
        {
            var obj = _db.GlobalValues.Single(x => x.Key == "ChangeRegistrationStatus");
            return obj.Value == "true";
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

            return errors;
        }
        #endregion
    }
}

