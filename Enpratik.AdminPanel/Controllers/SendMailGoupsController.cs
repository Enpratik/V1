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
    public class SendMailGoupsController : BaseController
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: SendMailGoups
        public ActionResult Index()
        {
            return View(db.SendMailGoups.ToList());
        }

        // GET: SendMailGoups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SendMailGoups sendMailGoups = db.SendMailGoups.Find(id);
            if (sendMailGoups == null)
            {
                return HttpNotFound();
            }
            return View(sendMailGoups);
        }

        // GET: SendMailGoups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SendMailGoups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MailGroupName,MailAddress,LanguageId,CreatedDate,CreatedUserId,UpdatedDate,UpdatedUserId,DeletedDate,DeletedUserId,IsActive")] SendMailGoups sendMailGoups)
        {
            if (ModelState.IsValid)
            {
                db.SendMailGoups.Add(sendMailGoups);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sendMailGoups);
        }

        // GET: SendMailGoups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SendMailGoups sendMailGoups = db.SendMailGoups.Find(id);
            if (sendMailGoups == null)
            {
                return HttpNotFound();
            }
            return View(sendMailGoups);
        }

        // POST: SendMailGoups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MailGroupName,MailAddress,LanguageId,CreatedDate,CreatedUserId,UpdatedDate,UpdatedUserId,DeletedDate,DeletedUserId,IsActive")] SendMailGoups sendMailGoups)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sendMailGoups).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sendMailGoups);
        }

        // GET: SendMailGoups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SendMailGoups sendMailGoups = db.SendMailGoups.Find(id);
            if (sendMailGoups == null)
            {
                return HttpNotFound();
            }
            return View(sendMailGoups);
        }

        // POST: SendMailGoups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SendMailGoups sendMailGoups = db.SendMailGoups.Find(id);
            db.SendMailGoups.Remove(sendMailGoups);
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
