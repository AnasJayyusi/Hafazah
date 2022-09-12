using Hafazah.Model.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Hafazah.Model
{
    public class Member : IBaseEntity
    {
        #region Main Information
        [Required]
        [MaxLength(128)]
        public string FirstName { get; set; }

        [MaxLength(128)]
        public string SecondName { get; set; }

        [MaxLength(128)]
        public string ThirdName { get; set; }

        [Required]
        [MaxLength(128)]
        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public DateTime? BirthDate { get; set; }

        [MaxLength(128)]
        public string Country { get; set; }

        [MaxLength(256)]
        public string Address { get; set; }

        [MaxLength(128)]
        public string EducationLevel { get; set; }

        [MaxLength(128)]
        public string JobTitle { get; set; }

        [MaxLength(50)]
        public string Username { get; set; }

        [MaxLength(10)]
        [RegularExpression("[0-0][7-7][7-9]")]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        [MaxLength(128)]
        public string Email { get; set; }
        #endregion

        #region More Info
        public string QuranMemorized { get; set; }
        public DateTime? InterviewDate { get; set; }
        #endregion

        [MaxLength(256)]
        public string KnownFrom { get; set; }

        #region Releated To The Program
        //public int? CurrentLevel { get; set; }
        //public string CurrentPath { get; set; }
        #endregion

        #region  Not Showing ON UI
        public bool IsActive { get; set; }
        #endregion
    }
}

public enum Gender
{
    Female,
    Male
}