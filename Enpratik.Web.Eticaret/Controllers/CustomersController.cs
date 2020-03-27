using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Enpratik.Data;
using Enpratik.Core;
using System.Configuration;
using Enpratik.Web.Eticaret.Model;

namespace Enpratik.Web.Eticaret.Controllers
{
    public class CustomersController : Controller
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: Customers
        public ActionResult Index()
        {
            var user = Current.User;

            if (user==null)
                return RedirectToAction("Index", "Default");

            var basketItems = db.Product_BasketItem.Where(p => p.CustomerId == user.Id & p.IsActive == true).OrderByDescending(o=>o.RegDate).ToList();

            List<Product_BasketItem> orders = new List<Product_BasketItem>();

            foreach (var item in basketItems)
            {
                if (orders.Any(p => p.OrderNumber == item.OrderNumber))
                    continue;

                orders.Add(item);
            }


            ViewBag.BasketItems = orders;

            return View(user);
        } 
        

        // POST: Customers
        [HttpPost]
        public ActionResult Index(int id ,string firstname, string lastname)
        {
            var user = Current.User;

            if (user == null)
            {
                user = db.Customers.Where(c => c.Id == id).FirstOrDefault();
                Current.User = user;
            }

            var basketItems = db.Product_BasketItem.Where(p => p.CustomerId == user.Id & p.IsActive == true).OrderByDescending(o => o.RegDate).ToList();

            List<Product_BasketItem> orders = new List<Product_BasketItem>();

            foreach (var item in basketItems)
            {
                if (orders.Any(p => p.OrderNumber == item.OrderNumber))
                    continue;

                orders.Add(item);
            }


            ViewBag.BasketItems = orders;

            user.FirstName = firstname;
            user.LastName = lastname;
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();

            ViewBag.IsSuccess = "1";

            return View(user);
        }




        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customers customers = db.Customers.Find(id);
            if (customers == null)
            {
                return HttpNotFound();
            }
            return View(customers);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customers customers)
        {

            var url = Request.RawUrl;

            if (ModelState.IsValid)
            {
                var check = db.Customers.Where(c => c.IsActive == true & c.Mail == customers.Mail).FirstOrDefault();

                if (check != null)
                {
                    ViewBag.ErrorMessage = "Aynı mail adresi sistemde kayıtlıdır";
                    return View($"~/Themes/{Functions.GetThemeName()}/Views/Login/Index.cshtml", customers);
                }

                customers.CreatedDate = DateTime.Now;
                customers.Status = 0;
                customers.CustomerType = 1;
                customers.IsActive = true;
                db.Customers.Add(customers);
                db.SaveChanges();

                // send mail

                string body = "<table style=\"width:100%; font:500 13px arial, verdana\">" +
                            "    <tr>" +
                            "		<td>" +
                            "            <h3>Merhaba {user}</h3>" +
                            "            <p>" +
                            "				Sitemize üyeliğiniz tamamlanması için lütfen aşağıdaki linke tıklayarak aktivasyon işlemini gerçekleştiriniz." +
                            "            </p>" +
                            "            <p>" +
                            "				<a href=\"{link}\">Aktivasyonu Tamamla</a>" +
                            "            </p>" +
                            "            <p style=\"font-size:12px;\">" +
                            "                Link çalışmazsa bu linki kopyalayarak tarayıcınıza yapıştınız : {link}" +
                            "            </p>" +
                            "		</td>" +
                            "	</tr>" +
                            "</table>";

                body = body.Replace("{user}", customers.FirstName + " " + customers.LastName);
                body = body.Replace("{link}", ConfigurationManager.AppSettings["WebSiteUrl"] + "/Login/Activation/?u=" + Functions.Base64Encode(customers.Id.ToString()));

                MailHelper mail = new MailHelper();
                mail.SendMail(customers.Mail, "Üyelik Aktivasyonu", body);

                ViewBag.ErrorMessage = "Kaydınız oluşturuldu. Lütfen size gönderdiğimiz aktivasyon maili kontrol ediniz!";

                /// TODO guncellenecek
                
            }

            return View($"~/Themes/{Functions.GetThemeName()}/Views/Login/Index.cshtml", customers);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customers customers = db.Customers.Find(id);
            if (customers == null)
            {
                return HttpNotFound();
            }
            return View(customers);
        }

        // POST: Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customers customers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customers);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
