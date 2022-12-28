using Hafazah.Model.Entities;
using Hafazah.Model.Entities.DropDownListOptions;
using Hafazah.Model.Entities.Users;
using Hafazah.Model.Enums;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hafazah.Model
{
    public class Member : BaseEntity
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

        [Required]
        public Gender Gender { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        [MaxLength(256)]
        public string Address { get; set; }


        [Required]
        [MaxLength(128)]
        public string JobTitle { get; set; }

        [Required]
        [MaxLength(50)]
        [PasswordPropertyText]
        public string Username { get; set; }

        [Required]
        [MaxLength(50)]
        [DataType(DataType.Password)]
        public string SuggestPassword { get; set; }

        [Required]
        [MaxLength(50)]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(128)]
        public string Email { get; set; }
        #endregion

        #region More Info
        public DateTime? InterviewDate { get; set; }

        [Required]
        public TimeEnum PreferenceTime { get; set; }
        #endregion

        [Required]
        [MaxLength(256)]
        public string KnownFrom { get; set; }

        #region Releated To The Program
        [Required]
        public ProgramType ProgramType { get; set; }
        public string CurrentPath { get; set; }
        public int? CurrentLevel { get; set; }
        public int? WarningCounter { get; set; }
        [DefaultValue(0)]
        public int? AccomplishedPages { get; set; }
        public DateTime? LastSent { get; set; }
        #endregion

        #region  Not Showing ON UI
        public bool IsActive { get; set; }
        public string NotificationToken { get; set; }
        public string ProfilePictureBase64 { get; set; }
        #endregion

        #region RelationShips
        public int? InstrcutorId { get; set; }
        public Instructor Instrcutor { get; set; }

        [Required]
        public int EducationLevelId { get; set; }
        public EducationLevel EducationLevel { get; set; }

        [Required]
        public int QuranMemorizedId { get; set; }
        public QuranMemorized QuranMemorized { get; set; }

        [Required]
        public int CountryId { get; set; }
        public Country Country { get; set; }
        #endregion

        #region Not Mapped Member
        public int Age
        {
            get
            {
                int age = 0;
                DateTime now = DateTime.Today;
                age = now.Year - BirthDate.Year;
                if (BirthDate > now.AddYears(-age)) age--;
                return age;
            }
        }
        [NotMapped]
        public Role Role { get; set; }
        #endregion

    }
}




