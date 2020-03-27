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
    public class Admin_Role_AuthenticationsController : BaseController
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: Admin_Role_Authentications
        public ActionResult Index()
        {
            return View(db.Admin_Role_Authentications.ToList());
        }

        // GET: Admin_Role_Authentications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin_Role_Authentications admin_Role_Authentications = db.Admin_Role_Authentications.Find(id);
            if (admin_Role_Authentications == null)
            {
                return HttpNotFound();
            }
            return View(admin_Role_Authentications);
        }

        // GET: Admin_Role_Authentications/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin_Role_Authentications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SubMenuId,RoleId,ModuleId,ModuleActionId,CreatedDate,CreatedUserId,UpdatedDate,UpdatedUserId,DeletedDate,DeletedUserId,IsActive")] Admin_Role_Authentications admin_Role_Authentications)
        {
            if (ModelState.IsValid)
            {
                db.Admin_Role_Authentications.Add(admin_Role_Authentications);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(admin_Role_Authentications);
        }

        // GET: Admin_Role_Authentications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin_Role_Authentications admin_Role_Authentications = db.Admin_Role_Authentications.Find(id);
            if (admin_Role_Authentications == null)
            {
                return HttpNotFound();
            }
            return View(admin_Role_Authentications);
        }

        // POST: Admin_Role_Authentications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SubMenuId,RoleId,ModuleId,ModuleActionId,CreatedDate,CreatedUserId,UpdatedDate,UpdatedUserId,DeletedDate,DeletedUserId,IsActive")] Admin_Role_Authentications admin_Role_Authentications)
        {
            if (ModelState.IsValid)
            {
                db.Entry(admin_Role_Authentications).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(admin_Role_Authentications);
        }

        // GET: Admin_Role_Authentications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin_Role_Authentications admin_Role_Authentications = db.Admin_Role_Authentications.Find(id);
            if (admin_Role_Authentications == null)
            {
                return HttpNotFound();
            }
            return View(admin_Role_Authentications);
        }

        // POST: Admin_Role_Authentications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Admin_Role_Authentications admin_Role_Authentications = db.Admin_Role_Authentications.Find(id);
            db.Admin_Role_Authentications.Remove(admin_Role_Authentications);
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
