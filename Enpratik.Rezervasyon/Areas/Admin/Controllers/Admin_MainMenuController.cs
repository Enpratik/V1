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
    public class Admin_MainMenuController : BaseController
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: Admin_MainMenu
        public ActionResult Index()
        {
            return View(db.Admin_MainMenu.ToList());
        }

        // GET: Admin_MainMenu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin_MainMenu admin_MainMenu = db.Admin_MainMenu.Find(id);
            if (admin_MainMenu == null)
            {
                return HttpNotFound();
            }
            return View(admin_MainMenu);
        }

        // GET: Admin_MainMenu/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin_MainMenu/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MainMenuName,Link,OrderIndex,IsActive")] Admin_MainMenu admin_MainMenu)
        {
            if (ModelState.IsValid)
            {
                db.Admin_MainMenu.Add(admin_MainMenu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(admin_MainMenu);
        }

        // GET: Admin_MainMenu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin_MainMenu admin_MainMenu = db.Admin_MainMenu.Find(id);
            if (admin_MainMenu == null)
            {
                return HttpNotFound();
            }
            return View(admin_MainMenu);
        }

        // POST: Admin_MainMenu/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Admin_MainMenu admin_MainMenu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(admin_MainMenu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(admin_MainMenu);
        }

        // GET: Admin_MainMenu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin_MainMenu admin_MainMenu = db.Admin_MainMenu.Find(id);
            if (admin_MainMenu == null)
            {
                return HttpNotFound();
            }
            return View(admin_MainMenu);
        }

        // POST: Admin_MainMenu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Admin_MainMenu admin_MainMenu = db.Admin_MainMenu.Find(id);
            db.Admin_MainMenu.Remove(admin_MainMenu);
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
