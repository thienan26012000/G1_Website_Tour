using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G1_Website_Tour.Models;

namespace G1_Website_Tour.Areas.Admin.Controllers
{
    public class StaffController : Controller
    {
        public TourDB_Entities db = new TourDB_Entities();
        // GET: Admin/Staff
        ////////////////////////////////////////////////////////INDEX/////////////////////////////////////////////////////
        public ActionResult Index()
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "AdminLogin");
            var models = db.Staffs.ToList();
            return View(models);
        }
        ////////////////////////////////////////////////////////DETAIL/////////////////////////////////////////////////////

        public ActionResult Details(int id)
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "AdminLogin");
            var model = db.Staffs.Find(id);
            return View(model);
        }
        /////////////////////////////////////////////////////////EDIT//////////////////////////////////////////////////////

        public ActionResult Edit(int id)
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "AdminLogin");
            var model = db.Staffs.Find(id);
            //ViewBag.types = new SelectList(db.Type_Tour.ToList(), "type_id", "type_name", model.type_id);
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Staff staff, HttpPostedFileBase newImages)
        {
            if (newImages != null && newImages.ContentLength > 0)
            {
                staff.image = newImages.FileName;
                string urlImage = Server.MapPath("~/Content/images/" + staff.image);
                newImages.SaveAs(urlImage);
            }
            if (ModelState.IsValid)
            {
                db.Entry(staff).State = EntityState.Modified/*System.Data.Entity.EntityState.Modified*/;
                db.SaveChanges();
                return RedirectToAction("Index", "Staff");
            }
            //ViewBag.listTypes = new SelectList(db.Type_Tour, "type_id", "type_name", tour.type_id);
            return View(staff);
        }
    }
}