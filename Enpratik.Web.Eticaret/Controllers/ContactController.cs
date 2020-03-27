using Enpratik.Core;
using Enpratik.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Enpratik.Web.Eticaret.Controllers
{
    public class ContactController : Controller
    {

        EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }


        // POST: Contact
        [HttpPost]
        public ActionResult Index(string name, string phoneNumber, string email, string message)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(message))
            {
                ViewBag.Name = name;
                ViewBag.PhoneNumber = phoneNumber;
                ViewBag.Email = email;
                ViewBag.Message = message;

                return View();
            }


            string body = "<table style=\"width:100%; font:500 13px arial, verdana\">" +
                        "    <tr>" +
                        "		<td>" +
                        "            <p>" +
                        "				Ad-Soyad : {name}" +
                        "            </p>" +
                        "            <p>" +
                        "				Telefon : {phoneNumber}" +
                        "            </p>" +
                        "            <p>" +
                        "				Email : {email}" +
                        "            </p>" +
                        "            <p>" +
                        "				Mesaj : {message}" +
                        "            </p>" +
                        "		</td>" +
                        "	</tr>" +
                        "</table>";
            
            body = body.Replace("{name}", name);
            body = body.Replace("{phoneNumber}", phoneNumber);
            body = body.Replace("{email}", email);
            body = body.Replace("{message}", message);

            MailHelper mail = new MailHelper();
            mail.SendMail("iletisim@enpratik.net", "iletişim Formu", body);

           return Redirect("/iletisim/?m=tesekkurler");
        }

        // POST: Contact
        [HttpPost]
        public JsonResult Demo(string name, string phoneNumber, string email, string companyname)
        {
            string body = "<table style=\"width:100%; font:500 13px arial, verdana\">" +
                        "    <tr>" +
                        "		<td>" +
                        "            <p>" +
                        "				Ad-Soyad : {name}" +
                        "            </p>" +
                        "            <p>" +
                        "				Telefon : {phoneNumber}" +
                        "            </p>" +
                        "            <p>" +
                        "				Email : {email}" +
                        "            </p>" +
                        "            <p>" +
                        "				Şirket Adı : {companyname}" +
                        "            </p>" +
                        "		</td>" +
                        "	</tr>" +
                        "</table>";

            body = body.Replace("{name}", name);
            body = body.Replace("{phoneNumber}", phoneNumber);
            body = body.Replace("{email}", email);
            body = body.Replace("{companyname}", companyname);

            MailHelper mail = new MailHelper();
            mail.SendMail("iletisim@enpratik.net", "Demo Talebi Formu", body);

            return Json("{\"status\":\"success\"}");
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