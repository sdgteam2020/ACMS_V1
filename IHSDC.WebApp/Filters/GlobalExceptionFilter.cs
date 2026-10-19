using System;
using System.IO;
using System.Text;
using System.Web.Mvc;

namespace IHSDC.WebApp.Filters
{
    public class GlobalExceptionFilter : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext == null)
                return;

            if (!filterContext.ExceptionHandled)
            {
                try
                {
                    Exception ex = filterContext.Exception;

                    string folderPath = filterContext.HttpContext.Server
                        .MapPath("~/App_Data/ErrorLogs");

                    Directory.CreateDirectory(folderPath);

                    string filePath = Path.Combine(
                        folderPath,
                        "ErrorLog_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt"
                    );

                    var log = new StringBuilder();

                    log.AppendLine("======================================");
                    log.AppendLine("Date       : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    log.AppendLine("Controller : " + filterContext.RouteData.Values["controller"]);
                    log.AppendLine("Action     : " + filterContext.RouteData.Values["action"]);
                    log.AppendLine("URL        : " + filterContext.HttpContext.Request.RawUrl);
                    log.AppendLine("Method     : " + filterContext.HttpContext.Request.HttpMethod);
                    log.AppendLine("User       : " + filterContext.HttpContext.User?.Identity?.Name);
                    log.AppendLine("Exception  : " + ex.GetType().FullName);
                    log.AppendLine("Message    : " + ex.Message);
                    log.AppendLine("Details    : " + ex.ToString());
                    log.AppendLine("======================================");
                    log.AppendLine();

                    File.AppendAllText(filePath, log.ToString(), Encoding.UTF8);
                }
                catch (Exception logEx)
                {
                    System.Diagnostics.Trace.TraceError(
                        "Failed to write exception log: " + logEx);
                }
            }

            base.OnException(filterContext);
        }
    }
}