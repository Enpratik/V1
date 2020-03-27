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
    public class WidgetsController : Controller
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: Widgets
        public ActionResult Index()
        {
            return View(db.Widgets.ToList());
        }

        // GET: Widgets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Widgets widgets = db.Widgets.Find(id);
            if (widgets == null)
            {
                return HttpNotFound();
            }
            return View(widgets);
        }

        // GET: Widgets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Widgets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,WidgetName,HtmlCode,WidgetControlPath,IconUrl,IsControlWidget,IsActive")] Widgets widgets)
        {
            if (ModelState.IsValid)
            {
                db.Widgets.Add(widgets);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(widgets);
        }

        // GET: Widgets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Widgets widgets = db.Widgets.Find(id);
            if (widgets == null)
            {
                return HttpNotFound();
            }
            return View(widgets);
        }

        // POST: Widgets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,WidgetName,HtmlCode,WidgetControlPath,IconUrl,IsControlWidget,IsActive")] Widgets widgets)
        {
            if (ModelState.IsValid)
            {
                db.Entry(widgets).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(widgets);
        }

        // GET: Widgets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Widgets widgets = db.Widgets.Find(id);
            if (widgets == null)
            {
                return HttpNotFound();
            }
            return View(widgets);
        }

        // POST: Widgets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Widgets widgets = db.Widgets.Find(id);
            db.Widgets.Remove(widgets);
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
