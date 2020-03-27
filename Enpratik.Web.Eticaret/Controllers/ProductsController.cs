using Enpratik.Data;
using Enpratik.Web.Eticaret.Model;
using Enpratik.Web.Eticaret.Themes.Hypedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Enpratik.Web.Eticaret.Controllers
{
    public class ProductsController : Controller
    {
        EnPratik_DataHelper db = new EnPratik_DataHelper();
        // GET: Products
        [HttpGet]
        public ActionResult Index()
        {
            var products = db.Products.Where(p => p.IsActive == true & p.Published == true).OrderBy(p=>p.DisplayOrder).ToList();
            return View(products);
        }

        [HttpPost]
        public ActionResult Index(int? id, int quantity, string variantAttributesJson)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            var data = db.Products.Where(p => p.Id == id & p.IsActive == true).FirstOrDefault();

            if (data == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);



            Current.SetBasketItem(data, quantity, variantAttributesJson);

            ViewBag.Message = "Ürün sepete eklendi";

            var products = db.Products.Where(p => p.IsActive == true & p.Published == true).OrderBy(p => p.DisplayOrder).ToList();
            return View(products);
        }

        // GET: Products
        [HttpGet]
        public ActionResult createbox()
        {
            var products = db.Products.Where(p => p.IsActive == true & p.Published == true).OrderBy(p=>p.DisplayOrder).ToList();
            return View(products);
        }
        [HttpGet]
        public ActionResult shop()
        {
            var products = db.Products.Where(p => p.IsActive == true & p.Published == true).OrderBy(p => p.DisplayOrder).ToList();
            return View(products);
        }

        // GET: Products
        [HttpGet]
        public ActionResult CategoryListIndex(string categoryUrl, int? id, string orderBy)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            var products = (from p in db.Products
                            join cx in db.Product_Category_Mapping on p.Id equals cx.ProductId into gj
                            from ca in gj.DefaultIfEmpty()
                            where p.IsActive==true & p.Published==true & ca.CategoryId == id
                            select p).OrderBy(p=>p.DisplayOrder).ToList();

            var productCategory = db.Categories.Where(c => c.Id == id).FirstOrDefault();
            if (productCategory != null)
                ViewBag.ProductCategoryName = productCategory.CategoryName;
                
            return View(products);
        }

        [HttpPost]
        public ActionResult CategoryListIndex(string categoryUrl, int? id, int productId, int quantity, string variantAttributesJson)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            var data = db.Products.Where(p => p.Id == productId & p.IsActive == true).FirstOrDefault();

            if (data == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);



            Current.SetBasketItem(data, quantity, variantAttributesJson);

            ViewBag.Message = "Ürün sepete eklendi";

            var products = (from p in db.Products
                            join cx in db.Product_Category_Mapping on p.Id equals cx.ProductId into gj
                            from ca in gj.DefaultIfEmpty()
                            where p.IsActive == true & p.Published == true & ca.CategoryId == id
                            select p).OrderBy(p => p.DisplayOrder).ToList();

            var productCategory = db.Categories.Where(c => c.Id == id).FirstOrDefault();
            if (productCategory != null)
                ViewBag.ProductCategoryName = productCategory.CategoryName;

            return View(products);
        }
        // GET: Products
        public ActionResult Detail(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            
            var data = db.Products.Where(p => p.Id == id & p.IsActive == true).FirstOrDefault();

            if(data==null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return View(data);
        }

        [HttpGet]
        public JsonResult ProductPrice(int productId, int variantId, int variantAttributeId)
        {
            double productPrice = 0;
            var productMap = db.Product_VariantAttribute_Mapping.Where(p => p.IsActive == true & p.ProductId == productId & p.VariantAttributeId == variantId & p.VariantAttributeValueId == variantAttributeId).FirstOrDefault();

            if (productMap != null) {
                productPrice = productMap.ProductPrice ?? 0;
            }
            
            return Json(productPrice, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ProductVariantImages(int productId, int variantId, int variantAttributeId)
        {
            string imageUrl = "";
            var productMap = db.Product_VariantAttribute_Mapping.Where(p => p.IsActive == true & p.ProductId == productId & p.VariantAttributeId == variantId & p.VariantAttributeValueId == variantAttributeId).FirstOrDefault();

            if (productMap != null)
            {
                imageUrl = productMap.ImageUrl;

                if (!string.IsNullOrEmpty(imageUrl))
                {
                    imageUrl = "/admin/" + (imageUrl.StartsWith("/") ? imageUrl.Substring(1, imageUrl.Length - 1) : imageUrl);
                }
            }

            return Json(imageUrl, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Detail(int? id, int quantity, string variantAttributesJson, string pPrice)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            var data = db.Products.Where(p => p.Id == id & p.IsActive == true).FirstOrDefault();
 
            if (data == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            if (!string.IsNullOrEmpty(pPrice))
                data.Price = Convert.ToDouble(pPrice.Replace(".",","));

            Current.SetBasketItem(data, quantity, variantAttributesJson);

            ViewBag.Message = "Ürün sepete eklendi";

            //return RedirectToAction("Index", "Checkout");

            return View(data);
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