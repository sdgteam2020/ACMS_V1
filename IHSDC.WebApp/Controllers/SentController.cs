using IHSDC.Common.Models;
using IHSDC.WebApp.Helper;
using IHSDC.WebApp.Models;
using Microsoft.AspNet.Identity.Owin;
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
    public class SentController : Controller
    {
        readonly Connection.DBConnection con = new Connection.DBConnection();
        private ApplicationDbContext _db = new ApplicationDbContext();

        private ApplicationUserManager _userManager;

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

        #region Sent Box
        [HttpGet]
        public ActionResult AddSentBox()
        {
            try
            {
                if (Session["UserIntId"] == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                //watermark dataTable
                var ipAddress = Request.UserHostAddress;
                var currentDatetime = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                var watermarkText = $" {ipAddress}\n  {currentDatetime}";
                ViewBag.ipadd = watermarkText;

                InboxCRUD model1 = new InboxCRUD();
                var data1 = con.CreateAPCRUD(12, model1);
                model1.ILInboxCRUD = data1;
                ViewBag.count = model1.ILInboxCRUD.Count;


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

        [HttpPost]
        public ActionResult AddSentBox(string id, string fid)
        {
            try
            {
                int OrdId = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(id));
                int FwdId = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(fid));

                InboxCRUD model = new InboxCRUD();
                model.Ord = OrdId;
                model.FwdId = FwdId;
                var data = con.RevertMailCRUD(2, model);
                if (data[0].IsSuccess == true)
                {
                    return Json(new { success = true, Msg = data[0].Msg });
                }
                else
                {
                    return Json(new { success = false, Msg = data[0].Msg });
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
        public ActionResult ChangeNodal(InboxCRUD model)
        {
            try
            {
                var data = con.ForwardCRUD(4, model);
                if (data[0].IsSuccess == true)
                {
                    DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);
                }
                return RedirectToAction("AddSentBox");
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