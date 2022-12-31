using System;
using System.Globalization;

namespace Hafazah.Utility
{
    public class DateTimeResolver
    {
        public static DateTime GetStringAsDateTime(string strDate)
        {
            if (!string.IsNullOrWhiteSpace(strDate))
            {
                CultureInfo culture = new CultureInfo("en-US");
                DateTime oDate = Convert.ToDateTime(strDate, culture);

                return oDate;
            }
            return DateTime.Now;
        }
    }
}