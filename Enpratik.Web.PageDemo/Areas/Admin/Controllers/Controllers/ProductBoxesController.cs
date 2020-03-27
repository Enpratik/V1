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
    public class ProductBoxesController : BaseController
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: ProductBoxes
        public ActionResult Index()
        {
            return View(db.ProductBoxes.Where(p=>p.IsActive==true).ToList());
        }
        
        // GET: ProductBoxes/Create
        public ActionResult Create()
        {
            getViewBag();

            ViewBag.ProductList = GetProducts(0);
            ViewBag.ProductBoxList = GetProductBoxList(0);
            return View(ProductBoxDTO.initialize);
        }

        // POST: ProductBoxes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductBoxDTO productBox, string[] ProductIds)
        {
            getViewBag();
            ViewBag.ProductList = GetProducts(0);
            ViewBag.ProductBoxList = GetProductBoxList(0);

            if (ModelState.IsValid)
            {
                if (ProductIds == null || ProductIds.Length == 0)
                {
                    Functions.SetMessageViewBag(this, "Lütfen kategori seçiniz!", 0);
                }
                else
                {
                    if (db.ProductBoxes.Any(o => o.BoxName == productBox.BoxName & o.IsActive == true))
                        Functions.SetMessageViewBag(this, "Aynı kutu ismi zaten var!", 0);
                    else
                    {


                        var url = Functions.GetUrl(productBox.BoxName);
                        if (db.WebSiteUrls.Any(w => w.LanguageId == productBox.LanguageId & w.UrlTypeId == 11 & w.Url == url))
                            Functions.SetMessageViewBag(this, "Aynı url sistemde kayıtlıdır!", 0);
                        else
                        {
                            int urlId = WebSiteUrls.initialize.Insert(1, url, productBox.LanguageId);

                            productBox.UrlId = urlId;
                            productBox.CreatedDate = DateTime.Now;
                            ProductBox pb = productBox.GetProductBox(); 
                            db.ProductBoxes.Add(pb);
                            db.SaveChanges();

                            foreach (var item in ProductIds)
                            {
                                ProductBox_Product_Mapping pm = new ProductBox_Product_Mapping();
                                pm.ProductId = item.ToInt32();
                                pm.ProductBoxId = pb.Id;
                                db.ProductBox_Product_Mapping.Add(pm);
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

            return View(productBox);
        }

        // GET: ProductBoxes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            ProductBox productBox = db.ProductBoxes.Find(id);
            if (productBox == null)
            {
                return HttpNotFound();
            }

            getViewBag();
            ViewBag.ProductList = GetProducts(0);
            ViewBag.ProductBoxList = GetProductBoxList(0);

            return View(productBox.GetProductBoxDTO());
        }

        // POST: ProductBoxes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductBoxDTO productBox, string[] ProductIds)
        {
            if (ModelState.IsValid)
            {
                if (ProductIds == null || ProductIds.Length == 0)
                {
                    Functions.SetMessageViewBag(this, "Lütfen kategori seçiniz!", 0);
                }
                else
                {
                    if (db.ProductBoxes.Any(o => o.BoxName == productBox.BoxName & o.IsActive == true))
                        Functions.SetMessageViewBag(this, "Aynı kutu ismi zaten var!", 0);
                    else
                    {
                        productBox.UpdatedDate = DateTime.Now;
                        ProductBox pb = productBox.GetProductBox();
                        db.ProductBoxes.Add(pb);
                        db.SaveChanges();

                        foreach (var item in ProductIds)
                        {
                            ProductBox_Product_Mapping pm = new ProductBox_Product_Mapping();
                            pm.ProductId = item.ToInt32();
                            pm.ProductBoxId = pb.Id;
                            db.ProductBox_Product_Mapping.Add(pm);
                            db.SaveChanges();
                        }
                        Functions.SetMessageViewBag(this, "Kayıt başarıyla eklendi!", 1, "Index");
                    }
                }
            }

            return View(productBox);

            //if (ModelState.IsValid)
            //{
            //    getViewBag();
            //    ViewBag.ProductList = GetProducts(0);
            //    ViewBag.ProductBoxList = GetProductBoxList(0);

            //    db.Entry(productBox).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //return View(productBox);
        }

        // GET: ProductBoxes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductBox productBox = db.ProductBoxes.Find(id);
            if (productBox == null)
            {
                return HttpNotFound();
            }
            return View(productBox);
        }

        // POST: ProductBoxes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductBox productBox = db.ProductBoxes.Find(id);
            db.ProductBoxes.Remove(productBox);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        private void getViewBag()
        {
            var Currencies = db.Currencies.Where(a => a.IsActive == true).ToList();
            ViewBag.Currencies = new SelectList(Currencies, "Id", "CurrencyName", "1");

            var languages = db.Languages.Where(l => l.IsActive == true).ToList();
            ViewBag.Languages = new SelectList(languages, "Id", "LanguageName");
        }

        private List<SelectListItem> GetProducts(int boxId)
        {
            var productBox_Product_Mappings = db.ProductBox_Product_Mapping.Where(p => p.ProductBoxId == boxId).ToList();


            List<Products> productList = db.Products.Where(p => p.IsActive == true && p.Published == true).ToList();

            var data = productList.Where(p => !productBox_Product_Mappings.Any(u => u.ProductBoxId == p.Id)).ToList();

            List<SelectListItem> products = new List<SelectListItem>();

            foreach (Products item in data)
            {
                SelectListItem i = new SelectListItem();
                i.Text = item.ProductName;
                i.Value = item.Id.ToString();
                products.Add(i);
            }

            return products;
        }

        private List<SelectListItem> GetProductBoxList(int boxId)
        {
            var data = from p in db.Products
                       join
                       pm in db.ProductBox_Product_Mapping on p.Id equals pm.ProductId
                       where pm.ProductBoxId == boxId
                       select p;


            List<SelectListItem> productBoxList = new List<SelectListItem>();

            foreach (Products item in data)
            {
                SelectListItem i = new SelectListItem();
                i.Text = item.ProductName;
                i.Value = item.Id.ToString();
                productBoxList.Add(i);
            }

            return productBoxList;
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
