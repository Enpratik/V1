using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Enpratik.Data;

namespace Enpratik.AdminPanel.Controllers
{
    public class SendMailingsController : BaseController
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: SendMailings
        public ActionResult Index()
        {
            return View(db.SendMailing.ToList());
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
