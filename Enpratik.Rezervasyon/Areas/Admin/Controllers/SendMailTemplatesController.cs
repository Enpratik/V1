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
    public class SendMailTemplatesController : BaseController
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: SendMailTemplates
        public ActionResult Index()
        {
            return View(db.SendMailTemplate.ToList());
        }

        // GET: SendMailTemplates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SendMailTemplates sendMailTemplate = db.SendMailTemplate.Find(id);
            if (sendMailTemplate == null)
            {
                return HttpNotFound();
            }
            return View(sendMailTemplate);
        }

        // GET: SendMailTemplates/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SendMailTemplates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TemplateName,TemplateTop,TemplateContent,TemplateBottom,IsPublished,LanguageId,CreatedDate,CreatedUserId,UpdatedDate,UpdatedUserId,DeletedDate,DeletedUserId,IsActive")] SendMailTemplates sendMailTemplate)
        {
            if (ModelState.IsValid)
            {
                db.SendMailTemplate.Add(sendMailTemplate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sendMailTemplate);
        }

        // GET: SendMailTemplates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SendMailTemplates sendMailTemplate = db.SendMailTemplate.Find(id);
            if (sendMailTemplate == null)
            {
                return HttpNotFound();
            }
            return View(sendMailTemplate);
        }

        // POST: SendMailTemplates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TemplateName,TemplateTop,TemplateContent,TemplateBottom,IsPublished,LanguageId,CreatedDate,CreatedUserId,UpdatedDate,UpdatedUserId,DeletedDate,DeletedUserId,IsActive")] SendMailTemplates sendMailTemplate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sendMailTemplate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sendMailTemplate);
        }

        // GET: SendMailTemplates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SendMailTemplates sendMailTemplate = db.SendMailTemplate.Find(id);
            if (sendMailTemplate == null)
            {
                return HttpNotFound();
            }
            return View(sendMailTemplate);
        }

        // POST: SendMailTemplates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SendMailTemplates sendMailTemplate = db.SendMailTemplate.Find(id);
            db.SendMailTemplate.Remove(sendMailTemplate);
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
