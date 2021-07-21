using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace G1_Website_Tour.Areas.Admin.Controllers
{
    public class AdminHomeController : Controller
    {
        // GET: Admin/AdminHome
        public ActionResult Index()
        {
            if(Session["admin"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "AdminLogin");
            }
        }
    }
}