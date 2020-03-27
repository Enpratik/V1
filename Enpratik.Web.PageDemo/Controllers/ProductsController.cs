using Enpratik.Data;
using Enpratik.Web.Eticaret.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Enpratik.Web.PageDemo.Controllers
{
    public class ProductsController : Controller
    {
        EnPratik_DataHelper db = new EnPratik_DataHelper();
        int sayfaBoyutu = 9;

        // GET: Products
        public ActionResult Index(int? sayfano, string categoryUrl, int? categoryId, string brandUrl, int? brandId, double? priceMin, double? priceMax)
        {
            List<Products> productList = getData();
            if (sayfano == null)
            {
                productList = db.Products.Where(p => p.IsActive == true & p.Published == true).OrderByDescending(p => p.Id).Take(sayfaBoyutu).ToList();
            }
            else
            {
                productList = db.Products.Where(p => p.IsActive == true & p.Published == true).OrderByDescending(p => p.Id).Skip(sayfaBoyutu * sayfano.Value).Take(sayfaBoyutu).ToList();
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Views/Shared/_ProductListControl.cshtml", productList);
            }

            return View(productList ?? new List<Products>());

        }

        private List<Products> getData()
        {
           return db.Products.Where(p => p.IsActive == true & p.Published == true).ToList();
        }

        public ActionResult Detail(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            var data = db.Products.Where(p => p.Id == id & p.IsActive == true).FirstOrDefault();

            if (data == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            //ViewBag.RelatedProducts = db.RelatedProducts.Where(p=>p.ProductId = )

            return View(data);
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
                data.Price = Convert.ToDouble(pPrice.Replace(".", ","));

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