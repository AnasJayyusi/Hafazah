using Hafazah.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [MaxLength(10)]
        public string PhoneNumber { get; set; }

    }
}
