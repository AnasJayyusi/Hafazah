namespace Hafazah.Localization
{
    public class Resources
    {
        public class GetApplicationLang
        {
            public static string Lang
            {
                get
                {
                    //return SysContext.Language == Language.Arabic ? "ar" : "en";
                    var cult = System.Threading.Thread.CurrentThread.CurrentCulture.ToString();
                    if (cult.StartsWith("ar"))
                        return "ar";

                    return "en";
                }
            }
        }


        public static System.String Login
        {
            get
            {
                return DbRes.T("Login", "LoginPage", GetApplicationLang.Lang);
            }
        }

    }
}

