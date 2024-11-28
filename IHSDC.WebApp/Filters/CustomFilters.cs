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
               
                if (HttpContext.Current.Session["UserIntId"] == null&& HttpContext.Current.Session["UserId"] == null)
                {
                    ctx.Session.Abandon();
                    ctx.Session.Clear();

                    filterContext.Result = new RedirectResult("~/Account/Login");
                    return;

                }
                base.OnActionExecuting(filterContext);
            }
        }
    }
}