using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Enpratik.Data;
using Enpratik.Core;

namespace Enpratik.AdminPanel.Controllers
{
    public class ImporterCompaniesController : BaseController
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: ImporterCompanies
        public ActionResult Index()
        {
            return View(db.ImporterCompanies.ToList());
        }

        // GET: ImporterCompanies/Create
        public ActionResult Create()
        {
            getLanguages();
            return View(ImporterCompanies.initialize);
        }

        // POST: ImporterCompanies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ImporterCompanies importerCompanies)
        {
            if (ModelState.IsValid)
            {

                if (db.ImporterCompanies.Any(o => o.ImporterCompanyName == importerCompanies.ImporterCompanyName & o.IsActive == true))
                    Functions.SetMessageViewBag(this, "Aynı isimde kayıt zaten var!", 0);
                else
                {
                    var url = Functions.GetUrl(importerCompanies.ImporterCompanyName);

                    if (db.WebSiteUrls.Any(w => w.LanguageId == importerCompanies.LanguageId & w.UrlTypeId == 4 & w.Url == url))
                        Functions.SetMessageViewBag(this, "Aynı url sistemde kayıtlıdır!", 0);
                    else
                    {
                        int urlId = WebSiteUrls.initialize.Insert(6, url, importerCompanies.LanguageId);

                        importerCompanies.UrlId = urlId;
                        db.ImporterCompanies.Add(importerCompanies);
                        db.SaveChanges();
                        Functions.SetMessageViewBag(this, "Kayıt başarıyla eklendi!", 1, "Index");
                    }
                }
            }

            getLanguages();
            return View(importerCompanies);
        }

        // GET: ImporterCompanies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            ImporterCompanies importerCompanies = db.ImporterCompanies.Find(id);
            if (importerCompanies == null)
            {
                return HttpNotFound();
            }
            getLanguages();
            return View(importerCompanies);
        }

        // POST: ImporterCompanies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ImporterCompanies importerCompanies)
        {
            if (ModelState.IsValid)
            {

                if (db.ImporterCompanies.Any(o => o.ImporterCompanyName == importerCompanies.ImporterCompanyName & o.IsActive == true & o.Id != importerCompanies.Id))
                    Functions.SetMessageViewBag(this, "Aynı isimde kayıt zaten var!", 0);
                else
                {
                    db.Entry(importerCompanies).State = EntityState.Modified;
                    db.SaveChanges();
                    Functions.SetMessageViewBag(this, "Kayıt başarıyla eklendi!", 1, "Index");
                }
            }
            return View(importerCompanies);
        }

        // GET: ImporterCompanies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImporterCompanies importerCompanies = db.ImporterCompanies.Find(id);
            if (importerCompanies == null)
            {
                return HttpNotFound();
            }
            return View(importerCompanies);
        }

        // POST: ImporterCompanies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ImporterCompanies importerCompanies = db.ImporterCompanies.Find(id);
            db.ImporterCompanies.Remove(importerCompanies);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private void getLanguages()
        {
            var languages = db.Languages.Where(l => l.IsActive == true).ToList();
            ViewBag.Languages = new SelectList(languages, "Id", "LanguageName");
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
