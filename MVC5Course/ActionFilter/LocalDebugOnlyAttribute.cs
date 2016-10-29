using System;
using System.Web.Mvc;   // 要用對

namespace MVC5Course.Controllers
{
    public class LocalDebugOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.Request.IsLocal)     // 判斷要求是否來自本地端
            {
                filterContext.Result = new RedirectResult("/"); // 如果不是本地端要求就導回首頁
            }
        }
    }
}