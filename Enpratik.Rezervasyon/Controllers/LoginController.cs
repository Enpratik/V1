using Enpratik.Data;
using Enpratik.Rezervasyon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Enpratik.Rezervasyon.Controllers
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


        [HttpPost]
        public ActionResult Index(Customers customer, string rememberme)
        {

            Customers user = db.Customers.Where(c => c.IsActive == true & c.Status == 1 & c.Mail.Equals(customer.Mail) & c.Password.Equals(customer.Password)).FirstOrDefault();

            if (user == null)
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

    }
}