using IHSDC.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IHSDC.WebApp.Controllers
{
    public class CounterController : Controller
    {
        public JsonResult GetHitCounter()
        {
            HitCRUD model = new HitCRUD();
            Connection.DBConnection con1 = new Connection.DBConnection();
            var ss = con1.HitCRUD(17, model);
            return Json(ss, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetHitCounterD()
        {
            HitCRUD model = new HitCRUD();
            Connection.DBConnection con1 = new Connection.DBConnection();
            var ss = con1.HitCRUD(16, model);
            return Json(ss, JsonRequestBehavior.AllowGet);
        }
    }
}