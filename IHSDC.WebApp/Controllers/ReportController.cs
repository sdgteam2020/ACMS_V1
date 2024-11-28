using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IHSDC.WebApp.Models;
using IHSDC.WebApp.Connection;
using static IHSDC.WebApp.Filters.CustomFilters;
using IHSDC.Common.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using IHSDC.WebApp.Helper;

namespace IHSDC.WebApp.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        readonly Connection.DBConnection con = new Connection.DBConnection();
        private ApplicationDbContext _db = new ApplicationDbContext();

        private ApplicationUserManager _userManager;

        int item = 0;
        public ActionResult UnitStrReturn(string id)
        {

            TransationStrengthReturn model = new TransationStrengthReturn();
            ViewBag.ButtonName = "Add";
            if (id != null && id != string.Empty)
            {
                ViewBag.ButtonName = "Update";
                 item = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(id));
                model.StrId = item;
                var editData = con.IUDUnitStrReturn(model, 2).ToList();
                model = editData[0];

            }
            var result = con.IUDUnitStrReturn(model, 2).ToList();
            ViewBag.count = result.Count();
            model.ILTransationStrengthReturn = result;
            return View(model);
        }


        [HttpPost]
        public ActionResult UnitStrReturn(string btnName, TransationStrengthReturn model)
        {
            if(btnName == "Add")
            {
                var result = con.IUDUnitStrReturn(model, 1);
                if (result[0].IsSuccess == true)
                {
                    DisplayMessage(result[0].Msg, result[0].MidMsg, result[0].MsgStatus);

                }
                if (result[0].IsExist == true)
                {
                    DisplayMessage(result[0].Msg, result[0].MidMsg, result[0].MsgStatus);

                }

            }

            if (btnName == "Update")
            {
                 var result = con.IUDUnitStrReturn(model, 4);
                if (result[0].IsSuccess == true)
                {
                    DisplayMessage(result[0].Msg, result[0].MidMsg, result[0].MsgStatus);

                }               
            }
            return RedirectToAction("UnitStrReturn", new { id = string.Empty });
        }


        public ActionResult DeleteUnitStrReturn(string id)
        {

            TransationStrengthReturn model = new TransationStrengthReturn();
            int item = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(id));
            model.Aviator_ID = item;
            model.UserId = Convert.ToInt32(SessionManager.UserIntId);
            var data = con.IUDUnitStrReturn( model,5);
            if (data[0].IsSuccess == true)
            {
                DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);         
            }
            return RedirectToAction("UnitStrReturn", new { id = string.Empty });
        }

        [HttpGet]
        public ActionResult GenerateIAFZ(string id )
        {
            

            TransationStrengthReturn model = new TransationStrengthReturn();
           model.StrId = item = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(id));
            var result = con.IUDUnitStrReturn(model, 3).ToList();
            model.ILTransationStrengthReturn = result;
            return View(model);
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

        #region Reports

        [HttpGet]
        public ActionResult Reports()
        {
            try
            {
                ReportsModels model = new ReportsModels();
                model.TypeOfConfId = 0;
                model.ScheduleId = 0;
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
                return View(model);
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
                    return Redirect("/Error/Error");
                }

                return Redirect("/Error/Error");
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
                    return Redirect("/Error/Error");
                }
                return Redirect("/Error/Error");
            }
        }



        [HttpPost]
        public ActionResult Reports(ReportsModels model)
        {
            try
            {
                if (model.Type==1)
                {
                    return RedirectToAction("AppxAReport", new { Conf = RepositryManager.EncryptionManager.Encryption(model.ScheduleId.ToString()), RefNo = RepositryManager.EncryptionManager.Encryption(model.ConfRefNo.ToString()) });
                }
                else
                {
                    return RedirectToAction("AppxBReport", new { Conf = RepositryManager.EncryptionManager.Encryption(model.ScheduleId.ToString()), RefNo = RepositryManager.EncryptionManager.Encryption(model.ConfRefNo.ToString()) });
                }
                //return View();
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
                    return Redirect("/Error/Error");
                }

                return Redirect("/Error/Error");
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
                    return Redirect("/Error/Error");
                }
                return Redirect("/Error/Error");
            }
        }

        [HttpGet]
        public ActionResult AppxAReport(string Conf,string RefNo)
        {
            try
            {
                InboxCRUD model1 = new InboxCRUD();

                if (Session["UserIntId"] == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                if (Conf != null && Conf != string.Empty)
                {
                    int item = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(Conf));
                    model1.Schedule_ID = item;
                }

                if (RefNo != null && RefNo != string.Empty)
                {
                    ViewBag.RefNo = RepositryManager.EncryptionManager.Decryption(RefNo);
                }

                var CurrentUser = UserManager.FindById(User.Identity.GetUserId());
                if (User.IsInRole("Administrator"))
                    ViewBag.count1 = Common.Helpers.Identity.Hierarchy.GetHierarchyUsers(CurrentUser).OrderBy(u => u.IntId).Count() - 1;

            
                ViewBag.ipadd = Request.UserHostAddress + " :: " + DateTime.Now;

                DataSet data1 = con.GetAppxReport(1, "proc_AppxReport", model1.Schedule_ID);
                if (data1.Tables.Count > 1)
                {
                    ViewBag.tbl_Data = data1.Tables[0];
                    ViewBag.Header = data1.Tables[1];
                    ViewBag.Record = data1.Tables.Count;
                }
                else
                {
                    ViewBag.Record = 0;
                }

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
                    return Redirect("/Error/Error");
                }

                return Redirect("/Error/Error");
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
                    return Redirect("/Error/Error");
                }
                return Redirect("/Error/Error");
            }
        }

        [HttpGet]
        public ActionResult AppxBReport(string Conf, string RefNo)
        {
            try
            {
                InboxCRUD model1 = new InboxCRUD();

                if (Session["UserIntId"] == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                if (Conf != null && Conf != string.Empty)
                {
                    int item = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(Conf));
                    model1.Schedule_ID = item;
                }

                if (RefNo != null && RefNo != string.Empty)
                {
                    ViewBag.RefNo = RepositryManager.EncryptionManager.Decryption(RefNo);
                }

                var CurrentUser = UserManager.FindById(User.Identity.GetUserId());
                if (User.IsInRole("Administrator"))
                    ViewBag.count1 = Common.Helpers.Identity.Hierarchy.GetHierarchyUsers(CurrentUser).OrderBy(u => u.IntId).Count() - 1;


                ViewBag.ipadd = Request.UserHostAddress + " :: " + DateTime.Now;

                DataSet data1 = con.GetAppxReport(2, "proc_AppxReport", model1.Schedule_ID);
                if (data1.Tables.Count > 1)
                {
                    ViewBag.tbl_Data = data1.Tables[0];
                    ViewBag.Header = data1.Tables[1];
                    ViewBag.Record = data1.Tables.Count;
                }
                else
                {
                    ViewBag.Record = 0;
                }

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
                    return Redirect("/Error/Error");
                }

                return Redirect("/Error/Error");
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
                    return Redirect("/Error/Error");
                }
                return Redirect("/Error/Error");
            }
        }
        #endregion


        #region message
        public void DisplayMessage(string message, string midMsg, string messageStatus_s_e_w_i_Or_blank)
        {
            string status = messageStatus_s_e_w_i_Or_blank.ToLower();
            TempData["Message"] = message;
            TempData["messagemidStatus"] = midMsg;
            if (status == "s")
                TempData["messageStatus"] = "success";
            else if (status == "e")
                TempData["messageStatus"] = "error";
            else if (status == "w")
                TempData["messageStatus"] = "warning";
            else if (status == "i")
                TempData["messageStatus"] = "info";
        }
        #endregion


    }
}