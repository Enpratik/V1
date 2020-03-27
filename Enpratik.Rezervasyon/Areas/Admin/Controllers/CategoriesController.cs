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
    public class CategoriesController : BaseController
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: Categories
        public ActionResult Index()
        {
            var data = GetCategoryList();

            return View(data);
        }       

        // GET: Categories/Create
        public ActionResult Create()
        {
            GetParentCategory();
            return View(Categories.initialize);
        }

        private void GetParentCategory() 
        {
            var parentCategory = GetCategoryList(); 
            parentCategory.Insert(0, new CategoriesDTO() { Id = 0, CategoryName = "-- Üst Kategori Yok --" });
            ViewBag.ParentCategory = new SelectList(parentCategory, "Id", "CategoryName");

            var languages = db.Languages.Where(l => l.IsActive == true).ToList();
            ViewBag.Languages = new SelectList(languages, "Id", "LanguageName");
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Categories categories)
        {
            if (ModelState.IsValid)
            {
                if (db.Categories.Any(o => o.CategoryName == categories.CategoryName & o.IsActive == true))
                    Functions.SetMessageViewBag(this, "Aynı isim daha önce eklenmiştir!", 0);
                else
                {
                    var url = Functions.GetUrl(categories.CategoryName);

                    if (db.WebSiteUrls.Any(w => w.LanguageId == categories.LanguageId & w.UrlTypeId == 4 & w.Url == url))
                        Functions.SetMessageViewBag(this, "Aynı url sistemde kayıtlıdır!", 0);
                    else
                    {
                        int urlId = WebSiteUrls.initialize.Insert(4, url, categories.LanguageId);

                        categories.UrlId = urlId;
                        db.Categories.Add(categories);
                        db.SaveChanges();
                        Functions.SetMessageViewBag(this, "Kayıt başarıyla eklendi!", 1, "Index");
                    }
                }
            }

            GetParentCategory();
            return View(categories);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            GetParentCategory();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            Categories categories = db.Categories.Find(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Categories categories)
        {
            if (ModelState.IsValid)
            {
                if (db.Categories.Any(o => o.CategoryName == categories.CategoryName & o.IsActive == true & o.Id != categories.Id))
                    Functions.SetMessageViewBag(this, "Aynı kategori ismi zaten var!", 0);
                else
                {
                    db.Categories.Add(categories);
                    db.Entry(categories).State = EntityState.Modified;
                    db.SaveChanges();
                    Functions.SetMessageViewBag(this, "Kayıt başarıyla güncellendi!", 1, "Index");
                }
            }
            GetParentCategory();
            return View(categories);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            Categories categories = db.Categories.Find(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Categories categories = db.Categories.Find(id);
            db.Categories.Remove(categories);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
        private List<CategoriesDTO> GetCategoryList()
        {
            List<CategoriesDTO> result = new List<CategoriesDTO>();
            var categories = db.Categories.Where(c => c.IsActive == true && c.Published == true).OrderBy(c => c.DisplayOrder).ToList();

            var rootCat = categories.Where(c => c.ParentId == 0).ToList();

            foreach (var item in rootCat)
            {
                result.Add(item.getCategoriesDTO(""));
                var subCat = categories.Where(c => c.ParentId == item.Id).ToList();

                if (subCat.Count == 0)
                    continue;

                GetSubCategoryList(result, categories, subCat, item.CategoryName, 2);
            }

            return result;
        }
        private void GetSubCategoryList(List<CategoriesDTO> result, List<Categories> categories, List<Categories> subCat, string catName, int speratorCount)
        {
            string speratorText = getSperatorText(speratorCount);

            foreach (var item in subCat)
            {
                var tempName = item.CategoryName;
                item.CategoryName = speratorText + " " + item.CategoryName;
                result.Add(item.getCategoriesDTO(catName));
                var subCat1 = categories.Where(c => c.ParentId == item.Id).ToList();

                if (subCat1.Count == 0)
                    continue;

                int spratorIndex = speratorCount + 2;

                GetSubCategoryList(result, categories, subCat1, tempName, spratorIndex);
            }
        }
        private string getSperatorText(int speratorCount)
        {
            var speratorText = "";
            for (int i = 0; i < speratorCount; i++)
                speratorText += "-";
            return speratorText;
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
