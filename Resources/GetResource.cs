using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Resources
{
    public class GetResource
    {

        public static string T(string resourcesName)
        {
            var cult = System.Threading.Thread.CurrentThread.CurrentCulture.ToString();
            ResourceManager rm;
            CultureInfo resourceCulture;

            if (cult.Equals("ar"))
            {
                resourceCulture = new CultureInfo("ar");
                rm = new ResourceManager("Resources.Resource-ar", Assembly.GetExecutingAssembly());
                return rm.GetString(resourcesName, resourceCulture);
            }
            else
            {
                resourceCulture = new CultureInfo("en");
                rm = new ResourceManager("Resources.Resource-en", Assembly.GetExecutingAssembly());
                return rm.GetString(resourcesName, resourceCulture);
            }
        }


        public static string GetLang()
        {
            var cult = System.Threading.Thread.CurrentThread.CurrentCulture.ToString();
            return cult;
        }
    }
}


