using IHSDC.Common.Models;
using IHSDC.WebApp.Connection;
using IHSDC.WebApp.Helper;
using IHSDC.WebApp.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using static IHSDC.WebApp.Filters.CustomFilters;

namespace IHSDC.WebApp.Controllers
{
    [SessionTimeoutAttribute]
    public class NotingController : Controller
    {
        readonly Connection.DBConnection con = new Connection.DBConnection();
        private ApplicationDbContext _db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;

        #region Noting
        [HttpGet]
        public ActionResult AddNoting(string id)
        {
            try
            {
                if (Session["UserIntId"] == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                NotingDetail Notingmodel = new NotingDetail();
                InboxCRUD model1 = new InboxCRUD();


                if (id != null && id != string.Empty)
                {
                    int item = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(id));
                    Notingmodel.InboxId = item;

                    model1.Inbox_ID = item;
                    var data = con.CreateAPCRUD(8, model1);
                    if (data.Count > 0)
                    {
                        model1 = data[0];
                        ViewBag.UploadPath = data[0].UploadPath;
                        ViewBag.WordFilePath = data[0].WordFilePath;
                        ViewBag.ConfTitle = data[0].Title;
                        AgendaPointLog APLog = new AgendaPointLog();
                        APLog.Inbox_ID = item;
                        var Logdata = con.AgendaPointLogCRUD(9, APLog);
                        if (Logdata.Count > 0)
                        {
                            model1.ILAgendaPointLog = Logdata;
                            ViewBag.ipadd = Request.UserHostAddress + " :: " + DateTime.Now;
                        }
                        int ReadUnRead = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(id));
                        model1.Status = ReadUnRead;

                        var editdata = con.CreateAPCRUD(6, model1);
                    }
                }
                var data1 = con.CreateNotingDetailCRUD(2, Notingmodel);
                model1.ILNotingCRUD = data1;
                ViewBag.NotingCount = model1.ILNotingCRUD.Count;

                UnitCRUD model2 = new UnitCRUD();
                model2.ScheduleId = model1.Schedule_ID;
                model2.InboxID = model1.Inbox_ID;
                var data2 = con.UnitCRUD(11, model2);
                model1.ILUnitCRUD = data2;

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


        #region Open Agenda Pt Noting after Closed Agenda Pt
        [HttpGet]
        public ActionResult OpenAgendaPtNoting(string id)
        {
            try
            {
                if (Session["UserIntId"] == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                NotingDetail Notingmodel = new NotingDetail();
                InboxCRUD model1 = new InboxCRUD();


                if (id != null && id != string.Empty)
                {
                    int item = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(id));
                    Notingmodel.InboxId = item;

                    model1.Inbox_ID = item;
                    var data = con.CreateAPCRUD(17, model1);
                    if (data.Count > 0)
                    {
                        model1 = data[0];
                        ViewBag.UploadPath = data[0].UploadPath;
                        ViewBag.WordFilePath = data[0].WordFilePath;
                        ViewBag.ConfTitle = data[0].Title;
                        AgendaPointLog APLog = new AgendaPointLog();
                        APLog.Inbox_ID = item;
                        var Logdata = con.AgendaPointLogCRUD(18, APLog);
                        if (Logdata.Count > 0)
                        {
                            model1.ILAgendaPointLog = Logdata;
                            ViewBag.ipadd = Request.UserHostAddress + " :: " + DateTime.Now;
                        }
                        int ReadUnRead = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(id));
                        model1.Status = ReadUnRead;

                        var editdata = con.CreateAPCRUD(6, model1);
                    }
                }
                var data1 = con.CreateNotingDetailCRUD(4, Notingmodel);
                model1.ILNotingCRUD = data1;
                ViewBag.NotingCount = model1.ILNotingCRUD.Count;

                UnitCRUD model2 = new UnitCRUD();
                model2.ScheduleId = model1.Schedule_ID;
                model2.InboxID = model1.Inbox_ID;
                var data2 = con.UnitCRUD(11, model2);
                model1.ILUnitCRUD = data2;

                #region For Unread AP Count
                InboxCRUD CntInbox = new InboxCRUD();
                var Cnt = con.CreateAPCRUD(19, CntInbox);
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
        public ActionResult AddCommentOpenAgenda(InboxCRUD model)
        {

            try
            {
                NotingDetail Notingmodel = new NotingDetail();
                Notingmodel.UserId = Convert.ToInt32(SessionManager.UserIntId);
                Notingmodel.InboxId = model.Inbox_ID;
                Notingmodel.Comment = model.Comment;

                var file = Request.Files[0];
                if (file != null && file.ContentLength > 0)
                {
                    var originalFileName = Path.GetFileName(file.FileName);

                    var newFileName = GenerateNewFileName(originalFileName);

                    var fileModel = new FileModel
                    {
                        FileName = newFileName
                    };

                    using (var reader = new BinaryReader(file.InputStream))
                    {
                        fileModel.Data = reader.ReadBytes(file.ContentLength);
                    }

                    var filePath = Path.Combine(Server.MapPath("~/Uploads"), fileModel.FileName);
                    Notingmodel.Attachment = Request.Url.GetLeftPart(UriPartial.Authority) + "/Uploads/" + fileModel.FileName;
                    System.IO.File.WriteAllBytes(filePath, fileModel.Data);
                }

                if (Convert.ToString(SessionManager.RoleId) != enum1.Administrator)
                {
                    var data = con.CreateNotingDetailCRUD(1, Notingmodel);
                    if (data[0].IsSuccess == true)
                    {
                        DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);
                    }
                }
                ViewBag.ButtonName = "Save";
                return RedirectToAction("OpenAgendaPtNoting", new { id = RepositryManager.EncryptionManager.Encryption(model.Inbox_ID.ToString()) });

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

        [HttpPost]
        public ActionResult AddComment(InboxCRUD model)
        {

            try
            {
                NotingDetail Notingmodel = new NotingDetail();
                Notingmodel.UserId = Convert.ToInt32(SessionManager.UserIntId);
                Notingmodel.InboxId = model.Inbox_ID;
                Notingmodel.Comment = model.Comment;

                var file = Request.Files[0];
                if (file != null && file.ContentLength > 0)
                {
                    var originalFileName = Path.GetFileName(file.FileName);

                    var newFileName = GenerateNewFileName(originalFileName);

                    var fileModel = new FileModel
                    {
                        FileName = newFileName
                    };

                    using (var reader = new BinaryReader(file.InputStream))
                    {
                        fileModel.Data = reader.ReadBytes(file.ContentLength);
                    }

                    var filePath = Path.Combine(Server.MapPath("~/Uploads"), fileModel.FileName);
                    Notingmodel.Attachment = Request.Url.GetLeftPart(UriPartial.Authority) + "/Uploads/" + fileModel.FileName;
                    System.IO.File.WriteAllBytes(filePath, fileModel.Data);
                }

                if (Convert.ToString(SessionManager.RoleId) != enum1.Administrator)
                {
                    var data = con.CreateNotingDetailCRUD(1, Notingmodel);
                    if (data[0].IsSuccess == true)
                    {
                        DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);
                    }
                }
                ViewBag.ButtonName = "Save";
                return RedirectToAction("AddNoting", new { id = RepositryManager.EncryptionManager.Encryption(model.Inbox_ID.ToString()) });

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

        #region Forwarding AP

        [HttpPost]
        public ActionResult ForwardAP(InboxCRUD model)
        {
            try
            {
                if (model.ForwardID != 0)
                {
                    var data = con.ForwardCRUD(3, model);
                    if (data[0].IsSuccess == true)
                    {
                        DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);
                    }
                }
                else
                {
                    var data = con.ForwardCRUD(1, model);
                    if (data[0].IsSuccess == true)
                    {
                        DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);
                    }
                }

                return RedirectToAction("AddNoting", new { id = RepositryManager.EncryptionManager.Encryption(model.Inbox_ID.ToString()) });
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

        #region Other

        private string GenerateNewFileName(string originalFileName)
        {
            var timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            var fileExtension = Path.GetExtension(originalFileName);
            var filename = Path.GetFileNameWithoutExtension(originalFileName);
            return filename + timestamp + fileExtension;
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
        #endregion
    }
}