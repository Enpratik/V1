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
using System.Data.Entity.Validation;

namespace Enpratik.AdminPanel.Controllers
{
    public class BlogsController : BaseController
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: Blogs
        public ActionResult Index()
        {
            return View(db.Blogs.ToList());
        }        

        // GET: Blogs/Create
        public ActionResult Create()
        {
            GetBlogCategories();
            return View(Blogs.initialize);
        }


        // POST: Blogs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Blogs blogs, string[] BlogCategoryIds)
        {
            GetBlogCategories();

            if (ModelState.IsValid)
            {
                if (BlogCategoryIds == null || BlogCategoryIds.Length == 0)
                {
                    Functions.SetMessageViewBag(this, "Lütfen kategori seçiniz!", 0);
                }
                else
                {
                    if (db.Blogs.Any(o => o.BlogTitle == blogs.BlogTitle & o.IsActive == true))
                        Functions.SetMessageViewBag(this, "Aynı blog ismi zaten var!", 0);
                    else
                    {


                        var url = Functions.GetUrl(blogs.BlogTitle);
                        if (db.WebSiteUrls.Any(w => w.LanguageId == blogs.LanguageId & w.UrlTypeId == 1 & w.Url == url))
                            Functions.SetMessageViewBag(this, "Aynı url sistemde kayıtlıdır!", 0);
                        else
                        {
                            int urlId = WebSiteUrls.initialize.Insert(1, url, blogs.LanguageId);

                            blogs.UrlId = urlId;
                            blogs.FullDescription= blogs.FullDescription.Replace("../../../Themes", "/Themes");
                            blogs.FullDescription = blogs.FullDescription.Replace("../", "").Replace("Upload", "/admin/Upload");

                            blogs.CreatedDate = DateTime.Now;
                            db.Blogs.Add(blogs);
                            db.SaveChanges();

                            foreach (var item in BlogCategoryIds)
                            {
                                Blog_CategoryMapping b = new Blog_CategoryMapping();
                                b.BlogId = blogs.Id;
                                b.BlogCategoryId = item.ToInt32();
                                db.Blog_CategoryMapping.Add(b);
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

            return View(blogs);
        }

        // GET: Blogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            Blogs blogs = db.Blogs.Find(id);
            if (blogs == null)
            {
                return HttpNotFound();
            }

            ViewBag.BlogCategorySelectedIds = db.Blog_CategoryMapping.Where(a => a.BlogId == id).Select(a=>a.BlogCategoryId.ToString()).ToList(); 
            GetBlogCategories();

            return View(blogs);
        }

        // POST: Blogs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Blogs blogs, string[] BlogCategoryIds)
        {
            GetBlogCategories();

            if (ModelState.IsValid)
            {
                if (BlogCategoryIds == null || BlogCategoryIds.Length == 0)
                {
                    Functions.SetMessageViewBag(this, "Lütfen kategori seçiniz!", 0);
                }
                else
                {
                    if (db.Blogs.Any(o => o.BlogTitle == blogs.BlogTitle & o.IsActive == true & o.Id != blogs.Id))
                        Functions.SetMessageViewBag(this, "Aynı blog ismi zaten var!", 0);
                    else
                    {

                        blogs.FullDescription = blogs.FullDescription.Replace("../../../Themes", "/Themes");
                        blogs.FullDescription = blogs.FullDescription.Replace("../", "").Replace("Upload", "/admin/Upload");

                        blogs.UpdatedDate = DateTime.Now;
                        db.Entry(blogs).State = EntityState.Modified;
                        db.SaveChanges();

                        var bc = db.Blog_CategoryMapping.Where(b => b.BlogId == blogs.Id);
                        db.Blog_CategoryMapping.RemoveRange(bc);
                        db.SaveChanges();

                        foreach (var item in BlogCategoryIds)
                        {
                            Blog_CategoryMapping b = new Blog_CategoryMapping();
                            b.BlogId = blogs.Id;
                            b.BlogCategoryId = item.ToInt32();
                            db.Blog_CategoryMapping.Add(b);
                            db.SaveChanges();
                        }

                        Functions.SetMessageViewBag(this, "Kayıt başarıyla eklendi!", 1, "Index");
                    }
                }
            }
            return View(blogs);
        }

        // GET: Blogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blogs blogs = db.Blogs.Find(id);
            if (blogs == null)
            {
                return HttpNotFound();
            }
            return View(blogs);
        }

        // POST: Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Blogs blogs = db.Blogs.Find(id);
            db.Blogs.Remove(blogs);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private void GetBlogCategories()
        {
            var parentCategory = GetCategoryList();
            ViewBag.BlogCategories = new SelectList(parentCategory, "Id", "BlogCategoryName");

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
