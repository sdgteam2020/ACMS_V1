using IHSDC.Common.Models;
using IHSDC.WebApp.Connection;
using IHSDC.WebApp.Helper;
using IHSDC.WebApp.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using static IHSDC.WebApp.Filters.CustomFilters;

namespace IHSDC.WebApp.Controllers
{
    [SessionTimeoutAttribute]
    public class AgendaPointMergeController : Controller
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

        #region Agenda Point Merge

        [HttpGet]
        public ActionResult AgendaPointMerge(string Conf)
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
                    ViewBag.Schedule_ID = item;
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


                model1.UnitName = data[0].UnitName;
                var data1 = con.CreateAPCRUD(14, model1);
                model1.ILInboxCRUD = data1;
                ViewBag.count = model1.ILInboxCRUD.Count;
                APSupportingDocu SuppModel = new APSupportingDocu();
                var SuppData = con.GetSupportingData(2, SuppModel);
                model1.ILAPSupportingDocu = SuppData;

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
        public ActionResult AgendaPointMerge(string[] sortedModelList, string withoutWatermark)
        {
            try
            {
                if (sortedModelList == null || sortedModelList.Length == 0)
                {
                    return HttpNotFound("At least one input PDF is required.");
                }

                (byte[] mergedPdfBytes, string[] SkipPage) = MergePdfFiles(sortedModelList);



                byte[] NewFile = AddPageNumberHeadeFooter(mergedPdfBytes, SkipPage);
                var ipAddress = Request.UserHostAddress;
                var currentDatetime = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                var watermarkText = $"{ipAddress}\n{currentDatetime}";

                ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                string PdfFileName = "ACMS_" + SessionManager.UserName.ToUpper() + "_" + DateTime.Now.ToString("ddMMyyyy_HHmm") + "_hrs.pdf";

                var filePath = Path.Combine(Server.MapPath("~/MergedFile"), PdfFileName);
                var DownloadPath = Request.Url.GetLeftPart(UriPartial.Authority) + "/MergedFile/" + PdfFileName;

                if (withoutWatermark == "false")
                {
                    using (MemoryStream mergedPdfStream = new MemoryStream(NewFile))
                    {
                        using (MemoryStream watermarkedPdfStream = new MemoryStream())
                        {
                            PdfReader reader = new PdfReader(mergedPdfStream);
                            using (PdfStamper stamper = new PdfStamper(reader, watermarkedPdfStream))
                            {
                                int pageCount = reader.NumberOfPages;
                                BaseFont baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.WINANSI, BaseFont.NOT_EMBEDDED);

                                for (int i = 1; i <= pageCount; i++)
                                {
                                    PdfContentByte content = stamper.GetOverContent(i);
                                    content.SaveState();
                                    content.SetFontAndSize(baseFont, 48);
                                    content.SetColorFill(BaseColor.GRAY);
                                    content.BeginText();
                                    content.ShowTextAligned(Element.ALIGN_CENTER, watermarkText, reader.GetPageSizeWithRotation(i).Width / 2, reader.GetPageSizeWithRotation(i).Height / 2, 45);
                                    content.EndText();
                                    content.RestoreState();
                                }
                            }
                            System.IO.File.WriteAllBytes(filePath, watermarkedPdfStream.ToArray());
                        }
                    }
                }
                else
                {
                    System.IO.File.WriteAllBytes(filePath, NewFile.ToArray());
                }
                return Json(new { success = true, DownloadPath = DownloadPath, FileName = PdfFileName });
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

        #region Merge/ Add header Footer
        private (byte[] mergedPdffile, string[] SkipPages) MergePdfFiles(string[] inputPdfPaths)
        {
            string[] SkipPages = new string[50];

            int pageNumber = 0;
            int j = 0;

            using (MemoryStream mergedPdfStream = new MemoryStream())
            {
                using (var document = new Document())
                {
                    using (var pdfCopy = new PdfCopy(document, mergedPdfStream))
                    {
                        document.Open();
                        foreach (var inputPdfUrl in inputPdfPaths)
                        {
                            string[] all = inputPdfUrl.Split(',');
                            String pdfName = all[0];
                            bool IsAddHeaderFooterPageNo = false;

                            if (all[1].ToString() == "")
                            {
                                IsAddHeaderFooterPageNo = false;
                            }
                            else
                            {
                                IsAddHeaderFooterPageNo = Convert.ToBoolean(all[1]);
                            }

                            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
                            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                            using (var client = new WebClient())
                            {
                                byte[] pdfData = client.DownloadData(pdfName);
                                using (var inputPdfStream = new MemoryStream(pdfData))
                                {
                                    var reader = new PdfReader(inputPdfStream);
                                    if (IsAddHeaderFooterPageNo == false)
                                    {
                                        int numberOfPages = reader.NumberOfPages;
                                        SkipPages[j] = Convert.ToString(pageNumber) + ',' + Convert.ToString(pageNumber + numberOfPages);
                                        j = j + 1;
                                    }
                                    for (int i = 1; i <= reader.NumberOfPages; i++)
                                    {
                                        pageNumber = pageNumber + 1;
                                        pdfCopy.AddPage(pdfCopy.GetImportedPage(reader, i));
                                    }

                                }
                            }
                        }
                    }
                }
                var FinalSkippages = SkipPages.Where(page => page != null).ToList();

                return (mergedPdfStream.ToArray(), FinalSkippages.ToArray());
            }
        }

        private byte[] AddPageNumberHeadeFooter(byte[] inputbytefile, string[] skipPage)
        {

            using (MemoryStream inputfilebyte = new MemoryStream(inputbytefile))
            {
                using (MemoryStream AmendedPdfStream = new MemoryStream())
                {
                    PdfReader reader = new PdfReader(inputfilebyte);
                    using (PdfStamper stamper = new PdfStamper(reader, AmendedPdfStream))
                    {
                        int pageCount = reader.NumberOfPages;

                        for (int i = 1; i <= pageCount; i++)
                        {
                            foreach (var item in skipPage)
                            {
                                string[] Page = item.Split(',');
                                int StartPage = Convert.ToInt32(Page[0]);
                                int EndPage = Convert.ToInt32(Page[1]);
                                if (StartPage <= i && i <= EndPage)
                                {
                                    goto NextPage;
                                }
                            }
                            PdfContentByte content = stamper.GetOverContent(i);
                            content.SaveState();
                            BaseColor backgroundColor = new BaseColor(255, 255, 255);
                            float x = reader.GetPageSizeWithRotation(i).Width / 2;
                            float topY = reader.GetPageSizeWithRotation(i).Height - 50 + 12;
                            float width = reader.GetPageSizeWithRotation(i).Width - 100;
                            float height = 25;

                            float x1 = reader.GetPageSizeWithRotation(i).Width / 2;
                            float y1 = 50;
                            float width1 = reader.GetPageSizeWithRotation(i).Width - 100;
                            float height1 = 25;

                            topY -= 2;
                            y1 -= 2;

                            content.SetColorFill(backgroundColor);
                            content.Rectangle(50, topY - height / 2, width, height);
                            content.Rectangle(50, y1 - height1 / 2, width1, height1);
                            content.Fill();
                            BaseFont baseFont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.WINANSI, BaseFont.NOT_EMBEDDED);
                            content.SetFontAndSize(baseFont, 12);
                            content.SetColorFill(BaseColor.BLACK);

                            content.BeginText();
                            content.ShowTextAligned(Element.ALIGN_CENTER, "RESTRICTED", x, topY, 0);
                            content.ShowTextAligned(Element.ALIGN_CENTER, "___________", x, topY, 0);
                            content.ShowTextAligned(Element.ALIGN_CENTER, "RESTRICTED", x1, y1, 0);
                            content.ShowTextAligned(Element.ALIGN_CENTER, "___________", x1, y1, 0);
                            content.EndText();
                            content.RestoreState();

                            PdfContentByte content1 = stamper.GetOverContent(i);
                            content1.SaveState();
                            BaseColor backgroundColor1 = new BaseColor(255, 255, 255);
                            BaseFont baseFont1 = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.WINANSI, BaseFont.NOT_EMBEDDED);
                            float x2 = reader.GetPageSizeWithRotation(i).Width / 2;
                            float y2 = reader.GetPageSizeWithRotation(i).Height - 70;
                            float width2 = reader.GetPageSizeWithRotation(i).Width - 100; // 100% width
                            float height2 = 25;

                            y2 -= -3;

                            content1.SetColorFill(backgroundColor1);
                            content1.Rectangle(50, y2 - height2 / 2, width2, height2);
                            content1.Fill();
                            content1.SetFontAndSize(baseFont1, 12);
                            content1.SetColorFill(BaseColor.BLACK);

                            content1.BeginText();
                            if (i != 1)
                            {
                                content1.ShowTextAligned(Element.ALIGN_CENTER, Convert.ToString(i), x2, y2, 0);
                            }
                            content1.EndText();
                            content1.RestoreState();


                        NextPage:;
                        }
                    }
                    return AmendedPdfStream.ToArray();
                }
            }
        }
        #endregion

        #region Search
        [HttpGet]
        public ActionResult SearchInboxNoting(string Conf)
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
                    ViewBag.Schedule_ID = item;
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


                model1.UnitName = data[0].UnitName;

                var data1 = con.CreateAPCRUD(4, model1);
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

        #region Support Docu 
        [HttpGet]
        public ActionResult DeleteSuppDocu(string id)
        {
            try
            {
                APSupportingDocu model = new APSupportingDocu();

                if (id != string.Empty && id != null)
                {
                    model.SuppFileName = RepositryManager.EncryptionManager.Decryption(id);
                }
                var filePath = Path.Combine(Server.MapPath("~/Uploads"), model.SuppFileName);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
                var data = con.GetSupportingData(3, model);

                return RedirectToAction("AgendaPointMerge", new { Conf = string.Empty });
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

        public ActionResult SupportDocuUpload(InboxCRUD model)
        {
            try
            {
                APSupportingDocu Suppmodel = new APSupportingDocu();

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

                            var filePath = Path.Combine(Server.MapPath("~/Uploads"), fileModel.FileName);
                            Suppmodel.SuppFileName = originalFileName;
                            Suppmodel.SuppFilePath = Request.Url.GetLeftPart(UriPartial.Authority) + "/Uploads/" + fileModel.FileName;
                            System.IO.File.WriteAllBytes(filePath, fileModel.Data);
                            var data = con.GetSupportingData(1, Suppmodel);
                        }
                    }
                }

                return RedirectToAction("AgendaPointMerge", new { Conf = string.Empty });
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