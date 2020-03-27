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
    public class Admin_SubMenuController : BaseController
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: Admin_SubMenu
        public ActionResult Index()
        {
            var data = from s in db.Admin_SubMenu
                       join sa in db.Admin_MainMenu on s.MainMenuId equals sa.Id
                       select new Admin_SubMenuDTO
                       {
                           Id = s.Id,
                           MainMenuId = s.ModuleId,
                           MainMenuName = sa.MainMenuName,
                           MenuName = s.MenuName,
                           ModuleId = s.ModuleId,
                           ActionId = s.ActionId,
                           ActionParams = s.ActionParams,
                           OrderIndex = s.OrderIndex,
                           IsActive = s.IsActive
                       };

            return View(data);
        }

        // GET: Admin_SubMenu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin_SubMenu admin_SubMenu = db.Admin_SubMenu.Find(id);
            if (admin_SubMenu == null)
            {
                return HttpNotFound();
            }
            return View(admin_SubMenu);
        }

        // GET: Admin_SubMenu/Create
        public ActionResult Create()
        {
            var MainMenus = db.Admin_MainMenu.Where(a => a.IsActive == true).ToList();
            ViewBag.MainMenus = new SelectList(MainMenus, "Id", "MainMenuName");
            var Modules = db.Admin_Modules.Where(a => a.IsActive == true).OrderBy(a=>a.ModuleName).ToList();
            ViewBag.Modules = new SelectList(Modules, "Id", "ModuleName");
            Admin_SubMenu model = new Admin_SubMenu();
            return View(model);
        }

        // POST: Admin_SubMenu/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Admin_SubMenu admin_SubMenu)
        {
            if (ModelState.IsValid)
            {
                db.Admin_SubMenu.Add(admin_SubMenu);
                db.SaveChanges();

                ViewBag.message = "Kayıt eklendi!";
                ViewBag.messageType = 1;
                return RedirectToAction("Index");
            }

            return View(admin_SubMenu);
        }

        // GET: Admin_SubMenu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin_SubMenu admin_SubMenu = db.Admin_SubMenu.Find(id);
            if (admin_SubMenu == null)
            {
                return HttpNotFound();
            }
            return View(admin_SubMenu);
        }

        // POST: Admin_SubMenu/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MainMenuId,MenuName,ModuleId,ActionId,ActionParams,OrderIndex,IsActive")] Admin_SubMenu admin_SubMenu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(admin_SubMenu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(admin_SubMenu);
        }

        // GET: Admin_SubMenu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin_SubMenu admin_SubMenu = db.Admin_SubMenu.Find(id);
            if (admin_SubMenu == null)
            {
                return HttpNotFound();
            }
            return View(admin_SubMenu);
        }

        // POST: Admin_SubMenu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Admin_SubMenu admin_SubMenu = db.Admin_SubMenu.Find(id);
            db.Admin_SubMenu.Remove(admin_SubMenu);
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
