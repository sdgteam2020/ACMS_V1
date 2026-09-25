using IHSDC.Common.Models;
using IHSDC.WebApp.Connection;
using IHSDC.WebApp.Helper;
using IHSDC.WebApp.Models;
using IHSDC.WebApp.Services;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace IHSDC.WebApp.Controllers
{
    public class PolicyCornerController : Controller
    {
        readonly Connection.DBConnection con = new Connection.DBConnection();
        private ApplicationDbContext _db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;

        //private readonly SearchService searchService;
        private readonly string _CurrentDirectoryPath;
        private readonly Dictionary<string, SearchService> _docPathServices;

        public PolicyCornerController(string Inipath, Dictionary<string, SearchService> docPathServices)
        {
            _CurrentDirectoryPath = Inipath;
            _docPathServices = docPathServices;
        }

        #region Title search only
        public JsonResult SearchAgendaPointTitle(string Term, string PolicyID,string ConType)
        {

            string searchTerm = "";
            string Policy = "";
            int ConTypeId = 0;
            List<SearchData> DataList = new List<SearchData>();
            if (Term != string.Empty && Term != null)
            {
                searchTerm = RepositryManager.EncryptionManager.Decryption(Term);
            }

            if (PolicyID != string.Empty && PolicyID != null)
            {
                Policy = RepositryManager.EncryptionManager.Decryption(PolicyID);
            }

            if (ConType != string.Empty && ConType != null)
            {
                ConTypeId = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(ConType));
            }

            ViewBag.Policy = Policy;
            string currentDirectoryPath = "";
            try
            {
                foreach (var item in MvcApplication.AllPath)
                {
                    string Folder = Path.GetFileName(item);
                    if (Policy == Folder)
                    {
                        currentDirectoryPath = item;
                    }
                }
            }
            catch { }
            if (_docPathServices.TryGetValue(currentDirectoryPath, out var searchService))
            {
                if (string.IsNullOrEmpty(searchTerm))
                {
                    return Json("");
                }
                var results = searchService.Search(searchTerm);

                
                
                if (results.Count > 0)
                {
                    foreach (var item in results)
                    {
                        SearchData Data = new SearchData();
                        Data.paragraph = item.ParagraphBefore + ' ' + item.MatchedParagraph + ' ' + item.ParagraphAfter;
                        Data.FileName = Request.Url.GetLeftPart(UriPartial.Authority) + "/Policy/" + Policy + "/"+item.FileName;
                        DataList.Add(Data);
                    }
                }
                
            }
            SearchData PolicyTitleData = new SearchData();
            PolicyTitleData.Id = ConTypeId;
            var data = con.GetPolicyTitleData(5, PolicyTitleData);
            if (data.Count > 0)
            {
                foreach (var PItem in data)
                {
                    var content = PItem.paragraph;

                    int index = content.IndexOf(searchTerm.ToLower(), StringComparison.OrdinalIgnoreCase);
                    if (index != -1)
                    {
                        SearchData DataNew = new SearchData();
                        DataNew.paragraph = content;
                        DataNew.FileName = PItem.FileName;
                        DataList.Add(DataNew);
                    }
                }
            }
            return Json(DataList.ToList());

        }
        #endregion

        #region Search Engine Popup
        public ActionResult SearchTitle(string Term, string PolicyID, string ConType)
        {
            try
            {
                string searchTerm = "";
                string Policy = "";
                int ConTypeId = 0;
                List<SearchData> DataList = new List<SearchData>();
                if (!string.IsNullOrEmpty(Term))
                {
                    searchTerm = RepositryManager.EncryptionManager.Decryption(Term);
                }

                if (!string.IsNullOrEmpty(PolicyID))
                {
                    Policy = RepositryManager.EncryptionManager.Decryption(PolicyID);
                }

                if (!string.IsNullOrEmpty(ConType))
                {
                    ConTypeId = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(ConType));
                }

                ViewBag.Policy = Policy;
                string currentDirectoryPath = "";
                try
                {
                    foreach (var item in MvcApplication.AllPath)
                    {
                        string Folder = Path.GetFileName(item);
                        //match Policy Name and folder name and then set the directory for the service
                        if (Policy == Folder)
                        {
                            currentDirectoryPath = item;
                        }
                    }
                }
                catch { }

                if (_docPathServices.TryGetValue(currentDirectoryPath, out var searchService))
                {
                    if (string.IsNullOrEmpty(searchTerm))
                    {
                        return PartialView("_TitleSearchResults", new List<SearchData>());
                    }

                    var results = searchService.Search(searchTerm);
                    

                    if (results.Count > 0)
                    {
                        foreach (var item in results)
                        {
                            SearchData Data = new SearchData();
                            Data.paragraph = item.ParagraphBefore + ' ' + item.MatchedParagraph + ' ' + item.ParagraphAfter;
                            Data.FileName = Request.Url.GetLeftPart(UriPartial.Authority) + "/Policy/" + Policy + "/" + item.FileName;
                            DataList.Add(Data);
                        }
                    }

                    
                }
                SearchData PolicyTitleData = new SearchData();
                PolicyTitleData.Id = ConTypeId;
                var data = con.GetPolicyTitleData(5, PolicyTitleData);
                if (data.Count > 0)
                {
                    foreach (var PItem in data)
                    {
                        var content = PItem.paragraph;

                        int index = content.IndexOf(searchTerm.ToLower(), StringComparison.OrdinalIgnoreCase);
                        if (index != -1)
                        {
                            SearchData DataNew = new SearchData();
                            DataNew.paragraph = content;
                            DataNew.FileName = PItem.FileName;
                            DataList.Add(DataNew);
                        }
                    }
                }
                return PartialView("_TitleSearchResults", DataList);
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

        public ActionResult Search(string Term, string id,string type,string PLid)
        {
            try
            {
            string searchTerm = "";
            string PolicyId = "";
            string DocType = "";
            string Policy = "";
            if (Term != string.Empty && Term != null)
            {
                searchTerm = RepositryManager.EncryptionManager.Decryption(Term);
            }
            if (id != string.Empty && id != null)
            {
                PolicyId = RepositryManager.EncryptionManager.Decryption(id);
            }
            if (type != string.Empty && type != null)
            {
                    DocType = RepositryManager.EncryptionManager.Decryption(type);
            }
            if (PLid != string.Empty && PLid != null)
            {
                Policy = RepositryManager.EncryptionManager.Decryption(PLid);
            }
                else
                {
                    Policy = "";
                }
                ViewBag.DocType = DocType;
                ViewBag.Policy = Policy;
                string currentDirectoryPath = "";
            try
            {
                foreach (var item in MvcApplication.AllPath)
                {
                    string Folder = Path.GetFileName(item);
                    if (PolicyId == Folder)
                    {
                        currentDirectoryPath = item;
                        break;
                    }
                }
            }
            catch { }
            if (_docPathServices.TryGetValue(currentDirectoryPath, out var searchService))
            {
                if (string.IsNullOrEmpty(searchTerm))
                {
                    return PartialView("_SearchResults", new List<SearchableItem>());
                }

                var results = searchService.Search(searchTerm);
                return PartialView("_SearchResults", results);
            }

            return PartialView("_SearchResults", new List<SearchableItem>());
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

        #region PolicyCorner
        [HttpGet]
        public ActionResult PolicyCorner()
        {
            try
            {
                if (Session["UserIntId"] == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                ViewBag.ButtonName = "Add";
                PolicyCRUD model = new PolicyCRUD();
                model.PUserId = Convert.ToInt16(SessionManager.UserIntId);
                var data = con.PolicyCRUD(7, model);
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


        #endregion

        public ActionResult GetFilesToShow(string FName, string Type)
        {
            try
            {
                FileList Files = new FileList();
                string FolderName = "";
                int DocType = 0;
                if (!string.IsNullOrEmpty(FName))
                {
                    FolderName = RepositryManager.EncryptionManager.Decryption(FName);
                }
                if (!string.IsNullOrEmpty(Type))
                {
                    DocType = Convert.ToInt16(RepositryManager.EncryptionManager.Decryption(Type));
                }

                string PolicyPath = Server.MapPath("~/Policy/") + FolderName;
                List<string> fileNames = Directory.GetFiles(PolicyPath, "*", SearchOption.AllDirectories).ToList();

                List<FileList> fileList = fileNames.Select(fileName => new FileList
                {
                    FileName = Path.GetFileName(fileName),
                    FilePath = fileName,
                    FileType = DocType
                }).ToList();

                return PartialView("_ShowFile", fileList);

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
        public ActionResult AddAccessLog(string Pid)
        {
            try
            {
                if (Session["UserIntId"] == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                int PolicyId = 0;
                if (!string.IsNullOrEmpty(Pid))
                {
                    PolicyId = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(Pid));
                }

                downloadLog log = new downloadLog();
                log.FileName = "";
                log.ICNumber = SessionManager.ArmyNo;
                log.IpAddress = ClientIPAddress.GetIPAddress();
                log.PolicyId = Convert.ToInt16(PolicyId);
                log.AccessDownload = "A";
                var data = con.downloadLogCRUD(1, log);

                return Json(new { success = true});
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
        
        public ActionResult DownloadpdfWithWatermark2FA(string title, string path, string type, string Pid)
        {
            String pathss = "";
            String filename = "";
            String titles = "";
            String pathName = "";
            String Doctype = "";
            String PolicyId = "";

            if (Session["UserIntId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            CertificateData certificateData = null;
            try
            {
                if (type != string.Empty && type != null)
                {
                    Doctype = RepositryManager.EncryptionManager.Decryption(type);
                }

                if (path != string.Empty && path != null && title != string.Empty && title != null)
                {
                    pathss = RepositryManager.EncryptionManager.Decryption(path);
                    titles = RepositryManager.EncryptionManager.Decryption(title);


                    pathName = Path.GetFileName(pathss);

                    filename = titles + "_" + pathName;

                }

                if (Pid != string.Empty && Pid != null)
                {
                    PolicyId = RepositryManager.EncryptionManager.Decryption(Pid);
                }

                // if (Doctype == "2")
                if (Doctype == "2" || Doctype == "1")
                {
                    try
                    {
                        //string response = GetRequest("http://localhost/Temporary_Listen_Addresses/FetchUniqueTokenDetails");
                       
                        //List<CertificateData> certificates = JsonConvert.DeserializeObject<List<CertificateData>>(response);
                        //if (certificates == null)
                        //{
                        //    DisplayMessage("No Token Found !", "", "i");
                        //    return RedirectToAction("PolicyCorner");
                        //}
                        //certificateData = certificates[0];
                        //if (certificates[0].Status == "404")
                        //{
                        //    DisplayMessage(certificates[0].Remarks, "", "i");
                        //    return RedirectToAction("PolicyCorner");
                        //}
                        //else
                        //{
                        //    if (certificates[0].TokenValid == true)
                        //    {

                        //        //var Armynumber = SessionManager.ArmyNo;
                        //        string Data = "{\"inputPersID\":\"" + SessionManager.ArmyNo + "\"}";
                        //        //string Data = "{\"inputPersID\":\""+ Armynumber + "\"}";
                        //        //string Data = $"{{\"inputPersID\":\"'"+@SessionManager.ArmyNo+"'\"}}";
                        //        string PostResponse = PostRequest("http://localhost/Temporary_Listen_Addresses/ValidatePersID2FA", Data);
                        //        ResponseList responseData = JsonConvert.DeserializeObject<ResponseList>(PostResponse);
                        //        if (!responseData.ValidatePersID2FAResult)
                        //        {
                        //            DisplayMessage("You are not authorized for this policy", "", "i");
                        //            return RedirectToAction("PolicyCorner");
                        //        }
                        //    }
                        //    else
                        //    {
                        //        DisplayMessage("Hastakshar SEWA Not Running/Not Installed.", "To install Hastakshar SEWA - Download ADN version of Hastakshar SEWA and run the setup.", "i");
                        //        return RedirectToAction("PolicyCorner");
                        //    }

                        //}
                        
                        
                        downloadLog download = new downloadLog();
                        download.FileName = pathName;
                        download.ICNumber = SessionManager.ArmyNo;
                        download.IpAddress = ClientIPAddress.GetIPAddress();
                        download.PolicyId = Convert.ToInt16(PolicyId);
                        download.AccessDownload = "D";
                        var data = con.downloadLogCRUD(1, download);
                    }
                    catch (Exception ex)
                    {
                        DisplayMessage("Hastakshar SEWA Not Running/Not Installed.", "To install Hastakshar SEWA - Download ADN version of Hastakshar SEWA and run the setup.", "i");
                        return RedirectToAction("PolicyCorner");
                    }
                }




                var ipAddress = ClientIPAddress.GetIPAddress();
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


        public static string GetRequest(string URI)
        {
            try
            {
                System.Net.WebRequest req = System.Net.WebRequest.Create(URI);
                req.ContentType = "application/json";
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback
                (
                   delegate { return true; }
                );

                using (HttpWebResponse res = (HttpWebResponse)req.GetResponse())
                {
                    lObj.mStatusCode = (int)res.StatusCode;
                    StreamReader reader = new StreamReader(res.GetResponseStream());
                    lObj.mStrResponse = reader.ReadToEnd().Trim();
                    reader.Close();
                }
                return lObj.mStrResponse.Trim();
            }
            catch (WebException e)
            {
                var response = e.Response as HttpWebResponse;
                var reader = new StreamReader(e.Response.GetResponseStream());
                lObj.mStrResponse = reader.ReadToEnd();
                lObj.mStatusCode = (Int32)response.StatusCode;
                reader.Close();
                return lObj.mStrResponse;
            }
            catch (Exception ex)
            {
                return lObj.mStrResponse + " Message :" + ex.Message;
            }
        }


        public static string PostRequest(string URI, string postData)
        {
            try
            {
                System.Net.WebRequest req = System.Net.WebRequest.Create(URI);
                req.Method = "POST";
                req.ContentType = "application/json";
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                req.ContentLength = byteArray.Length;
                using (Stream dataStream = req.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                }
                using (HttpWebResponse res = (HttpWebResponse)req.GetResponse())
                {
                    lObj.mStatusCode = (int)res.StatusCode;
                    StreamReader reader = new StreamReader(res.GetResponseStream());
                    lObj.mStrResponse = reader.ReadToEnd().Trim();
                    reader.Close();
                }
                return lObj.mStrResponse.Trim();
            }
            catch (WebException e)
            {
                var response = e.Response as HttpWebResponse;
                var reader = new StreamReader(e.Response.GetResponseStream());
                lObj.mStrResponse = reader.ReadToEnd();
                lObj.mStatusCode = (Int32)response.StatusCode;
                reader.Close();
                return lObj.mStrResponse;
            }
            catch (Exception ex)
            {
                return lObj.mStrResponse + " Message :" + ex.Message;
            }
        }


        public static class lObj
        {
            public static string mStrResponse { get; set; }
            public static int mStatusCode = 400;
            public static String gurl { get; set; }
        }

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