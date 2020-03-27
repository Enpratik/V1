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
using System.Text;
using System.Collections;

namespace Enpratik.AdminPanel.Controllers
{
    public class ProductsController : BaseController
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: Products
        public ActionResult Index()
        {
            return View(db.Products.Where(p=>p.IsActive==true).ToList());
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            getLanguages();
            GetViewBagProduct();
            return View(ProductDTO.initialize);
        }

        private void GetViewBagProduct()
        {
            var Brands = db.Brands.Where(a => a.IsActive == true).ToList();
            ViewBag.Brands = new SelectList(Brands, "Id", "BrandName");

            var Categories = db.Categories.Where(a => a.IsActive == true).ToList();
            ViewBag.Categories = new SelectList(Categories, "Id", "CategoryName");

            var Currencies = db.Currencies.Where(a => a.IsActive == true).ToList();
            ViewBag.Currencies = new SelectList(Currencies, "Id", "CurrencyName", "1");

            var ImporterCompanies = db.ImporterCompanies.Where(a => a.IsActive == true).ToList();
            ViewBag.ImporterCompanies = new SelectList(ImporterCompanies, "Id", "ImporterCompanyName");

            var Manufacturers = db.Manufacturers.Where(a => a.IsActive == true).ToList();
            ViewBag.Manufacturers = new SelectList(Manufacturers, "Id", "ManufacturerName");


        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(ProductDTO product, string[] CategoryIds)
        {
            if (ModelState.IsValid)
            {
                Products products = product.GetProduct();

                if (db.Products.Any(p => p.ProductName.Equals(products.ProductName) & p.IsActive == true))
                    Functions.SetMessageViewBag(this, "Aynı isim daha önce eklenmiştir!", 0);
                else
                {
                    var url = Functions.GetUrl(products.ProductName);


                    if (db.WebSiteUrls.Any(w => w.LanguageId == product.LanguageId & w.UrlTypeId == 10 & w.Url == url))
                        Functions.SetMessageViewBag(this, "Aynı url sistemde kayıtlıdır!", 0);
                    else
                    {
                        int urlId = WebSiteUrls.initialize.Insert(10, url, product.LanguageId);

                        products.UrlId = urlId;
                        products.UpdatedDate = DateTime.Now;
                        products.UpdatedUserId = Functions.GetUserId();

                        db.Products.Add(products);
                        db.SaveChanges();
                        Functions.SetMessageViewBag(this, "Kayıt başarıyla eklendi!", 1, "Index");
                    }




                    //}
                    //catch (DbEntityValidationException e)
                    //{
                    //    string aaaaa = "";
                    //    foreach (var eve in e.EntityValidationErrors)
                    //    {
                    //        aaaaa+=String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                    //            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    //        foreach (var ve in eve.ValidationErrors)
                    //        {
                    //            aaaaa += string.Format("- Property: \"{0}\", Error: \"{1}\"",
                    //                ve.PropertyName, ve.ErrorMessage);
                    //        }
                    //    }

                    //    aaaaa += "";
                    //}


                    if (CategoryIds != null)
                    {

                        foreach (string CategoryId in CategoryIds)
                        {
                            if (string.IsNullOrEmpty(CategoryId))
                                continue;

                            Product_Category_Mapping cat = new Product_Category_Mapping();
                            cat.CategoryId = CategoryId.ToInt32();
                            cat.ProductId = products.Id;
                            db.Product_Category_Mapping.Add(cat);
                            db.SaveChanges();

                        }
                    }
                    Functions.SetMessageViewBag(this, "Kayıt başarıyla eklendi!", 1, "Index");
                }
            }

            getLanguages();
            GetViewBagProduct();
            return View(product);
        }


        // POST: Products/CreateProductVariantAttributeMapping/5
        [HttpPost]
        public string CreateProductVariantAttributeMapping(int productId, string data)
        {
            try
            {
                string[] variantAttribute = data.Split(';');

                //List<ProductVariant> pList = new List<ProductVariant>();

                foreach (var item in variantAttribute)
                {
                    if (string.IsNullOrEmpty(item))
                        continue;

                    string[] temp = item.Split(':');

                    int variantId = temp[0].ToInt32();
                    
                    var variantAttributeValue = temp[1].Split('-');

                    // ProductVariant pv = new ProductVariant();

                    //  pv.ParentId = productId;
                    //  pv.VariantAttributeId = variantId;

                    Product_VariantAttribute_Mapping p = new Product_VariantAttribute_Mapping();
                    p.ProductId = productId;
                    p.VariantAttributeId = variantId;
                    
                    int counter = 0;

                    foreach (var item1 in variantAttributeValue)
                    {
                        if (string.IsNullOrEmpty(item1))
                            continue;

                        string[] temp2 = item1.Split('|');

                        string key = temp2[0];
                        string value = temp2[1];

                        p.VariantAttributeValueId = key.ToInt32();
                        db.Product_VariantAttribute_Mapping.Add(p);
                        db.SaveChanges();

                        counter++;

                      //  pv.ProductVariantAttributeValueList.Add(new ProductVariantAttributeValue() { VariantAttributeValue = value, VariantAttributeValueId = key.ToInt32() });

                        //sb.Append("variantId:" + variantId + " --  attr : " + item1 + System.Environment.NewLine);

                        //sl.Add(key, value);

                    }

                    if (counter == 0)
                    {
                        db.Product_VariantAttribute_Mapping.Add(p);
                        db.SaveChanges();
                    }

                    // pList.Add(pv);
                }

                


                //Products product = Products.initialize.Copy(productId);

                //var url = Functions.GetUrl(product.ProductName + "-" +);

                //if (db.WebSiteUrls.Any(w => w.LanguageId == product.LanguageId & w.UrlTypeId == 10 & w.Url == url))
                //    Functions.SetMessageViewBag(this, "Aynı url sistemde kayıtlıdır!", 0);
                //else
                //{
                //    int urlId = WebSiteUrls.initialize.Insert(10, url, product.LanguageId);
                //    product.ParentId = productId;
                //    product.UrlId = urlId;
                //    product.UpdatedDate = DateTime.Now;
                //    product.UpdatedUserId = Functions.GetUserId();

                //    db.Products.Add(product);
                //    db.SaveChanges();
                //    Functions.SetMessageViewBag(this, "Kayıt başarıyla eklendi!", 1, "Index");
                //}

                //Product_VariantAttribute_Mapping p = new Product_VariantAttribute_Mapping();
                //p.ProductId = productId;
                //p.VariantAttributeId = variantAttributeId;
                //p.VariantAttributeValueId = variantAttributeValueId;

                //db.Product_VariantAttribute_Mapping.Add(p);
                //db.SaveChanges();

                //  List<Products> products = GetProductVariantCombinationData(variantAttributeValueList);


                // var json = new JavaScriptSerializer().Serialize(products);

                //  return json;

            }
            catch (Exception ex)
            {
                string log = ex.Message;


                return "Ürün varyasyonları oluşturulmada hata!";

            }



            return "Ürün varyasyonları oluşturuldu!";
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            GetViewBagProduct();

            int? catId = db.Product_Category_Mapping.Where(p => p.ProductId == id).Select(p => p.CategoryId).FirstOrDefault();

            if (catId != null)
            {
                var Categories = db.Categories.Where(a => a.IsActive == true).ToList();
                ViewBag.Categories = new SelectList(Categories, "Id", "CategoryName");
            }
            
            var variantList = db.Product_VariantAttribute.Where(a => a.IsActive == true).ToList();
            ViewBag.VariantAttributeList = variantList;

            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }

            ProductDTO product = products.GetProductDTO();

            getLanguages();
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(ProductDTO product, string[] chkVariantAttributeList, string[] CategoryIds)
        {
            GetViewBagProduct();

            Products products = product.GetProduct();

            var variantList = db.Product_VariantAttribute.Where(a => a.IsActive == true).ToList();
            ViewBag.VariantAttributeList = variantList;

            if (ModelState.IsValid)
            {
                if (db.Products.Any(p => p.ProductName.Equals(products.ProductName) & p.IsActive == true & p.Id != products.Id))
                {
                    Functions.SetMessageViewBag(this, "Aynı isim daha önce eklenmiştir!", 0);
                }
                else
                {
                    products.UpdatedDate = DateTime.Now;
                    products.UpdatedUserId = Functions.GetUserId();
                    db.Entry(products).State = EntityState.Modified;
                    db.SaveChanges();

                    if (CategoryIds != null)
                    {
                        if (CategoryIds.Length > 0)
                        {
                            var datas = db.Product_Category_Mapping.Where(p => p.ProductId == products.Id);
                            db.Product_Category_Mapping.RemoveRange(datas);
                            db.SaveChanges();
                        }

                        foreach (string CategoryId in CategoryIds)
                        {
                            if (string.IsNullOrEmpty(CategoryId))
                                continue;

                            Product_Category_Mapping cat = new Product_Category_Mapping();
                            cat.CategoryId = CategoryId.ToInt32();
                            cat.ProductId = products.Id;
                            db.Product_Category_Mapping.Add(cat);
                            db.SaveChanges();
                        }
                    }

                    Functions.SetMessageViewBag(this, "Kayıt başarıyla güncellendi!", 1, "Index");
                }
            }
            getLanguages();
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Products products = db.Products.Find(id);
            db.Products.Remove(products);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private void getLanguages()
        {
            var languages = db.Languages.Where(l => l.IsActive == true).ToList();
            ViewBag.Languages = new SelectList(languages, "Id", "LanguageName");
        }



        private static List<Products> GetProductVariantCombinationData(ArrayList variantAttributeValueList)
        {
            string productName = "product-name";

            List<Products> productList = new List<Products>();

            try
            {

                switch (variantAttributeValueList.Count)
                {
                    case 1:
                        foreach (var item in (SortedList<string, string>)variantAttributeValueList[0])
                        {
                            Products p = new Products();
                            p.ProductName = productName + "-" + item.Value;
                            productList.Add(p);
                        }
                        break;
                    case 2:
                        foreach (var item in (SortedList<string, string>)variantAttributeValueList[0])
                        {
                            foreach (var item1 in (SortedList<string, string>)variantAttributeValueList[1])
                            {
                                Products p = new Products();
                                p.ProductName = productName + "-" + item.Value + "-" + item1.Value;
                                productList.Add(p);
                            }
                        }
                        break;
                    case 3:
                        foreach (var item in (SortedList<string, string>)variantAttributeValueList[0])
                        {
                            foreach (var item1 in (SortedList<string, string>)variantAttributeValueList[1])
                            {
                                foreach (var item2 in (SortedList<string, string>)variantAttributeValueList[2])
                                {
                                    Products p = new Products();
                                    p.ProductName = productName + "-" + item.Value + "-" + item1.Value + "-" + item2.Value;
                                    productList.Add(p);
                                }
                            }
                        }
                        break;
                    case 4:
                        foreach (var item in (SortedList<string, string>)variantAttributeValueList[0])
                        {
                            foreach (var item1 in (SortedList<string, string>)variantAttributeValueList[1])
                            {
                                foreach (var item2 in (SortedList<string, string>)variantAttributeValueList[2])
                                {
                                    foreach (var item3 in (SortedList<string, string>)variantAttributeValueList[3])
                                    {
                                        Products p = new Products();
                                        p.ProductName = productName + "-" + item.Value + "-" + item1.Value + "-" + item2.Value + "-" + item3.Value;
                                        productList.Add(p);
                                    }
                                }
                            }
                        }
                        break;
                    case 5:
                        foreach (var item in (SortedList<string, string>)variantAttributeValueList[0])
                        {
                            foreach (var item1 in (SortedList<string, string>)variantAttributeValueList[1])
                            {
                                foreach (var item2 in (SortedList<string, string>)variantAttributeValueList[2])
                                {
                                    foreach (var item3 in (SortedList<string, string>)variantAttributeValueList[3])
                                    {
                                        foreach (var item4 in (SortedList<string, string>)variantAttributeValueList[4])
                                        {
                                            Products p = new Products();
                                            p.ProductName = productName + "-" + item.Value + "-" + item1.Value + "-" + item2.Value + "-" + item3.Value + "-" + item4.Value;
                                            productList.Add(p);
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception) { }
            return productList;
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
