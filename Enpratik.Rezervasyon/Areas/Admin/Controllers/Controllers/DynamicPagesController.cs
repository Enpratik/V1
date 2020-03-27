using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Enpratik.Data;
using System.Data.Entity.Validation;
using System.Text;
using Enpratik.Core;

namespace Enpratik.AdminPanel.Controllers
{
    public class DynamicPagesController : BaseController
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: DynamicPages
        public ActionResult MainPageEdit()
        {
            return Edit(1);
        }

        // POST: DynamicPages/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult MainPageEdit(DynamicPages dynamicPages)
        {
            if (ModelState.IsValid)
            {
                if (db.DynamicPages.Any(o => o.PageName == dynamicPages.PageName & o.IsActive == true & o.Id != dynamicPages.Id))
                    Functions.SetMessageViewBag(this, "Aynı isim daha önce eklenmiştir!", 0);
                else
                {
                    dynamicPages.UpdatedDate = DateTime.Now;
                    dynamicPages.UpdatedUserId = 1;
                    dynamicPages.IsActive = true;

                    dynamicPages.PageContent = dynamicPages.PageContent.Replace("../../../Themes", "/Themes");
                    dynamicPages.PageContent = dynamicPages.PageContent.Replace("../", "").Replace("Upload", "/admin/Upload");

                    db.Entry(dynamicPages).State = EntityState.Modified;
                    db.SaveChanges();
                    Functions.SetMessageViewBag(this, "Kayıt başarıyla güncellendi!", 1, "Index");
                }
            }
            getLanguages();
            return View(dynamicPages);
        }



        // GET: DynamicPages
        public ActionResult Index()
        {
            return View(db.DynamicPages.AsNoTracking().ToList());
        }
        
        // GET: DynamicPages/Create
        public ActionResult Create()
        {
            DynamicPages dynamicPages = new DynamicPages();
            if (Session["DynamicPagesPost"] != null)
            {
                dynamicPages = (DynamicPages)Session["DynamicPagesPost"];
                
                 int templateId = (int)Session["templateId"];
                EnPratik_DataHelper db = new EnPratik_DataHelper();
                var data = db.DynamicPage_Templates.Where(w => w.Id == templateId).FirstOrDefault();
                dynamicPages.PageContent = data.PageTemplateHtml;

                Session.Remove("DynamicPagesPost");
                Session.Remove("templateId");
            }

            getLanguages();
            getPageTemplates();
            getParentDynamicPages();
            return View(dynamicPages);
        }

        private void getParentDynamicPages()
        {
            var parentCategory = GetDynamicPageList();
            parentCategory.Insert(0, new DynamicPagesDTO() { Id = 0, PageName = "-- Üst Menü Yok --" });
            ViewBag.PageList = new SelectList(parentCategory, "Id", "PageName");
        }
        
        // POST: DynamicPages/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(DynamicPages dynamicPages)
        {
            if (ModelState.IsValid)
            {
                if (db.DynamicPages.Any(o => o.PageName == dynamicPages.PageName & o.IsActive == true & o.ParentId==dynamicPages.ParentId))
                    Functions.SetMessageViewBag(this, "Aynı isim daha önce eklenmiştir!", 0);
                else
                {
                    string pageUrl=Functions.GetUrl(dynamicPages.PageName);
                    string url = pageUrl;

                    if (dynamicPages.ParentId != 0)
                    {
                        var parentPage = db.DynamicPages.Where(d => d.Id == dynamicPages.ParentId).FirstOrDefault();
                        if (parentPage != null)
                            url = Functions.GetUrl(parentPage.PageName) + "/" + pageUrl;
                    }

                    if (db.WebSiteUrls.Any(w => w.LanguageId == dynamicPages.LanguageId & w.UrlTypeId == 5 & w.Url == url))
                        Functions.SetMessageViewBag(this, "Aynı url sistemde kayıtlıdır!", 0);
                    else
                    {
                        int urlId = WebSiteUrls.initialize.Insert(5, url, dynamicPages.LanguageId);
                        dynamicPages.PageContent = dynamicPages.PageContent.Replace("../../../Themes", "/Themes");
                        dynamicPages.PageContent = dynamicPages.PageContent.Replace("../", "").Replace("Upload", "/admin/Upload");
                        dynamicPages.UrlId = urlId;
                        dynamicPages.CreatedDate = DateTime.Now;
                        dynamicPages.CreatedUserId = 1;
                        dynamicPages.IsActive = true;

                        db.DynamicPages.Add(dynamicPages);
                        db.SaveChanges();
                        Functions.SetMessageViewBag(this, "Kayıt başarıyla eklendi!", 1, "Index");
                    }
                }
            }
            //  try
            //{   
            //}
            //catch (DbEntityValidationException e)
            //{
            //    StringBuilder sb = new StringBuilder();

            //    foreach (var eve in e.EntityValidationErrors)
            //    {
            //        sb.Append(string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
            //            eve.Entry.Entity.GetType().Name, eve.Entry.State));
            //        foreach (var ve in eve.ValidationErrors)
            //        {
            //            sb.Append(string.Format("- Property: \"{0}\", Error: \"{1}\"",
            //                ve.PropertyName, ve.ErrorMessage));
            //        }
            //    }

            //    var text = sb.ToString();
            //    Functions.SetMessageViewBag(this, "Kayıt eklemede hata! Error : " + text, 0);
            //}

            getLanguages();
            getPageTemplates();
            getParentDynamicPages();
            return View(dynamicPages);
        }

        // GET: DynamicPages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            DynamicPages dynamicPages = db.DynamicPages.Find(id);
            getParentDynamicPages();
            getLanguages();
            if (dynamicPages == null)
            {
                return HttpNotFound();
            }
            
            return View(dynamicPages);
        }

        // POST: DynamicPages/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(DynamicPages dynamicPages)
        {
            if (ModelState.IsValid)
            {
                if (db.DynamicPages.Any(o => o.PageName == dynamicPages.PageName & o.IsActive == true & o.Id != dynamicPages.Id & o.ParentId == dynamicPages.ParentId))
                    Functions.SetMessageViewBag(this, "Aynı isim daha önce eklenmiştir!", 0);
                else
                {
                    dynamicPages.UpdatedDate = DateTime.Now;
                    dynamicPages.UpdatedUserId = 1;
                    dynamicPages.IsActive = true;

                    dynamicPages.PageContent = dynamicPages.PageContent.Replace("../../../Themes", "/Themes");
                    dynamicPages.PageContent = dynamicPages.PageContent.Replace("../", "").Replace("Upload", "/admin/Upload");

                    db.Entry(dynamicPages).State = EntityState.Modified;
                    db.SaveChanges();
                    Functions.SetMessageViewBag(this, "Kayıt başarıyla güncellendi!", 1, "Index");
                }
            }
            getParentDynamicPages();
            getLanguages();
            return View(dynamicPages);
        }

        // GET: DynamicPages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DynamicPages dynamicPages = db.DynamicPages.Find(id);
            if (dynamicPages == null)
            {
                return HttpNotFound();
            }
            return View(dynamicPages);
        }

        // POST: DynamicPages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DynamicPages dynamicPages = db.DynamicPages.Find(id);
            db.DynamicPages.Remove(dynamicPages);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private void getLanguages()
        {
            var languages = db.Languages.Where(l => l.IsActive == true).ToList();
            ViewBag.Languages = new SelectList(languages, "Id", "LanguageName");
        }
        private void getPageTemplates()
        {
            var templates = db.DynamicPage_Templates.ToList(); 
            ViewBag.PageTemplates = templates;
        }

        [HttpPost]
        public ActionResult GetPageTemplateHtml(DynamicPages dynamicPages, int templateId) {
            try
            {
                Session["DynamicPagesPost"] = dynamicPages;
                Session["templateId"] = templateId;   
            }
            catch (Exception ex)
            {
                string log = ex.Message;
            }

            return RedirectToAction("Create");
        }


        private List<DynamicPagesDTO> GetDynamicPageList()
        {
            List<DynamicPagesDTO> result = new List<DynamicPagesDTO>();
            var categories = db.DynamicPages.Where(c => c.IsActive == true).ToList();

            var rootCat = categories.Where(c => c.ParentId == 0).ToList();

            foreach (var item in rootCat)
            {
                result.Add(item.getDynamicPagesDTO(""));
                var subCat = categories.Where(c => c.ParentId == item.Id).ToList();

                if (subCat.Count == 0)
                    continue;

                GetSubDynamicPageList(result, categories, subCat, item.PageName, 2);
            }

            return result;
        }

        private void GetSubDynamicPageList(List<DynamicPagesDTO> result, List<DynamicPages> categories, List<DynamicPages> subCat, string catName, int speratorCount)
        {
            string speratorText = getSperatorText(speratorCount);

            foreach (var item in subCat)
            {
                var tempName = item.PageName;
                item.PageName = speratorText + " " + item.PageName;
                result.Add(item.getDynamicPagesDTO(catName));
                var subCat1 = categories.Where(c => c.ParentId == item.Id).ToList();

                if (subCat1.Count == 0)
                    continue;

                int spratorIndex = speratorCount + 2;

                GetSubDynamicPageList(result, categories, subCat1, tempName, spratorIndex);
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
