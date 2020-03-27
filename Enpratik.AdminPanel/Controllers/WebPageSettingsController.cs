using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Enpratik.Data;
using Enpratik.Data.Entity;

namespace Enpratik.AdminPanel.Controllers
{
    public class WebPageSettingsController : Controller
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: WebPageSettings
        public ActionResult Index()
        {
            return View(db.WebPageSettings.ToList());
        }

        // GET: WebPageSettings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WebPageSettings webPageSettings = db.WebPageSettings.Find(id);
            if (webPageSettings == null)
            {
                return HttpNotFound();
            }
            return View(webPageSettings);
        }

        // GET: WebPageSettings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WebPageSettings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SettingKey,SettingValue")] WebPageSettings webPageSettings)
        {
            if (ModelState.IsValid)
            {
                db.WebPageSettings.Add(webPageSettings);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(webPageSettings);
        }

        // GET: WebPageSettings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WebPageSettings webPageSettings = db.WebPageSettings.Find(id);
            if (webPageSettings == null)
            {
                return HttpNotFound();
            }
            return View(webPageSettings);
        }

        // POST: WebPageSettings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SettingKey,SettingValue")] WebPageSettings webPageSettings)
        {
            if (ModelState.IsValid)
            {
                db.Entry(webPageSettings).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(webPageSettings);
        }

        // GET: WebPageSettings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WebPageSettings webPageSettings = db.WebPageSettings.Find(id);
            if (webPageSettings == null)
            {
                return HttpNotFound();
            }
            return View(webPageSettings);
        }

        // POST: WebPageSettings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WebPageSettings webPageSettings = db.WebPageSettings.Find(id);
            db.WebPageSettings.Remove(webPageSettings);
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
