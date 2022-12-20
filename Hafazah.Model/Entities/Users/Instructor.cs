using Hafazah.Model.Entities.DropDownListOptions;
using Hafazah.Model.Enums;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hafazah.Model.Entities.Users
{
    public class Instructor : BaseEntity
    {
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

        [MaxLength(50)]
        public string PhoneNumber { get; set; }

        public string OtherDetails { get; set; }

        #region UnRequired Columns

        [Required]
        [MaxLength(50)]
        [PasswordPropertyText]
        public string Username { get; set; }


        [Required]
        [MaxLength(50)]
        [DataType(DataType.Password)]
        public string SuggestPassword { get; set; }

        [EmailAddress]
        [MaxLength(128)]
        public string Email { get; set; }

        public ProgramType ProgramType { get; set; }

        public bool IsActive { get; set; } = false;
        #endregion

        #region RelationShips
        public int? EducationLevelId { get; set; }
        public EducationLevel EducationLevel { get; set; }

        public int? QuranMemorizedId { get; set; }
        public QuranMemorized QuranMemorized { get; set; }
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
