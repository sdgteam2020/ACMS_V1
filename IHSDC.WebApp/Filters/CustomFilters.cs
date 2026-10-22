using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace IHSDC.WebApp.Filters
{
    public class CustomFilters
    {
        public class SessionTimeoutAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                HttpContext ctx = HttpContext.Current;

                bool allowAnonymous =
                    filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) ||
                    filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true);

                if (allowAnonymous)
                {
                    base.OnActionExecuting(filterContext);
                    return;
                }

                if (ctx.Session?["UserIntId"] == null && ctx.Session?["UserId"] == null)
                {
                    ctx.Session?.Clear();
                    ctx.Session?.Abandon();

                    filterContext.Result = new RedirectResult("~/Account/LocalLogout");
                    return;
                }

                base.OnActionExecuting(filterContext);
            }
        }
    }
}