using IHSDC.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IHSDC.WebApp.Controllers
{
    public class ErrorController : Controller
    {
        readonly Connection.DBConnection con = new Connection.DBConnection();
        // GET: Error
        public ActionResult Error()
        {
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
            ViewBag.CurrentTime = DateTime.Now;
            return View("Error");
        }
    }
}