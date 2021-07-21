using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G1_Website_Tour.Models;

namespace G1_Website_Tour.Areas.Admin.Controllers
{
    public class TourController : Controller
    {
        public TourDB_Entities db = new TourDB_Entities();
        // GET: Admin/Tour
        public ActionResult Index()
        {
            var models = db.tours.ToList();
            return View(models);
        }

        ///////////////////////////////////////////////////////CREATE//////////////////////////////////////////////////////
       
        public ActionResult Create()
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "AdminLogin");
            ViewBag.types = new SelectList(db.Type_Tour.ToList(), "type_id", "type_name");
            //ViewBag.manus = new SelectList(db.Manufacturers.ToList(), "manufactureID", "manufactureName");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(tour tour, HttpPostedFileBase images)
        {
            if(images != null && images.ContentLength > 0)
            {
                tour.image_tour = images.FileName;
                string urlImage = Server.MapPath("~/Content/images/" + tour.image_tour);
                images.SaveAs(urlImage);
            }
            if (ModelState.IsValid)
            {
                db.tours.Add(tour);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.listTypes = new SelectList(db.Type_Tour, "type_id", "type_name", tour.id);
            return View(tour);
        }

        ////////////////////////////////////////////////////////DETAIL/////////////////////////////////////////////////////

        public ActionResult Details(int id)
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "AdminLogin");
            var model = db.tours.Find(id);
            return View(model);
        }

        ////////////////////////////////////////////////////////DELETE/////////////////////////////////////////////////////

        public ActionResult Delete(int id)
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "AdminLogin");
            var model = db.tours.Find(id);
            return View(model);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            var item = db.tours.Find(id);
            var chitiet = db.bill_detail.ToList();

            int checkx = 0;
            foreach (bill_detail i in chitiet)
            {
                if(id == i.tour_id)
                {
                    checkx += 1;
                }
            }
            if (checkx > 0)
            {
                TempData["check"] = "Tour này đã tồn tại trong chi tiết hóa đơn, vui lòng xóa tại chi tiết hóa đơn trước khi xóa tour";
                return RedirectToAction("Delete", "Tour");
            }
            else
            {
                db.tours.Remove(item);
                db.SaveChanges();
                TempData["check"] = "Bạn đã xóa Tour thành công";
                return RedirectToAction("Index", "Tour");
            }
        }

        /////////////////////////////////////////////////////////EDIT//////////////////////////////////////////////////////

        public ActionResult Edit(int id)
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "AdminLogin");
            var model = db.tours.Find(id);
            ViewBag.types = new SelectList(db.Type_Tour.ToList(), "type_id", "type_name", model.type_id);
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(tour tour, HttpPostedFileBase newImages)
        {
            if(newImages != null && newImages.ContentLength > 0)
            {
                tour.image_tour = newImages.FileName;
                string urlImage = Server.MapPath("~/Content/images/" + tour.image_tour);
                newImages.SaveAs(urlImage);
            }
            if (ModelState.IsValid)
            {
                db.Entry(tour).State = EntityState.Modified/*System.Data.Entity.EntityState.Modified*/;
                db.SaveChanges();
                return RedirectToAction("Index", "Tour");
            }
            ViewBag.listTypes = new SelectList(db.Type_Tour, "type_id", "type_name", tour.type_id);
            return View(tour);
        }
    }
}