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
    public class Blog_CategoriesController : BaseController
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: Blog_Categories
        public ActionResult Index()
        {
            var data = GetCategoryList();
            return View(data);
        }
        

        // GET: Blog_Categories/Create
        public ActionResult Create()
        {
            GetParentCategory();
            return View(Blog_Categories.initialize);
        }

        // POST: Blog_Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Blog_Categories categories)
        {
            if (ModelState.IsValid)
            {

                if (db.Blog_Categories.Any(o => o.BlogCategoryName == categories.BlogCategoryName & o.IsActive == true))
                    Functions.SetMessageViewBag(this, "Aynı isim daha önce eklenmiştir!", 0);
                else
                {

                    var url = Functions.GetUrl(categories.BlogCategoryName);

                    if (db.WebSiteUrls.Any(w => w.LanguageId == categories.LanguageId & w.UrlTypeId == 2 & w.Url == url))
                        Functions.SetMessageViewBag(this, "Aynı url sistemde kayıtlıdır!", 0);
                    else
                    {
                        int urlId = WebSiteUrls.initialize.Insert(2, url, categories.LanguageId);

                        categories.UrlId = urlId;
                        db.Blog_Categories.Add(categories);
                        db.SaveChanges();
                        Functions.SetMessageViewBag(this, "Kayıt başarıyla eklendi!", 1, "Index");
                    }
                }
            }
            GetParentCategory();
            return View(categories);
        }

        // GET: Blog_Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog_Categories blog_Categories = db.Blog_Categories.Find(id);
            GetParentCategory();
            if (blog_Categories == null)
            {
                return HttpNotFound();
            }
            return View(blog_Categories);
        }

        // POST: Blog_Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Blog_Categories categories)
        {

            if (ModelState.IsValid)
            {
                if (db.Blog_Categories.Any(o => o.BlogCategoryName == categories.BlogCategoryName & o.IsActive == true & o.Id != categories.Id))
                    Functions.SetMessageViewBag(this, "Aynı isim daha önce eklenmiştir!", 0);
                else
                {
                    db.Blog_Categories.Add(categories);
                    db.Entry(categories).State = EntityState.Modified;
                    db.SaveChanges();
                    Functions.SetMessageViewBag(this, "Kayıt başarıyla güncellendi!", 1, "Index");
                }
            }
            GetParentCategory();
            return View(categories);            
        }

        // GET: Blog_Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog_Categories blog_Categories = db.Blog_Categories.Find(id);
            if (blog_Categories == null)
            {
                return HttpNotFound();
            }
            return View(blog_Categories);
        }

        // POST: Blog_Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Blog_Categories blog_Categories = db.Blog_Categories.Find(id);
            blog_Categories.DeletedDate = DateTime.Now;
            blog_Categories.IsActive = false;
            db.Entry(blog_Categories).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private void GetParentCategory()
        {
            var parentCategory = GetCategoryList();
            parentCategory.Insert(0, new Blog_CategoriesDTO() { Id = 0, BlogCategoryName = "-- Üst Kategori Yok --" });
            ViewBag.ParentCategory = new SelectList(parentCategory, "Id", "BlogCategoryName");

            var languages = db.Languages.Where(l => l.IsActive == true).ToList();
            ViewBag.Languages = new SelectList(languages, "Id", "LanguageName");
        }

        private List<Blog_CategoriesDTO> GetCategoryList()
        {
            List<Blog_CategoriesDTO> result = new List<Blog_CategoriesDTO>();
            var categories = db.Blog_Categories.Where(c => c.IsActive == true).ToList();

            var rootCat = categories.Where(c => c.ParentId == 0).ToList();

            foreach (var item in rootCat)
            {
                result.Add(item.getBlogCategoriesDTO(""));
                var subCat = categories.Where(c => c.ParentId == item.Id).ToList();

                if (subCat.Count == 0)
                    continue;

                GetSubCategoryList(result, categories, subCat, item.BlogCategoryName, 2);
            }

            return result;
        }

        private void GetSubCategoryList(List<Blog_CategoriesDTO> result, List<Blog_Categories> categories, List<Blog_Categories> subCat, string catName, int speratorCount)
        {
            string speratorText = getSperatorText(speratorCount);

            foreach (var item in subCat)
            {
                var tempName = item.BlogCategoryName;
                item.BlogCategoryName = speratorText + " " + item.BlogCategoryName;
                result.Add(item.getBlogCategoriesDTO(catName));
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
