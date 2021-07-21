using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G1_Website_Tour.Models;

namespace G1_Website_Tour.Areas.Admin.Controllers
{
    public class TypeTourController : Controller
    {
        public TourDB_Entities db = new TourDB_Entities();
        // GET: Admin/TypeProduct
        public ActionResult Index()
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "AdminLogin");
            var models = db.Type_Tour.ToList();
            return View(models);
        }
        public ActionResult Create()
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "AdminLogin");
            return View();
        }
        [HttpPost]
        public ActionResult Create(Type_Tour typeTour)
        {
            try
            {
                db.Type_Tour.Add(typeTour);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Details(string id)
        {
            var model = db.Type_Tour.Find(id);
            return View(model);
        }
        public ActionResult Delete(string id)
        {
            var model = db.Type_Tour.Find(id);
            return View(model);
        }
        [HttpPost,ActionName("Delete")]
        public ActionResult ConfirmDelete(string id)
        {
            var item = db.Type_Tour.Find(id);
            db.Type_Tour.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index", "TypeTour");
        }
        public ActionResult Edit(string id)
        {
            var model = db.Type_Tour.Find(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(Type_Tour typeTour)
        {
            if (ModelState.IsValid)
            {
                db.Entry(typeTour).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "TypeTour");
            }
            return View(typeTour);
        }
    }
}