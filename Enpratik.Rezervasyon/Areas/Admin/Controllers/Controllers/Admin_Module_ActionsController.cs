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
    public class Admin_Module_ActionsController : BaseController
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: Admin_Module_Actions
        public ActionResult Index()
        {
            var data = from s in db.Admin_Module_Actions
                       join sa in db.Admin_Modules on s.ModuleId equals sa.Id
                       select new Admin_Module_ActionsDTO
                       {
                           Id = s.Id,
                           ModuleId = s.ModuleId,
                           ActionName = s.ActionName,
                           IsActive = s.IsActive,
                           ModuleName = sa.ModuleName
                       };

            return View(data);
        }

        // GET: Admin_Module_Actions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin_Module_Actions admin_Module_Actions = db.Admin_Module_Actions.Find(id);
            if (admin_Module_Actions == null)
            {
                return HttpNotFound();
            }
            return View(admin_Module_Actions);
        }

        // GET: Admin_Module_Actions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin_Module_Actions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Admin_Module_Actions admin_Module_Actions)
        {
            if (ModelState.IsValid)
            {
                db.Admin_Module_Actions.Add(admin_Module_Actions);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(admin_Module_Actions);
        }

        // GET: Admin_Module_Actions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin_Module_Actions admin_Module_Actions = db.Admin_Module_Actions.Find(id);
            if (admin_Module_Actions == null)
            {
                return HttpNotFound();
            }
            return View(admin_Module_Actions);
        }

        // POST: Admin_Module_Actions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Admin_Module_Actions admin_Module_Actions)
        {
            if (ModelState.IsValid)
            {
                db.Entry(admin_Module_Actions).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(admin_Module_Actions);
        }

        // GET: Admin_Module_Actions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin_Module_Actions admin_Module_Actions = db.Admin_Module_Actions.Find(id);
            if (admin_Module_Actions == null)
            {
                return HttpNotFound();
            }
            return View(admin_Module_Actions);
        }

        // POST: Admin_Module_Actions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Admin_Module_Actions admin_Module_Actions = db.Admin_Module_Actions.Find(id);
            db.Admin_Module_Actions.Remove(admin_Module_Actions);
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
