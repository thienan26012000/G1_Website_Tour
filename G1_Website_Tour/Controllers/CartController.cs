using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using G1_Website_Tour.Models;

namespace G1_Website_Tour.Controllers
{
    public class CartController : Controller
    {
        public TourDB_Entities db = new TourDB_Entities();
        // GET: Cart
        public ActionResult Index()
        {
            ViewBag.menu = db.Type_Tour.ToList();
            List<CartItem> list = (List<CartItem>)Session["cartSession"];

            return View(list);
        }
        public ActionResult AddItem(int id)
        {
            ViewBag.menu = db.Type_Tour.ToList();
            var cart = Session["cartSession"];
            List<CartItem> list = new List<CartItem>();
            //cart is null
            if (cart == null)
            {
                tour tour = db.tours.Where(x => x.id == id).SingleOrDefault();
                CartItem item = new CartItem();
                item.tours = tour;
                item.quantity = 1;
                list.Add(item);
                Session["cartSession"] = list;
            }
            else
            {
                list = (List<CartItem>)Session["cartSession"];
                if (list.Exists(x => x.tours.id == id))
                {
                    foreach (var a in list)
                    {
                        if (a.tours.id == id)
                            a.quantity = a.quantity + 1;
                    }
                    Session["cartSession"] = list;
                }
                else
                {
                    tour tour = db.tours.Where(x => x.id == id).SingleOrDefault();
                    CartItem item = new CartItem();
                    item.tours = tour;
                    item.quantity = 1;
                    list.Add(item);
                    Session["cartSession"] = list;
                }
            }
            return RedirectToAction("Index", "Cart");
        }
        public ActionResult UpdateItem(int id, int quantity)
        {
            ViewBag.menu = db.Type_Tour.ToList();
            List<CartItem> list = (List<CartItem>)Session["cartSession"];
            if (quantity != 0)
                list.Where(p => p.tours.id.Equals(id)).FirstOrDefault().quantity = quantity;
            return RedirectToAction("Index", "Cart");
        }
        public ActionResult DeleteItem(int id)
        {
            ViewBag.menu = db.Type_Tour.ToList();
            List<CartItem> list = (List<CartItem>)Session["cartSession"];
            CartItem item = list.Where(p => p.tours.id.Equals(id)).FirstOrDefault();
            list.Remove(item);
            Session["cartSession"] = list;
            return RedirectToAction("Index", "Cart");
        }
        public ActionResult Order()
        {
            if (Session["customer"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                //ortour
                bill bi = new bill();
                bi.id = db.bills.OrderByDescending(p => p.id).First().id + 1;
                bi.create_date = DateTime.Now;
                bi.user_id = (int)Session["customer"];
                bi.status = 0;
                db.bills.Add(bi);
                db.SaveChanges();
                List<CartItem> list = (List<CartItem>)Session["cartSession"];
                string message = "";
                float sum = 0;
                foreach (CartItem item in list)
                {
                    bill_detail bill_Detail = new bill_detail();
                    bill_Detail.bill_id = bi.id;
                    bill_Detail.tour_id = item.tours.id;
                    bill_Detail.quantity = item.quantity;
                    bill_Detail.price = (item.quantity * item.tours.price);
                    db.bill_detail.Add(bill_Detail);
                    db.SaveChanges();
                    
                    message += "<br /> TourId: " + item.tours.id;
                    message += "<br /> TourName: " + item.tours.name_tour;
                    message += "<>br / Quantity: " + item.quantity;
                    message += "<br /> Price: " + item.tours.price; 
                    message += "<br /> Quab*Price: " + String.Format("{0:0,0 VND}", item.quantity * item.tours.price);
                    sum += (float)(item.quantity * item.tours.price);
                }
                message += "<br /> Total money: " + String.Format("{0:0,0 VND}", sum);
                bi.total = (double)sum;
                db.SaveChanges();
                //send to customer       
                user us = db.users.Find(bi.user_id);
                //SendEmail
                SendMail(us.email, "Hello",message);
                Session["cartSession"] = null;
                return RedirectToAction("Index", "Home");
            }
        }

        public void SendMail(string address, string subject, string message)
        {
            string email = "thienandemo2601@gmail.com";
            string password = "thienan2601";

            var loginInfo = new NetworkCredential(email, password);
            var msg = new MailMessage();
            var smtpClient = new SmtpClient("smtp.gmail.com", 587);

            msg.From = new MailAddress(email);
            msg.To.Add(new MailAddress(address));
            msg.Subject = subject;
            msg.Body = message;
            msg.IsBodyHtml = true;

            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = loginInfo;
            smtpClient.Send(msg);

        }
    }
}