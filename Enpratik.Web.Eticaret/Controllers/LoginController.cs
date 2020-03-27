using Enpratik.Core;
using Enpratik.Data;
using Enpratik.Web.Eticaret.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Enpratik.Web.Eticaret.Controllers
{
    public class LoginController : Controller
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: Login
        public ActionResult Index()
        {
            if (Current.User == null)
                return View();

            if (Session["RedirectCheckout"] != null)
                return Redirect("/Checkout");

            string returnUrl = Request.QueryString["returnUrl"];
            return Redirect(string.IsNullOrEmpty(returnUrl) ? "/" : returnUrl);
        }
        
        // POST: Login
        [HttpPost]
        public ActionResult Index(Customers customer, string rememberme)
        {

            Customers user = db.Customers.Where(c => c.IsActive == true & c.Status == 1 & c.Mail.Equals(customer.Mail) & c.Password.Equals(customer.Password)).FirstOrDefault();

            if (user==null)
            {
                ViewBag.ErrorMessage = "Aktif kullanıcı bulunamadı. Lütfen email ve şifrenizi kontrol ederek tekrar deneyiniz.";

                return View();
            }

            Current.User = user;

            // TODO
            // sepeti varsa db ye kaydet

            if (Session["RedirectCheckout"] != null)
            {
                return Redirect("/Checkout");
            }

            string returnUrl = Request.QueryString["returnUrl"];
            return Redirect(string.IsNullOrEmpty(returnUrl) ? "/" : returnUrl);
        }
        
        public ActionResult Activation(string u)
        {
            if(string.IsNullOrEmpty(u))
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            int userId = Functions.Base64Decode(u).ToInt32();

            Customers customer = db.Customers.Where(c => c.Id == userId & c.IsActive == true).FirstOrDefault();

            if (customer == null)
            {
                ViewBag.Message = "Üzgünüz. Aktivasyon için uygun kullanıcı bulunamadı. Lütfen linki kontrol edeniz.";
                return View();
            }

            customer.Status = 1;
            db.Entry(customer).State = EntityState.Modified;
            db.SaveChanges();
            
            return View();
        }

        // GET: Login
        public ActionResult ForgotPassword()
        {
            return View(new Customers());
        }
        
        // POST: Login
        [HttpPost]
        public ActionResult ForgotPassword(Customers customer)
        {

            if(string.IsNullOrEmpty(customer.Mail))
            {
                ViewBag.ErrorMessage = "Lütfen email adresinizi giriniz!";

                return View();
            }


            Customers user = db.Customers.Where(c => c.IsActive == true & c.Status == 1 & c.Mail.Equals(customer.Mail)).FirstOrDefault();

            if (user==null)
            {
                ViewBag.ErrorMessage = "Email adresine kayıtlı kullanıcı bulunmadı! Lütfen kontrol ederek tekrar deneyiniz";

                return View();
            }

            // send mail

            string body = "<table style=\"width:100%; font:500 13px arial, verdana\">" +
                        "    <tr>" +
                        "		<td>" +
                        "            <h3>Merhaba {user}</h3>" +
                        "            <p>" +
                        "				Şifre hatırlatma talebiniz başarıyla alındı. Aşağıdaki linke tıklayarak şifrenizi değiştirebilirsiniz." +
                        "            </p>" +
                        "            <p>" +
                        "				<a href=\"{link}\">Şifre Değiştir</a>" +
                        "            </p>" +
                        "            <p style=\"font-size:12px;\">" +
                        "                Link çalışmazsa bu linki kopyalayarak tarayıcınıza yapıştınız : {link}" +
                        "            </p>" +
                        "		</td>" +
                        "	</tr>" +
                        "</table>";

            body = body.Replace("{user}", user.FirstName + " " + user.LastName);
            body = body.Replace("{link}", ConfigurationManager.AppSettings["WebSiteUrl"] + "/Login/ChangePassword/?u=" + Functions.Base64Encode(user.Id.ToString()));

            MailHelper mail = new MailHelper();
            mail.SendMail(user.Mail, "Şifre Değişiklik Talebi", body);

            ViewBag.Success = "1"; 

            ViewBag.ErrorMessage = "Şifre değişikliği yapabileceğiniz link email adresinize gönderilmiştir.";

            return View();
        }
        


        // GET: Login
        public ActionResult ChangePassword()
        {
            ViewBag.UserID = Request["u"];
            return View();
        }

        // POST: Login
        [HttpPost]
        public ActionResult ChangePassword(Customers customer, string passwordAgain, string u)
        {
            if (string.IsNullOrEmpty(u))
            {
                ViewBag.ErrorMessage = "Bir hata oluştu ve kullanıcı bilgisi alınamadı! Lütfen size gönderilen linke tıklayınız.";
                return View();
            }


            int userId = Functions.Base64Decode(u).ToInt32();

            Customers user = db.Customers.Where(c=>c.Id==userId).FirstOrDefault();


            if (user == null)
            {
                ViewBag.ErrorMessage = "Bir hata oluştu! Lütfen size gönderilen linke tıklayınız.";
                return View();
            }

            if (string.IsNullOrEmpty(customer.Password))
            {
                ViewBag.ErrorMessage = "Lütfen yeni şifrenizi giriniz!";
                return View();
            }
            if (!customer.Password.Equals(passwordAgain))
            {
                ViewBag.ErrorMessage = "Girdiğiniz şifreler birbiriyle aynı değil!";
                return View();
            }


            user.Password = customer.Password;
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();

            Current.User = user;

            return Redirect("/Login/ChangePassword/?m=success");
        }


        // GET: Login
        public ActionResult Logout()
        {
            Current.RemoveSessionUser();
            return Redirect("/");
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