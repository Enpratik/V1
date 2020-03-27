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
    public class MenusController : BaseController
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: Menus
        public ActionResult Index()
        {
            var data = from m in db.Menus
                       join mtn in db.MenuTypes on m.MenuPageType equals mtn.Id
                       join cx in db.Menus on m.ParentId equals cx.Id into gj
                       from ca in gj.DefaultIfEmpty()
                       select new MenusDTO
                       {
                           Id = m.Id,
                           MenuTypeName = mtn.MenuTypeName,
                           MenuParentName = (ca.MenuName == null ? string.Empty : ca.MenuName),
                           MenuName = m.MenuName,
                           DisplayOrder = m.DisplayOrder,
                           CreatedDate = m.CreatedDate,
                           MenuPageId = m.MenuPageId,
                           MenuPageType = m.MenuPageType,
                           MenuUrl = m.MenuUrl
                       };

            return View(data);
        }

        // GET: Menus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menus menus = db.Menus.Find(id);
            if (menus == null)
            {
                return HttpNotFound();
            }
            return View(menus);
        }

        // GET: Menus/Create
        public ActionResult Create()
        {
            SetMenuViewBagData();
            return View(Menus.initialize);
        }

        private void SetMenuViewBagData()
        {
            GetParentMenus();

            var MenuTypes = db.MenuTypes.ToList();
            ViewBag.MenuTypes = new SelectList(MenuTypes, "Id", "MenuTypeName");

            var Targets = Functions.Targets();
            ViewBag.Targets = new SelectList(Targets, "TargetType", "TargetName");
        }

        private void GetParentMenus()
        {
            var parentCategory = GetMenuList();
            parentCategory.Insert(0, new MenusDTO() { Id = 0, MenuName = "-- Üst Menü Yok --" });
            ViewBag.MenuList = new SelectList(parentCategory, "Id", "MenuName");

            var languages = db.Languages.Where(l => l.IsActive == true).ToList();
            ViewBag.Languages = new SelectList(languages, "Id", "LanguageName");
        }

        private List<MenusDTO> GetMenuList()
        {
            List<MenusDTO> result = new List<MenusDTO>();
            var categories = db.Menus.Where(c => c.IsActive == true).ToList();

            var rootCat = categories.Where(c => c.ParentId == 0).ToList();

            foreach (var item in rootCat)
            {
                result.Add(item.getMenuDTO(""));
                var subCat = categories.Where(c => c.ParentId == item.Id).ToList();

                if (subCat.Count == 0)
                    continue;

                GetSubCategoryList(result, categories, subCat, item.MenuName, 2);
            }

            return result;
        }

        private void GetSubCategoryList(List<MenusDTO> result, List<Menus> categories, List<Menus> subCat, string catName, int speratorCount)
        {
            string speratorText = getSperatorText(speratorCount);

            foreach (var item in subCat)
            {
                var tempName = item.MenuName;
                item.MenuName = speratorText + " " + item.MenuName;
                result.Add(item.getMenuDTO(catName));
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



        // POST: Menus/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Menus menus)
        {
            if (ModelState.IsValid)
            {
                menus.CreatedUserId = Functions.GetUserId();

                if (db.Menus.Any(o => o.MenuName == menus.MenuName & o.IsActive == true & o.ParentId == menus.ParentId))
                    Functions.SetMessageViewBag(this, "Aynı menü adı zaten var!", 0);
                else
                {
                    db.Menus.Add(menus);
                    db.SaveChanges();
                    Functions.SetMessageViewBag(this, "Kayıt başarıyla eklendi!", 1, "Index");
                }
            }
            SetMenuViewBagData();
            return View(menus);
        }

        // GET: Menus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            Menus menus = db.Menus.Find(id);
            if (menus == null)
            {
                return HttpNotFound();
            }
            SetMenuViewBagData();
            return View(menus);
        }

        // POST: Menus/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Menus menus)
        {
            if (ModelState.IsValid)
            {
                menus.UpdatedUserId = Functions.GetUserId();
                menus.UpdatedDate = DateTime.Now;
                menus.LanguageId = 1;
                
                if (db.Menus.Any(o => o.MenuName == menus.MenuName & o.IsActive == true & o.ParentId == menus.ParentId & o.Id != menus.Id))
                    Functions.SetMessageViewBag(this, "Aynı menü adı zaten var!", 0);
                else
                {
                    db.Entry(menus).State = EntityState.Modified;
                    db.SaveChanges();
                    Functions.SetMessageViewBag(this, "Kayıt başarıyla güncellendi!", 1, "Index");
                }
            }
            SetMenuViewBagData();
            return View(menus);
        }

        // GET: Menus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menus menus = db.Menus.Find(id);
            if (menus == null)
            {
                return HttpNotFound();
            }
            return View(menus);
        }

        // POST: Menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Menus menus = db.Menus.Find(id);
            db.Menus.Remove(menus);
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
