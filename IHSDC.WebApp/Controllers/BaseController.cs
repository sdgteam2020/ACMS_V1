using IHSDC.Common.Helpers.Bootstrap4;
using IHSDC.Common.Helpers.MVC;
using IHSDC.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IHSDC.WebApp.Controllers
{
    public class BaseController : Controller
    {
        public void Success(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Success, message, dismissable);
        }

        public void Information(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Information, message, dismissable);
        }

        public void Warning(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Warning, message, dismissable);
        }

        public void Danger(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Danger, message, dismissable);
        }

        private void AddAlert(string alertStyle, string message, bool dismissable)
        {
            var alerts = TempData.ContainsKey(Alert.TempDataKey)
                ? (List<Alert>)TempData[Alert.TempDataKey]
                : new List<Alert>();

            alerts.Add(new Alert
            {
                AlertStyle = alertStyle,
                Message = message,
                Dismissable = dismissable
            });

            TempData[Alert.TempDataKey] = alerts;
        }

        public void LogVisit()
        {
            Analytics.LogVisit(DateTime.Now,
                        VisitDetails.GetIPAddress(),
                        VisitDetails.GetMACAddress(),
                        VisitDetails.GetMachineName(),
                        VisitDetails.GetBrowser());
        }

        internal static void LogSignin(string username, int intId, string sessionId)
        {
            using (var db = new ApplicationDbContext())
            {
                db.Logins.Add(new Common.Models.Login()
                {
                    Username = username,
                    LoggedInAt = DateTime.Now,
                    UserIntId=intId,
                    IsLoggedIn = true,
                    LoggedOutAt = DateTime.Now,
                    SessionId = sessionId
                });
                db.SaveChanges();
            }
        }
    }
}