using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Enpratik.AdminPanel.Model;
using Enpratik.Core;
using Enpratik.Data;

namespace Enpratik.AdminPanel.Controllers
{
    public class NewsController : BaseController
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: News
        public ActionResult Index()
        {
            return View(db.News.ToList());
        }

        // GET: News/Create
        public ActionResult Create()
        {
            GetNewsCategories("");
            return View(News.initialize);
        }

        // POST: News/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(News news, string[] NewsCategoryIds)
        {
            GetNewsCategories("");


            if (ModelState.IsValid)
            {
                if (NewsCategoryIds == null || NewsCategoryIds.Length == 0)
                {
                    Functions.SetMessageViewBag(this, "Lütfen kategori seçiniz!", 0);
                }
                else
                {
                    if (db.News.Any(o => o.NewsTitle == news.NewsTitle & o.IsActive == true))
                        Functions.SetMessageViewBag(this, "Aynı haber ismi zaten var!", 0);
                    else
                    {


                        var url = Functions.GetUrl(news.NewsTitle);
                        if (db.WebSiteUrls.Any(w => w.LanguageId == news.LanguageId & w.UrlTypeId == 8 & w.Url == url))
                            Functions.SetMessageViewBag(this, "Aynı url sistemde kayıtlıdır!", 0);
                        else
                        {
                            int urlId = WebSiteUrls.initialize.Insert(1, url, news.LanguageId);

                            news.UrlId = urlId;
                            news.FullDescription = news.FullDescription.Replace("../../../Themes", "/Themes");
                            news.FullDescription = news.FullDescription.Replace("../", "").Replace("Upload", "/admin/Upload");

                            news.CreatedDate = DateTime.Now;
                            news.CreatedUserId = Current.User.Id;
                            db.News.Add(news);
                            db.SaveChanges();

                            foreach (var item in NewsCategoryIds)
                            {
                                News_CategoryMapping b = new News_CategoryMapping();
                                b.NewsId = news.Id;
                                b.NewsCategoryId = item.ToInt32();
                                db.News_CategoryMapping.Add(b);
                                db.SaveChanges();
                            }

                            Functions.SetMessageViewBag(this, "Kayıt başarıyla eklendi!", 1, "Index");
                        }
                        //try
                        //{
                        //}
                        //catch (DbEntityValidationException e)
                        //{
                        //    foreach (var eve in e.EntityValidationErrors)
                        //    {
                        //        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        //            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        //        foreach (var ve in eve.ValidationErrors)
                        //        {
                        //            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                        //                ve.PropertyName, ve.ErrorMessage);
                        //        }
                        //    }
                        //}
                    }
                }
            }

            return View(news);
            
        }

        // GET: News/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }

            var selectedId = db.News_CategoryMapping.Where(a => a.NewsId == id).Select(a => a.NewsCategoryId.ToString()).FirstOrDefault();
            GetNewsCategories(selectedId);

            return View(news);
        }

        // POST: News/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(News news, string[] NewsCategoryIds)
        {
            var selectedId = db.News_CategoryMapping.Where(a => a.NewsId == news.Id).Select(a => a.NewsCategoryId.ToString()).FirstOrDefault();
            GetNewsCategories(selectedId);

            if (ModelState.IsValid)
            {

                if (NewsCategoryIds == null || NewsCategoryIds.Length == 0)
                {
                    Functions.SetMessageViewBag(this, "Lütfen kategori seçiniz!", 0);
                }
                else
                {
                    if (db.News.Any(o => o.NewsTitle == news.NewsTitle & o.IsActive == true & o.Id!=news.Id))
                        Functions.SetMessageViewBag(this, "Aynı haber ismi zaten var!", 0);
                    else
                    {


                        var url = Functions.GetUrl(news.NewsTitle);
                        if (db.WebSiteUrls.Any(w => w.LanguageId == news.LanguageId & w.UrlTypeId == 8 & w.Url == url))
                            Functions.SetMessageViewBag(this, "Aynı url sistemde kayıtlıdır!", 0);
                        else
                        {
                            int urlId = WebSiteUrls.initialize.Insert(1, url, news.LanguageId);

                            news.UrlId = urlId;
                            news.FullDescription = news.FullDescription.Replace("../../../Themes", "/Themes");
                            news.FullDescription = news.FullDescription.Replace("../", "").Replace("Upload", "/admin/Upload");

                            news.UpdatedDate = DateTime.Now;
                            news.UpdatedUserId = Current.User.Id;
                            db.Entry(news).State = EntityState.Modified;
                            db.SaveChanges();

                            var bc = db.News_CategoryMapping.Where(b => b.NewsId== news.Id);
                            db.News_CategoryMapping.RemoveRange(bc);
                            db.SaveChanges();

                            foreach (var item in NewsCategoryIds)
                            {
                                News_CategoryMapping b = new News_CategoryMapping();
                                b.NewsId = news.Id;
                                b.NewsCategoryId = item.ToInt32();
                                db.News_CategoryMapping.Add(b);
                                db.SaveChanges();
                            }

                            Functions.SetMessageViewBag(this, "Kayıt başarıyla güncellendi!", 1, "Index");
                        }
                        //try
                        //{
                        //}
                        //catch (DbEntityValidationException e)
                        //{
                        //    foreach (var eve in e.EntityValidationErrors)
                        //    {
                        //        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        //            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        //        foreach (var ve in eve.ValidationErrors)
                        //        {
                        //            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                        //                ve.PropertyName, ve.ErrorMessage);
                        //        }
                        //    }
                        //}
                    }
                }

                
            }
            return View(news);
        }

        // GET: News/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            News news = db.News.Find(id);
            db.News.Remove(news);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        private void GetNewsCategories(string selectetValue)
        {
            var parentCategory = GetCategoryList();
            ViewBag.NewsCategories = new SelectList(parentCategory, "Id", "NewsCategoryName", selectetValue);

            var languages = db.Languages.Where(l => l.IsActive == true).ToList();
            ViewBag.Languages = new SelectList(languages, "Id", "LanguageName");

            var authors = db.NewsAuthors.ToList();
            ViewBag.Authors = new SelectList(authors, "Id", "AuthorName");
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
