using System.Text.RegularExpressions;

namespace Riode.WebUI.AppCode.Extensions
{
    static public partial class Extension
    {
        static public string GetCurrentCulture(this HttpContext ctx)
        {
            Match match = Regex.Match(ctx.Request.Path, @"\/(?<lang>az|en|ru)\/?.*");
            if (match.Success)
                return match.Groups["lang"].Value;
            if (ctx.Request.Cookies.TryGetValue("lang", out string lang))
                return lang;
            return "az";
        }

    }
}
