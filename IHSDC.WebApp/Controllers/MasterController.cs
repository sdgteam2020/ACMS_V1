using System;
using System.Linq;
using System.Web.Mvc;
using IHSDC.WebApp.Models;
using IHSDC.WebApp.Connection;
using static IHSDC.WebApp.Filters.CustomFilters;
using PagedList;
using IHSDC.WebApp.Helper;
using System.Data.SqlClient;
using System.Text;
using System.Collections.Generic;
using System.IO;
using IHSDC.WebApp.Services;
using static IHSDC.WebApp.MvcApplication;
using IHSDC.WebApp.RepositryManager;
using System.Configuration;
using static IHSDC.WebApp.Controllers.PolicyCornerController;

namespace IHSDC.WebApp.Controllers
{
    [SessionTimeoutAttribute]
    public class MasterController : Controller
    {
        readonly Connection.DBConnection con = new Connection.DBConnection();

        public ActionResult GetFilesCount(string FName)
        {
            try
            {
                FileList Files = new FileList();
                string FolderName = "";
                if (!string.IsNullOrEmpty(FName))
                {
                    FolderName = RepositryManager.EncryptionManager.Decryption(FName);
                }
                string PolicyPath = Server.MapPath("~/Policy/") + FolderName;
                List<string> fileNames = Directory.GetFiles(PolicyPath, "*", SearchOption.AllDirectories).ToList();

                List<FileList> fileList = fileNames.Select(fileName => new FileList
                {
                    FileName = Path.GetFileName(fileName),
                    FilePath = fileName
                }).ToList();


                int fileCount = fileNames.Count;
                ViewBag.FileCount = fileCount;
                // Return the file count as a JSON result
                return Json(new { fileCount = fileCount }, JsonRequestBehavior.AllowGet);


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

        #region Policy
        // GET: Policy


        [HttpGet]
        public ActionResult DeletePolicyFile(string id)
        {
            try
            {
                string Filename = "";
                if (!string.IsNullOrEmpty(id))
                {
                    Filename = RepositryManager.EncryptionManager.Decryption(id);
                }
                try
                {
                    if (System.IO.File.Exists(Filename))
                    {
                        System.IO.File.Delete(Filename);
                    }
                    TempData["message"] = "File Deleted Successfully";
                }
                catch { }
                return RedirectToAction("AddPolicy", new { id = string.Empty });

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

       
        public ActionResult GetFiles(string FName)
        {
            try
            {
                FileList Files = new FileList();
                string FolderName = "";
                if (!string.IsNullOrEmpty(FName))
                {
                    FolderName = RepositryManager.EncryptionManager.Decryption(FName);
                }
                string PolicyPath = Server.MapPath("~/Policy/") + FolderName;
                List<string> fileNames = Directory.GetFiles(PolicyPath, "*", SearchOption.AllDirectories).ToList();

                List<FileList> fileList = fileNames.Select(fileName => new FileList
                {
                    FileName = Path.GetFileName(fileName),
                    FilePath = fileName
                }).ToList();

                return PartialView("FileView", fileList);

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

        [HttpPost]
        public ActionResult AddPolicyDocu(PolicyCRUD modal)
        {
            try
            {
                if (Request.Files.Count > 0)
                {
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        var file = Request.Files[i];
                        if (file != null && file.ContentLength > 0)
                        {
                            var originalFileName = Path.GetFileName(file.FileName);


                            var fileModel = new FileModel
                            {
                                FileName = originalFileName
                            };

                            using (var reader = new BinaryReader(file.InputStream))
                            {
                                fileModel.Data = reader.ReadBytes(file.ContentLength);
                            }

                            var filePath = Path.Combine(Server.MapPath("~/Policy/") + modal.PolicyFolder, fileModel.FileName);
                            System.IO.File.WriteAllBytes(filePath, fileModel.Data);
                        }
                    }
                    string rootPath = Server.MapPath("~/Policy");
                    string[] uploadsDirectoryPath = Directory.GetDirectories(rootPath, "*", SearchOption.TopDirectoryOnly);
                    AllPath = uploadsDirectoryPath;
                    foreach (var item in uploadsDirectoryPath)
                    {
                        DocPathServices[item] = new SearchService(SearchService.InitializeSearchData(item));
                    }
                    DependencyResolver.SetResolver(new MyDependencyResolver(DocPathServices));
                    TempData["message"] = "File Upload Successfully";
                }

                return RedirectToAction("AddPolicy", new { id = string.Empty });
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

        [HttpGet]
        public ActionResult AddPolicy(string id)
        {
            try
            {
                if (Session["UserIntId"] == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                ViewBag.ButtonName = "Add";
                PolicyCRUD model = new PolicyCRUD();
                if (id != null && id != string.Empty)
                {
                    ViewBag.ButtonName = "Update";
                    int item = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(id));
                    model.PolicyId = item;
                    var editdata = con.PolicyCRUD(2, model);
                    model = editdata[0];
                }
                model.PolicyId = 0;
                var data = con.PolicyCRUD(2, model);
                model.ILPolicyCRUD = data;
                ViewBag.count = model.ILPolicyCRUD.Count();

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

        [HttpPost]        
        public ActionResult AddPolicy(string id, PolicyCRUD model, string btnval, string oldid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.UserId = Convert.ToInt32(SessionManager.UserIntId);
                    if (btnval == "Add")
                    {

                        var data = con.PolicyCRUD(1, model);

                        var folder = Server.MapPath("~/Policy/") + model.PolicyName;
                        if (!Directory.Exists(folder))
                        {
                            Directory.CreateDirectory(folder);
                        }
                        if (data[0].IsSuccess == true)
                        {
                            DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);

                        }
                        if (data[0].IsExist == true)
                        {
                            DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);

                        }
                    }
                    if (btnval == "Update")
                    {
                        var NewFolder = Server.MapPath("~/Policy/") + model.PolicyName;
                        var Oldfolder = Server.MapPath("~/Policy/") + oldid;
                        if (model.PolicyName != oldid)
                        {
                            System.IO.Directory.Move(Oldfolder, NewFolder);
                        }

                        int item = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(id));
                        model.PolicyId = item;
                        var data = con.PolicyCRUD(3, model);
                        if (data[0].IsSuccess == true)
                        {
                            DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);

                        }
                    }
                }
                ViewBag.ButtonName = "Add";

                return RedirectToAction("AddPolicy", new { id = string.Empty });
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

        public ActionResult DeletePolicy(string id)
        {
            try
            {
                PolicyCRUD model = new PolicyCRUD();
                int item = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(id));
                model.PolicyId = item;
                model.UserId = Convert.ToInt32(SessionManager.UserIntId);
                var data = con.PolicyCRUD(4, model);
                if (data[0].IsSuccess == true)
                {
                    DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);
                    //DisplayMessage("Policy Deleted Successfully", " ", "s");
                }
                return RedirectToAction("AddPolicy", new { id = string.Empty });
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
        #endregion

        #region Comd
        // GET: Comd
        [HttpGet]
        public ActionResult AddComd(string id)
        {
            try
            {
                if (Session["UserIntId"] == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                ViewBag.ButtonName = "Add";
                ComdCRUD model = new ComdCRUD();
                if (id != null && id != string.Empty)
                {
                    ViewBag.ButtonName = "Update";
                    int item = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(id));
                    model.ComdId = item;
                    var editdata = con.ComdCRUD(2, model);
                    model = editdata[0];
                }
                model.ComdId = 0;
                var data = con.ComdCRUD(2, model);
                model.ILComdCRUD = data;
                ViewBag.count = model.ILComdCRUD.Count();
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddComd(string id, ComdCRUD model, string btnval)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.UserId = Convert.ToInt32(SessionManager.UserIntId);
                    if (btnval == "Add")
                    {

                        var data = con.ComdCRUD(1, model);
                        if (data[0].IsSuccess == true)
                        {
                            DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);

                        }
                        if (data[0].IsExist == true)
                        {
                            DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);

                        }
                    }
                    if (btnval == "Update")
                    {
                        int item = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(id));
                        model.ComdId = item;
                        var data = con.ComdCRUD(3, model);
                        if (data[0].IsSuccess == true)
                        {
                            DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);

                        }
                    }
                }
                ViewBag.ButtonName = "Add";

                return RedirectToAction("AddComd", new { id = string.Empty });
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

        public ActionResult DeleteComd(string id)
        {
            try
            {
                ComdCRUD model = new ComdCRUD();
                int item = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(id));
                model.ComdId = item;
                model.UserId = Convert.ToInt32(SessionManager.UserIntId);
                var data = con.ComdCRUD(4, model);
                if (data[0].IsSuccess == true)
                {
                    DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);
                    //DisplayMessage("Comd Deleted Successfully", " ", "s");
                }
                return RedirectToAction("AddComd", new { id = string.Empty });
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
        #endregion

        #region Sqn
        // GET: Sqn
        [HttpGet]
        public ActionResult AddSqn(string id)
        {
            try
            {
                if (Session["UserIntId"] == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                ViewBag.corpsId = 1;
                ViewBag.BdeCatId = 1;
                ViewBag.ButtonName = "Add";
                SqnCRUD model = new SqnCRUD();
                if (id != null && id != string.Empty)
                {
                    ViewBag.ButtonName = "Update";
                    int item = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(id));
                    model.SqnId = item;
                    var editdata = con.SqnCRUD(2, model);
                    model = editdata[0];


                    ViewBag.corpsId = model.CorpsId;
                    ViewBag.BdeCatId = model.BdeCatId;
                }
                model.SqnId = 0;
                var data = con.SqnCRUD(2, model);
                model.ILSqnCRUD = data;
                ViewBag.count = model.ILSqnCRUD.Count();
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSqn(string id, SqnCRUD model, string btnval)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.UserId = Convert.ToInt32(SessionManager.UserIntId);
                    if (btnval == "Add")
                    {

                        var data = con.SqnCRUD(1, model);
                        if (data[0].IsSuccess == true)
                        {
                            DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);

                        }
                        if (data[0].IsExist == true)
                        {
                            DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);

                        }
                    }
                    if (btnval == "Update")
                    {
                        int item = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(id));
                        model.SqnId = item;
                        var data = con.SqnCRUD(3, model);
                        if (data[0].IsSuccess == true)
                        {
                            DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);

                        }
                    }
                }
                ViewBag.ButtonName = "Add";

                return RedirectToAction("AddSqn", new { id = string.Empty });
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

        public ActionResult DeleteSqn(string id)
        {
            try
            {
                SqnCRUD model = new SqnCRUD();
                int item = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(id));
                model.SqnId = item;
                model.UserId = Convert.ToInt32(SessionManager.UserIntId);
                var data = con.SqnCRUD(4, model);
                if (data[0].IsSuccess == true)
                {
                    DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);
                    //DisplayMessage("Comd Deleted Successfully", " ", "s");
                }
                return RedirectToAction("AddSqn", new { id = string.Empty });
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
        #endregion

        #region BdeCat
        // GET: BdeCat
        [HttpGet]
        public ActionResult AddBdeCat(string id)
        {
            try
            {
                if (Session["UserIntId"] == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                ViewBag.corpsId = 1;
                ViewBag.ButtonName = "Add";
                BdeCatCRUD model = new BdeCatCRUD();
                if (id != null && id != string.Empty)
                {
                    ViewBag.ButtonName = "Update";
                    int item = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(id));
                    model.BdeCatId = item;
                    var editdata = con.BdeCatCRUD(2, model);
                    model = editdata[0];
                    ViewBag.corpsId = model.CorpsId;
                }
                model.BdeCatId = 0;
                var data = con.BdeCatCRUD(2, model);
                model.ILBdeCatCRUD = data;
                ViewBag.count = model.ILBdeCatCRUD.Count();
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddBdeCat(string id, BdeCatCRUD model, string btnval)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.UserId = Convert.ToInt32(SessionManager.UserIntId);
                    if (btnval == "Add")
                    {

                        var data = con.BdeCatCRUD(1, model);
                        if (data[0].IsSuccess == true)
                        {
                            DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);

                        }
                        if (data[0].IsExist == true)
                        {
                            DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);

                        }
                    }
                    if (btnval == "Update")
                    {
                        int item = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(id));
                        model.BdeCatId = item;
                        var data = con.BdeCatCRUD(3, model);
                        if (data[0].IsSuccess == true)
                        {
                            DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);

                        }
                    }
                }
                ViewBag.ButtonName = "Add";

                return RedirectToAction("AddBdeCat", new { id = string.Empty });
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

        public ActionResult DeleteBdeCat(string id)
        {
            try
            {
                BdeCatCRUD model = new BdeCatCRUD();
                int item = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(id));
                model.BdeCatId = item;
                model.UserId = Convert.ToInt32(SessionManager.UserIntId);
                var data = con.BdeCatCRUD(4, model);
                if (data[0].IsSuccess == true)
                {
                    DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);
                    //DisplayMessage("BdeCat Deleted Successfully", " ", "s");
                }
                return RedirectToAction("AddBdeCat", new { id = string.Empty });
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
        #endregion

        #region Rank
        // GET: Comd
        [HttpGet]
        public ActionResult AddRank(string id)
        {
            try
            {
                if (Session["UserIntId"] == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                ViewBag.ButtonName = "Add";
                RankCRUD model = new RankCRUD();
                if (id != null && id != string.Empty)
                {
                    ViewBag.ButtonName = "Update";
                    int item = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(id));
                    model.RankId = item;
                    var editdata = con.RankCRUD(2, model);
                    model = editdata[0];
                }
                model.RankId = 0;
                var data = con.RankCRUD(2, model);
                model.ILRankCRUD = data;
                ViewBag.count = model.ILRankCRUD.Count();
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRank(string id, RankCRUD model, string btnval)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.UserId = Convert.ToInt32(SessionManager.UserIntId);
                    if (btnval == "Add")
                    {

                        var data = con.RankCRUD(1, model);
                        if (data[0].IsSuccess == true)
                        {
                            DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);

                        }
                        if (data[0].IsExist == true)
                        {
                            DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);

                        }
                    }
                    if (btnval == "Update")
                    {
                        int item = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(id));
                        model.RankId = item;
                        var data = con.RankCRUD(3, model);
                        if (data[0].IsSuccess == true)
                        {
                            DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);

                        }
                    }
                }
                ViewBag.ButtonName = "Add";

                return RedirectToAction("AddRank", new { id = string.Empty });
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

        public ActionResult DeleteRank(string id)
        {
            try
            {
                RankCRUD model = new RankCRUD();
                int item = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(id));
                model.RankId = item;
                model.UserId = Convert.ToInt32(SessionManager.UserIntId);
                var data = con.RankCRUD(4, model);
                if (data[0].IsSuccess == true)
                {
                    DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);
                    //DisplayMessage("Comd Deleted Successfully", " ", "s");
                }
                return RedirectToAction("AddRank", new { id = string.Empty });
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
        #endregion

        #region Appt
        // GET: Comd
        [HttpGet]
        public ActionResult AddAppt(string id)
        {
            try
            {
                ViewBag.ButtonName = "Add";
                ApptCRUD model = new ApptCRUD();
                if (id != null && id != string.Empty)
                {
                    ViewBag.ButtonName = "Update";
                    int item = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(id));
                    model.ApptId = item;
                    var editdata = con.ApptCRUD(2, model);
                    model = editdata[0];
                }
                model.ApptId = 0;
                var data = con.ApptCRUD(2, model);
                model.ILApptCRUD = data;
                ViewBag.count = model.ILApptCRUD.Count();
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


        [HttpPost]
        public ActionResult AddAppt(string id, ApptCRUD model, string btnval)
        {
            try
            {
                model.UserId = Convert.ToInt32(SessionManager.UserIntId);
                if (btnval == "Add")
                {

                    var data = con.ApptCRUD(1, model);
                    if (data[0].IsSuccess == true)
                    {
                        DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);

                    }
                    if (data[0].IsExist == true)
                    {
                        DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);

                    }
                }
                if (btnval == "Update")
                {
                    int item = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(id));
                    model.ApptId = item;
                    var data = con.ApptCRUD(3, model);
                    if (data[0].IsSuccess == true)
                    {
                        DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);

                    }
                }
                ViewBag.ButtonName = "Add";

                return RedirectToAction("AddAppt", new { id = string.Empty });
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

        public ActionResult DeleteAppt(string id)
        {
            try
            {
                ApptCRUD model = new ApptCRUD();
                int item = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(id));
                model.ApptId = item;
                model.UserId = Convert.ToInt32(SessionManager.UserIntId);
                var data = con.ApptCRUD(4, model);
                if (data[0].IsSuccess == true)
                {
                    DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);
                    //DisplayMessage("Comd Deleted Successfully", " ", "s");
                }
                return RedirectToAction("AddAppt", new { id = string.Empty });
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
        #endregion

        #region Corps
        // GET: Corps
        [HttpGet]
        public ActionResult AddCorps(string id)
        {
            try
            {
                //if (Session["UserIntId"] == null)
                //{
                //    return RedirectToAction("Login", "Account");
                //}
                ViewBag.ButtonName = "Add";
                CorpsCRUD model = new CorpsCRUD();
                if (id != null && id != string.Empty)
                {
                    ViewBag.ButtonName = "Update";
                    int item = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(id));
                    model.CorpsId = item;
                    var editdata = con.CorpsCRUD(2, model);
                    model = editdata[0];
                }
                model.CorpsId = 0;
                var data = con.CorpsCRUD(2, model);
                model.ILCorpsCRUD = data;
                ViewBag.count = model.ILCorpsCRUD.Count();
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


        [HttpPost]       
        public ActionResult AddCorps(string id, CorpsCRUD model, string btnval)
        {
            try
            {
                   // model.UserId = Convert.ToInt32(SessionManager.UserIntId);
                    if (id == null)
                    {

                        var data = con.CorpsCRUD(1, model);
                        if (data[0].IsSuccess == true)
                        {
                            DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);

                        }
                        if (data[0].IsExist == true)
                        {
                            DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);

                        }
                    }
                    else
                    {
                        int item = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(id));
                        model.CorpsId = item;
                        var data = con.CorpsCRUD(3, model);
                        if (data[0].IsSuccess == true)
                        {
                            DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);

                        }
                    }
                
                ViewBag.ButtonName = "Add";

                return RedirectToAction("AddCorps", new { id = string.Empty });
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

        public ActionResult DeleteCorps(string id)
        {
            try
            {
                CorpsCRUD model = new CorpsCRUD();
                int item = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(id));
                model.CorpsId = item;
                model.UserId = Convert.ToInt32(SessionManager.UserIntId);
                var data = con.CorpsCRUD(4, model);
                if (data[0].IsSuccess == true)
                {
                    DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);
                    //DisplayMessage("Corps Deleted Successfully", " ", "s");
                }
                return RedirectToAction("AddCorps", new { id = string.Empty });
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
        #endregion

        #region Unit
        // GET: Corps
        [HttpGet]
        public ActionResult AddUnit(string id)
        {
            if (Session["UserIntId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            try
            {
                ViewBag.corpsId = 1;
                ViewBag.BdeCatId = 1;
                ViewBag.SqnIdbind = 1;
                ViewBag.ButtonName = "Add";
                UnitCRUD model = new UnitCRUD();
                if (id != null && id != string.Empty)
                {
                    ViewBag.ButtonName = "Update";
                    int item = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(id));
                    model.Unit_ID = item;
                    var editdata = con.UnitCRUD(2, model);
                    model = editdata[0];

                    ViewBag.corpsId = model.CorpsId;
                    ViewBag.BdeCatId = model.BdeCatId;
                    ViewBag.SqnIdbind = model.SqnId;
                }
                model.Unit_ID = 0;
                var data = con.UnitCRUD(2, model);
                model.ILUnitCRUD = data;
                ViewBag.count = model.ILUnitCRUD.Count();
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUnit(string id, UnitCRUD model, string btnval)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    ViewBag.corpsId = 0;
                    ViewBag.BdeCatId = 0;
                    ViewBag.SqnIdbind = 0;
                    model.UserId = Convert.ToInt32(SessionManager.UserIntId);
                    if (btnval == "Add")
                    {

                        var data = con.UnitCRUD(1, model);
                        if (data[0].IsSuccess == true)
                        {
                            DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);

                        }
                        if (data[0].IsExist == true)
                        {
                            DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);

                        }
                    }
                    if (btnval == "Update")
                    {
                        int item = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(id));
                        model.Unit_ID = item;
                        var data = con.UnitCRUD(3, model);

                        if (data[0].IsSuccess == true)
                        {
                            DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);

                        }
                    }
                }
                ViewBag.ButtonName = "Add";

                return RedirectToAction("AddUnit", new { id = string.Empty });
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

        public ActionResult DeleteUnit(string id)
        {
            try
            {
                UnitCRUD model = new UnitCRUD();
                int item = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(id));
                model.Unit_ID = item;
                model.UserId = Convert.ToInt32(SessionManager.UserIntId);
                var data = con.UnitCRUD(4, model);
                if (data[0].IsSuccess == true)
                {
                    DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);
                    //DisplayMessage("Corps Deleted Successfully", " ", "s");
                }
                return RedirectToAction("AddUnit", new { id = string.Empty });
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
        #endregion

        #region All Dropdown Filter
        public JsonResult PolicyOrderby(int OrderbyId)
        {
            MasterModels masterModels = new MasterModels();
            var data = masterModels.PolicyOrderby(OrderbyId);
            return Json(data);
        }
        public JsonResult CommadOrderby(int OrderbyId)
        {
            MasterModels masterModels = new MasterModels();
            var data = masterModels.CommadOrderby(OrderbyId);
            return Json(data);
        }
        public JsonResult CorpsOrderby(int OrderbyId)
        {
            MasterModels masterModels = new MasterModels();
            var data = masterModels.CorpsOrderby(OrderbyId);
            return Json(0);
        }
        public JsonResult bdeOrderby(int OrderbyId)
        {
            MasterModels masterModels = new MasterModels();
            var data = masterModels.bdeOrderby(OrderbyId);
            return Json(0);
        }
        public JsonResult sqnOrderby(int OrderbyId)
        {
            MasterModels masterModels = new MasterModels();
            var data = masterModels.sqnOrderby(OrderbyId);
            return Json(0);
        }

        public JsonResult BranchOrderby(int OrderbyId)
        {
            MasterModels masterModels = new MasterModels();
            var data = masterModels.BranchOrderby(OrderbyId);
            return Json(0);
        }

        public JsonResult CATOrderby(int OrderbyId)
        {
            MasterModels masterModels = new MasterModels();
            var data = masterModels.CATOrderby(OrderbyId);
            return Json(0);
        }

        public JsonResult ApptOrderby(int OrderbyId,int NewOrderbyId)
        {
            MasterModels masterModels = new MasterModels();
            var data = masterModels.ApptOrderby(OrderbyId, NewOrderbyId);
            return Json(0);
        }

        public JsonResult LoadCommandName(int ComdId)
        {
            MasterModels masterModels = new MasterModels();
            var data = masterModels.LoadCommandName();
            return Json(data);
        }


        public JsonResult LoadCorpsNameByCommandId(int ComdId)
        {
            MasterModels masterModels = new MasterModels();
            var data = masterModels.LoadCorpsNameByCommandId(ComdId);
            return Json(data);
        }
        
        public JsonResult LoadCorpsAll(int Id)
        {
            MasterModels masterModels = new MasterModels();
            var data = masterModels.LoadCorpsAll();
            return Json(data);
        }
        public JsonResult LoadBdeCATAll(int Id)
        {
            MasterModels masterModels = new MasterModels();
            var data = masterModels.LoadBdeCATAll();
            return Json(data);
        }
        public JsonResult LoadSqnAll(int Id)
        {
            MasterModels masterModels = new MasterModels();
            var data = masterModels.LoadSqnAll();
            return Json(data);
        }
        public static string GetUnitNamebyId(int UnitId)
        {

            UnitCRUD model = new UnitCRUD();
            Connection.DBConnection con1 = new Connection.DBConnection();
            model.Unit_ID = UnitId;
            var ss = con1.UnitCRUD(2, model);
            string unitname = "";
            foreach (UnitCRUD db in ss)
            {
                unitname = db.UnitName;
            }
            return unitname;
        }
        public JsonResult GetUnitByhierarchy(int Unit_ID)
        {
            UnitCRUD model = new UnitCRUD();
            Connection.DBConnection con1 = new Connection.DBConnection();
            model.Unit_ID = Unit_ID;

            var ss = con1.GetUnitDropdown(8, model);

            return Json(ss);
        }
        public JsonResult LoadBdeCATbyId(string Alldata)
        {
            string[] all = Alldata.Split(',');
            int ComdId = 0;
            int CorpsId = 0;
            if (all[0] == "null")
                ComdId = 0;
            else
                ComdId = Convert.ToInt32(all[0]);
            if (all[1] == "null")
                CorpsId = 0;
            else
                CorpsId = Convert.ToInt32(all[1]);
            MasterModels masterModels = new MasterModels();
            var data = masterModels.LoadBdeCAT(ComdId, CorpsId);
            return Json(data);
        }
        public JsonResult LoadGetSQNId(string Alldata)
        {
            string[] all = Alldata.Split(',');
            int ComdId = 0;
            int CorpsId = 0;
            int SqnId = 0;
            if (all[0] == "null")
                ComdId = 0;
            else
                ComdId = Convert.ToInt32(all[0]);
            if (all[1] == "null")
                CorpsId = 0;
            else
                CorpsId = Convert.ToInt32(all[1]);

            if (all[2] == "null")
                SqnId = 0;
            else
                SqnId = Convert.ToInt32(all[2]);
            MasterModels masterModels = new MasterModels();
            var data = masterModels.LoadGetSQNId(ComdId, CorpsId, SqnId);
            return Json(data);
        }

        public JsonResult LoadUnitbyId(string Alldata)
        {
            string[] all = Alldata.Split(',');
            int ComdId = 0;
            int CorpsId = 0;
            int BdeId = 0;
            if (all[0] == "null")
                ComdId = 0;
            else
                ComdId = Convert.ToInt32(all[0]);
            if (all[1] == "null")
                CorpsId = 0;
            else
                CorpsId = Convert.ToInt32(all[1]);

            if (all[2] == "null")
                BdeId = 0;
            else
                BdeId = Convert.ToInt32(all[2]);

            MasterModels masterModels = new MasterModels();
            var data = masterModels.LoadUnit(ComdId, CorpsId, BdeId);
            return Json(data);
        }

        #endregion

        #region Category

        // GET: Category
        [HttpGet]
        public ActionResult AddCategory(string id)
        {
            try
            {
                if (Session["UserIntId"] == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                ViewBag.ButtonName = "Add";
                CategoryCRUD model = new CategoryCRUD();
                if (id != null && id != string.Empty)
                {
                    ViewBag.ButtonName = "Update";
                    int item = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(id));
                    model.CategoryId = item;
                    var editdata = con.CategoryCRUD(1, model);
                    model = editdata[0];
                }
                model.CategoryId = 0;
                var data = con.CategoryCRUD(1, model);
                model.ILCategoryCRUD = data;
                ViewBag.count = model.ILCategoryCRUD.Count();
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCategory(string id, CategoryCRUD model, string btnval)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.UserId = Convert.ToInt32(SessionManager.UserIntId);
                    if (btnval == "Add")
                    {

                        var data = con.CategoryCRUD(2, model);
                        if (data[0].IsSuccess == true)
                        {
                            DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);

                        }
                        if (data[0].IsExist == true)
                        {
                            DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);

                        }
                    }
                    if (btnval == "Update")
                    {
                        int item = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(id));
                        model.CategoryId = item;
                        var data = con.CategoryCRUD(3, model);
                        if (data[0].IsSuccess == true)
                        {
                            DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);

                        }
                    }
                }
                ViewBag.ButtonName = "Add";

                return RedirectToAction("AddCategory", new { id = string.Empty });
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

        public ActionResult DeleteCategory(string id)
        {
            try
            {
                CategoryCRUD model = new CategoryCRUD();
                int item = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(id));
                model.CategoryId = item;
                model.UserId = Convert.ToInt32(SessionManager.UserIntId);
                var data = con.CategoryCRUD(4, model);
                if (data[0].IsSuccess == true)
                {
                    DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);
                    //DisplayMessage("Comd Deleted Successfully", " ", "s");
                }
                return RedirectToAction("AddCategory", new { id = string.Empty });
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


        public JsonResult GetCategory(int Proc,int Schedule_ID)
        {
            CategoryCRUD model = new CategoryCRUD();
            Connection.DBConnection con1 = new Connection.DBConnection();
            model.ScheduleId = Schedule_ID;

            var ss = con1.CategoryCRUD(Proc, model);

            return Json(ss);
        }


        public JsonResult GetOrganizerId(int Schedule_ID)
        {
            ScheduleLetter model = new ScheduleLetter();
            Connection.DBConnection con1 = new Connection.DBConnection();
            model.ScheduleId = Schedule_ID;

            var ss = con1.ScheduleLetter_CRUD(10, model);

            return Json(ss);
        }


        #endregion

        #region Branch

        // GET: Branch
        [HttpGet]
        public ActionResult AddBranch(string id)
        {
            try
            {
                if (Session["UserIntId"] == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                ViewBag.ButtonName = "Add";
                BranchCRUD model = new BranchCRUD();
                if (id != null && id != string.Empty)
                {
                    ViewBag.ButtonName = "Update";
                    int item = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(id));
                    model.BranchId = item;
                    var editdata = con.BranchCRUD(1, model);
                    model = editdata[0];
                }
                model.BranchId = 0;
                var data = con.BranchCRUD(1, model);
                model.ILBranchCRUD = data;
                ViewBag.count = model.ILBranchCRUD.Count();
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddBranch(string id, BranchCRUD model, string btnval)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.UserId = Convert.ToInt32(SessionManager.UserIntId);
                    if (btnval == "Add")
                    {

                        var data = con.BranchCRUD(2, model);
                        if (data[0].IsSuccess == true)
                        {
                            DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);

                        }
                        if (data[0].IsExist == true)
                        {
                            DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);

                        }
                    }
                    if (btnval == "Update")
                    {
                        int item = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(id));
                        model.BranchId = item;
                        var data = con.BranchCRUD(3, model);
                        if (data[0].IsSuccess == true)
                        {
                            DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);

                        }
                    }
                }
                ViewBag.ButtonName = "Add";

                return RedirectToAction("AddBranch", new { id = string.Empty });
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

        public ActionResult DeleteBranch(string id)
        {
            try
            {
                BranchCRUD model = new BranchCRUD();
                int item = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(id));
                model.BranchId = item;
                model.UserId = Convert.ToInt32(SessionManager.UserIntId);
                var data = con.BranchCRUD(4, model);
                if (data[0].IsSuccess == true)
                {
                    DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);
                    //DisplayMessage("Comd Deleted Successfully", " ", "s");
                }
                return RedirectToAction("AddBranch", new { id = string.Empty });
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


        public JsonResult GetBranch(int Proc, int Schedule_ID)
        {
            BranchCRUD model = new BranchCRUD();
            Connection.DBConnection con1 = new Connection.DBConnection();
            model.ScheduleId = Schedule_ID;

            var ss = con1.BranchCRUD(Proc, model);

            return Json(ss);
        }

        #endregion

        #region  Folder Control Permission [04-07-2024] 
        [HttpGet]
        public ActionResult AddFolderControl(string id)
        {
            try
            {


                if (Session["UserIntId"] == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                ViewBag.ButtonName = "Add";
                //UserPolicyControlCRUD model = new UserPolicyControlCRUD();
                FolderControlPermissionCRUD model = new FolderControlPermissionCRUD();
                if (id != null && id != string.Empty)
                {
                    ViewBag.ButtonName = "Update";
                    int item = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(id));
                    model.FolderControlId = item;
                    var editdata = con.FolderControlPermissionCRUD(1, model);
                    model = editdata[0];
                }
                UserCRUD Policy = new UserCRUD();
                Policy.PolicyId = model.PolicyId;

                //Policy.PUserId = model.UserId;
                Policy.FolderControlId = model.FolderControlId;


                var Policydata = con.UserCRUD(1, Policy);
                model.ILFolderUserCRUD = Policydata;

                var data = con.FolderControlPermissionCRUD(1, model);
                model.ILFolderControlPermissionCRUD = data;
                ViewBag.count = model.ILFolderControlPermissionCRUD.Count();

                ViewBag.userintid = model.UserId = Convert.ToInt32(SessionManager.UserIntId);
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddFolderControl(string id, FolderControlPermissionCRUD model, string btnval)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.UserId = Convert.ToInt32(SessionManager.UserIntId);
                    if (btnval == "Add")
                    {

                        var data = con.FolderControlPermissionCRUD(2, model);
                        if (data[0].IsSuccess == true)
                        {
                            DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);

                        }
                        if (data[0].IsExist == true)
                        {
                            DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);

                        }
                    }
                    if (btnval == "Update")
                    {
                        int item = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(id));
                        model.FolderControlId = item;
                        var data = con.FolderControlPermissionCRUD(3, model);
                        if (data[0].IsSuccess == true)
                        {
                            DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);

                        }
                    }
                }
                ViewBag.ButtonName = "Add";

                return RedirectToAction("AddFolderControl", new { id = string.Empty });
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

        #endregion
        public JsonResult GetConf(int Proc, int TypeOfConf, int ScheduleId)
        {
            ScheduleLetter model = new ScheduleLetter();
            Connection.DBConnection con1 = new Connection.DBConnection();
            model.TypeOfConfId = TypeOfConf;
            model.ScheduleId = ScheduleId;
            var ss = con1.ScheduleLetter_CRUD(Proc, model);

            return Json(ss);
        }

       
        public JsonResult GetCounter(int Proc)
        {
            UnitCRUD model = new UnitCRUD();
            Connection.DBConnection con1 = new Connection.DBConnection();
            var ss = con1.UnitCRUD(Proc, model);

            return Json(ss);
        }



        public JsonResult GetPolicyByConfId(int Proc, int Id)
        {
            TypeofConfCRUD model = new TypeofConfCRUD();
            model.TypeOfConfId = Id;
            Connection.DBConnection con1 = new Connection.DBConnection();
            var ss = con1.TypeofConfCRUD(Proc, model);

            return Json(ss);
        }


        #region Type of Conf

        // GET: Type of Conf
        [HttpGet]
        public ActionResult AddTypeofConf(string id)
        {
            try
            {
                if (Session["UserIntId"] == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                ViewBag.ButtonName = "Add";
                TypeofConfCRUD model = new TypeofConfCRUD();
                if (id != null && id != string.Empty)
                {
                    ViewBag.ButtonName = "Update";
                    int item = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(id));
                    model.TypeOfConfId = item;
                    var editdata = con.TypeofConfCRUD(1, model);
                    model = editdata[0];
                }
                model.TypeOfConfId = 0;
                var data = con.TypeofConfCRUD(1, model);
                model.ILTypeofConfCRUD = data;
                ViewBag.count = model.ILTypeofConfCRUD.Count();

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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTypeofConf(string id, TypeofConfCRUD model, string btnval)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.UserId = Convert.ToInt32(SessionManager.UserIntId);
                    if (btnval == "Add")
                    {

                        var data = con.TypeofConfCRUD(2, model);
                        if (data[0].IsSuccess == true)
                        {
                            DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);

                        }
                        if (data[0].IsExist == true)
                        {
                            DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);

                        }
                    }
                    if (btnval == "Update")
                    {
                        int item = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(id));
                        model.TypeOfConfId = item;
                        var data = con.TypeofConfCRUD(3, model);
                        if (data[0].IsSuccess == true)
                        {
                            DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);

                        }
                    }
                }
                ViewBag.ButtonName = "Add";

                return RedirectToAction("AddTypeofConf", new { id = string.Empty });
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

        #endregion

        #region message
        public void DisplayMessage(string message, string midMsg, string messageStatus_s_e_w_i_Or_blank)
        {

            string status = messageStatus_s_e_w_i_Or_blank.ToLower();
            TempData["Message"] = message.Replace("Deleted", "Deactive");
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



        #region User Polices
        [HttpGet]
        public ActionResult AddUserPolicyControl(string id)
        {
            try
            {
                if (Session["UserIntId"] == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                ViewBag.ButtonName = "Add";
                UserPolicyControlCRUD model = new UserPolicyControlCRUD();
                if (id != null && id != string.Empty)
                {
                    ViewBag.ButtonName = "Update";
                    int item = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(id));
                    model.UserPolicyControlId = item;
                    var editdata = con.UserPolicyControlCRUD(2, model);
                    model = editdata[0];
                }
                PolicyCRUD Policy = new PolicyCRUD();
                Policy.PUserId = model.PUserId;
                Policy.UserPolicyControlId = model.UserPolicyControlId;
                var Policydata = con.PolicyCRUD(6, Policy);
                model.ILUserPolicyCRUD = Policydata;

                var data = con.UserPolicyControlCRUD(2, model);
                model.ILUserPolicyControlCRUD = data;
                ViewBag.count = model.ILUserPolicyControlCRUD.Count();

                ViewBag.userintid= model.UserId = Convert.ToInt32(SessionManager.UserIntId);
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUserPolicyControl(string id, UserPolicyControlCRUD model, string btnval)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.UserId = Convert.ToInt32(SessionManager.UserIntId);
                    if (btnval == "Add")
                    {

                        var data = con.UserPolicyControlCRUD(1, model);
                        if (data[0].IsSuccess == true)
                        {
                            DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);

                        }
                        if (data[0].IsExist == true)
                        {
                            DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);

                        }
                    }
                    if (btnval == "Update")
                    {
                        int item = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(id));
                        model.UserPolicyControlId = item;
                        var data = con.UserPolicyControlCRUD(3, model);
                        if (data[0].IsSuccess == true)
                        {
                            DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);

                        }
                    }
                }
                ViewBag.ButtonName = "Add";

                return RedirectToAction("AddUserPolicyControl", new { id = string.Empty });
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

        #endregion


        #region UserLog
        [HttpGet]
        public ActionResult UserLog(string id)
        {
            try
            {
                UserLog model = new UserLog();
                model.Unit_ID = Convert.ToInt16(SessionManager.Unit_ID);
                model.LogFromDate = DateTime.Now.ToString("dd/MMM/yyyy").Replace("-", "/");
                model.LogToDate = DateTime.Now.ToString("dd/MMM/yyyy").Replace("-", "/");
                var data = con.UserLog(1, model);
                model.IUserLog = data;
                ViewBag.count = model.IUserLog.Count();
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
        public JsonResult GetUserLog(int Procid, int UserID, string FromDate, string ToDate)
        {
            try
            {
                UserLog model = new UserLog();
                model.UserId = UserID;
                model.LogFromDate = FromDate;
                model.LogToDate = ToDate;
                return Json(con.UserLog(Procid, model));


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
                    return Json(0);
                }

                return Json(0);
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
                    return Json(0);
                }
                return Json(0);
            }
        }

        #endregion

        #region
        public ActionResult clientIp()
        {
            //GetIpAddress obj = new GetIpAddress();
            //var ipaddress = obj.getAddress();
            var ipaddress = Request.UserHostAddress;
            string settingValue = ConfigurationManager.AppSettings["ServerIPAddress"];       
            string userrole = SessionManager.RoleId;
            if (ipaddress == settingValue && userrole == enum1.Administrator.ToString())
            {            
                           
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            else
            {
             
                return Json(0, JsonRequestBehavior.AllowGet);
            } 
        }
        #endregion

        #region Agenda Point Logs Histrory

        [HttpGet]
        public ActionResult APUserLog(string id)
        {
            try
            {              

                APUserLog model = new APUserLog();             

                model.Unit_ID = Convert.ToInt16(SessionManager.Unit_ID);
                model.LogFromDate = DateTime.Now.ToString("dd/MMM/yyyy").Replace("-", "/");
                model.LogToDate = DateTime.Now.ToString("dd/MMM/yyyy").Replace("-", "/");
                var data = con.APUserLog(4, model);
                model.IAPUserLog = data;
                ViewBag.count = model.IAPUserLog.Count();
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
        public JsonResult GetAPUserLog(int Procid, int UserID, string FromDate, string ToDate)
        {
            try
            {
                APUserLog model = new APUserLog();
                model.UserId = UserID;
                model.LogFromDate = FromDate;
                model.LogToDate = ToDate;
                return Json(con.APUserLog(Procid, model));


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
                    return Json(0);
                }

                return Json(0);
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
                    return Json(0);
                }
                return Json(0);
            }
        }


        [HttpPost]
        public JsonResult GetAgendaPointUserLog(int Procid, int UserID, string FromDate, string ToDate)
        {
            try
            {
                APUserLog model = new APUserLog();
                model.UserId = UserID;
                model.LogFromDate = FromDate;
                model.LogToDate = ToDate;
                return Json(con.APUserLog(Procid, model));


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
                    return Json(0);
                }

                return Json(0);
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
                    return Json(0);
                }
                return Json(0);
            }
        }
        #endregion

        #region Counter Logs

        [HttpGet]
        public ActionResult CounterLog(string id)
        {
            try
            {
                CounterLog model = new CounterLog();
                var data = con.CounterLog(17, model);
                model.ICounterLog = data;
                ViewBag.count = model.ICounterLog.Count();
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
        public JsonResult GetCounterLog(int Procid, string Command)
        {
            try
            {
                CounterLog model = new CounterLog();
                model.ComdName = Command;
                return Json(con.CounterLog(Procid, model));
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
                    return Json(0);
                }

                return Json(0);
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
                    return Json(0);
                }
                return Json(0);
            }
        }

        #endregion

        #region Load All Unit/Sql/Bde/Crps/Comd Master

        public JsonResult LoadUnitAll(int ComndId, int CorpsId, int BdeId, int SqnId)
        {
            try
            {
                int UserId = Convert.ToInt32(SessionManager.UserIntId);

                var editdata1 = con.LoadUnitAll(1, UserId, ComndId, CorpsId, BdeId, SqnId);
                return Json(editdata1);
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
                    return Json(0);
                }

                return Json(0);
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
                    return Json(0);
                }
                return Json(0);
            }
        }
        public JsonResult LoadSqnAll(int ComndId, int CorpsId, int BdeId, int SqnId)
        {
            try
            {
                int UserId = Convert.ToInt32(SessionManager.UserIntId);

                var editdata1 = con.LoadUnitAll(2, UserId, ComndId, CorpsId, BdeId, SqnId);
                return Json(editdata1);
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
                    return Json(0);
                }

                return Json(0);
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
                    return Json(0);
                }
                return Json(0);
            }
        }
        public JsonResult LoadBdeAll(int ComndId, int CorpsId, int BdeId, int SqnId)
        {
            try
            {
                int UserId = Convert.ToInt32(SessionManager.UserIntId);

                var editdata1 = con.LoadUnitAll(3, UserId, ComndId, CorpsId, BdeId, SqnId);
                return Json(editdata1);
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
                    return Json(0);
                }

                return Json(0);
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
                    return Json(0);
                }
                return Json(0);
            }
        }
        public JsonResult LoadCrpsAll(int ComndId, int CorpsId, int BdeId, int SqnId)
        {
            try
            {
                int UserId = Convert.ToInt32(SessionManager.UserIntId);

                var editdata1 = con.LoadUnitAll(4, UserId, ComndId, CorpsId, BdeId, SqnId);
                return Json(editdata1);
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
                    return Json(0);
                }

                return Json(0);
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
                    return Json(0);
                }
                return Json(0);
            }
        }
        public JsonResult LoadComdAll(int ComndId, int CorpsId, int BdeId, int SqnId)
        {
            try
            {
                int UserId = Convert.ToInt32(SessionManager.UserIntId);

                var editdata1 = con.LoadUnitAll(5, UserId, ComndId, CorpsId, BdeId, SqnId);
                return Json(editdata1);
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
                    return Json(0);
                }

                return Json(0);
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
                    return Json(0);
                }
                return Json(0);
            }
        }
        #endregion

        #region LoadUserList
        [HttpPost]
        public JsonResult GetUserList(int Procid, int ForwardID, int scheduleId)
        {
            try
            {
                UnitCRUD model = new UnitCRUD();
                model.ScheduleId = scheduleId;
                var editdata1 = con.UnitCRUD(12, model);
                return Json(editdata1);
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
                    return Json(0);
                }

                return Json(0);
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
                    return Json(0);
                }
                return Json(0);
            }
        }
        #endregion
        public ActionResult GetPolicyLog(string Id)
        {
            try
            {
                downloadLog model = new downloadLog();
                int PolicyId = 0;
                if (!string.IsNullOrEmpty(Id))
                {
                    PolicyId = Convert.ToInt16(RepositryManager.EncryptionManager.Decryption(Id));
                }
                model.PolicyId = PolicyId;
                var data = con.downloadLogCRUD(2, model);
                model.ILdownloadLogCRUD = data;
                return PartialView("_PolicyLog", model);

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
