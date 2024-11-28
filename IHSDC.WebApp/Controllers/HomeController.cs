using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc;
using IHSDC.WebApp.Models;
using IHSDC.WebApp.Connection;
using static IHSDC.WebApp.Filters.CustomFilters;
using IHSDC.WebApp.Helper;
using Microsoft.AspNet.Identity;
using IHSDC.Common.Models;
using Microsoft.AspNet.Identity.Owin;
using System.Data.SqlClient;

namespace IHSDC.WebApp.Controllers
{
    [Authorize]
    [SessionTimeoutAttribute]
    public class HomeController : Controller
    {

        readonly Connection.DBConnection con = new Connection.DBConnection();

        private ApplicationDbContext _db = new ApplicationDbContext();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        // GET: Session Time Out

        public ActionResult SessionTimeOut()
        {
            return View();

        }
        public ActionResult index2()
        {


            return View();

        }
        public ActionResult Policies()
        {
            return View();
        }
        public ActionResult HelpPdf()
        {
            string pdfFilePath = Server.MapPath("~/Help/aa7helpfile.pdf");

            Response.ContentType = "application/pdf";


            //return File(pdfFilePath, "application/pdf");
            return File(new FileStream(pdfFilePath, FileMode.Open), "application/pdf");
            //return View();

        }
        public ActionResult Maintenance()
        {


            return View();

        }
        public ActionResult ContactUs()
        {
          
            ViewBag.CurrentTime = DateTime.Now;
            #region For Unread AP Count
            InboxCRUD CntInbox = new InboxCRUD();
            var Cnt = con.CreateAPCRUD(10, CntInbox);
            if (Cnt.Count > 0)
            {
                ViewBag.UnReadMsg = Cnt.Count;
            }
            else
            {
                ViewBag.UnReadMsg = 0;
            }
            #endregion

            return View();

        }
        public ActionResult Index(string UnitId)
        {
            try
            {
                if (Session["UserIntId"] == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                var CurrentUser = UserManager.FindById(User.Identity.GetUserId());
                if (User.IsInRole("Administrator"))
                    ViewBag.count1 = Common.Helpers.Identity.Hierarchy.GetHierarchyUsers(CurrentUser).OrderBy(u => u.IntId).Count() - 1;

               

                UnitCRUD model = new UnitCRUD();
                model.Unit_ID = Convert.ToInt16(SessionManager.Unit_ID);
                var data = con.UnitCRUD(2, model);
                model.ILUnitCRUD = data;
                
                ViewBag.UnitName = data[0].UnitName;

                InboxCRUD model1 = new InboxCRUD();
                model1.UnitName = data[0].UnitName;

                return View(model1);
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
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
    }
}