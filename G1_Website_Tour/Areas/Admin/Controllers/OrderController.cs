using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G1_Website_Tour.Models;

namespace G1_Website_Tour.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        public TourDB_Entities db = new TourDB_Entities();
        // GET: Admin/Order
        public ActionResult Index()
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "AdminLogin");
            var models = db.bills.ToList();
            return View(models);
        }
        public ActionResult Details(int id) // add view -> list -> bill_detail
        {
            ViewBag.od = db.bills.Find(id);
            var models = db.bill_detail.Where(p => p.bill_id == id).ToList();
            return View(models);
        }
        public ActionResult Edit(int id)
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "AdminLogin");
            var model = db.bills.Find(id);
            model.Staffid = (int)Session["admin"];
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(bill bill)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bill).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Order");
        }
    }
}