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
    public class WebSiteUrlSettingsController : Controller
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: WebSiteUrlSettings
        public ActionResult Index()
        {
            var data = (from w in db.WebSiteUrlSetting
                        join t in db.WebSiteUrlTypeName on w.WebSiteUrlTypeId equals t.Id
                        select new WebSiteUrlSettingDTO()
                        {
                            Id = w.Id,
                            WebSiteUrlTypeId = w.WebSiteUrlTypeId,
                            WebSiteUrlTypeName = t.UrlTypeName,
                            UrlPattern = w.UrlPattern,
                            UrlPatternDescription = w.UrlPatternDescription
                        }).ToList();



            return View(data);
        }
        

        // GET: WebSiteUrlSettings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WebSiteUrlSetting webSiteUrlSetting = db.WebSiteUrlSetting.Find(id);
            if (webSiteUrlSetting == null)
            {
                return HttpNotFound();
            }
            return View(webSiteUrlSetting);
        }

        // POST: WebSiteUrlSettings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(WebSiteUrlSetting webSiteUrlSetting)
        {
            if (ModelState.IsValid)
            {
                db.Entry(webSiteUrlSetting).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(webSiteUrlSetting);
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
