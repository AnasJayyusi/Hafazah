using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hafazah.Model.Entities.Users
{
    public class Instructor : IBaseEntity
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
        [RegularExpression("[0-0][7-7][7-9]\\d{7}")]
        public string PhoneNumber { get; set; }

        public List<Member> Members { get; set; }
    }
}
