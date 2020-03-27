using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Enpratik.Core;
using Enpratik.Data;

namespace Enpratik.AdminPanel.Controllers
{
    public class News_CategoriesController : Controller
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: News_Categories
        public ActionResult Index()
        {
            var data = GetCategoryList();
            return View(data);
        }
        

        // GET: News_Categories/Create
        public ActionResult Create()
        {
            GetParentCategory();
            return View(News_Categories.initialize);
        }

        // POST: Blog_Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(News_Categories categories)
        {
            if (ModelState.IsValid)
            {

                if (db.News_Categories.Any(o => o.NewsCategoryName == categories.NewsCategoryName & o.IsActive == true))
                    Functions.SetMessageViewBag(this, "Aynı isim daha önce eklenmiştir!", 0);
                else
                {

                    var url = Functions.GetUrl(categories.NewsCategoryName);

                    if (db.WebSiteUrls.Any(w => w.LanguageId == categories.LanguageId & w.UrlTypeId == 9 & w.Url == url))
                        Functions.SetMessageViewBag(this, "Aynı url sistemde kayıtlıdır!", 0);
                    else
                    {
                        int urlId = WebSiteUrls.initialize.Insert(9, url, categories.LanguageId);

                        categories.UrlId = urlId;
                        db.News_Categories.Add(categories);
                        db.SaveChanges();
                        Functions.SetMessageViewBag(this, "Kayıt başarıyla eklendi!", 1, "Index");
                    }
                }
            }
            GetParentCategory();
            return View(categories);
        }
        

        // GET: News_Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News_Categories news_Categories = db.News_Categories.Find(id);
            GetParentCategory();
            if (news_Categories == null)
            {
                return HttpNotFound();
            }
            return View(news_Categories);
        }

        // POST: News_Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(News_Categories categories)
        {
            if (ModelState.IsValid)
            {
                if (db.News_Categories.Any(o => o.NewsCategoryName == categories.NewsCategoryName & o.IsActive == true & o.Id != categories.Id))
                    Functions.SetMessageViewBag(this, "Aynı isim daha önce eklenmiştir!", 0);
                else
                {
                    db.News_Categories.Add(categories);
                    db.Entry(categories).State = EntityState.Modified;
                    db.SaveChanges();
                    Functions.SetMessageViewBag(this, "Kayıt başarıyla güncellendi!", 1, "Index");
                }
            }
            GetParentCategory();
            return View(categories);
        }

        // GET: News_Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News_Categories news_Categories = db.News_Categories.Find(id);
            if (news_Categories == null)
            {
                return HttpNotFound();
            }
            return View(news_Categories);
        }

        // POST: News_Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            News_Categories news_Categories = db.News_Categories.Find(id);
            db.News_Categories.Remove(news_Categories);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        private void GetParentCategory()
        {
            var parentCategory = GetCategoryList();
            parentCategory.Insert(0, new News_CategoriesDTO() { Id = 0, NewsCategoryName = "-- Üst Kategori Yok --" });
            ViewBag.ParentCategory = new SelectList(parentCategory, "Id", "NewsCategoryName");

            var languages = db.Languages.Where(l => l.IsActive == true).ToList();
            ViewBag.Languages = new SelectList(languages, "Id", "LanguageName");
        }

        private List<News_CategoriesDTO> GetCategoryList()
        {
            List<News_CategoriesDTO> result = new List<News_CategoriesDTO>();
            var categories = db.News_Categories.Where(c => c.IsActive == true).ToList();

            var rootCat = categories.Where(c => c.ParentId == 0).ToList();

            foreach (var item in rootCat)
            {
                result.Add(item.getNewsCategoriesDTO(""));
                var subCat = categories.Where(c => c.ParentId == item.Id).ToList();

                if (subCat.Count == 0)
                    continue;

                GetSubCategoryList(result, categories, subCat, item.NewsCategoryName, 2);
            }

            return result;
        }

        private void GetSubCategoryList(List<News_CategoriesDTO> result, List<News_Categories> categories, List<News_Categories> subCat, string catName, int speratorCount)
        {
            string speratorText = getSperatorText(speratorCount);

            foreach (var item in subCat)
            {
                var tempName = item.NewsCategoryName;
                item.NewsCategoryName = speratorText + " " + item.NewsCategoryName;
                result.Add(item.getNewsCategoriesDTO(catName));
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
