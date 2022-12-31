using System;

namespace Hafazah.Model.Dtos
{
    public class MemberProfileInfo
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthDateAsString { get; set; }
        public string Address { get; set; }
        public string JobTitle { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int EducationLevelId { get; set; }
        public int QuranMemorizedId { get; set; }
        public int CountryId { get; set; }
    }
}
