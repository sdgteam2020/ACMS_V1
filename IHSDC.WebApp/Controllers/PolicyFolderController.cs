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
    public class PolicyFolderController : Controller
    {


        readonly Connection.DBConnection con = new Connection.DBConnection();
        private ApplicationDbContext _db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;

        //private readonly SearchService searchService;
        private readonly string _CurrentDirectoryPath;
        private readonly Dictionary<string, SearchService> _docPathServices;


        #region Policy Folder [08-07-2024]
        [HttpGet]
        public ActionResult PolicyFolder()
        {
            try
            {
                if (Session["UserIntId"] == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                ViewBag.ButtonName = "Add";
                PolicyFolderCRUD model = new PolicyFolderCRUD();
                model.PUserId = Convert.ToInt16(SessionManager.UserIntId);
                var data = con.proc_PolicyFolder_CRUD(7, model);
                model.ILPolicyFolderCRUD = data;
                ViewBag.count = model.ILPolicyFolderCRUD.Count();

                //#region For Unread AP Count
                //InboxCRUD CntInbox = new InboxCRUD();
                //var Cnt = con.CreateAPCRUD(10, CntInbox);
                //if (Cnt.Count > 0)
                //{
                //    ViewBag.UnReadMsg = Cnt.Count;
                //}
                //else
                //{
                //    ViewBag.UnReadMsg = 0;
                //}
                //#endregion
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
    }
}