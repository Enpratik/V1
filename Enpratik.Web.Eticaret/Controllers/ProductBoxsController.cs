using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Enpratik.Core;
using Enpratik.Data;
using Enpratik.Web.Eticaret.Model;
using Enpratik.Web.Eticaret.Themes.Hypedia.Models;

namespace Enpratik.AdminPanel.Controllers
{
    public class ProductBoxsController : Controller
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: ProductBoxs
        public ActionResult Index()
        {
            var products = db.Products.Where(p => p.IsActive == true & p.Published == true).OrderBy(p => p.DisplayOrder).ToList();
            ViewBag.ProductList = products;

            return View(db.ProductBoxs.ToList());
        }


        [HttpPost]
        public JsonResult BoxSteps(string basketItems, string currentSection)
        {
            if(string.IsNullOrEmpty(currentSection))
                return Json("{message:'error'}");

            if (string.IsNullOrEmpty(basketItems))
                return Json("{message:'error'}");

            var data = new JavaScriptSerializer().Deserialize<List<BoxBasketItem>>(basketItems);
            Current.SetBoxBasketItem(data);

            var boxList = Current.GetProductBoxsItem();

            var currentValue = data.Where(d => d.key == currentSection).Select(d => d.value).FirstOrDefault();

            List<ProductBoxs> resultData = new List<ProductBoxs>();
            
            BoxResponse response = new BoxResponse();

            switch (currentSection)
            {
                case "choose-product":
                    boxList = db.ProductBoxs.ToList();
                    resultData = boxList.Where(b => b.ProductType == currentValue).ToList();
                    var tamponList = resultData.Where(t => t.TamponType != "").ToList();
                    if (tamponList == null || tamponList.Count == 0)
                    {
                        var temps = resultData.GroupBy(p => p.ReglDay);
                        foreach (var item in temps)
                        {
                            response.BoxResponseItem.Add(new BoxResponseItem() { key = item.Key });
                        }

                        response.NextSection = "choose-days";
                    }
                    else
                    {
                        var temps = resultData.GroupBy(p => p.TamponType);
                        foreach (var item in temps)
                        {
                            response.BoxResponseItem.Add(new BoxResponseItem() { key = item.Key });
                        }
                        response.NextSection = "choose-tampon";
                    }
                    break;

                case "choose-tampon":
                    resultData = boxList.Where(b => b.TamponType == currentValue).ToList();
                    var temps1 = resultData.GroupBy(p => p.ReglDay);
                    foreach (var item in temps1)
                    {
                        response.BoxResponseItem.Add(new BoxResponseItem() { key = item.Key });
                    }
                    response.NextSection = "choose-days";
                    break;

                case "choose-days":
                    resultData = boxList.Where(b => b.ReglDay == currentValue).ToList();
                    var temps2 = resultData.GroupBy(p => p.DensityType);
                    foreach (var item in temps2)
                    {
                        response.BoxResponseItem.Add(new BoxResponseItem() { key = item.Key });
                    }
                    response.NextSection = "choose-densitys";
                    break;

                case "choose-densitys":
                    resultData = boxList.Where(b => b.DensityType == currentValue).ToList();
                    var temps3 = resultData.GroupBy(p => p.BrandName);
                    foreach (var item in temps3)
                    {
                        response.BoxResponseItem.Add(new BoxResponseItem() { key = item.Key });
                    }
                    response.NextSection = "choose-brands";
                    break;

                case "choose-brands":
                    resultData = boxList.Where(b => b.BrandName == currentValue).ToList();
                    var temps4 = resultData.GroupBy(p => p.IsThinPad);
                    foreach (var item in temps4)
                    {
                        response.BoxResponseItem.Add(new BoxResponseItem() { key = item.Key });
                    }
                    response.NextSection = "choose-thinped";
                    break;

                case "choose-thinped":
                    resultData = boxList.Where(b => b.IsThinPad == currentValue).ToList();
                    var temps5 = resultData.FirstOrDefault();
                    response.BoxResponseItem.Add(new BoxResponseItem() { key = temps5.MonthlyPrice.ToString() });
                    response.BoxResponseItem.Add(new BoxResponseItem() { key = temps5.ThreeMonthsPrice.ToString() });
                    response.BoxResponseItem.Add(new BoxResponseItem() { key = temps5.SixMonthsPrice.ToString() });
                    response.NextSection = "choose-subscription";
                    break;

                case "choose-subscription":

                    var boxProduct = boxList.FirstOrDefault();

                    resultData.Add(boxProduct);

                    string[] contents = boxProduct.Contents.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

                    string subscription = GetDataKey(data, "choose-subscription").Split('-')[1];

                    var htmlData = " <div class=\"row\">" +
                            "            <div class=\"col-md-offset-3 col-md-6\">" +
                            "                <div class=\"create-box-header\">" +
                            "                    <span class=\"icon icon-hy-icon-left-arrow\" onclick=\"getBack('box-is-ready')\"></span>" +
                            "                    <h3 class=\"text-center process-headline\">Kutunun İçeriği</h3>" +
                            "                    <p class=\"subtitle caption-small-wrapper process-headline\">İçerisine koyduğumuz sürprizlerle birlikte kutun gönderime hazır…</p>" +
                            "                </div>" +
                            "            </div>" +
                            "        </div>" +
                            "        <div class=\"row\">" +
                            "            <div class=\"col-md-offset-2 col-md-4\">" +
                            "                <table class=\"product-summary-table\">";
                                                    foreach (var item in contents)
                                                    {
                                                        htmlData += "<tr>" +
                                                            "           <td width=\"50%\">" + FormatProductContent(item, false) + " :</td>" +
                                                            "           <td>"+FormatProductContent(item, true)+"</td>" +
                                                            "        </tr>";
                                                    }
                           

                            htmlData+="      </table>"+
                            "            </div>" +
                            "            <div class=\"col-md-4\">" +
                            "                <table class=\"product-summary-table hy-primary-light-bg\">" +
                            "                    <tr>" +
                            "                        <td>Gönderim Süresi :</td>" +
                            "                        <td>HER AY</td>" +
                            "                    </tr>" +
                            "                    <tr>" +
                            "                        <td>Ödeme Planı :</td>" +
                            "                        <td>"+ (subscription=="1" ? "" : (subscription+" ")) + "AYLIK</td>" +
                            "                    </tr>" +
                            "                    <tr>" +
                            "                        <td>Kargo Ücreti :</td>" +
                            "                        <td>0 TL</td>" +
                            "                    </tr>" +
                            "                    <tr>" +
                            "                        <td>Aylık Fiyat :</td>" +
                            "                        <td>"+GetProductPrice(boxProduct, subscription, true)+" TL / AY</td>" +
                            "                    </tr>" +
                            "                    <tr>" +
                            "                        <td>Ek Ürün Fiyatı :</td>" +
                            "                        <td>0 TL</td>" +
                            "                    </tr>" +
                            "                    <tr>" +
                            "                        <td>Toplam Fiyat :</td>" +
                            "                        <td>" + GetProductPrice(boxProduct, subscription) + " TL</td>" +
                            "                    </tr>" +
                            "                </table>" +
                            "            </div>" +
                            "        </div>" +
                            "        <div class=\"row\">" +
                            "            <div class=\"col-md-offset-3 col-md-6\">" +
                            "                <p class=\"summary-price caption\">Ödeyeceğin Tutar: "+ GetProductPrice(boxProduct, subscription) + " TL</p>";

                    if (subscription != "1")
                    {
                                htmlData += "<p class=\"summary-attention hy-brown\">Kutun <span>"+subscription+"</span> ay boyunca <span>HER AY</span> kapında olacak</p>" +
                                            "<p class=\"attention-text\">* İstediğin zaman kutunun içeriğini değiştirebilir ya da iptal edebilirsin.</p>";
                    }
                            htmlData+="    <a class=\"btn btn-primary\" href=\"/checkout\">DEVAM ET</a>" +
                            "            </div>" +
                            "        </div>";
                    
                    response.BoxResponseItem.Add(new BoxResponseItem() { key = htmlData });
                    //

                    response.NextSection = "choose-additional-product";
                    break;

                case "choose-additional-product":
                    response.NextSection = "box-is-ready";
                    //response.NextSection = "";
                    break;

                default:

                    break;
            }

            Current.SetProductBoxsItem(resultData);
            
            return Json(response);
        }


        [HttpPost]
        public JsonResult HypadiaUrunListesi() {

            var productCount = db.Products.Where(p => p.IsActive == true & p.Published == true).ToList().Count();

            return Json(productCount);
        }

        private string FormatProductContent(string text, bool isNumber) {

            char[] arr = text.ToArray();

            string result = "";

            foreach (var item in arr)
            {
                if (!Functions.isNumeric(item.ToString()))
                {
                    if (isNumber)
                        result += item.ToString();
                }
                else
                {
                    if (isNumber)
                        continue;
                    result += item.ToString();
                }
            }

            return result.Trim();
        }
        
        private string GetProductPrice(ProductBoxs boxProduct, string subscription)
        {
            if (subscription == "1")
                return Math.Ceiling(boxProduct.MonthlyPrice).ToString();
            if (subscription == "3")
                return Math.Ceiling(boxProduct.ThreeMonthsPrice).ToString();
            if (subscription == "6")
                return Math.Ceiling(boxProduct.SixMonthsPrice).ToString();

            return "";
        }

        private string GetProductPrice(ProductBoxs boxProduct, string subscription, bool ii)
        {
            double price = Convert.ToDouble(GetProductPrice(boxProduct, subscription));
            price = price / Convert.ToDouble(subscription);
            return Math.Ceiling(price).ToString();
        }

        private string GetDataKey(List<BoxBasketItem> data, string key)
        {
            return data.Where(d => d.key == key).Select(d => d.value).FirstOrDefault();
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
