using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G1_Website_Tour.Models;

namespace G1_Website_Tour.Areas.Admin.Controllers
{
    public class AdminLoginController : Controller
    {
        public TourDB_Entities db = new TourDB_Entities();   
        // GET: Admin/AdminLogin
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string username, string password)
        {
            string pass = Encode.EncodeMD5(password);
            var result = db.Staffs.Where(s => s.username.Equals(username) && s.password.Equals(pass)).FirstOrDefault();
            if (result != null)
            {
                Session["admin"] = result.staffid;
                return RedirectToAction("Index", "AdminHome");
            }
            else
            {
                return View();
            }
        }
    }
}