using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G1_Website_Tour.Models;
using PagedList;

namespace G1_Website_Tour.Controllers
{
    public class HomeController : Controller
    {
        public TourDB_Entities db = new TourDB_Entities();

        public IEnumerable<tour> AllListPaging(int page, int pageSize)
        {
            return db.tours.OrderByDescending(x => x.name_tour).ToPagedList(page, pageSize);
        }
        public IEnumerable<tour> AllListPagingByType(int page, int pageSize, string typeId)
        {
            return db.tours.OrderByDescending(x => x.name_tour).Where(x => x.type_id.Equals(typeId)).ToPagedList(page, pageSize);
        }
        public ActionResult Index(string id, int page = 1, int pageSize = 20)
        {
            ViewBag.menu = db.Type_Tour.ToList();

            if (id == null)
            {
                var models = AllListPaging(page, pageSize);
                ViewBag.id = null;
                return View(models);
            }
            else
            {
                var models = AllListPagingByType(page, pageSize, id);
                ViewBag.id = id;
                return View(models);
            }
        }
        public ActionResult About()
        {
            ViewBag.menu = db.Type_Tour.ToList();
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.menu = db.Type_Tour.ToList();
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Details(int id)
        {
            ViewBag.menu = db.Type_Tour.ToList();
            var model = db.tours.Find(id);
            return View(model);
        }
        public ActionResult HistoryOrder()
        {
            ViewBag.menu = db.Type_Tour.ToList();
            int userID = (int)Session["customer"];
            var models = db.bills.Where(p => p.user_id == userID).OrderBy(p => p.create_date).ToList();
            return View(models);
        }
        public ActionResult DetailsOD(int id)
        {
            ViewBag.menu = db.Type_Tour.ToList();
            ViewBag.od = db.bills.Find(id);
            var models = db.bill_detail.Where(p => p.bill_id == id).ToList();
            return View(models);
        }
    }
}