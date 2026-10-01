using AA7.Models;
using IHSDC.Common.Models;
using IHSDC.WebApp.Connection;
using IHSDC.WebApp.Helper;
using IHSDC.WebApp.Models;
using IHSDC.WebApp.RepositryManager;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using OneLogin.Saml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace IHSDC.WebApp.Controllers
{
    //[Authorize] // remove in case of IAM
    [AllowAnonymous]
    public class AccountController : BaseController
    {
        readonly Connection.DBConnection con = new Connection.DBConnection();

        private ApplicationDbContext _db = new ApplicationDbContext();

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        string debugWithIAMflag= ConfigurationManager.AppSettings["debugWithIAM"];
        string loginWithIAMflag = ConfigurationManager.AppSettings["withIAMLogin"];

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
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

        //
        //GET: /Account
        [Authorize]
        public ActionResult Hierarchy()
        {
            try
            {
                if (Session["UserIntId"] == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                UserWiseRole URole = new UserWiseRole();
                var UR = con.GetUserRole(URole);
                ViewBag.UserRole = UR;

                UnitCRUD UUnit = new UnitCRUD();
                var UR1 = con.UnitCRUD(2, UUnit);
                ViewBag.Unit = UR1;


                var CurrentUser = UserManager.FindById(User.Identity.GetUserId());
                if (User.IsInRole("Administrator"))
                    Information("You have <strong>Administrative Previleges</strong>, please make changes carefully!", true);
                return View(Common.Helpers.Identity.Hierarchy.GetHierarchyUsers(CurrentUser).OrderBy(u => u.UserName));
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

        //GET: /Account
        [Authorize]
        public ActionResult HierarchyUsers()
        {
            try
            {
                if (Session["UserIntId"] == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                var CurrentUser = UserManager.FindById(User.Identity.GetUserId());
                if (User.IsInRole("Unit"))
                    Information("You have <strong>Administrative Previleges</strong>, please make changes carefully!", true);
                return View(Common.Helpers.Identity.Hierarchy.GetHierarchyUsers(CurrentUser).OrderBy(u => u.IntId));
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


        // Edit current user
        [Authorize]
        public ActionResult Edit(string id)
        {
            ViewBag.ButtonName = "Save";

            try
            {
                if (Session["UserIntId"] == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                if (string.IsNullOrEmpty(id))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                IHSDCAA7DBDBContext db = new IHSDCAA7DBDBContext();
                //var AllRoles = (new ApplicationDbContext()).Roles.OrderBy(r => r.IsPermission).Where(n => n.Name != "Administrator").ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
                //ViewBag.Roles = AllRoles;
                //ViewData["Roles"] = AllRoles;
                SelectList list_of_unit = new SelectList(db.dbo_tbl_Unit, "Unit_ID", "UnitName");
                ViewData["Unit_ID"] = list_of_unit;

                ViewData["EstablishmentFull"] = new SelectList(db.dbo_tbl_Unit, "UnitName", "UnitName");
                var CurrentUser = _db.Users.FirstOrDefault(i => i.UserName == User.Identity.Name);
                var EditUser = _db.Users.FirstOrDefault(i => i.Id == id);


                if (EditUser == null) return HttpNotFound("No such user exists. Edit User Failed!");
                //if (EditUser == CurrentUser) return Redirect("~/Manage/Index");
                //if (EditUser.Superior == CurrentUser)
                //{
                ViewBag.EditUsername = EditUser.UserName;
                ViewBag.UserId = EditUser.Id;
                var ss = EditUser.Roles.Single();


                var existsrole = _db.Roles.Where(r => r.Id == ss.RoleId.ToString()).Single();

                var AllRoles = (new ApplicationDbContext()).Roles.OrderBy(r => r.IsPermission).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
                ViewBag.Roles = AllRoles;
                ViewData["Roles"] = AllRoles;

                ViewBag.sel_unit_id = EditUser.Unit_ID;
                ViewBag.sel_roles_id = existsrole.Name;
                ViewBag.pass = EditUser.PasswordHash;


                ViewData["UserTypeId"] = EditUser.UserTypeId;
                ViewData["UnitId"] = EditUser.Unit_ID;
                var UnitData = db.dbo_tbl_Unit.Where(c => c.Unit_ID == EditUser.Unit_ID).Single();
                ViewBag.ComdId = UnitData.Command;
                ViewBag.CorpId = UnitData.Corps;



                return View(new EditUserViewModel(EditUser));
                //}
                //Danger(string.Format("You are not authorised to edit this user. The <strong>{0}</strong> is not your immediate subordinate.", EditUser.UserName), true);
                //return RedirectToAction("Hierarchy");
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
                    return Redirect("/Error/Error");
                }
                return Redirect("/Error/Error");
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var CurrentUser = _db.Users.FirstOrDefault(i => i.UserName == User.Identity.Name);
                var EditUser = _db.Users.FirstOrDefault(i => i.Id == model.UserId);
                var DEFwdAuth = _db.Users.FirstOrDefault();
                //if (EditUser.Superior == CurrentUser)
                //{
                EditUser.DEFwdAuth = model.DEFwdAuth;
                EditUser.GEBFwdAuth = model.GEBFwdAuth;
                EditUser.Active = model.Active;
                EditUser.EstablishmentFull = model.EstablishmentFull;
                //EditUser.EstablishmentAbbreviation = model.EstablishmentAbbreviation;
                EditUser.Appointment = model.Appointment;
                EditUser.MobNo = model.MobNo;
                EditUser.Rank = model.Rank;
                EditUser.PersonnelNumber = model.Number;
                EditUser.FullName = model.FullName;
                EditUser.PhoneNumber = model.ASCON;
                EditUser.UserTypeId = model.UserTypeId;
                EditUser.Unit_ID = model.Unit_ID;
                EditUser.PasswordHash = model.Password;
                EditUser.UserName = model.Username;
                EditUser.Email = string.Format("{0}@{1}", model.Username.ToLower(), Common.Configurations.Application.MailDomain);
                try
                {
                    var ss = EditUser.Roles.Single();
                    var existsrole = _db.Roles.Where(r => r.Id == ss.RoleId.ToString()).Single();
                    UserManager.RemoveFromRole(EditUser.Id, existsrole.Name);

                    foreach (var role in model.Roles)
                    {
                        UserManager.AddToRole(EditUser.Id, role);

                    }
                }
                catch (Exception)
                {
                    UserManager.AddToRole(EditUser.Id, "User");
                }

                //}
                _db.Entry(EditUser).State = EntityState.Modified;
                _db.SaveChanges();
                //TempData["message"] = "Update Successfully";
                DisplayMessage("User editted successfully", "s", "s");
                return RedirectToAction("Hierarchy");
            }
            Warning("Please correct the errors", true);
            return View(model);
        }


        // GET & POST: Delete current user
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var CurrentUser = _db.Users.FirstOrDefault(i => i.UserName == User.Identity.Name);
            var DeleteUser = _db.Users.FirstOrDefault(i => i.Id == id);
            var AdminRoleId = _db.Roles.FirstOrDefault(n => n.Name == "Administrator");
            foreach (var role in DeleteUser.Roles)
            {
                if (role.RoleId == AdminRoleId.Id) return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Administrator cannot be deleted.");
            }
            if (DeleteUser == null)
                return HttpNotFound("No such user exists. Delete User Failed!");
            if (DeleteUser == CurrentUser) return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "You cannot delete yourself.");
            var model = new DeletetUserViewModel(DeleteUser);
            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var CurrentUser = _db.Users.FirstOrDefault(i => i.UserName == User.Identity.Name);
            var DeleteUser = _db.Users.FirstOrDefault(i => i.Id == id);
            var AdminRoleId = _db.Roles.FirstOrDefault(n => n.Name == "Administrator");
            foreach (var role in DeleteUser.Roles)
            {
                if (role.RoleId == AdminRoleId.Id) return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Administrator cannot be deleted.");
            }
            if (DeleteUser == null)
                return HttpNotFound("No such user exists. Delete User Failed!");
            if (DeleteUser == CurrentUser) return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "You cannot delete yourself.");

            var Superior = DeleteUser.Superior;
            var Subordinates = DeleteUser.Subordinates;
            foreach (var Subordinate in Subordinates)
            {
                Subordinate.Superior = Superior;
            }

            //TODO: Update Hierarchy and Full Hierarchy on account delete and adding superior check reoccurence of _db.SavChanges()

            _db.Users.Remove(DeleteUser);
            _db.SaveChanges();

            return RedirectToAction("Hierarchy");
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (loginWithIAMflag == "true")
            {
                Response.Redirect("https://iam2.army.mil/IAM/User", true);
                return View();
            }
            else
            {
                ViewBag.ReturnUrl = returnUrl;


                if (Session["UserIntId"] != null)
                {

                    AuthenticationManager.SignOut();
                    LogOff();
                }
                LoginViewModel obj = new LoginViewModel();
                return View(obj);
            }
            
        }

        public class Log
        {
            public string NameId { get; set; }
            public string SAMLRole { get; set; }
        }
        [AllowAnonymous]


        public async Task<ActionResult> UserLogin()
        {

            //LoggerNew.LogError("User Registered and redirect to login page. log 3");

            // return Redirect("/Identity/Account/login");
            //String EncryptedResponse = Request.Form["SAMLResponse"];
            String EncryptedResponse = "";


            if (debugWithIAMflag == "true")
            {

                if (ConfigurationManager.AppSettings["LocalHostActive"] == "0")
                {

                    EncryptedResponse = Request.Form["SAMLResponse"];
                    Configuration config = WebConfigurationManager.OpenWebConfiguration("~");
                    if (config.AppSettings.Settings["debugWithIAM"] != null)
                    {
                        config.AppSettings.Settings["debugWithIAM"].Value = "false";
                    }
                    config.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection("appSettings");

                    using (HttpClient client = new HttpClient())
                    {
                        var values = new FormUrlEncodedContent(new[]
                        {
                          new KeyValuePair<string, string>("SAMLResponse", EncryptedResponse)
                        });

                        HttpResponseMessage response = await client.PostAsync("http://localhost:8080/Account/UserLogin", values);

                        string responseString = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Server Response: " + responseString);
                    }
                }
                else if (ConfigurationManager.AppSettings["LocalHostActive"] == "1")
                {
                    EncryptedResponse = Request.Form["SAMLResponse"];
                    Configuration config = WebConfigurationManager.OpenWebConfiguration("~");
                    if (config.AppSettings.Settings["hardSAMLResonoce"] != null)
                    {
                        config.AppSettings.Settings["hardSAMLResonoce"].Value = EncryptedResponse;
                    }
                   
                    if (config.AppSettings.Settings["LocalHostActive"] != null)
                    {
                        config.AppSettings.Settings["LocalHostActive"].Value = "2";
                    }
                    config.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection("appSettings");
                    //stop code directelly from here to debug code with IAM, and Run this on this URL "http://localhost:8080/Account/UserLogin". 
                }
                else
                {
                    EncryptedResponse = ConfigurationManager.AppSettings["hardSAMLResonoce"];
                }


            }
            else
            {
                EncryptedResponse = Request.Form["SAMLResponse"];
            }


            if (!string.IsNullOrEmpty(EncryptedResponse))
            {
                //LoggerNew.LogError("User Registered and redirect to login page. log 4");
                string decryptedsamlresponse = DecryptSAmlResponseNew(EncryptedResponse, "C:\\Cert\\App Certificate\\acms.army.mil.pfx", "Abc@2022");

                AccountSettings accountSettings = new AccountSettings();
                OneLogin.Saml.Response samlResponse = new Response(accountSettings);

                samlResponse.LoadXmlFromBase64(decryptedsamlresponse);

                if (samlResponse.IsValid_sign())
                {
                    Log log = new Log();
                    log.NameId = samlResponse.GetNameID();
                    log.SAMLRole = samlResponse.GetSAMLRole();

                    if (log.NameId != null)
                    {

                        //log.NameId = "dte";
                        Session["NameId"] = log.NameId;
                        Session["SAMLRole"] = log.SAMLRole;
                        LoggerNew.LogError(log.NameId + "444");
                        try
                        {
                            LoggerNew.LogError(_db.Users.FirstOrDefault(i => i.UserName == log.NameId).Active + "447");
                            if (_db.Users.FirstOrDefault(i => i.UserName == log.NameId).Active == "0")
                            {
                                LoggerNew.LogError("User Registered deactive 449");
                                ApplicationUser userdet = new ApplicationUser();
                                Session["UserIntId"] = _db.Users.FirstOrDefault(i => i.UserName == log.NameId).IntId;
                                SessionManager.ArmyNo = _db.Users.FirstOrDefault(i => i.UserName == log.NameId).PersonnelNumber;
                                SessionManager.UserEditId = _db.Users.FirstOrDefault(i => i.UserName == log.NameId).Id;

                                SessionManager.UserName = log.NameId;
                                SessionManager.UserId = Convert.ToString(Session["UserIntId"]);
                               
                                var Unit_ID = _db.Users.FirstOrDefault(i => i.UserName == log.NameId).Unit_ID;
                                SessionManager.Unit_ID = Unit_ID.ToString();
                                var RankId = _db.Users.FirstOrDefault(i => i.UserName == log.NameId).Rank;

                                var UserFullName = _db.Users.FirstOrDefault(i => i.UserName == log.NameId).FullName;
                                SessionManager.UserFullName = UserFullName;
                                IHSDCAA7DBDBContext db = new IHSDCAA7DBDBContext();
                                var UnitName = db.dbo_tbl_Unit.FirstOrDefault(i => i.Unit_ID == Unit_ID).UnitName;
                                SessionManager.UnitName = UnitName;
                                var UnitType = db.dbo_tbl_Unit.Where(x => x.UnitName == UnitName).FirstOrDefault();
                                if (UnitType != null)
                                {
                                    if (UnitType.TypeOfUnit == "INDEPENDENT" || UnitType.TypeOfUnit == "NORMAL")
                                    {
                                        SessionManager.UnitType = UnitType.TypeOfUnit.ToString();

                                    }
                                }
                                GetIpAddress obj = new GetIpAddress();

                                Session["ip"] = obj.getAddress();

                                var user = await UserManager.FindByNameAsync(log.NameId);

                                var RoleName = await UserManager.GetRolesAsync(user.Id.ToString());
                                RoleName = RoleName.Where(val => val != "User").ToArray();
                                SessionManager.Role = RoleName[0].ToString();


                                var ss = user.Roles.Single();
                                
                                SessionManager.RoleId = ss.RoleId.ToString();
                                SessionManager.UserType = Convert.ToString(user.UserTypeId);
                                var existsrole = _db.Roles.Where(r => r.Id == ss.RoleId.ToString()).Single();

                                SessionManager.IsPermission = Convert.ToInt32(existsrole.IsPermission);


                                //var data1 = con.CheckLoggedIn(1, Convert.ToInt16(Session["UserIntId"]), string.Empty, string.Empty);
                                var currentuser = UserManager.FindById(User.Identity.GetUserId());

                                Session["Username"] = log.NameId;
                                //LoggerNew.LogError("User Registered and redirect to login page. log 6 InActive Registerd" + Session["Username"]);
                                con.GetAviatorForDashboard2(2, 0, user.IntId, obj.getAddress(), "");

                                RankCRUD RankModel = new RankCRUD();
                                RankModel.RankId = Convert.ToInt16(RankId);
                                var RankData = con.RankCRUD(2, RankModel);
                                if (RankData.Count > 0)
                                {

                                    SessionManager.RankName = RankData[0].RankName;
                                }

                                ApplicationUserRole Arole = new ApplicationUserRole();
                                LogSignin(log.NameId, _db.Users.FirstOrDefault(i => i.UserName == log.NameId).IntId, Session.SessionID);
                                LogVisit();

                                SessionManager.Active = "0";
                                //LoggerNew.LogError("User Registered and redirect to login page. log 10 InActive Registerd ");
                                //  LoggerNew.LogError("Final Register User login attempt with active Registered User " + SessionManager.Active + " condition for User ");

                                return RedirectToAction("ContactUs", "Home");
                            }
                        }
                        catch { }

                        //log.NameId = "C8TPSUNIT1";
                        var result = await SignInManager.PasswordSignInAsync(log.NameId, "Admin123!", false, shouldLockout: true);

                     //   var result = await SignInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, shouldLockout: true);

                       // LoggerNew.LogError("Start User Registered and redirect to login page. log 5 with result " + result +" User Name " + log.NameId.ToString());
                        switch (result)
                        {
                            case SignInStatus.Success:
                                //if (_db.Users.FirstOrDefault(i => i.UserName == log.NameId).Active == "0")
                                //{
                                //    DisplayMessage("User is Pending for approval from Admin, Contact to Administrator", "", "w");
                                //    return RedirectToAction("Login");
                                //}

                                ApplicationUser userdet = new ApplicationUser();
                                Session["UserIntId"] = _db.Users.FirstOrDefault(i => i.UserName == log.NameId).IntId;
                                SessionManager.ArmyNo = _db.Users.FirstOrDefault(i => i.UserName == log.NameId).PersonnelNumber;
                                SessionManager.UserEditId = _db.Users.FirstOrDefault(i => i.UserName == log.NameId).Id;

                                SessionManager.UserName = log.NameId;
                                SessionManager.UserId = Convert.ToString(Session["UserIntId"]);
                                ////var UnitName = _db.Users.FirstOrDefault(i => i.UserName == log.NameId).EstablishmentFull;
                                ////SessionManager.UnitName = UnitName;
                                var Unit_ID = _db.Users.FirstOrDefault(i => i.UserName == log.NameId).Unit_ID;
                                SessionManager.Unit_ID = Unit_ID.ToString();
                                var RankId = _db.Users.FirstOrDefault(i => i.UserName == log.NameId).Rank;

                                var UserFullName = _db.Users.FirstOrDefault(i => i.UserName == log.NameId).FullName;
                                SessionManager.UserFullName = UserFullName;
                                IHSDCAA7DBDBContext db = new IHSDCAA7DBDBContext();
                                var UnitName = db.dbo_tbl_Unit.FirstOrDefault(i => i.Unit_ID == Unit_ID).UnitName;
                                SessionManager.UnitName = UnitName;
                                var UnitType = db.dbo_tbl_Unit.Where(x => x.UnitName == UnitName).FirstOrDefault();
                                if (UnitType != null)
                                {
                                    if (UnitType.TypeOfUnit == "INDEPENDENT" || UnitType.TypeOfUnit == "NORMAL")
                                    {
                                        SessionManager.UnitType = UnitType.TypeOfUnit.ToString();

                                    }
                                }
                                GetIpAddress obj = new GetIpAddress();

                                Session["ip"] = obj.getAddress();

                                var user = await UserManager.FindByNameAsync(log.NameId);

                                var RoleName = await UserManager.GetRolesAsync(user.Id.ToString());
                                RoleName = RoleName.Where(val => val != "User").ToArray();
                                SessionManager.Role = RoleName[0].ToString();


                                var ss = user.Roles.Single();
                                //// Date 04-07-2023 IAM Role Disable
                                //if (RoleName[0].ToString() == log.SAMLRole)
                                //{
                                SessionManager.RoleId = ss.RoleId.ToString();
                                SessionManager.UserType = Convert.ToString(user.UserTypeId);
                                var existsrole = _db.Roles.Where(r => r.Id == ss.RoleId.ToString()).Single();

                                SessionManager.IsPermission = Convert.ToInt32(existsrole.IsPermission);


                                //var data1 = con.CheckLoggedIn(1, Convert.ToInt16(Session["UserIntId"]), string.Empty, string.Empty);
                                var currentuser = UserManager.FindById(User.Identity.GetUserId());

                                Session["Username"] = log.NameId;
                                //LoggerNew.LogError("User Registered and redirect to login page. log 6" + Session["Username"]);
                                con.GetAviatorForDashboard2(2, 0, user.IntId, obj.getAddress(), "");

                                RankCRUD RankModel = new RankCRUD();
                                RankModel.RankId = Convert.ToInt16(RankId);
                                var RankData = con.RankCRUD(2, RankModel);
                                if (RankData.Count > 0)
                                {
                                    SessionManager.RankName = RankData[0].RankName;
                                }

                                ApplicationUserRole Arole = new ApplicationUserRole();
                                LogSignin(log.NameId, _db.Users.FirstOrDefault(i => i.UserName == log.NameId).IntId, Session.SessionID);
                                LogVisit();
                               // LoggerNew.LogError("User Registered and redirect to login page. log 7" + Session["Username"]);
                                if (SessionManager.RoleId == enum1.Administrator)
                                {
                                    SessionManager.Active = "1";
                                    //LoggerNew.LogError("login attempt with active Administrator " + SessionManager.Active + " condition for User ");
                                    return RedirectToAction("Index", "Home");
                                }
                                else
                                {

                                   // LoggerNew.LogError("User Registered and redirect to login page. log 8" + Session["Username"]);
                                    if (_db.Users.FirstOrDefault(i => i.UserName == log.NameId).Active == "0")
                                    {
                                        //DisplayMessage("User is Pending for approval from Admin, Contact to Administrator", "", "w");
                                        //return RedirectToAction("Login");
                                        SessionManager.Active = "0";
                                       // LoggerNew.LogError("User Registered and redirect to login page. log 9 " + Session["Username"]);
                                      //  LoggerNew.LogError("Final Register User login attempt with active Registered User " + SessionManager.Active + " condition for User ");
                                        return RedirectToAction("ContactUs", "Home");
                                    }

                                    SessionManager.Active = "1";
                                   // LoggerNew.LogError("Register User login attempt with active GSO1 " + SessionManager.Active + " condition for User ");
                                    return RedirectToAction("AddInboxNotingFwd", "ACC");
                                }
                            //}
                            //else
                            //{
                            //    return RedirectToAction("RoleNotAuth", "Account");
                            //}
                            case SignInStatus.Failure:
                                //return RedirectToAction("UnAuthorized", "Account");
                              
                                return RedirectToAction("SignUp", new { User = IHSDC.WebApp.RepositryManager.EncryptionManager.Encryption(log.NameId), DPass = IHSDC.WebApp.RepositryManager.EncryptionManager.Encryption("Admin123!") });

                        }
                       
                        //HttpContext.Session.SetString("NameId", log.NameId);
                        //HttpContext.Session.SetString("SAMLRole", log.SAMLRole);

                        // string nameid = HttpContext.Session.GetString("NameId");

                    }
                    else
                    {

                        Response.Redirect("https://iam2.army.mil/IAM/User", true);
                    }
                }
                else
                {

                    Response.Redirect("https://iam2.army.mil/IAM/User", true);
                }
            }
            else
            {
                Response.Redirect("https://iam2.army.mil/IAM/User", true);
            }
            return RedirectToAction("AddInboxNotingFwd", "ACC");

        }
        [AllowAnonymous]
        public void Logout()
        {

            if (HttpContext.Request.QueryString.Count > 0)
            {
                if (!string.IsNullOrEmpty(Convert.ToString(HttpContext.Request.QueryString["SAMLRequest"])))
                {
                    AccountSettings accountSettings = new AccountSettings();
                    Response samlResponse = new Response(accountSettings);

                    samlResponse.LoadXmlFromBase64(HttpContext.Request.QueryString["SAMLRequest"]);
                    string nameid = string.Empty;
                    string issuer = string.Empty;
                    samlResponse.GetLogoutParameter(out nameid, out issuer);
                    HttpContext.Session.Clear();
                    try
                    {
                        //SendResponseToIAM("https://152.1.21.237/Account/Logout", accountSettings.entityId, nameid);
                        SendResponseToIAM("https://152.1.34.153/Account/Logout", accountSettings.entityId, nameid);
                    }
                    catch (Exception exx)
                    {

                    }
                }
                else if (!string.IsNullOrEmpty(Convert.ToString(HttpContext.Request.QueryString["SAMLResponse"])))
                {
                    HttpContext.Session.Clear();
                    // RedirectToAction("FinalLogout", "Home");
                    //Response.Redirect("https://152.1.21.237/Account/FinalLogout");
                    Response.Redirect("https://acms.army.mil/Account/FinalLogout");
                }
                else
                {
                    AccountSettings acs = new AccountSettings();

                    string NameId = Session["NameID"].ToString();
                    string userRole = Session["SAMLRole"].ToString(); ;
                    Session.Clear();
                    LogoutRequesttoIAM(userRole, acs.entityId, NameId);
                }
            }
            else
            {
                AccountSettings acs = new AccountSettings();
                string NameId = Session["NameID"].ToString();
                string role = Session["SAMLRole"].ToString();



                //await HttpContext.SignOutAsync();
                HttpContext.Session.Clear();
                //HttpContext.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);



                LogoutRequesttoIAM(role, acs.entityId, NameId);
            }
        }
        [AllowAnonymous]
        public ActionResult FinalLogout()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult RoleNotAuth()
        {
            return View();
        }

        public ActionResult UnAuthorized()
        {
            return View();
        }

        [AllowAnonymous]
        public void SendResponseToIAM(String issueurl, string entityid, string usernam)
        {
            AccountSettings accountSettings = new AccountSettings();

            OneLogin.Saml.AuthRequest req = new AuthRequest(new AppSettings(), accountSettings);

            //string ReuestXML = req.GetRequest(AuthRequest.AuthRequestFormat.Base64);
            //string ReuestXML = req.GetLogOutRequest(AuthRequest.AuthRequestFormat.Base64, issueurl, "https://iam2.army.mil/IAM/logout");
            string ReuestXML = req.GetLogOutRequest(AuthRequest.AuthRequestFormat.Base64, issueurl, "https://iam2.army.mil/IAM/logout");

            //Response.Redirect("https://iam2.army.mil/IAM/logout?SAMLResponse=" + ReuestXML);
            Response.Redirect("https://iam2.army.mil/IAM/logout?SAMLResponse=" + ReuestXML);

        }
        [AllowAnonymous]
        public void LogoutRequesttoIAM(String role, string entityid, string usernam)
        {
            AccountSettings accountSettings = new AccountSettings();
            OneLogin.Saml.AuthRequest req = new AuthRequest(new AppSettings(), accountSettings);

            string ReuestXML = req.SingleLogoutRequest(AuthRequest.AuthRequestFormat.Base64, entityid, role, usernam);
            //Response.Redirect("https://iam2.army.mil/IAM/singleAppLogout?SAMLRequest=" + HttpUtility.UrlEncode(ReuestXML), true);
            Response.Redirect("https://iam2.army.mil/IAM/singleAppLogout?SAMLRequest=" + HttpUtility.UrlEncode(ReuestXML), true);
        }
        [AllowAnonymous]
        public string DecryptSAmlResponseNew(string Encryptedtext, string certificatepath, string password)
        {

            string result = "True";
            try
            {
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes("alpha");

                String[] spearator = { Convert.ToBase64String(plainTextBytes) };

                // using the method
                String[] newstring = Encryptedtext.Split(spearator, StringSplitOptions.RemoveEmptyEntries);
                //string[] newstring = encryptedvalue.Split();
                string key = newstring[1].ToString();
                string plain = newstring[0].ToString();
                #region decryptkeyusingprivatekey
                try
                {
                    byte[] byteData = Convert.FromBase64String(key);
                    byte[] decryptedkey = new byte[16];
                    X509Certificate2 myCert2 = null;
                    RSACryptoServiceProvider rsa = null;

                    try
                    {
                        myCert2 = new X509Certificate2(@"C:\\Cert\\App Certificate\\acms.army.mil.pfx", "Abc@2022");
                        // rsa = (RSACryptoServiceProvider)myCert2.PrivateKey;
                        #region test
                        using (RSA rs = myCert2.GetRSAPrivateKey())
                        {
                            // rs.KeySize = 16;
                            decryptedkey = rs.Decrypt(byteData, RSAEncryptionPadding.Pkcs1);

                        }
                        #endregion
                    }
                    catch (Exception e)
                    {

                    }
                    byte[] iv = new byte[16];


                    byte[] iv1 = new byte[16] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };

                    // result = DecryptString0705222_Final(plain, rsa.Decrypt(byteData, RSAEncryptionPadding.Pkcs1), iv1);
                    result = DecryptString0705222_Final(plain, decryptedkey, iv1);
                }
                catch (Exception exxx)
                {
                    result = exxx.Message;
                }
                #endregion

            }
            catch (Exception exx)
            {
                result = exx.Message;
            }

            return result;
        }
        [AllowAnonymous]
        private string DecryptString0705222_Final(string cipherText, byte[] key, byte[] iv)
        {
            // Instantiate a new Aes object to perform string symmetric encryption
            Aes encryptor = Aes.Create();

            encryptor.Mode = CipherMode.ECB;

            // Set key and IV
            byte[] aesKey = new byte[16];
            Array.Copy(key, 0, aesKey, 0, 16);
            encryptor.Key = aesKey;
            encryptor.IV = iv;
            encryptor.Padding = PaddingMode.PKCS7;

            // Instantiate a new MemoryStream object to contain the encrypted bytes
            MemoryStream memoryStream = new MemoryStream();

            // Instantiate a new encryptor from our Aes object
            ICryptoTransform aesDecryptor = encryptor.CreateDecryptor();

            // Instantiate a new CryptoStream object to process the data and write it to the 
            // memory stream
            CryptoStream cryptoStream = new CryptoStream(memoryStream, aesDecryptor, CryptoStreamMode.Write);

            // Will contain decrypted plaintext
            string plainText = String.Empty;

            try
            {
                // Convert the ciphertext string into a byte array
                byte[] cipherBytes = Convert.FromBase64String(cipherText);

                // Decrypt the input ciphertext string
                cryptoStream.Write(cipherBytes, 0, cipherBytes.Length);

                // Complete the decryption process
                cryptoStream.FlushFinalBlock();

                // Convert the decrypted data from a MemoryStream to a byte array
                byte[] plainBytes = memoryStream.ToArray();

                // Convert the decrypted byte array to string
                plainText = Encoding.ASCII.GetString(plainBytes, 0, plainBytes.Length);
            }
            catch (Exception exx)
            {

            }
            finally
            {
                // Close both the MemoryStream and the CryptoStream
                memoryStream.Close();
                cryptoStream.Close();
            }

            // Return the decrypted data as a string
            return plainText;

        }





        //[AllowAnonymous]
        //public ActionResult Login(LoginViewModel returnUrl)
        //{
        //   // ViewBag.ReturnUrl = returnUrl;
        //    return View(returnUrl);
        //}
        private static string URL = "http://localhost:8090/Service1/GetData1";
        private const string DATA = "<?xml version='1.0' encoding='UTF-8'?><letter><title maxlength = '10' > Quote Letter</title>  <salutation limit = '40' > Dear Daniel,</salutation></letter>";
        private static string CreateObject()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.Method = "POST";
            request.ContentType = "application/xml";
            request.ContentLength = DATA.Length;
            StreamWriter requestWriter = new StreamWriter(request.GetRequestStream(), System.Text.Encoding.ASCII);
            requestWriter.Write(DATA);
            requestWriter.Close();

            try
            {
                WebResponse webResponse = request.GetResponse();
                Stream webStream = webResponse.GetResponseStream();
                StreamReader responseReader = new StreamReader(webStream);
                string response = responseReader.ReadToEnd();
                responseReader.Close();
                return response;
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }



        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
           // LoggerNew.LogError("User Registered and redirect to login page. log 2");
            string[] splitval;
            try
            {
                 if (!string.IsNullOrEmpty(returnUrl) && returnUrl != "https://iam2.army.mil/IAM/User"  && returnUrl != null)
               // if (!string.IsNullOrEmpty(returnUrl) && returnUrl != "/" && returnUrl != "/" && returnUrl != null)
                {
                    splitval = returnUrl.Split('^');

                    if (splitval[0] == "APLOG")
                    {
                        model.Username = splitval[1].ToString();
                        model.Password = "Admin123!";
                        model.RememberMe = false;
                        model.IpAddress = "";

                    }

                }
                else
                {
                    if (!ModelState.IsValid)
                    {
                        var errors = ModelState.Select(x => x.Value.Errors)
                                             .Where(y => y.Count > 0)
                                             .ToList();
                        Information("You are required to login in order to continue, please login with credentials provided by your superior HQ.", true);
                        return View(model);
                    }

                }

                URL = "http://" + model.IpAddress + ":8090/Service1/GetData1";
     
                var result = await SignInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, shouldLockout: true);
                switch (result)
                {
                    case SignInStatus.Success:

                        //Login Previlage
                        var user = await UserManager.FindByNameAsync(model.Username);

                        if (user == null)
                        {
                            return RedirectToAction("Login");
                        }

                        // Invalidate previous sessions
                        var result1 = await UserManager.UpdateSecurityStampAsync(user.Id);

                        user = await UserManager.FindByIdAsync(user.Id);

                        // Store NEW stamp
                        Session["SecurityStamp"] = user.SecurityStamp;

                        var identity = await UserManager.CreateIdentityAsync(
                            user,
                            DefaultAuthenticationTypes.ApplicationCookie);

                        AuthenticationManager.SignOut(
                            DefaultAuthenticationTypes.ApplicationCookie);

                        AuthenticationManager.SignIn(
                            new AuthenticationProperties
                            {
                                IsPersistent = model.RememberMe
                            },
                            identity);


                        string GetSalt = AESEncrytDecry.GetSalt();
                        DtoSessions dtoSessions = new DtoSessions();
                        dtoSessions.Salt = GetSalt;
                        SessionHeplers.SetObject(HttpContext.Session, "Token", dtoSessions);
                        Session["UserIntId"] = _db.Users.FirstOrDefault(i => i.UserName == model.Username).IntId;
                        SessionManager.ArmyNo = _db.Users.FirstOrDefault(i => i.UserName == model.Username).PersonnelNumber;
                        SessionManager.UserEditId = _db.Users.FirstOrDefault(i => i.UserName == model.Username).Id;

                        SessionManager.UserName = model.Username;
                        SessionManager.UserId = Convert.ToString(Session["UserIntId"]);

                        var Unit_ID = _db.Users.FirstOrDefault(i => i.UserName == model.Username).Unit_ID;
                        SessionManager.Unit_ID = Unit_ID.ToString();


                        var RankId = _db.Users.FirstOrDefault(i => i.UserName == model.Username).Rank;

                        var UserFullName = _db.Users.FirstOrDefault(i => i.UserName == model.Username).FullName;
                        SessionManager.UserFullName = UserFullName.ToString();

                        IHSDCAA7DBDBContext db = new IHSDCAA7DBDBContext();
                      
                        var UnitName = db.dbo_tbl_Unit.FirstOrDefault(i => i.Unit_ID == Unit_ID).UnitName;
                        SessionManager.UnitName = UnitName;

                        var UnitType = db.dbo_tbl_Unit.Where(x => x.UnitName == UnitName).FirstOrDefault();

                        if (UnitType != null)
                        {
                            if (UnitType.TypeOfUnit == "INDEPENDENT" || UnitType.TypeOfUnit == "NORMAL")
                            {
                                SessionManager.UnitType = UnitType.TypeOfUnit.ToString();

                            }
                        }
                        GetIpAddress obj = new GetIpAddress();

                        Session["ip"] = obj.getAddress();

                        //var user = await UserManager.FindByNameAsync(model.Username);
                        var RoleName = await UserManager.GetRolesAsync(user.Id.ToString());
                        RoleName = RoleName.Where(val => val != "User").ToArray();
                        SessionManager.Role = RoleName[0].ToString();


                        var ss = user.Roles.Single();

                        SessionManager.RoleId = ss.RoleId.ToString();
                        SessionManager.UserType = Convert.ToString(user.UserTypeId);
                        var existsrole = _db.Roles.Where(r => r.Id == ss.RoleId.ToString()).Single();

                        SessionManager.IsPermission = Convert.ToInt32(existsrole.IsPermission);


                        //var data1 = con.CheckLoggedIn(1, Convert.ToInt16(Session["UserIntId"]), string.Empty, string.Empty);
                        var currentuser = UserManager.FindById(User.Identity.GetUserId());

                        Session["Username"] = model.Username;


                        RankCRUD RankModel = new RankCRUD();
                        RankModel.RankId = Convert.ToInt16(RankId);
                        var RankData = con.RankCRUD(2, RankModel);
                        if (RankData.Count > 0)
                        {
                            SessionManager.RankName = RankData[0].RankName;
                        }

                        ApplicationUserRole Arole = new ApplicationUserRole();
                        //LogSignin(model.Username, _db.Users.FirstOrDefault(i => i.UserName == model.Username).IntId, Session.SessionID);
                        LogVisit();
                        // return RedirectToLocal(returnUrl);
                        if (SessionManager.RoleId == enum1.Administrator)
                        {
                            SessionManager.Active = "1";
                           //con.GetAviatorForDashboard2(2, 0, user.IntId, obj.getAddress(), null);
                            return RedirectToLocal(returnUrl);
                        }
                        else
                        {
                            if (user.Active == "0")
                            {

                                SessionManager.Active = "0";
                                return RedirectToAction("ContactUs", "Home");
                            }
                            //if (!string.IsNullOrEmpty(returnUrl) && returnUrl != "https://iam2.army.mil/IAM/User" && returnUrl != null) --for ADN 
                            if (!string.IsNullOrEmpty(returnUrl) && returnUrl != "/"  && returnUrl != null)  //-- for Local
                            {

                                SessionManager.Active = "1";
                                //con.GetAviatorForDashboard2(2, 0, user.IntId, obj.getAddress(), "Admin Switching to User " + model.Username);
                                return Json(1);
                            }
                            else
                            {
                             
                                SessionManager.Active = "1";
                                //con.GetAviatorForDashboard2(2, 0, user.IntId, obj.getAddress(), null);
                                return RedirectToAction("AddInboxNotingFwd", "ACC");
                            }
                        }
                    case SignInStatus.LockedOut:
                        return View("Lockout");
                    case SignInStatus.RequiresVerification:
                        return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                    case SignInStatus.Failure:
                        //return RedirectToAction("UnAuthorized", "Account");
                        return RedirectToAction("SignUp", new { User = IHSDC.WebApp.RepositryManager.EncryptionManager.Encryption(model.Username), DPass = IHSDC.WebApp.RepositryManager.EncryptionManager.Encryption("Admin123!") });
                    default:
                        Danger("Invalid login attempt.", true);
                        ModelState.AddModelError("", "Invalid login attempt.");
                        return View(model);
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
                    return Redirect("/Error/Error");
                }
                return Redirect("/Error/Error");
            }
        }

        #region message
        public void DisplayMessage(string message, string midMsg, string messageStatus_s_e_w_i_Or_blank)
        {
            string status = messageStatus_s_e_w_i_Or_blank.ToLower();
            //ViewBag.msg = message;
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

        [Authorize]
        public ActionResult Add()
        {
            try
            {
                if (Session["UserIntId"] == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                RegisterViewModel model = new RegisterViewModel();
                IHSDCAA7DBDBContext db = new IHSDCAA7DBDBContext();
                var AllRoles = (new ApplicationDbContext()).Roles.OrderBy(r => r.IsPermission).Where(n => n.Name != "Administrator").ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
                ViewBag.Roles = AllRoles;
                ViewData["Roles"] = AllRoles;
                ViewData["Unit_ID"] = new SelectList(db.dbo_tbl_Unit, "Unit_ID", "UnitName");

                ViewData["EstablishmentFull"] = new SelectList(db.dbo_tbl_Unit, "UnitName", "UnitName");

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
                    return Redirect("/Error/Error");
                }
                return Redirect("/Error/Error");
            }
        }



        public ActionResult SignUp(string User, string DPass)
        {
            string ModelUserName = "";
            string DefaultPass = "";
            try
            {
                if (User != string.Empty && User != null)
                {
                    ModelUserName = RepositryManager.EncryptionManager.Decryption(User);
                }

                if (DPass != string.Empty && DPass != null)
                {
                    DefaultPass = RepositryManager.EncryptionManager.Decryption(DPass);
                }

                RegisterViewModel model = new RegisterViewModel();
                model.Username = ModelUserName;
                model.Password = DefaultPass;
                model.ConfirmPassword = DefaultPass;

                IHSDCAA7DBDBContext db = new IHSDCAA7DBDBContext();
                var AllRoles = (new ApplicationDbContext()).Roles.OrderBy(r => r.IsPermission).Where(n => n.Name != "Administrator").ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
                ViewBag.Roles = AllRoles;
                ViewData["Roles"] = AllRoles;
                ViewData["Unit_ID"] = new SelectList(db.dbo_tbl_Unit, "Unit_ID", "UnitName");

                ViewData["EstablishmentFull"] = new SelectList(db.dbo_tbl_Unit, "UnitName", "UnitName");


                UnitCRUD model1 = new UnitCRUD();
                model1.Unit_ID = 0;
                var data = con.UnitCRUD(2, model1);
                ViewBag.ILUnitCRUD = data;

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
                    return Redirect("/Error/Error");
                }
                return Redirect("/Error/Error");
            }
        }


        [HttpGet]
        public ActionResult AddNewUnitPopup(string id)
        {
            try
            {
                //ViewBag.ButtonName = "Add";
                //ApptCRUD model = new ApptCRUD();
                //if (id != null && id != string.Empty)
                //{
                //    ViewBag.ButtonName = "Update";
                //    int item = Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(id));
                //    model.ApptId = item;
                //    var editdata = con.ApptCRUD(2, model);
                //    model = editdata[0];
                //}
                //model.ApptId = 0;
                //var data = con.ApptCRUD(2, model);
                //model.ILApptCRUD = data;
                //ViewBag.count = model.ILApptCRUD.Count();
                //return View(model);
                // List<SearchData> DataList = new List<SearchData>();

                UnitCRUD model = new UnitCRUD();
                model.Unit_ID = 0;
                var data = con.UnitCRUD(2, model);
                //   ViewBag.ILUnitCRUD = data;

                return View(data);
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

        // POST: /Account/Add
        [HttpPost]
        [Authorize]
        // [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {



                    if (model.Active == null)
                    {
                        model.Active = "0";
                    }

                    var CurrentUser = UserManager.FindById(User.Identity.GetUserId());
                    if (CurrentUser == null)
                    {
                        UnitCRUD UnitCR = new UnitCRUD();
                        var CurrentUser1 = con.UnitCRUD(13, UnitCR);
                        string UserID = CurrentUser1[0].id;

                        var CurrentUser2 = UserManager.FindById(UserID);

                        model.CreatedByIntId = 1;
                        model.Superior = CurrentUser2;
                    }
                    else
                    {

                        model.CreatedByIntId = 1;
                        model.Superior = CurrentUser;
                    }

                    var NewUser = new Common.Models.ApplicationUser
                    {
                        UserName = model.Username.ToLower(),
                        Email = string.Format("{0}@{1}", model.Username.ToLower(), Common.Configurations.Application.MailDomain),
                        EmailConfirmed = true,
                        Rank = model.Rank,
                        PersonnelNumber = model.Number,
                        FullName = model.FullName,
                        DEFwdAuth = model.DEFwdAuth,
                        GEBFwdAuth = model.GEBFwdAuth,
                        Active = model.Active,
                        EstablishmentFull = model.EstablishmentFull,
                        EstablishmentAbbreviation = model.EstablishmentAbbreviation,
                        CreatedByIntId = 1,
                        CreatedOn = DateTime.Now,
                        Superior = model.Superior,
                        PhoneNumber = model.ASCON,
                        Unit_ID = model.Unit_ID,
                        UserTypeId = model.UserTypeId,
                        ParentId = model.ParentId,
                        Appointment = model.Appointment,
                        MobNo = model.MobNo
                    };

                    var result = await UserManager.CreateAsync(NewUser, model.Password);
                    if (result.Succeeded)
                    {

                        try
                        {
                            foreach (var role in model.Roles)
                            {
                                UserManager.AddToRole(NewUser.Id, role);
                                // UserManager.AddToRole(NewUser.Id, "User");
                            }
                        }
                        catch (Exception)
                        {
                            UserManager.AddToRole(NewUser.Id, "User");
                        }


                        using (var db = new ApplicationDbContext())
                        {
                            db.Hierarchy.Add(new Hierarchy
                            {
                                UserId = NewUser.Superior.IntId,
                                ChildId = NewUser.IntId
                            });
                            db.SaveChanges();
                        }

                        Common.Helpers.Identity.Hierarchy.UpdateHierarchyTable();

                        if (model.Active == "0")
                        {
                           //DisplayMessage("User Created Successfully, Pending for Admin Approval !", "", "s");
                          // LoggerNew.LogError("User Registered and redirect to login page. log 1");


                            SessionManager.Active = "0";
                           // LoggerNew.LogError("Register User login attempt with active " + SessionManager.Active + " condition for User " + model.Username);
                            return RedirectToAction("ContactUs", "Home");
                       
                            //return RedirectToAction("Login");



                            //  return RedirectToAction("ContactUs", "Home");

                        }
                        else
                        {
                            DisplayMessage("User Created Successfully !", "", "s");
                            return RedirectToAction("Hierarchy", "Account");
                        }
                    }
                    AddErrors(result);
                }
                catch (Exception)
                {
                    var AllRoles = (new ApplicationDbContext()).Roles.OrderBy(r => r.Name).Where(n => n.Name != "Administrator").ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
                    ViewBag.Roles = AllRoles;
                    ViewData["Roles"] = AllRoles;

                    IHSDCAA7DBDBContext db1 = new IHSDCAA7DBDBContext();
                    ViewData["EstablishmentFull"] = new SelectList(db1.dbo_tbl_Unit, "UnitName", "UnitName");
                    ViewData["Unit_ID"] = new SelectList(db1.dbo_tbl_Unit, "Unit_ID", "UnitName");
                }

            }
            else
            {

                var AllRoles = (new ApplicationDbContext()).Roles.OrderBy(r => r.Name).Where(n => n.Name != "Administrator" && n.Name != "User").ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
                ViewBag.Roles = AllRoles;
                ViewData["Roles"] = AllRoles;
                IHSDCAA7DBDBContext db1 = new IHSDCAA7DBDBContext();
                ViewData["EstablishmentFull"] = new SelectList(db1.dbo_tbl_Unit, "UnitName", "UnitName");
                ViewData["Unit_ID"] = new SelectList(db1.dbo_tbl_Unit, "Unit_ID", "UnitName");

            }

            var AllRoles1 = (new ApplicationDbContext()).Roles.OrderBy(r => r.Name).Where(n => n.Name != "Administrator" && n.Name != "User").ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = AllRoles1;
            ViewData["Roles"] = AllRoles1;
            IHSDCAA7DBDBContext db2 = new IHSDCAA7DBDBContext();
            ViewData["EstablishmentFull"] = new SelectList(db2.dbo_tbl_Unit, "UnitName", "UnitName");
            ViewData["Unit_ID"] = new SelectList(db2.dbo_tbl_Unit, "Unit_ID", "UnitName");
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // Add Superior
        [Authorize(Roles = "Administrator")]
        public ActionResult AddSuperior()
        {
            var AllSuperiorLessUsers = (new ApplicationDbContext())
                .Users
                .Where(s => s.Superior == null)
                .ToList()
                .Select(
                rr => new SelectListItem { Value = rr.Id, Text = rr.UserName }).ToList();
            ViewBag.SuperiorLessUsers = AllSuperiorLessUsers;
            var AllRoles = (new ApplicationDbContext()).Roles.OrderBy(r => r.Name).Where(n => n.Name != "Administrator" && n.Name != "NoSubordinates").ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = AllRoles;
            return View();
        }


        //TODO: Add superior & update subordinates
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddSuperior(AddSuperiorViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var NewUser = new Common.Models.ApplicationUser
                    {
                        UserName = model.Username.ToLower(),
                        Email = string.Format("{0}@{1}", model.Username.ToLower(), Common.Configurations.Application.MailDomain),
                        EmailConfirmed = true,
                        Rank = model.Rank,
                        PersonnelNumber = model.Number,
                        FullName = model.FullName,
                        DEFwdAuth = model.DEFwdAuth,
                        GEBFwdAuth = model.GEBFwdAuth,
                        Active = model.Active,
                        EstablishmentFull = model.EstablishmentFull,
                        EstablishmentAbbreviation = model.EstablishmentAbbreviation,
                        CreatedByIntId = UserManager.FindById(User.Identity.GetUserId()).IntId,
                        CreatedOn = DateTime.Now,
                        PhoneNumber = model.ASCON,
                        MobNo = model.MobNo
                    };

                    var result = await UserManager.CreateAsync(NewUser, model.Password);
                    if (result.Succeeded)
                    {
                        try
                        {
                            foreach (var role in model.Roles)
                            {
                                UserManager.AddToRole(NewUser.Id, role);
                                UserManager.AddToRole(NewUser.Id, "User");
                            }
                        }
                        catch (Exception)
                        {
                            UserManager.AddToRole(NewUser.Id, "User");
                        }

                        var SubordinateUser = UserManager.FindById(model.SubordinateId);
                        var EditUser = _db.Users.FirstOrDefault(i => i.UserName == NewUser.UserName);

                        if (SubordinateUser != null)
                        {
                            _db.Hierarchy.Add(new Hierarchy
                            {
                                UserId = NewUser.Superior.IntId,
                                ChildId = SubordinateUser.IntId
                            });
                            SubordinateUser.Superior = EditUser;
                        }
                        _db.Entry(SubordinateUser).State = EntityState.Modified;
                        _db.Entry(EditUser).State = EntityState.Modified;
                        _db.SaveChanges();

                        Success(string.Format("<strong>{0}</b> as superior to <strong>{1}</strong> was successfully added.", NewUser.UserName, SubordinateUser.UserName), true);
                        return RedirectToAction("Hierarchy", "Account");
                    }
                    AddErrors(result);
                }
                else
                {
                    {
                        var AllSuperiorLessUsers = (new ApplicationDbContext())
                    .Users
                    .Where(s => s.Superior == null)
                    .ToList()
                    .Select(
                    rr => new SelectListItem { Value = rr.Id, Text = rr.UserName }).ToList();
                        ViewBag.SuperiorLessUsers = AllSuperiorLessUsers;
                        var AllRoles = (new ApplicationDbContext()).Roles.OrderBy(r => r.Name).Where(n => n.Name != "Administrator" && n.Name != "NoSubordinates").ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
                        ViewBag.Roles = AllRoles;
                    }
                }
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
                    return Redirect("/Error/Error");
                }
                return Redirect("/Error/Error");
            }
        }

        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user != null)
                {
                    ForgotPasswordViewModel obj = new ForgotPasswordViewModel();
                    obj.superiorId = user.SuperiorId;
                    obj.UserName = model.Email;
                    obj.UserId = user.IntId;
                    var result = con.SendForgot(obj);
                    if (result > 0)
                    {
                        DisplayMessage("Your forgot password request send successfully", "", "s");
                        return RedirectToAction("Login", "Account");
                    }

                }
                else
                {
                    DisplayMessage("user id/ user name is not exist", "", "w");
                }

            }
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {

            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        [HttpGet]
        public ActionResult ResetPassword()
        {
            ForgotPasswordViewModel model = new ForgotPasswordViewModel();
            model.ilForgotPasswordViewModel = con.GetListResetPassword();
            ViewBag.count = model.ilForgotPasswordViewModel.Count();
            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Reset(string id, string fid)
        {
            String newPassword = "Admin";
            String hashedNewPassword = UserManager.PasswordHasher.HashPassword(newPassword);

            int updateId = con.UpdatePassword(Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(id)), hashedNewPassword);

            if (updateId > 0)
            {
                int appid = con.UpdateForgotPasswordTable(Convert.ToInt32(RepositryManager.EncryptionManager.Decryption(fid)));

                if (appid > 0)
                {

                    DisplayMessage("Password Reset Successfully", "", "s");
                }
            }

            return RedirectToAction("ResetPassword", "Account");
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult LocalLogout()
        {
            System.Web.Security.FormsAuthentication.SignOut();
            Session.Abandon();
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            using (var db = new ApplicationDbContext())
            {
                var CurrentVisit = db.Visits.FirstOrDefault(s => s.SessionId == Session.SessionID.ToString());
                if (CurrentVisit != null)
                {
                    CurrentVisit.End = DateTime.Now;
                    db.Entry(CurrentVisit).State = EntityState.Modified;
                }
                var CurrentLogin = db.Logins.FirstOrDefault(u => u.Username == User.Identity.Name && u.SessionId == Session.SessionID);
                if (CurrentLogin != null)
                {
                    CurrentLogin.LoggedOutAt = DateTime.Now;
                    CurrentLogin.IsLoggedIn = false;
                    Session.Abandon();
                    HttpContext.Request.Cookies.Clear();

                    db.Entry(CurrentLogin).State = EntityState.Modified;
                    foreach (var l in db.Logins.Where(u => u.Username == User.Identity.Name))
                    {
                        db.Entry(l).State = EntityState.Modified;
                    }
                }
                db.SaveChanges();
            }
            HttpContext.Session.Remove("Token");
            // Logout Forms Authentication
            FormsAuthentication.SignOut();

            // Logout OWIN Authentication
            AuthenticationManager.SignOut(
                DefaultAuthenticationTypes.ApplicationCookie
            );
           
            // Clear Session
            Session.Clear();
            Session.Abandon();

            // Delete all cookies
            foreach (string cookieName in Request.Cookies.AllKeys)
            {
                if (!string.IsNullOrEmpty(cookieName))
                {
                    Response.Cookies.Add(new HttpCookie(cookieName)
                    {
                        Value = "",
                        Expires = DateTime.Now.AddDays(-1),
                        HttpOnly = true
                    });
                }
            }
            return RedirectToAction("Index", "Home");
        }


        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            using (var db = new ApplicationDbContext())
            {
                var CurrentVisit = db.Visits.FirstOrDefault(s => s.SessionId == Session.SessionID.ToString());
                if (CurrentVisit != null)
                {
                    CurrentVisit.End = DateTime.Now;
                    db.Entry(CurrentVisit).State = EntityState.Modified;
                }
                var CurrentLogin = db.Logins.FirstOrDefault(u => u.Username == User.Identity.Name && u.SessionId == Session.SessionID);
                if (CurrentLogin != null)
                {
                    CurrentLogin.LoggedOutAt = DateTime.Now;
                    CurrentLogin.IsLoggedIn = false;
                    Session.Abandon();
                    HttpContext.Request.Cookies.Clear();

                    db.Entry(CurrentLogin).State = EntityState.Modified;
                    foreach (var l in db.Logins.Where(u => u.Username == User.Identity.Name))
                    {
                        db.Entry(l).State = EntityState.Modified;
                    }
                }
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }



        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

    

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion


        [HttpPost]
        public JsonResult GetUsername(string UnitName)
        {

            return Json((new IHSDC.WebApp.Models.MasterModels()).LoadUserList(UnitName));

        }

    }

    internal class LoggerNew
    {
        private static readonly string LogFilePath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/Uploads/").ToString(), "ErrorLog.txt");

        public static void LogError(string message)
        {
            using (var writer = new StreamWriter(LogFilePath, true))
            {
                writer.WriteLine($"{DateTime.Now}: {message}");
            }
        }
    }
}