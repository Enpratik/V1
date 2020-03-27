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
    public class WebSiteSettingsController : BaseController
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: WebSiteSettings
        public ActionResult Index()
        {
            return View(db.WebSiteSettings.ToList());
        }

        // GET: WebSiteSettings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WebSiteSettings webSiteSettings = db.WebSiteSettings.Find(id);
            if (webSiteSettings == null)
            {
                return HttpNotFound();
            }
            return View(webSiteSettings);
        }

        // GET: WebSiteSettings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WebSiteSettings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,WebSiteSettingGroupId,SettingKey,SettingValue,SettingDataType,Descriptions,UpdatedUserId,UpdatedDate")] WebSiteSettings webSiteSettings)
        {
            if (ModelState.IsValid)
            {
                db.WebSiteSettings.Add(webSiteSettings);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(webSiteSettings);
        }

        // GET: WebSiteSettings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WebSiteSettings webSiteSettings = db.WebSiteSettings.Find(id);
            if (webSiteSettings == null)
            {
                return HttpNotFound();
            }
            return View(webSiteSettings);
        }

        // POST: WebSiteSettings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,WebSiteSettingGroupId,SettingKey,SettingValue,SettingDataType,Descriptions,UpdatedUserId,UpdatedDate")] WebSiteSettings webSiteSettings)
        {
            if (ModelState.IsValid)
            {
                db.Entry(webSiteSettings).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(webSiteSettings);
        }

        // GET: WebSiteSettings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WebSiteSettings webSiteSettings = db.WebSiteSettings.Find(id);
            if (webSiteSettings == null)
            {
                return HttpNotFound();
            }
            return View(webSiteSettings);
        }

        // POST: WebSiteSettings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WebSiteSettings webSiteSettings = db.WebSiteSettings.Find(id);
            db.WebSiteSettings.Remove(webSiteSettings);
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
