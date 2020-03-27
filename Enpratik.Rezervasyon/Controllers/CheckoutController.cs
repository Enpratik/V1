using Enpratik.Core;
using Enpratik.Data;
using Enpratik.Rezervasyon.Models;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Enpratik.Rezervasyon.Controllers
{
    public class CheckoutController : Controller
    {
        EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: Checkout
        public ActionResult Index(string eventUrl, int? id)
        {
            var events = db.Events.Where(e => e.IsActive == true & e.IsPublished == true & e.Id == id).FirstOrDefault();

            if (Current.User != null) {

                var cm = db.EventCustomerMaps.Where(e => e.IsActive == true & e.IsCheckIn == false & e.EventId == id & e.CustomerId == Current.User.Id).FirstOrDefault();

                Session["EventCustomerMaps"] = cm;
            }


            Customers customer = Current.User;
            ViewBag.EventUrl = eventUrl;
            ViewBag.Events = events;

            var eventDateMaps = db.EventDateMaps.Where(e => e.EventId == id).ToList();

            ViewBag.EventDateMaps = eventDateMaps;

            return View(customer);
        }

        // GET: Checkout
        public ActionResult Result(string id)
        {
            var usr = Current.User;

            ViewBag.ResultText = "HATA! Ödeme işlemi gerçekleşmedi.";
           
            var tempMaps = db.EventCustomerMaps_Temps.Where(e => e.GuidId == id).FirstOrDefault();

            if (tempMaps == null)
            {
                ViewBag.ResultText = "Ödeme adımında hata meydana geldi. Lütfen tekrar deneyiniz!";
                return View();
            }

            Current.User = db.Customers.Where(c => c.Id == tempMaps.CustomerId).FirstOrDefault();

            EventCustomerMaps ec = new EventCustomerMaps();           
            ec.CustomerId = tempMaps.CustomerId;
            ec.EventId = tempMaps.EventId;
            ec.IsActive = tempMaps.IsActive;
            ec.IsRezervation = tempMaps.IsRezervation;
            ec.IsCheckIn = tempMaps.IsCheckIn;
            ec.CheckInCode = tempMaps.CheckInCode;
            ec.EventDate = tempMaps.EventDate;
            ec.EventDescription = tempMaps.EventDescription;
            ec.CheckInDate = DateTime.Now;
            ec.CreatedDate = DateTime.Now;


            string basketID = tempMaps.CheckInCode;
            string token = tempMaps.Token;

            Options options = new Options();
            options.ApiKey = ConfigurationManager.AppSettings["IyzicoApiKey"];
            options.SecretKey = ConfigurationManager.AppSettings["IyzicoSecretKey"];
            options.BaseUrl = ConfigurationManager.AppSettings["IyzicoBaseUrl"];


            RetrieveCheckoutFormRequest request = new RetrieveCheckoutFormRequest();
            request.Token = token;

            CheckoutForm checkoutForm = CheckoutForm.Retrieve(request, options);
            if (checkoutForm == null)
            {
                ViewBag.ResultText = "Ödeme adımında banka verileri alınamadı. Lütfen tekrar deneyiniz!";
                return View();
            }
            if (checkoutForm.PaymentStatus == null)
            {
                ViewBag.ResultText = "Ödeme sonuç bilgisi bankadan alınamadı. Lütfen tekrar deneyiniz!";
                return View();
            }

            if (checkoutForm.PaymentStatus.Contains("SUCCESS"))
            {
                ViewBag.ResultText = "Ödemen Başarıyla Alındı. Siparişin en kısa zamanda kargoya verilecektir.";


                db.EventCustomerMaps.Add(ec);
                db.SaveChanges();


                string body = "<table style=\"width:100%; font:500 13px arial, verdana\">" +
                               "    <tr>" +
                               "		<td>" +
                               "            <h3>Merhaba {user}</h3>" +
                               "            <p>" +
                               "				Sitemizden kredi kartı ile satış işlemini gerçekleştirdiniz." +
                               "            </p>" +
                               "            <p>" +
                               "				SİPARİŞ NUMARANIZ : {orderNumber}" +
                               "            </p>" +
                               "		</td>" +
                               "	</tr>"+
                               "</table>";


                body = body.Replace("{user}", Current.User.FirstName + " " + Current.User.LastName);
                body = body.Replace("{orderNumber}", basketID);

                MailHelper mail = new MailHelper();
                mail.SendMail(Current.User.Mail, "Siparişiniz Alındı!", body);
                var infoMailAddress = ConfigurationManager.AppSettings["SendOrderMailAddress"];
                mail.SendMail(infoMailAddress, "Yeni Sipariş Alındı!", body);

                Current.RemoveIyzicoToken();

            }
            else
            {
                ViewBag.ResultText = "Ödeme adımında sorun meydana geldi. Lütfen tekrar deneyiniz.";
            }

            return View();
        }


        // GET: Checkout
        [HttpPost]
        public ActionResult Index(Customers customer, int eventId, string EventDescription, string EventDates, string IdentityNumber)
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

            if (string.IsNullOrEmpty(customer.FirstName) || string.IsNullOrEmpty(customer.LastName) || string.IsNullOrEmpty(customer.Mail) || string.IsNullOrEmpty(customer.PhoneNumber) || string.IsNullOrEmpty(IdentityNumber))
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

                Current.User = customer;

            }

            string id = Guid.NewGuid().ToString();            
            var basketID = "E" + DateTime.Now.ToString("yyyyMMddHHmmss");

            Current.User = customer;

            Options options = new Options();
            options.ApiKey = ConfigurationManager.AppSettings["IyzicoApiKey"];
            options.SecretKey = ConfigurationManager.AppSettings["IyzicoSecretKey"];
            options.BaseUrl = ConfigurationManager.AppSettings["IyzicoBaseUrl"];

            CreateCheckoutFormInitializeRequest request = new CreateCheckoutFormInitializeRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = "";
            request.Price = events.EventPrice.ToString().Replace(",", ".");
            request.PaidPrice = events.EventPrice.ToString().Replace(",", ".");   // kargo ücreti eklenmiş hali
            request.Currency = Currency.TRY.ToString();
            request.BasketId = basketID;
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();
            request.CallbackUrl = ConfigurationManager.AppSettings["WebSiteUrl"] + Url.Action("Result", "Checkout", new { id = id });

            List<int> enabledInstallments = new List<int>();
            enabledInstallments.Add(2);
            enabledInstallments.Add(3);
            enabledInstallments.Add(6);
            enabledInstallments.Add(9);
            request.EnabledInstallments = enabledInstallments;

            Buyer buyer = new Buyer();
            buyer.Id = Current.User.Id.ToString();
            buyer.Name = customer.FirstName;
            buyer.Surname = customer.LastName;
            buyer.GsmNumber = customer.PhoneNumber;
            buyer.Email = customer.Mail;
            buyer.IdentityNumber = IdentityNumber;
            buyer.LastLoginDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            buyer.RegistrationDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            buyer.RegistrationAddress = "Online";
            buyer.Ip = Request.UserHostAddress;
            buyer.City = "Istanbul";
            buyer.Country = "Turkey";
            buyer.ZipCode = "";
            request.Buyer = buyer;

            Address shippingAddress = new Address();
            shippingAddress.ContactName = customer.FirstName + " " + customer.LastName;
            shippingAddress.City = "Istanbul";
            shippingAddress.Country = "Turkey";
            shippingAddress.Description = "Online";
            shippingAddress.ZipCode = "";
            request.ShippingAddress = shippingAddress;

            Address billingAddress = new Address();
            billingAddress.ContactName = customer.FirstName + " " + customer.LastName;
            billingAddress.City = "Istanbul";
            billingAddress.Country = "Turkey";
            billingAddress.Description = "Online";
            billingAddress.ZipCode = "34742";
            request.BillingAddress = billingAddress;

            List<BasketItem> baskets = new List<BasketItem>();
           
           BasketItem firstBasketItem = new BasketItem();
           firstBasketItem.Id = events.Id.ToString();
           firstBasketItem.Name = events.EventName;
           firstBasketItem.Category1 = "Etkinlik Ders";
           firstBasketItem.ItemType = BasketItemType.PHYSICAL.ToString();
           firstBasketItem.Price = (events.EventPrice * Convert.ToDouble(1)).ToString().Replace(",", ".");
           baskets.Add(firstBasketItem);
            
            request.BasketItems = baskets;

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            CheckoutFormInitialize checkoutFormInitialize = CheckoutFormInitialize.Create(request, options);
            ViewBag.CheckoutFormContent = checkoutFormInitialize.CheckoutFormContent;

            Current.SetIyzicoToken(checkoutFormInitialize.Token);


            EventCustomerMaps_Temps ec = new EventCustomerMaps_Temps();
            ec.GuidId = id;
            ec.CustomerId = customer.Id;
            ec.EventId = eventId;
            ec.IsActive = true;
            ec.IsRezervation = true;
            ec.IsCheckIn = true;
            ec.CheckInCode = basketID;
            ec.EventDate = Convert.ToDateTime(EventDates);
            ec.EventDescription = EventDescription;
            ec.CheckInDate = DateTime.Now;
            ec.CreatedDate = DateTime.Now;
            ec.Token = checkoutFormInitialize.Token;

            db.EventCustomerMaps_Temps.Add(ec);
            db.SaveChanges();

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