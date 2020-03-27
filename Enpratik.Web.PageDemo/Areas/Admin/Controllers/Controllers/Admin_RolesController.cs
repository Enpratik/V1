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
    public class Admin_RolesController : BaseController
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: Admin_Roles
        public ActionResult Index()
        {
            return View(db.Admin_Roles.ToList());
        }

        // GET: Admin_Roles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin_Roles admin_Roles = db.Admin_Roles.Find(id);
            if (admin_Roles == null)
            {
                return HttpNotFound();
            }
            return View(admin_Roles);
        }

        // GET: Admin_Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin_Roles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RoleName,CreatedDate,CreatedUserId,UpdatedDate,UpdatedUserId,DeletedDate,DeletedUserId,IsActive")] Admin_Roles admin_Roles)
        {
            if (ModelState.IsValid)
            {
                db.Admin_Roles.Add(admin_Roles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(admin_Roles);
        }

        // GET: Admin_Roles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin_Roles admin_Roles = db.Admin_Roles.Find(id);
            if (admin_Roles == null)
            {
                return HttpNotFound();
            }
            return View(admin_Roles);
        }

        // POST: Admin_Roles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RoleName,CreatedDate,CreatedUserId,UpdatedDate,UpdatedUserId,DeletedDate,DeletedUserId,IsActive")] Admin_Roles admin_Roles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(admin_Roles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(admin_Roles);
        }

        // GET: Admin_Roles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin_Roles admin_Roles = db.Admin_Roles.Find(id);
            if (admin_Roles == null)
            {
                return HttpNotFound();
            }
            return View(admin_Roles);
        }

        // POST: Admin_Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Admin_Roles admin_Roles = db.Admin_Roles.Find(id);
            db.Admin_Roles.Remove(admin_Roles);
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
