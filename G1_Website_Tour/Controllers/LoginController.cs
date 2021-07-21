using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G1_Website_Tour.Models;

namespace G1_Website_Tour.Controllers
{
    public class LoginController : Controller
    {
        public TourDB_Entities db = new TourDB_Entities();
        // GET: Login
        public ActionResult Index()
        {
            ViewBag.menu = db.Type_Tour.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Index(string username, string password)
        {
            ViewBag.menu = db.Type_Tour.ToList();
            string pass = Encode.EncodeMD5(password);
            var rs = db.users.Where(p => p.username.Equals(username) && p.password_user.Equals(pass)).FirstOrDefault();
            if(rs != null)
            {
                Session["customer"] = rs.id;
                return RedirectToAction("Index","Cart");
            }
            else
            {
                return RedirectToAction("Index","Login");
            }
        }
        public ActionResult Register()
        {
            ViewBag.menu = db.Type_Tour.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Register(user users)
        {
            ViewBag.menu = db.Type_Tour.ToList();
            if (ModelState.IsValid)
            {
                users.password_user = Encode.EncodeMD5(users.password_user);
                db.users.Add(users);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}