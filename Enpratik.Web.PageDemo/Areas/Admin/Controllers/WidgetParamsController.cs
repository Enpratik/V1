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
    public class WidgetParamsController : Controller
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: WidgetParams
        public ActionResult Index()
        {
            return View(db.WidgetParams.ToList());
        }

        // GET: WidgetParams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WidgetParams widgetParams = db.WidgetParams.Find(id);
            if (widgetParams == null)
            {
                return HttpNotFound();
            }
            return View(widgetParams);
        }

        // GET: WidgetParams/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WidgetParams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,WidgetId,ParameterName,ParameterValue,IsActive")] WidgetParams widgetParams)
        {
            if (ModelState.IsValid)
            {
                db.WidgetParams.Add(widgetParams);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(widgetParams);
        }

        // GET: WidgetParams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WidgetParams widgetParams = db.WidgetParams.Find(id);
            if (widgetParams == null)
            {
                return HttpNotFound();
            }
            return View(widgetParams);
        }

        // POST: WidgetParams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,WidgetId,ParameterName,ParameterValue,IsActive")] WidgetParams widgetParams)
        {
            if (ModelState.IsValid)
            {
                db.Entry(widgetParams).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(widgetParams);
        }

        // GET: WidgetParams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WidgetParams widgetParams = db.WidgetParams.Find(id);
            if (widgetParams == null)
            {
                return HttpNotFound();
            }
            return View(widgetParams);
        }

        // POST: WidgetParams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WidgetParams widgetParams = db.WidgetParams.Find(id);
            db.WidgetParams.Remove(widgetParams);
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
