using Enpratik.AdminPanel.Model;
using Enpratik.Core;
using Enpratik.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Enpratik.AdminPanel.Controllers
{
    public class DefaultController : Controller
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: Default
        public ActionResult Index() {
            if (Current.User != null)
            {
                return RedirectToAction("Index", "Main");
            }
            return View();
        }

        //public ActionResult Index() => View();

        // POST:Default
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Admins admin, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(admin.Email) || string.IsNullOrEmpty(admin.Password))
                {
                    ViewBag.message = "Email ve şifrenizi giriniz!";
                    return View();
                }
                
                Admins user = db.Admins.Where(
                    a => a.Email.Equals(admin.Email) & 
                    a.Password.Equals(admin.Password) & 
                    a.IsActive).FirstOrDefault();

                if (user == null)
                {
                    ViewBag.message = "Email veya şifreniz hatalıdır!";
                    return View();
                }

                Current.User = user;

                if (string.IsNullOrEmpty(returnUrl))
                    if (!string.IsNullOrEmpty(Request["returnUrl"]))
                        returnUrl = Request["returnUrl"];

                return Redirect(string.IsNullOrEmpty(returnUrl) ? "/admin/Main" : returnUrl);
            }

            return View(admin);
        }

        public ActionResult AccountRecovery() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AccountRecovery(Admins admin)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(admin.Email))
                {
                    ViewBag.message = "Email adresinizi giriniz!";
                    return View();
                }


                Admins user = db.Admins.Where(a => a.Email.Equals(admin.Email) & a.IsActive).FirstOrDefault();

                if (user == null)
                {
                    ViewBag.message = "Email adresiniz hatalıdır!";
                    return View();
                }

                // Sender email

                


                return RedirectToAction("Index", "Default");
            }

            return View(admin);
        }


        //Logout
        public ActionResult logout()
        {
            Current.UserAbandon();
            return RedirectToAction("Index", "Default");
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