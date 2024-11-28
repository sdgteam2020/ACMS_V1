using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IHSDC.Common.Models;

namespace IHSDC.WebApp.Controllers
{
    public class AnalyticsController : Controller
    {
        
    }

    public class Analytics
    {
        internal static void LogVisit(DateTime start, string ip, string mac, string machine, _Browser browser)
        {


            using(var db = new ApplicationDbContext())
            {
                db.Visits.Add(new Visit()
                {
                    Start = start,
                    End = start,
                    IP = ip,
                    MAC = mac,
                    Browser = browser,
                    MachineName = machine
                });
                db.SaveChanges();
            }   
        }

 
    }

}