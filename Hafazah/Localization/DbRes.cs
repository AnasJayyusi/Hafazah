using Hafazah.DAL;
using System.Linq;

namespace Hafazah.Localization
{
    public static class DbRes
    {
        public static string T(string resId, string resourceSet = null, string lang = null)
        {
            using (var _context = new HafazahDbContext())
            {
                var value = _context.Localizations.
                                                   Where(x => x.ResourceId == resId
                                                   && x.ResourceSet == resourceSet
                                                   && x.Locale == "ar").
                                                   Select(x => x.Value).FirstOrDefault();
                return value;
            }
        }
    }
}