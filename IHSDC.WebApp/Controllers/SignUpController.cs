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


namespace IHSDC.WebApp.Controllers
{
    public class SignUpController : Controller
    {
        readonly Connection.DBConnection con = new Connection.DBConnection();
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult LoadCorpsNameByCommandId(int ComdId)
        {
            MasterModels masterModels = new MasterModels();
            var data = masterModels.LoadCorpsNameByCommandId(ComdId);
            return Json(data);
        }

        public JsonResult LoadBdeCATbyId(string Alldata)
        {
            string[] all = Alldata.Split(',');
            int ComdId = 0;
            int CorpsId = 0;
            //  if (all[0] == "null")
            if (all[0] == "null" || all[0] == "")
                ComdId = 0;
            else
                ComdId = Convert.ToInt32(all[0]);
            //if (all[1] == "null")
            if (all[1] == "null" || all[1] == "")
                CorpsId = 0;
            else
                CorpsId = Convert.ToInt32(all[1]);
            MasterModels masterModels = new MasterModels();
            var data = masterModels.LoadBdeCAT(ComdId, CorpsId);
            return Json(data);
        }

        //public JsonResult LoadUnitbyId(string Alldata)
        //{
        //    string[] all = Alldata.Split(',');
        //    int ComdId = 0;
        //    int CorpsId = 0;
        //    int BdeId = 0;
        //    if (all[0] == "null")
        //        ComdId = 0;
        //    else
        //        ComdId = Convert.ToInt32(all[0]);
        //    if (all[1] == "null")
        //        CorpsId = 0;
        //    else
        //        CorpsId = Convert.ToInt32(all[1]);

        //    if (all[2] == "null")
        //        BdeId = 0;
        //    else
        //        BdeId = Convert.ToInt32(all[2]);

        //    MasterModels masterModels = new MasterModels();
        //    var data = masterModels.LoadUnit(ComdId, CorpsId, BdeId);
        //    return Json(data);
        //}
        public JsonResult LoadUnitbyId(string Alldata)
        {
            try
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

                // if (all[2] == "null")
                if (all[2] == null || all[2] == "undefined")
                    BdeId = 0;
                else
                    BdeId = Convert.ToInt32(all[2]);

                MasterModels masterModels = new MasterModels();
                var data = masterModels.LoadUnit(ComdId, CorpsId, BdeId);
                return Json(data);
            }
            catch (Exception ex)
            {

                return Json(new { error = "error" });
            }
        }


        #region Corps
        // GET: Corps
        [HttpGet]
        public ActionResult AddCorps(string id)
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




        [HttpPost]
        public ActionResult AddCorps(string id, CorpsCRUD model, string btnval)
        {
            var data = con.CorpsCRUD(1, model);
            if (data[0].IsSuccess == true)
            {
                DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);
                TempData["message"] = "Save Successfully. Please Select Comd/PSOs Again.";
            }
            if (data[0].IsExist == true)
            {
                DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);
                TempData["message"] = "Already Exist";
            }

            ViewBag.ButtonName = "Add";
            return Json(new { message = TempData["message"] });
        }
        private void DisplayMessage(string message, string midMessage, string messageStatus)
        {

            ViewBag.Message = message;
            ViewBag.MidMessage = midMessage;
            ViewBag.MessageStatus = messageStatus;
        }
        #endregion

        #region Unit
        // GET: Corps
        [HttpGet]
        public ActionResult AddUnit(string id)
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

        [HttpPost]
        public ActionResult AddUnit(string id, UnitCRUD model, string btnval)
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
                        TempData["message"] = "Save Successfully";
                    }
                    if (data[0].IsExist == true)
                    {
                        DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);
                        TempData["message"] = "Already Exist";
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
                        TempData["message"] = "Update Successfully";
                    }
                }
            }
            ViewBag.ButtonName = "Add";
            return RedirectToAction("AddUnit", new { id = string.Empty });
        }


        [HttpGet]
        public ActionResult AddUnitNew(string id)
        {
            ViewBag.corpsId = 1;
            ViewBag.BdeCatId = 1;
            ViewBag.SqnIdbind = 1;

            UnitCRUD model = new UnitCRUD();
            model.Unit_ID = 0;
            var data = con.UnitCRUD(2, model);
            ViewBag.ILUnitCRUD = data;
            return Json(new { ilUnitCrud = data }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult AddUnitNew(string id, UnitCRUD model, string btnval, string spnCorpUnit)
        {

            string spnCorpUnitValue = spnCorpUnit;
            model.CorpId = Convert.ToInt32(spnCorpUnitValue);
            model.ComdId = model.ComdIdUnit;

            model.UserId = Convert.ToInt32(SessionManager.UserIntId);
            //if (btnval == "Add")
            //{
            var data = con.UnitCRUDNew(1, model);
            if (data[0].IsSuccess == true)
            {
                DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);
                TempData["message"] = "Save Successfully. Please Select Comd/PSOs Again.";
            }
            if (data[0].IsExist == true)
            {
                DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);
                TempData["message"] = "Already Exist";
            }
            // }


            ViewBag.ButtonName = "Add";
            //  return RedirectToAction("AddUnit", new { id = string.Empty });
            return Json(new { message = TempData["message"] });
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

        #region Appointment
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
                        TempData["message"] = "Appointment Save Successfully";
                    }
                    if (data[0].IsExist == true)
                    {
                        DisplayMessage(data[0].Msg, data[0].MidMsg, data[0].MsgStatus);
                        TempData["message"] = "Appointment Already Exist";
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
                        TempData["message"] = "Appointment Update Successfully";
                    }
                }
                ViewBag.ButtonName = "Add";

                return Json(new { message = TempData["message"] });
                //  return RedirectToAction("SignUp", new { id = string.Empty, message = TempData["message"] });
                // return RedirectToAction("AddAppt", new { id = string.Empty, message = TempData["message"] });
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
        public ActionResult ReloadAppointments()
        {
            // Reload appointments from the server (assuming LoadAppointment() does this)
            var appointments = (new IHSDC.WebApp.Models.MasterModels()).LoadAppointment();
            // Return appointments as JSON
            return Json(appointments, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ApptOrderby(int OrderbyId, int NewOrderbyId)
        {
            MasterModels masterModels = new MasterModels();
            var data = masterModels.ApptOrderby(OrderbyId, NewOrderbyId);
            return Json(0);
        }

        #endregion

    }

}