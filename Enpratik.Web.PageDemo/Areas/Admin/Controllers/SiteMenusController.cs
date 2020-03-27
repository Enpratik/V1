using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Enpratik.Data;
using Enpratik.Data.Model;
using Newtonsoft.Json.Linq;

namespace Enpratik.AdminPanel.Controllers
{
    public class SiteMenusController : Controller
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: Admin/SiteMenus
        public ActionResult Index()
        {
            return View(db.SiteMenus.ToList());
        }


        public JsonResult GetMenuJson() {
            var data = db.SiteMenus.Select(m => m.MenuJson).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        // GET: Admin/SiteMenus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SiteMenus siteMenus = db.SiteMenus.Find(id);
            if (siteMenus == null)
            {
                return HttpNotFound();
            }
            return View(siteMenus);
        }


        // GET: Admin/SiteMenus/Edit/
        public ActionResult Edit()
        {
            return View();
        }


        [HttpPost]
        public JsonResult SetMenuJson(string jsonText)
        {
            SiteMenus menu = db.SiteMenus.FirstOrDefault();
            menu.MenuJson = RemoveText(jsonText);
            db.Entry(menu).State = EntityState.Modified;
            db.SaveChanges();

            return Json("ok");
        }

        private string RemoveText(string jsonText)
        {
            if (jsonText.StartsWith("\"")) {
                jsonText = jsonText.Substring(1, jsonText.Length - 1);
            }

            if (jsonText.EndsWith("\""))
            {
                jsonText = jsonText.Substring(0, jsonText.Length - 1);
            }

            return jsonText;
        }

        // GET: Admin/SiteMenus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SiteMenus siteMenus = db.SiteMenus.Find(id);
            if (siteMenus == null)
            {
                return HttpNotFound();
            }
            return View(siteMenus);
        }

        // POST: Admin/SiteMenus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SiteMenus siteMenus = db.SiteMenus.Find(id);
            db.SiteMenus.Remove(siteMenus);
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
