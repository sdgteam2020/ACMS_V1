using IHSDC.Common.Models;
using IHSDC.WebApp.Connection;
using IHSDC.WebApp.Helper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using static IHSDC.WebApp.Filters.CustomFilters;

namespace IHSDC.WebApp.Controllers
{
    [SessionTimeoutAttribute]
    public class HierarchyController : Controller
    {
        readonly SearchDBConnection con = new SearchDBConnection();
        // GET: Hierarchy
        public ActionResult Index()
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    ViewBag.Root = db.Users.FirstOrDefault(p => p.Superior == null).UserName;
                    if (db.Users.Count() == 1) return HttpNotFound("No user exists in hierarchy. Nothing to display!");

                    var users = db.Users.ToList().OrderBy(u => u.IntId);
                    var FullHierarchyList = new List<FullHierarchyView>();
                    foreach (var user in users)
                    {
                        var HierarchyList = Common.Helpers.Identity.Hierarchy.GetHierarchy(user);
                        foreach (var item in HierarchyList)
                        {
                            if (user.IntId != item.IntId)
                            {
                                FullHierarchyList.Add(
                                    new FullHierarchyView
                                    {
                                        UserId = user.IntId,
                                        ChildId = item.IntId
                                    });
                            }
                            else
                            {
                                FullHierarchyList.Add(
                                    new FullHierarchyView
                                    {
                                        UserId = user.IntId,
                                        ChildId = 0
                                    });
                            }
                        }
                    }
                    return View(FullHierarchyList);
                }
            }
            catch (SqlException ex)
            {

                StringBuilder errorMessages = ErrorLog.BindErrorLog(ex);

                try
                {
                    con.ErrorLogDashboard2(3, errorMessages.ToString().Replace("'", "''"), ex.StackTrace.ToString(), "Database");
                }
                catch (Exception ex1)
                {
                    _ = ex1.Message;
                    return Redirect("/Error/");
                }

                return Redirect("/Error/");
            }
            catch (Exception ex)
            {
                // Handle generic ones here.  
                try
                {
                    con.ErrorLogDashboard2(3, ex.Message.ToString(), ex.StackTrace.ToString(), "Page");
                }
                catch (Exception ex1)
                {
                    _ = ex1.Message;
                    return Redirect("/Error/");
                }
                return Redirect("/Error/");
            }

        }
    }
}