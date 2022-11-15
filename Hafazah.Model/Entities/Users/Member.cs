using Hafazah.Model.Entities;
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

        [Required]
        [MaxLength(50)]
        [PasswordPropertyText]
        public string Username { get; set; }

        [Required]
        [MaxLength(50)]
        [DataType(DataType.Password)]
        public string SuggestPassword { get; set; }

        [MaxLength(50)]
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
        #endregion

        #region Not Mapped Member
        public int Age
        {
            get
            {
                int age = 0;
                DateTime now = DateTime.Today;
                if (BirthDate.HasValue)
                {
                    age = now.Year - BirthDate.Value.Year;
                    if (BirthDate > now.AddYears(-age)) age--;
                }
                return age;
            }
        }
        [NotMapped]
        public Role Role { get; set; }
        #endregion

    }
}




