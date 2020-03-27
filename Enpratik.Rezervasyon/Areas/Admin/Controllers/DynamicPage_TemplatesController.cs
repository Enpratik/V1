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
    public class DynamicPage_TemplatesController : Controller
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: DynamicPage_Templates
        public ActionResult Index()
        {
            return View(db.DynamicPage_Templates.ToList());
        }

        // GET: DynamicPage_Templates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DynamicPage_Templates dynamicPage_Templates = db.DynamicPage_Templates.Find(id);
            if (dynamicPage_Templates == null)
            {
                return HttpNotFound();
            }
            return View(dynamicPage_Templates);
        }

        // GET: DynamicPage_Templates/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DynamicPage_Templates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "Id,PageTemplateName,PageTemplateHtml")] DynamicPage_Templates dynamicPage_Templates)
        {
            if (ModelState.IsValid)
            {
                db.DynamicPage_Templates.Add(dynamicPage_Templates);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dynamicPage_Templates);
        }

        // GET: DynamicPage_Templates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DynamicPage_Templates dynamicPage_Templates = db.DynamicPage_Templates.Find(id);
            if (dynamicPage_Templates == null)
            {
                return HttpNotFound();
            }
            return View(dynamicPage_Templates);
        }

        // POST: DynamicPage_Templates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "Id,PageTemplateName,PageTemplateHtml")] DynamicPage_Templates dynamicPage_Templates)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dynamicPage_Templates).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dynamicPage_Templates);
        }

        // GET: DynamicPage_Templates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DynamicPage_Templates dynamicPage_Templates = db.DynamicPage_Templates.Find(id);
            if (dynamicPage_Templates == null)
            {
                return HttpNotFound();
            }
            return View(dynamicPage_Templates);
        }

        // POST: DynamicPage_Templates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DynamicPage_Templates dynamicPage_Templates = db.DynamicPage_Templates.Find(id);
            db.DynamicPage_Templates.Remove(dynamicPage_Templates);
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
