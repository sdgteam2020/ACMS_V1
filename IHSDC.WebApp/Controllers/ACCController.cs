using IHSDC.Common.Models;
using IHSDC.WebApp.Connection;
using IHSDC.WebApp.Helper;
using IHSDC.WebApp.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using static IHSDC.WebApp.Filters.CustomFilters;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;
using static iTextSharp.text.pdf.AcroFields;
using PdfReader = iTextSharp.text.pdf.PdfReader;

namespace IHSDC.WebApp.Controllers
{
    [SessionTimeoutAttribute]
    public class ACCController : Controller
    {

        readonly Connection.DBConnection con = new Connection.DBConnection();
        private ApplicationDbContext _db = new ApplicationDbContext();

        private ApplicationUserManager _userManager;



        #region Agenda        

        [HttpGet]
        public ActionResult AddAgenda(string id, string view, string Edit, string FromInbox, string MarkasRead)
        {
            try
            {
                ViewBag.ButtonName = "Save";
                ViewBag.UploadPath = "";
                ViewBag.WordFilePath = "";

                if (Session["UserIntId"] == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                var CurrentUser = UserManager.FindById(User.Identity.GetUserId());
                if (User.IsInRole("Administrator"))
                    ViewBag.count1 = Common.Helpers.Identity.Hierarchy.GetHierarchyUsers(CurrentUser).OrderBy(u => u.IntId).Count() - 1;

                var ipAddress = Request.UserHostAddress;
                var currentDatetime = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                var watermarkText = $" {ipAddress}\n  {currentDatetime}";
                ViewBag.ipadd = watermarkText;

                UnitCRUD model = new UnitCRUD();
                var data = con.UnitCRUD(9, model);
                //model.ILUnitCRUD = data;
                ViewBag.UnitName = data[0].UnitName;

                AgendaPointCRUD model1 = new AgendaPointCRUD();
                InboxCRUD modelInbox = new InboxCRUD();
                model1.UnitName = data[0].UnitName;

                if (MarkasRead != null && MarkasRead != string.Empty)
                {
                    int item = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(id));
                    model1.Inbox_ID = item;

                    int ReadUnRead = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(id));
                    model1.Status = ReadUnRead;

                    var editdata = con.CreateAgendaPoint(6, model1);
                }


                if (id != null && id != string.Empty)
                {
                    ViewBag.ButtonName = "Update";
                    int item = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(id));
                    model1.Inbox_ID = item;
                    modelInbox.Inbox_ID = item;
                    var editdata = con.CreateAgendaPoint(8, model1);

                    if (editdata.Count > 0)
                    {
                        model1 = editdata[0];
                        ViewBag.UploadPath = editdata[0].UploadPath;
                        ViewBag.WordFilePath = editdata[0].WordFilePath;
                    }

                }


                var data1 = con.CreateAPCRUD(2, modelInbox);
                model1.ILInboxCRUD = data1;
                ViewBag.count = model1.ILInboxCRUD.Count;

                if (view != string.Empty && view != null)
                {
                    int itemview = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(view));
                    if (itemview == 1)
                    {
                        model1.IsView = true;
                    }
                    else
                    {
                        if (model1.FinalSubmitYN == true)
                        {
                            if (model1.AllowEdit == false)
                            {
                                if (SessionManager.RoleId == enum1.GSO1)
                                {
                                    model1.IsView = false;
                                }
                                else if (Convert.ToInt32(SessionManager.UserId) == model1.NodalViewId)
                                {
                                    model1.IsView = false;
                                }
                                else
                                {
                                    model1.IsView = true;
                                }
                            }
                            else
                            {
                                model1.IsView = false;
                            }
                        }
                    }
                }

                if (Edit != string.Empty && Edit != null)
                {
                    String ItemEdit = RepositryManager.EncryptionManager.Decryption(Edit);
                    model1.IsEdit = ItemEdit;
                }

                if (FromInbox != string.Empty && FromInbox != null)
                {
                    String FromInboxval = RepositryManager.EncryptionManager.Decryption(FromInbox);
                    model1.FromInbox = FromInboxval;
                }
                else
                {
                    model1.FromInbox = "";
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

        [HttpPost]
        public ActionResult AddAgenda(AgendaPointCRUD model)
        {
            string FilePaths = "";
            try
            {
                model.UserId = Convert.ToInt32(SessionManager.UserIntId);
                model.Unit_ID = Convert.ToInt32(SessionManager.Unit_ID);

                if (Convert.ToString(SessionManager.RoleId) != enum1.Administrator)
                {
                    if (model.Inbox_ID == 0 && Request.Files[0].ContentLength == 0)
                    {
                        DisplayMessage("Please upload Pdf file", "", "w");
                        return RedirectToAction("AddAgenda", new { id = string.Empty, Edit = IHSDC.WebApp.RepositryManager.EncryptionManager.Encryption("1") });
                    }

                    if (model.Inbox_ID == 0 && Request.Files[1].ContentLength == 0)
                    {
                        DisplayMessage("Please upload Word file", "", "w");
                        return RedirectToAction("AddAgenda", new { id = string.Empty, Edit = IHSDC.WebApp.RepositryManager.EncryptionManager.Encryption("1") });
                    }

                    ScheduleLetter letter = new ScheduleLetter();
                    letter.ScheduleId = model.Schedule_ID;
                    var LetterData = con.ScheduleLetter_CRUD(10, letter);
                    var LetterSubject = LetterData[0].Subject;
                    var ConfTypeId = LetterData[0].TypeOfConfId;



                    if (model.Inbox_ID == 0 || Request.Files[0].ContentLength > 0)
                    {
                        var file = Request.Files[0];
                        if (file != null && file.ContentLength > 0)
                        {

                            var originalFileName = Regex.Replace(Path.GetFileName(file.FileName), @"[^a-zA-Z0-9_.]+", " ");
                            var newFileName = GenerateNewFileName(originalFileName);

                            var fileModel = new FileModel
                            {
                                FileName = newFileName
                            };

                            using (var reader = new BinaryReader(file.InputStream))
                            {
                                fileModel.Data = reader.ReadBytes(file.ContentLength);
                            }


                            if (!Directory.Exists(Path.Combine(Server.MapPath("~/Uploads"), ConfTypeId + "_" + LetterSubject + " /pdf ")))
                            {
                                Directory.CreateDirectory(Path.Combine(Server.MapPath("~/Uploads"), ConfTypeId + "_" + LetterSubject + " /pdf "));
                            }

                            var filePath = Server.MapPath("~/Uploads") + "\\" + ConfTypeId + "_" + LetterSubject + "\\pdf\\" + fileModel.FileName;
                            model.UploadPath = Request.Url.GetLeftPart(UriPartial.Authority) + "/Uploads/" + ConfTypeId + "_" + LetterSubject + "/pdf/" + fileModel.FileName;
                            System.IO.File.WriteAllBytes(filePath, fileModel.Data);
                            FilePaths = FilePaths + "," + fileModel.FileName;

                            model.Upload = fileModel.FileName;
                        }
                    }
                    if (model.Inbox_ID == 0 || Request.Files[1].ContentLength > 0)
                    {
                        var file = Request.Files[1];
                        if (file != null && file.ContentLength > 0)
                        {
                            // var originalFileName = Path.GetFileName(file.FileName);
                            var originalFileName = Regex.Replace(Path.GetFileName(file.FileName), @"[^a-zA-Z0-9_.]+", " ");


                            var newFileName = GenerateNewFileName(originalFileName);

                            var fileModel = new FileModel
                            {
                                FileName = newFileName
                            };

                            using (var reader = new BinaryReader(file.InputStream))
                            {
                                fileModel.Data = reader.ReadBytes(file.ContentLength);
                            }

                            if (!Directory.Exists(Path.Combine(Server.MapPath("~/Uploads"), ConfTypeId + "_" + LetterSubject + " /Word ")))
                            {
                                Directory.CreateDirectory(Path.Combine(Server.MapPath("~/Uploads"), ConfTypeId + "_" + LetterSubject + " /Word "));
                            }

                            var filePath = Server.MapPath("~/Uploads") + "\\" + ConfTypeId + "_" + LetterSubject + "\\Word\\" + fileModel.FileName;
                            model.WordFilePath = Request.Url.GetLeftPart(UriPartial.Authority) + "/Uploads/" + ConfTypeId + "_" + LetterSubject + "/Word/" + fileModel.FileName;
                            System.IO.File.WriteAllBytes(filePath, fileModel.Data);
                            FilePaths = FilePaths + "," + fileModel.FileName;

                            model.WordFile = fileModel.FileName;
                        }
                    }


                    if ((model.Inbox_ID != 0))
                    {
                        if (model.FileName != null)
                        {
                            if (model.Upload == null)
                            {
                                model.Upload = model.FileName;
                            }
                        }
                    }

                    var data = con.CreateAgendaPoint(1, model);
                    if (data[0].IsSuccess == true)
                    {
                        DisplayMessage(data[0].Msg, "", data[0].MsgStatus);
                        model.Inbox_ID = Convert.ToInt32(data[0].MidMsg);
                    }


                }
                ViewBag.ButtonName = "Save";

                if (model.Inbox_ID == 0)
                {
                    return RedirectToAction("AddAgenda", new { id = string.Empty });
                }
                else
                {
                    return RedirectToAction("AddAgenda", new { id = RepositryManager.EncryptionManager.Encryption(model.Inbox_ID.ToString()), Edit = RepositryManager.EncryptionManager.Encryption("1") });
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

        [HttpGet]
        public ActionResult FinalSubmitAgenda(string id, string fid)
        {
            try
            {
                AgendaPointCRUD model = new AgendaPointCRUD();
                int item = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(id));

                model.Inbox_ID = item;
                var data = con.CreateAgendaPoint(3, model);
                if (data[0].IsSuccess == true)
                {
                    DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);
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
                return RedirectToAction("AddAgenda", new { id = string.Empty });
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


        public ActionResult AddRequestUnfreezeAgendaPoint(string id, string fid)
        {
            try
            {
                InboxCRUD model = new InboxCRUD();
                int item = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(id));

                model.Inbox_ID = item;
                var data = con.CreateAPCRUD(13, model);
                if (data[0].IsSuccess == true)
                {
                    DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);
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
                return RedirectToAction("AddAgenda", new { id = string.Empty });
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
        public ActionResult SignAgendaPoint(string file, string path)
        {
            string DownloadPath = "";
            var outputFilepath = "";
            if (path != null && path != string.Empty)
            {
                DownloadPath = RepositryManager.EncryptionManager.Decryption(path);
            }
            string[] DownloadPathNewPath = DownloadPath.Split();

            string FilePathName = Path.GetFileName(DownloadPath);
            byte[] pdfBytes = Convert.FromBase64String(file);

            if (DownloadPath.Split('/')[4] == FilePathName)
            {
                outputFilepath = Path.Combine(Server.MapPath("~/Uploads"), FilePathName);
            }
            else
            {
                outputFilepath = Path.Combine(Server.MapPath("~/Uploads") + "\\" + DownloadPath.Split('/')[4] + "\\pdf\\", FilePathName);

            }



            System.IO.File.WriteAllBytes(outputFilepath, pdfBytes);
            return Json(new { success = true });
        }

        #endregion

        #region Closed Agenda List
        [HttpGet]
        public ActionResult ClosedAgendaList(string id, string view, string Edit, string FromInbox, string MarkasRead)
        {
            try
            {
                ViewBag.ButtonName = "Save";
                ViewBag.UploadPath = "";
                ViewBag.WordFilePath = "";

                if (Session["UserIntId"] == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                var CurrentUser = UserManager.FindById(User.Identity.GetUserId());
                if (User.IsInRole("Administrator"))
                    ViewBag.count1 = Common.Helpers.Identity.Hierarchy.GetHierarchyUsers(CurrentUser).OrderBy(u => u.IntId).Count() - 1;

                var ipAddress = Request.UserHostAddress;
                var currentDatetime = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                var watermarkText = $" {ipAddress}\n  {currentDatetime}";
                ViewBag.ipadd = watermarkText;

                UnitCRUD model = new UnitCRUD();
                var data = con.UnitCRUD(9, model);
                //model.ILUnitCRUD = data;
                ViewBag.UnitName = data[0].UnitName;

                InboxCRUD model1 = new InboxCRUD();
                model1.UnitName = data[0].UnitName;

                if (MarkasRead != null && MarkasRead != string.Empty)
                {
                    int item = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(id));
                    model1.Inbox_ID = item;
                    int ReadUnRead = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(id));
                    model1.Status = ReadUnRead;

                    var editdata = con.CreateAPCRUD(6, model1);
                }


                if (id != null && id != string.Empty)
                {
                    ViewBag.ButtonName = "Update";
                    int item = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(id));
                    model1.Inbox_ID = item;
                    var editdata = con.CreateAPCRUD(8, model1);

                    if (editdata.Count > 0)
                    {
                        model1 = editdata[0];
                        ViewBag.UploadPath = editdata[0].UploadPath;
                        ViewBag.WordFilePath = editdata[0].WordFilePath;
                    }

                }
                var data1 = con.CreateAPCRUD(16, model1);
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
        public ActionResult CloseAgendaPoint(InboxCRUD model)
        {
            try
            {

                var data = con.OpenCloseAgendaPoint(1, model);
                if (data[0].IsSuccess == true)
                {
                    DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);

                }

                return RedirectToAction("AddInboxNotingFwd", "ACC", new { id = RepositryManager.EncryptionManager.Encryption(model.Inbox_ID.ToString()) });
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

        #endregion End Closed Agenda

        #region Close All AP
        [HttpPost]
        public ActionResult CloseAllAgendaPoints(string CloseIdList)
        {
            try
            {
                if (CloseIdList == null || CloseIdList.Length == 0)
                {
                    return HttpNotFound("At least one input PDF is required.");
                }
                InboxCRUD model1 = new InboxCRUD();
                model1.CloseIds = CloseIdList;
                var editdata = con.OpenCloseAgendaPoint(3, model1);
                return Json(new { success = true });
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


        #region Open Agenda List


        [HttpPost]
        public ActionResult OpenAgendaPoint(InboxCRUD model)
        {
            try
            {

                var data = con.OpenCloseAgendaPoint(2, model);
                if (data[0].IsSuccess == true)
                {
                    DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);

                }

                return RedirectToAction("AddInboxNotingFwd", "ACC", new { id = RepositryManager.EncryptionManager.Encryption(model.Inbox_ID.ToString()) });
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
        #endregion Open Agenda List

        #region Inbox Noting Fwd
        [HttpGet]
        public ActionResult AddInboxNotingFwd()
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

                //watermark dataTable
                var ipAddress = Request.UserHostAddress;
                var currentDatetime = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                var watermarkText = $" {ipAddress}\n  {currentDatetime}";
                ViewBag.ipadd = watermarkText;


                UnitCRUD model = new UnitCRUD();
                model.Unit_ID = Convert.ToInt16(SessionManager.Unit_ID);
                var data = con.UnitCRUD(2, model);
                //model.ILUnitCRUD = data;
                ViewBag.UnitName = data[0].UnitName;

                InboxCRUD model1 = new InboxCRUD();
                model1.UnitName = data[0].UnitName;

                var data1 = con.CreateAPCRUD(5, model1);
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
        #endregion

        #region Analysis
        [HttpGet]
        public ActionResult Analysis()
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

                var ipAddress = Request.UserHostAddress;
                var currentDatetime = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                var watermarkText = $" {ipAddress}\n  {currentDatetime}";
                ViewBag.ipadd = watermarkText;

                UnitCRUD model = new UnitCRUD();
                model.Unit_ID = Convert.ToInt16(SessionManager.Unit_ID);
                var data = con.UnitCRUD(2, model);
                //model.ILUnitCRUD = data;
                ViewBag.UnitName = data[0].UnitName;

                InboxCRUD model1 = new InboxCRUD();
                model1.UnitName = data[0].UnitName;

                var data1 = con.CreateAPCRUD(15, model1);
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
        public ActionResult Analysis(string id, InboxNotingFwdCRUD model, string btnval)
        {
            try
            {
                model.UserId = Convert.ToInt32(SessionManager.UserIntId);

                if (Convert.ToString(SessionManager.RoleId) != enum1.Administrator)
                {
                    if (btnval == "Add")
                    {

                        var data = con.InboxNotingFwdCRUD(3, model);
                        if (data[0].IsSuccess == true)
                        {
                            DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);

                        }
                        if (data[0].IsExist == true)
                        {
                            DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);

                        }
                    }
                }





                if (btnval == "Update")
                {
                    int item = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(id));
                    model.Inbox_ID = item;
                    var data = con.InboxNotingFwdCRUD(3, model);
                    if (data[0].IsSuccess == true)
                    {
                        DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);

                    }
                }
                ViewBag.ButtonName = "Add";

                if (model.IsHistory != 0)
                {
                    return RedirectToAction("AddInbox", new { id = RepositryManager.EncryptionManager.Encryption(model.Inbox_ID.ToString()), IsHist = RepositryManager.EncryptionManager.Encryption(model.IsHistory.ToString()), ViewId = RepositryManager.EncryptionManager.Encryption(model.Aviator_ID.ToString()) });
                }
                else
                {
                    return RedirectToAction("AddInbox", new { id = string.Empty });
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

        public ActionResult DownloadAgendaWithWatermark(string title, string path, string ConfId)
        {
            String pathss = "";
            String filename = "";
            String titles = "";
            String pathName = "";
            String ConfIdText = "";
            try
            {
                if (path != string.Empty && path != null && title != string.Empty && title != null)
                {
                    pathss = RepositryManager.EncryptionManager.Decryption(path);
                    titles = RepositryManager.EncryptionManager.Decryption(title);
                    if (ConfId != string.Empty && ConfId != null)
                    {
                        ConfIdText = RepositryManager.EncryptionManager.Decryption(ConfId);
                    }

                    pathName = Path.GetFileName(pathss);
                    if (ConfIdText != "")
                    {
                        filename = ConfIdText + "_" + titles + "_" + pathName;
                    }
                    else
                    {
                        filename = titles + "_" + pathName;
                    }

                }

                var ipAddress = Request.UserHostAddress;
                var currentDatetime = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                var watermarkText = $" {ipAddress}\n  {currentDatetime}";
                ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                WebClient client = new WebClient();
                byte[] pdfBytes = client.DownloadData(pathss);
                using (MemoryStream ms = new MemoryStream())
                {

                    PdfReader reader = new PdfReader(pdfBytes);
                    using (PdfStamper stamper = new PdfStamper(reader, ms))
                    {
                        int pageCount = reader.NumberOfPages;
                        BaseFont baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.WINANSI, BaseFont.NOT_EMBEDDED);
                        PdfContentByte content;

                        for (int i = 1; i <= pageCount; i++)
                        {
                            content = stamper.GetOverContent(i);
                            content.SaveState();
                            content.SetFontAndSize(baseFont, 48);
                            content.SetColorFill(BaseColor.GRAY);
                            content.BeginText();
                            content.ShowTextAligned(iTextSharp.text.Element.ALIGN_CENTER, watermarkText, reader.GetPageSizeWithRotation(i).Width / 2, reader.GetPageSizeWithRotation(i).Height / 2, 45);
                            content.EndText();
                            content.RestoreState();
                        }
                    }


                    return File(ms.ToArray(), "application/pdf", filename);
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

        #endregion
    }
}