using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hafazah.Model.Enums
{

    public enum ProgramType
    {
        NotSet = -1,
        Hafazah,
        Fursan
    }

    public enum Role
    {
        Admin,
        Instrcutor,
        Student
    }

    public enum Gender
    {
        Female,
        Male
    }


    public enum MagicString
    {
        Administrator
    }


    public enum ErrorCode : ushort
    {
        None = 0,
        Unknown = 1,
        ConnectionLost = 100,
        OutlierReading = 200
    }


    public enum TimeEnum
    {
        Morning,
        Evening
    }




}
