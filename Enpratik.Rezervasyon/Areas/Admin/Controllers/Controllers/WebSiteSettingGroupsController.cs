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
    public class WebSiteSettingGroupsController : BaseController
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: WebSiteSettingGroups
        public ActionResult Index()
        {
            return View(db.WebSiteSettingGroups.ToList());
        }

        // GET: WebSiteSettingGroups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WebSiteSettingGroups webSiteSettingGroups = db.WebSiteSettingGroups.Find(id);
            if (webSiteSettingGroups == null)
            {
                return HttpNotFound();
            }
            return View(webSiteSettingGroups);
        }

        // GET: WebSiteSettingGroups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WebSiteSettingGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,WebSiteTypeId,SettingGroupName")] WebSiteSettingGroups webSiteSettingGroups)
        {
            if (ModelState.IsValid)
            {
                db.WebSiteSettingGroups.Add(webSiteSettingGroups);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(webSiteSettingGroups);
        }

        // GET: WebSiteSettingGroups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WebSiteSettingGroups webSiteSettingGroups = db.WebSiteSettingGroups.Find(id);
            if (webSiteSettingGroups == null)
            {
                return HttpNotFound();
            }
            return View(webSiteSettingGroups);
        }

        // POST: WebSiteSettingGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,WebSiteTypeId,SettingGroupName")] WebSiteSettingGroups webSiteSettingGroups)
        {
            if (ModelState.IsValid)
            {
                db.Entry(webSiteSettingGroups).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(webSiteSettingGroups);
        }

        // GET: WebSiteSettingGroups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WebSiteSettingGroups webSiteSettingGroups = db.WebSiteSettingGroups.Find(id);
            if (webSiteSettingGroups == null)
            {
                return HttpNotFound();
            }
            return View(webSiteSettingGroups);
        }

        // POST: WebSiteSettingGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WebSiteSettingGroups webSiteSettingGroups = db.WebSiteSettingGroups.Find(id);
            db.WebSiteSettingGroups.Remove(webSiteSettingGroups);
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
