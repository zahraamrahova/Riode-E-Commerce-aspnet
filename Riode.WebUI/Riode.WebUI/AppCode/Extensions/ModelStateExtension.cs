using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Riode.WebUI.AppCode.Extensions
{
    static public partial class Extension
    {
        static public bool IsModelStateValid(this IActionContextAccessor ctx){
            return ctx.ActionContext.ModelState.IsValid;
        }
    }
}
