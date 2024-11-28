using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IHSDC.WebApp.Models;
using IHSDC.WebApp.Connection;
using static IHSDC.WebApp.Filters.CustomFilters;
using Newtonsoft.Json;
using System.Xml.Linq;
using IHSDC.WebApp.Helper;
using System.Data.SqlClient;
using System.Text;
using System.IO;

namespace IHSDC.WebApp.Controllers
{
    [SessionTimeoutAttribute]
    public class ScheduleController : Controller
    {
        readonly Connection.DBConnection con = new Connection.DBConnection();

        #region Initiate Conf
        [HttpGet]
        public ActionResult ScheduleLetter(string id, string ComdId, string Gid)
        {

            try
            {
                ViewBag.ButtonName = "Add";
                ScheduleLetter model = new ScheduleLetter();
                if (id != null && id != string.Empty)
                {
                    ViewBag.ButtonName = "Update";
                    int item = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(id));
                    model.ScheduleId = item;
                    var editdata = con.ScheduleLetter_CRUD(2, model);
                    model = editdata[0];
                }


                CategoryCRUD OrgCat = new CategoryCRUD();
                BranchCRUD Branch = new BranchCRUD();
                UnitCRUD UnitCr = new UnitCRUD();

                OrgCat.ScheduleId = model.ScheduleId;
                Branch.ScheduleId = model.ScheduleId;
                UnitCr.ScheduleId = model.ScheduleId;

                var Org = con.CategoryCRUD(4, OrgCat);
                model.IOrganizerCatCRUD = Org;

                ViewBag.OrganizerCat = model.IOrganizerCatCRUD.Where(n => n.Msg == "Selected").ToList();

                var Sponsor = con.CategoryCRUD(5, OrgCat);
                model.ISponsorCatCRUD = Sponsor;

                ViewBag.SponsorCat = model.ISponsorCatCRUD.Where(n => n.Msg == "Selected").ToList();


                var Nodal = con.CategoryCRUD(6, OrgCat);
                model.INodalCatCRUD = Nodal;

                ViewBag.NodalCat = model.INodalCatCRUD.Where(n => n.Msg == "Selected").ToList();


                var Brach = con.BranchCRUD(4, Branch);
                model.IBranchCRUD = Brach;

                ViewBag.Branch = model.IBranchCRUD.Where(n => n.Msg == "Selected").ToList();


                var ParUser = con.UnitCRUD(14, UnitCr);
                model.IParticipateUsersCRUD = ParUser;

                var ParNewUser = con.UnitCRUD(19, UnitCr);
                model.IParticipateNewUsersCRUD = ParNewUser;


                ViewBag.ParticipateUsers = model.IParticipateUsersCRUD.Where(n => n.Msg == "Selected").ToList();

                ViewBag.ParticipateNewUsers = model.IParticipateNewUsersCRUD.Where(n => n.Msg == "Selected").ToList();

                if (SessionManager.RoleId == enum1.GSO1)
                {
                    model.ScheduleId = 0;
                    var data1 = con.ScheduleLetter_CRUD(9, model);
                    model.ILScheduleLetter = data1;
                    ViewBag.count = model.ILScheduleLetter.Count();
                }
                else
                {

                    model.ScheduleId = 0;
                    var data = con.ScheduleLetter_CRUD(2, model);
                    model.ILScheduleLetter = data;
                    ViewBag.count = model.ILScheduleLetter.Count();

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
        public ActionResult ScheduleLetter(string id, ScheduleLetter model, string btnval, string oldid)
        {
            try
            {

                model.UserId = Convert.ToInt32(SessionManager.UserIntId);
                model.Subject = model.Subject.TrimEnd();
                if (btnval == "Add")
                {

                    var folder = Server.MapPath("~/Uploads/") + model.TypeOfConfId + "_" + model.Subject;
                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }
                    var data = con.ScheduleLetter_CRUD(1, model);
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
                    var NewFolder = Server.MapPath("~/Uploads/") + model.TypeOfConfId + "_" + model.Subject;
                    var Oldfolder = Server.MapPath("~/Uploads/") + model.TypeOfConfId + "_" + oldid.TrimEnd();
                    if (model.Subject != oldid.TrimEnd())
                    {
                        System.IO.Directory.Move(Oldfolder, NewFolder);
                    }
                    else
                    {
                        if (!Directory.Exists(Oldfolder))
                        {
                            Directory.CreateDirectory(Oldfolder);
                        }
                    }

                    int item = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(id));
                    model.ScheduleId = item;
                    var data = con.ScheduleLetter_CRUD(11, model);
                    if (data[0].IsSuccess == true)
                    {
                        DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);

                    }
                }
                ViewBag.ButtonName = "Add";

                return RedirectToAction("ScheduleLetter", new { id = string.Empty });
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
        public ActionResult ScheduleLetterClose(string id, string fid)
        {
            try
            {
                string StatusName = "";
                ScheduleLetter model = new ScheduleLetter();
                int item = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(id));
                if (fid != null && fid != string.Empty)
                {
                    StatusName = RepositryManager.EncryptionManager.Decryption(fid);
                }

                model.ScheduleId = item;
                model.StatusName = StatusName;
                var data = con.ScheduleLetter_CRUD(8, model);
                if (data[0].IsSuccess == true)
                {
                    DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);
                }
                return RedirectToAction("ScheduleLetter", new { id = string.Empty, ComdId = string.Empty, Gid = string.Empty });
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