using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G1_Website_Tour.Models;

namespace G1_Website_Tour.Areas.Admin.Controllers
{
    public class CustomerController : Controller
    {
        public TourDB_Entities db = new TourDB_Entities();
        // GET: Admin/Customer
        public ActionResult Index()
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "AdminLogin");
            var models = db.users.ToList();
            return View(models);
        }
        ///////////////////////////////////////////////////////CREATE//////////////////////////////////////////////////////

        //public ActionResult Create()
        //{
        //    if (Session["admin"] == null)
        //        return RedirectToAction("Index", "AdminLogin");
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult Create(user users)
        //{
        //    try
        //    {
        //        db.users.Add(users);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
        ////////////////////////////////////////////////////////DETAIL/////////////////////////////////////////////////////

        public ActionResult Details(int id)
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "AdminLogin");
            var model = db.users.Find(id);
            return View(model);
        }
        /////////////////////////////////////////////////////////EDIT//////////////////////////////////////////////////////

        public ActionResult Edit(int id)
        {
            var model = db.users.Find(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(user users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(users).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Customer");
            }
            return View(users);
        }
        ////////////////////////////////////////////////////////DELETE/////////////////////////////////////////////////////

        public ActionResult Delete(int id)
        {
            var model = db.users.Find(id);
            return View(model);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            var item = db.users.Find(id);
            db.users.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index", "Customer");
        }
    }
}