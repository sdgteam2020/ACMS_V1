using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using PagedList;
using PagedList.Mvc;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using AA7.Models;
using System.Data.SqlClient;
using System.Text;
using IHSDC.WebApp.Helper;
using IHSDC.WebApp.Connection;

namespace AA7.Controllers
{
    public class dbo_FullHierarchyController : Controller
    {
        readonly SearchDBConnection con = new SearchDBConnection();
        private IHSDCAA7DBDBContext db = new IHSDCAA7DBDBContext();
        

        // GET: /dbo_FullHierarchy/
        public ActionResult Index(string sortOrder,  
                                  String SearchField,
                                  String SearchCondition,
                                  String SearchText,
                                  String Export,
                                  int? PageSize,
                                  int? page, 
                                  string command)
        {
            try
            {

                if (command == "Show All")
                {
                    SearchField = null;
                    SearchCondition = null;
                    SearchText = null;
                    Session["SearchField"] = null;
                    Session["SearchCondition"] = null;
                    Session["SearchText"] = null;
                }
                else if (command == "Add New Record") { return RedirectToAction("Create"); }
                else if (command == "Export") { Session["Export"] = Export; }
                else if (command == "Search" | command == "Page Size")
                {
                    if (!string.IsNullOrEmpty(SearchText))
                    {
                        Session["SearchField"] = SearchField;
                        Session["SearchCondition"] = SearchCondition;
                        Session["SearchText"] = SearchText;
                    }
                }
                if (command == "Page Size") { Session["PageSize"] = PageSize; }

                ViewData["SearchFields"] = GetFields((Session["SearchField"] == null ? "I D" : Convert.ToString(Session["SearchField"])));
                ViewData["SearchConditions"] = Library.GetConditions((Session["SearchCondition"] == null ? "Contains" : Convert.ToString(Session["SearchCondition"])));
                ViewData["SearchText"] = Session["SearchText"];
                ViewData["Exports"] = Library.GetExports((Session["Export"] == null ? "Pdf" : Convert.ToString(Session["Export"])));
                ViewData["PageSizes"] = Library.GetPageSizes();

                ViewData["CurrentSort"] = sortOrder;
                ViewData["IDSortParm"] = sortOrder == "ID_asc" ? "ID_desc" : "ID_asc";
                ViewData["UserIdSortParm"] = sortOrder == "UserId_asc" ? "UserId_desc" : "UserId_asc";
                ViewData["ChildIdSortParm"] = sortOrder == "ChildId_asc" ? "ChildId_desc" : "ChildId_asc";
                ViewData["UserNameSortParm"] = sortOrder == "UserName_asc" ? "UserName_desc" : "UserName_asc";
                ViewData["RoleNameSortParm"] = sortOrder == "RoleName_asc" ? "RoleName_desc" : "RoleName_asc";

                var Query = db.dbo_FullHierarchy.AsQueryable();

                try
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(Session["SearchField"])) && !string.IsNullOrEmpty(Convert.ToString(Session["SearchCondition"])) && !string.IsNullOrEmpty(Convert.ToString(Session["SearchText"])))
                    {
                        SearchField = Convert.ToString(Session["SearchField"]);
                        SearchCondition = Convert.ToString(Session["SearchCondition"]);
                        SearchText = Convert.ToString(Session["SearchText"]);

                        if (SearchCondition == "Contains")
                        {
                            Query = Query.Where(p =>
                                                     ("I D".ToString().Equals(SearchField) && p.ID.Value.ToString().Contains(SearchText))
                                                     || ("User Id".ToString().Equals(SearchField) && p.UserId.ToString().Trim().ToLower().Contains(SearchText.Trim().ToLower()))
                                                     || ("Child Id".ToString().Equals(SearchField) && p.ChildId.ToString().Trim().ToLower().Contains(SearchText.Trim().ToLower()))
                                                     || ("User Name".ToString().Equals(SearchField) && p.UserName.ToString().Trim().ToLower().Contains(SearchText.Trim().ToLower()))
                                                     || ("Role Name".ToString().Equals(SearchField) && p.RoleName.ToString().Trim().ToLower().Contains(SearchText.Trim().ToLower()))
                                             );
                        }
                        else if (SearchCondition == "Starts with...")
                        {
                            Query = Query.Where(p =>
                                                     ("I D".ToString().Equals(SearchField) && p.ID.Value.ToString().StartsWith(SearchText))
                                                     || ("User Id".ToString().Equals(SearchField) && p.UserId.ToString().Trim().ToLower().StartsWith(SearchText.Trim().ToLower()))
                                                     || ("Child Id".ToString().Equals(SearchField) && p.ChildId.ToString().Trim().ToLower().StartsWith(SearchText.Trim().ToLower()))
                                                     || ("User Name".ToString().Equals(SearchField) && p.UserName.ToString().Trim().ToLower().StartsWith(SearchText.Trim().ToLower()))
                                                     || ("Role Name".ToString().Equals(SearchField) && p.RoleName.ToString().Trim().ToLower().StartsWith(SearchText.Trim().ToLower()))
                                             );
                        }
                        else if (SearchCondition == "Equals")
                        {
                            if ("I D".Equals(SearchField)) { var mID = System.Convert.ToInt32(SearchText); Query = Query.Where(p => p.ID == mID); }
                            else if ("User Id".Equals(SearchField)) { var mUserId = System.Convert.ToInt32(SearchText); Query = Query.Where(p => p.UserId == mUserId); }
                            else if ("Child Id".Equals(SearchField)) { var mChildId = System.Convert.ToInt32(SearchText); Query = Query.Where(p => p.ChildId == mChildId); }
                            else if ("User Name".Equals(SearchField)) { var mUserName = System.Convert.ToString(SearchText); Query = Query.Where(p => p.UserName == mUserName); }
                            else if ("Role Name".Equals(SearchField)) { var mRoleName = System.Convert.ToString(SearchText); Query = Query.Where(p => p.RoleName == mRoleName); }
                        }
                        else if (SearchCondition == "More than...")
                        {
                            if (SearchField.Equals(SearchCondition)) { }
                            else if ("I D".Equals(SearchField)) { var mID = System.Convert.ToInt32(SearchText); Query = Query.Where(p => p.ID > mID); }
                            else if ("User Id".Equals(SearchField)) { var mUserId = System.Convert.ToInt32(SearchText); Query = Query.Where(p => p.UserId > mUserId); }
                            else if ("Child Id".Equals(SearchField)) { var mChildId = System.Convert.ToInt32(SearchText); Query = Query.Where(p => p.ChildId > mChildId); }
                        }
                        else if (SearchCondition == "Less than...")
                        {
                            if (SearchField.Equals(SearchCondition)) { }
                            else if ("I D".Equals(SearchField)) { var mID = System.Convert.ToInt32(SearchText); Query = Query.Where(p => p.ID < mID); }
                            else if ("User Id".Equals(SearchField)) { var mUserId = System.Convert.ToInt32(SearchText); Query = Query.Where(p => p.UserId < mUserId); }
                            else if ("Child Id".Equals(SearchField)) { var mChildId = System.Convert.ToInt32(SearchText); Query = Query.Where(p => p.ChildId < mChildId); }
                        }
                        else if (SearchCondition == "Equal or more than...")
                        {
                            if (SearchField.Equals(SearchCondition)) { }
                            else if ("I D".Equals(SearchField)) { var mID = System.Convert.ToInt32(SearchText); Query = Query.Where(p => p.ID >= mID); }
                            else if ("User Id".Equals(SearchField)) { var mUserId = System.Convert.ToInt32(SearchText); Query = Query.Where(p => p.UserId >= mUserId); }
                            else if ("Child Id".Equals(SearchField)) { var mChildId = System.Convert.ToInt32(SearchText); Query = Query.Where(p => p.ChildId >= mChildId); }
                        }
                        else if (SearchCondition == "Equal or less than...")
                        {
                            if (SearchField.Equals(SearchCondition)) { }
                            else if ("I D".Equals(SearchField)) { var mID = System.Convert.ToInt32(SearchText); Query = Query.Where(p => p.ID <= mID); }
                            else if ("User Id".Equals(SearchField)) { var mUserId = System.Convert.ToInt32(SearchText); Query = Query.Where(p => p.UserId <= mUserId); }
                            else if ("Child Id".Equals(SearchField)) { var mChildId = System.Convert.ToInt32(SearchText); Query = Query.Where(p => p.ChildId <= mChildId); }
                        }
                    }
                }
                catch (Exception) { }

                switch (sortOrder)
                {
                    case "ID_desc":
                        Query = Query.OrderByDescending(s => s.ID);
                        break;
                    case "ID_asc":
                        Query = Query.OrderBy(s => s.ID);
                        break;
                    case "UserId_desc":
                        Query = Query.OrderByDescending(s => s.UserId);
                        break;
                    case "UserId_asc":
                        Query = Query.OrderBy(s => s.UserId);
                        break;
                    case "ChildId_desc":
                        Query = Query.OrderByDescending(s => s.ChildId);
                        break;
                    case "ChildId_asc":
                        Query = Query.OrderBy(s => s.ChildId);
                        break;
                    case "UserName_desc":
                        Query = Query.OrderByDescending(s => s.UserName);
                        break;
                    case "UserName_asc":
                        Query = Query.OrderBy(s => s.UserName);
                        break;
                    case "RoleName_desc":
                        Query = Query.OrderByDescending(s => s.RoleName);
                        break;
                    case "RoleName_asc":
                        Query = Query.OrderBy(s => s.RoleName);
                        break;
                    default:  // Name ascending 
                        Query = Query.OrderBy(s => s.ID);
                        break;
                }

                if (command == "Export")
                {
                    GridView gv = new GridView();
                    DataTable dt = new DataTable();
                    dt.Columns.Add("I D", typeof(string));
                    dt.Columns.Add("User Id", typeof(string));
                    dt.Columns.Add("Child Id", typeof(string));
                    dt.Columns.Add("User Name", typeof(string));
                    dt.Columns.Add("Role Name", typeof(string));
                    foreach (var item in Query.ToList())
                    {
                        dt.Rows.Add(
                            item.ID
                           , item.UserId
                           , item.ChildId
                           , item.UserName
                           , item.RoleName
                        );
                    }
                    gv.DataSource = dt;
                    gv.DataBind();
                    //ExportData(Export, gv, dt);
                }

                int pageNumber = (page ?? 1);
                int? pageSZ = (Convert.ToInt32(Session["PageSize"]) == 0 ? 5 : Convert.ToInt32(Session["PageSize"]));
                return View(Query.ToPagedList(pageNumber, (pageSZ ?? 5)));
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

        // GET: /dbo_FullHierarchy/Details/<id>
        public ActionResult Details(
                                      Int64? ID
                                   )
        {
            if (
                    ID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            dbo_FullHierarchy dbo_FullHierarchy = db.dbo_FullHierarchy.Find(
                                                 ID
                                            );
            if (dbo_FullHierarchy == null)
            {
                return HttpNotFound();
            }
            return View(dbo_FullHierarchy);
        }

        // GET: /dbo_FullHierarchy/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: /dbo_FullHierarchy/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include=
				           "ID"
				   + "," + "UserId"
				   + "," + "ChildId"
				   + "," + "UserName"
				   + "," + "RoleName"
				  )] dbo_FullHierarchy dbo_FullHierarchy)
        {
            if (ModelState.IsValid)
            {
                db.dbo_FullHierarchy.Add(dbo_FullHierarchy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dbo_FullHierarchy);
        }

        // GET: /dbo_FullHierarchy/Edit/<id>
        public ActionResult Edit(
                                   Int64? ID
                                )
        {
            if (
                    ID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            dbo_FullHierarchy dbo_FullHierarchy = db.dbo_FullHierarchy.Find(
                                                 ID
                                            );
            if (dbo_FullHierarchy == null)
            {
                return HttpNotFound();
            }

            return View(dbo_FullHierarchy);
        }

        // POST: /dbo_FullHierarchy/Edit/<id>
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(dbo_FullHierarchy dbo_FullHierarchy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dbo_FullHierarchy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dbo_FullHierarchy);
        }

        // GET: /dbo_FullHierarchy/Delete/<id>
        public ActionResult Delete(
                                     Int64? ID
                                  )
        {
            if (
                    ID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            dbo_FullHierarchy dbo_FullHierarchy = db.dbo_FullHierarchy.Find(
                                                 ID
                                            );
            if (dbo_FullHierarchy == null)
            {
                return HttpNotFound();
            }
            return View(dbo_FullHierarchy);
        }

        // POST: /dbo_FullHierarchy/Delete/<id>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(
                                            Int64? ID
                                            )
        {
            dbo_FullHierarchy dbo_FullHierarchy = db.dbo_FullHierarchy.Find(
                                                 ID
                                            );
            db.dbo_FullHierarchy.Remove(dbo_FullHierarchy);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private static List<SelectListItem> GetFields(String select)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            SelectListItem Item1 = new SelectListItem { Text = "I D", Value = "I D" };
            SelectListItem Item2 = new SelectListItem { Text = "User Id", Value = "User Id" };
            SelectListItem Item3 = new SelectListItem { Text = "Child Id", Value = "Child Id" };
            SelectListItem Item4 = new SelectListItem { Text = "User Name", Value = "User Name" };
            SelectListItem Item5 = new SelectListItem { Text = "Role Name", Value = "Role Name" };

                 if (select == "I D") { Item1.Selected = true; }
            else if (select == "User Id") { Item2.Selected = true; }
            else if (select == "Child Id") { Item3.Selected = true; }
            else if (select == "User Name") { Item4.Selected = true; }
            else if (select == "Role Name") { Item5.Selected = true; }

            list.Add(Item1);
            list.Add(Item2);
            list.Add(Item3);
            list.Add(Item4);
            list.Add(Item5);

            return list.ToList();
        }

        //private void ExportData(String Export, GridView gv, DataTable dt)
        //{
        //    if (Export == "Pdf")
        //    {
        //        PDFform pdfForm = new PDFform(dt, "Dbo. Full Hierarchy", "Many");
        //        Document document = pdfForm.CreateDocument();
        //        PdfDocumentRenderer renderer = new PdfDocumentRenderer(true);
        //        renderer.Document = document;
        //        renderer.RenderDocument();

        //        MemoryStream stream = new MemoryStream();
        //        renderer.PdfDocument.Save(stream, false);

        //        Response.Clear();
        //        Response.AddHeader("content-disposition", "attachment;filename=" + "Report.pdf");
        //        Response.ContentType = "application/Pdf.pdf";
        //        Response.BinaryWrite(stream.ToArray());
        //        Response.Flush();
        //        Response.End();
        //    }
        //    else
        //    {
        //        Response.ClearContent();
        //        Response.Buffer = true;
        //        if (Export == "Excel")
        //        {
        //            Response.AddHeader("content-disposition", "attachment;filename=" + "Report.xls");
        //            Response.ContentType = "application/Excel.xls";
        //        }
        //        else if (Export == "Word")
        //        {
        //            Response.AddHeader("content-disposition", "attachment;filename=" + "Report.doc");
        //            Response.ContentType = "application/Word.doc";
        //        }
        //        Response.Charset = "";
        //        StringWriter sw = new StringWriter();
        //        HtmlTextWriter htw = new HtmlTextWriter(sw);
        //        gv.RenderControl(htw);
        //        Response.Output.Write(sw.ToString());
        //        Response.Flush();
        //        Response.End();
        //    }
        //}

    }
}
 
