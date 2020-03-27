using Enpratik.Core;
using Enpratik.Data;
using Enpratik.Rezervasyon.Models;
using System;
using System.Configuration;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;

namespace Enpratik.Rezervasyon.Controllers
{
    public class DefaultController : Controller
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: Default
        public ActionResult Index()
        {
            var events = db.Events.Where(e => e.IsActive == true & e.IsPublished == true).ToList();

            return View(events);
        }

        // GET: EventDetail
        public ActionResult EventDetail(string eventUrl, int? id)
        {
            var events = db.Events.Where(e => e.IsActive == true & e.IsPublished == true & e.Id == id).FirstOrDefault();
            if (events == null)
            {
                return View();
            }

            ViewBag.EventUrl = eventUrl;
            var eventDateMaps = db.EventDateMaps.Where(e => e.EventId == id).ToList();

            ViewBag.EventDateMaps = eventDateMaps;

            return View(events);
        }

        // GET: Rezervasyon
        public ActionResult Rezervasyon(string eventUrl, int? id)
        {
            var events = db.Events.Where(e => e.IsActive == true & e.IsPublished == true & e.Id == id).FirstOrDefault();
            
            Customers customer = Current.User;
            ViewBag.EventUrl = eventUrl;
            ViewBag.Events = events;

            var eventDateMaps = db.EventDateMaps.Where(e => e.EventId == id).ToList();

            ViewBag.EventDateMaps = eventDateMaps;

            return View(customer);
        }

        [HttpPost]
        public ActionResult Rezervasyon(Customers customer, int eventId, string EventDescription, string EventDates)
        {
            var events = db.Events.Where(e => e.IsActive == true & e.IsPublished == true & e.Id == eventId).FirstOrDefault();
            var eventDateMaps = db.EventDateMaps.Where(e => e.EventId == eventId).ToList();

            ViewBag.EventDateMaps = eventDateMaps;
            ViewBag.Events = events;

            if (customer == null)
            {
                ViewBag.ErrorMessage = "Bilgileriniz alınamadı. Lütfen tekrar deneyiniz!";
                return View(customer);
            }

            if (string.IsNullOrEmpty(customer.FirstName) || string.IsNullOrEmpty(customer.LastName) || string.IsNullOrEmpty(customer.Mail) || string.IsNullOrEmpty(customer.PhoneNumber))
            {
                ViewBag.ErrorMessage = "Lütfen aşağıdaki alanları eksiksiz olarak doldurunuz!";
                return View(customer);
            }

            if (string.IsNullOrEmpty(EventDates))
            {
                ViewBag.ErrorMessage = "Lütfen etkinliğe katılacağınız tarih ve saati seçiniz!";
                return View(customer);
            }

            if (Current.User == null)
            {
                var check = db.Customers.Where(c => c.IsActive == true & c.Mail == customer.Mail).FirstOrDefault();

                if (check != null)
                {
                    ViewBag.ErrorMessage = "Aynı mail adresi sistemde kayıtlıdır";
                    return View(customer);

                }

                customer.Password = "123456";
                customer.CreatedDate = DateTime.Now;
                customer.Status = 0;
                customer.CustomerType = 1;
                customer.IsActive = true;
                db.Customers.Add(customer);
                db.SaveChanges();
                
            }

            string rezervationCode = "E" + DateTime.Now.ToString("yyyyMMddHHmmss");

            Current.User = customer;

            EventCustomerMaps ec = new EventCustomerMaps();
            ec.CustomerId = customer.Id;
            ec.EventId = eventId;
            ec.IsActive = true;
            ec.IsRezervation = true;
            ec.IsCheckIn = false;
            ec.RezervationCode = rezervationCode;
            ec.EventDate = Convert.ToDateTime(EventDates);
            ec.EventDescription = EventDescription;
            ec.RezervationDate = DateTime.Now;
            ec.CreatedDate = DateTime.Now;


            db.EventCustomerMaps.Add(ec);
            db.SaveChanges();


            // mail at

            var userMailHtml = $"<table style='width:60 %'><tr><td>Merhaba {customer.FirstName} {customer.LastName}</td></tr><tr><td>Aşağıdaki etkinlik için rezervasyon işleminiz onaylanmıştır.</td></tr><tr><td>Rezervasyon Kodunuz : {ec.RezervationCode}</td></tr><tr><td><a href='{ ConfigurationManager.AppSettings["WebSiteUrl"] + "/event/" + events.getUrl() + "-" + eventId }'>Etkinlik detayı için tıklayınız</a></td></tr></table>";

            MailHelper mail = new MailHelper();
            mail.SendMail(customer.Mail, "Rezervasyon Onayı", userMailHtml);

            ViewBag.RezervationCode = rezervationCode;

            

            return View(customer);
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