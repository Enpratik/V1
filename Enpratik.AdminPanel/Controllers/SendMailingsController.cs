using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Enpratik.AdminPanel.Model;
using Enpratik.Core;
using Enpratik.Data;

namespace Enpratik.AdminPanel.Controllers
{
    public class SendMailingsController : BaseController
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();
        
        // GET: SendMailings
        public ActionResult Index()
        {
            getTamplate();
            return View(new SendMailModel());
        }

        private void getTamplate() {
            var templates = db.SendMailTemplate.ToList();
            ViewBag.Templates = new SelectList(templates, "Id", "TemplateName");
        }

        [HttpPost]
        public ActionResult Index(SendMailModel mailModel)
        {
            getTamplate();

            if (string.IsNullOrEmpty(mailModel.toAddresses))
            {
                Functions.SetMessageViewBag(this, "Lütfen mail adreslerini giriniz!", 0);
                return View(mailModel);
            }
            if (string.IsNullOrEmpty(mailModel.subject))
            {
                Functions.SetMessageViewBag(this, "Lütfen mail konu başlığını giriniz!", 0);
                return View(mailModel);
            }
            if (mailModel.templateId==null)
            {
                Functions.SetMessageViewBag(this, "Lütfen mail tasarımını seçiniz!", 0);
                return View(mailModel);
            }

            MailHelper mail = new MailHelper();

            var body = db.SendMailTemplate.Find(mailModel.templateId).TemplateContent;
            using (var reader = new StringReader(mailModel.toAddresses))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    mail.SendMail(line, mailModel.subject, body);
                    System.Threading.Thread.Sleep(500);
                }
            }
            

            Functions.SetMessageViewBag(this, "Mail gönderimi yapıldı!", 1);

            return View(mailModel);
        }

        // GET: SendMailings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SendMailings sendMailing = db.SendMailing.Find(id);
            if (sendMailing == null)
            {
                return HttpNotFound();
            }
            return View(sendMailing);
        }

        // GET: SendMailings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SendMailings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MailSendName,MailSenderGroupId,MailFrom,MailTo,MailCc,MailBcc,MailBody,MailIsHtml,StartSenderMailDate,EndSenderMailDate,LanguageId,CreatedDate,CreatedUserId,UpdatedDate,UpdatedUserId,DeletedDate,DeletedUserId,IsActive")] SendMailings sendMailing)
        {
            if (ModelState.IsValid)
            {
                db.SendMailing.Add(sendMailing);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sendMailing);
        }

        // GET: SendMailings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SendMailings sendMailing = db.SendMailing.Find(id);
            if (sendMailing == null)
            {
                return HttpNotFound();
            }
            return View(sendMailing);
        }

        // POST: SendMailings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MailSendName,MailSenderGroupId,MailFrom,MailTo,MailCc,MailBcc,MailBody,MailIsHtml,StartSenderMailDate,EndSenderMailDate,LanguageId,CreatedDate,CreatedUserId,UpdatedDate,UpdatedUserId,DeletedDate,DeletedUserId,IsActive")] SendMailings sendMailing)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sendMailing).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sendMailing);
        }

        // GET: SendMailings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SendMailings sendMailing = db.SendMailing.Find(id);
            if (sendMailing == null)
            {
                return HttpNotFound();
            }
            return View(sendMailing);
        }

        // POST: SendMailings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SendMailings sendMailing = db.SendMailing.Find(id);
            db.SendMailing.Remove(sendMailing);
            db.SaveChanges();
            return RedirectToAction("Index");
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
