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
    public class Admin_ModulesController : BaseController
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: Admin_Modules
        public ActionResult Index()
        {
            return View(db.Admin_Modules.ToList());
        }

        // GET: Admin_Modules/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin_Modules admin_Modules = db.Admin_Modules.Find(id);
            if (admin_Modules == null)
            {
                return HttpNotFound();
            }
            return View(admin_Modules);
        }

        // GET: Admin_Modules/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin_Modules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ModuleName,ModuleDescription,IsActive")] Admin_Modules admin_Modules)
        {
            if (ModelState.IsValid)
            {
                db.Admin_Modules.Add(admin_Modules);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(admin_Modules);
        }

        // GET: Admin_Modules/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin_Modules admin_Modules = db.Admin_Modules.Find(id);
            if (admin_Modules == null)
            {
                return HttpNotFound();
            }
            return View(admin_Modules);
        }

        // POST: Admin_Modules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ModuleName,ModuleDescription,IsActive")] Admin_Modules admin_Modules)
        {
            if (ModelState.IsValid)
            {
                db.Entry(admin_Modules).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(admin_Modules);
        }

        // GET: Admin_Modules/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin_Modules admin_Modules = db.Admin_Modules.Find(id);
            if (admin_Modules == null)
            {
                return HttpNotFound();
            }
            return View(admin_Modules);
        }

        // POST: Admin_Modules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Admin_Modules admin_Modules = db.Admin_Modules.Find(id);
            db.Admin_Modules.Remove(admin_Modules);
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
