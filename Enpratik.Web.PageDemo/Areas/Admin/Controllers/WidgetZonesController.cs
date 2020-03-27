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
    public class WidgetZonesController : Controller
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: WidgetZones
        public ActionResult Index()
        {
            return View(db.WidgetZones.ToList());
        }

        // GET: WidgetZones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WidgetZones widgetZones = db.WidgetZones.Find(id);
            if (widgetZones == null)
            {
                return HttpNotFound();
            }
            return View(widgetZones);
        }

        // GET: WidgetZones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WidgetZones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,WidgetZoneName,IsActive")] WidgetZones widgetZones)
        {
            if (ModelState.IsValid)
            {
                db.WidgetZones.Add(widgetZones);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(widgetZones);
        }

        // GET: WidgetZones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WidgetZones widgetZones = db.WidgetZones.Find(id);
            if (widgetZones == null)
            {
                return HttpNotFound();
            }
            return View(widgetZones);
        }

        // POST: WidgetZones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,WidgetZoneName,IsActive")] WidgetZones widgetZones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(widgetZones).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(widgetZones);
        }

        // GET: WidgetZones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WidgetZones widgetZones = db.WidgetZones.Find(id);
            if (widgetZones == null)
            {
                return HttpNotFound();
            }
            return View(widgetZones);
        }

        // POST: WidgetZones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WidgetZones widgetZones = db.WidgetZones.Find(id);
            db.WidgetZones.Remove(widgetZones);
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
