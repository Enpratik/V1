using Enpratik.Core;
using Enpratik.Data;
using Enpratik.Web.Eticaret.Model;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Enpratik.Web.Eticaret.Controllers
{
    public class CheckoutController : Controller
    {
        EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: Checkout
        public ActionResult Index()
        {
            if (Current.User == null)
            {
                Session["RedirectCheckout"] = "1";
                return Redirect("hesabim/uye-girisi");
            }
            var basketItems = Current.GetBasketItem();
            if (basketItems == null || basketItems.Count == 0)
            {
                return Redirect("/");
            }

            return View(basketItems);
        }

        public ActionResult ResultKapidaOdeme()
        {
            ViewBag.ResultText = "Kapıda Ödeme ile  siparişiniz tarafımıza ulaştı. En kısa zamanda sizinle iletişime geçeceğiz.";
            return View();
        }

        // GET: Checkout
        public ActionResult Result(string id)
        {
            var usr = Current.User;
            
            ViewBag.ResultText = "HATA! Ödeme işlemi gerçekleşmedi.";

            
            List<Product_BasketItem> basketItems = Current.GetBasketItem();
            
            basketItems = new List<Product_BasketItem>();

            var tempProductBasketItems = db.Product_BasketItem_Temp.Where(p => p.GuidId == id).ToList();

            if (tempProductBasketItems == null)
            {
                ViewBag.ResultText = "Ödeme adımında hata meydana geldi. Lütfen tekrar deneyiniz!";
                return View();
            }

            string iyzicoToken = tempProductBasketItems.Select(p => p.Token).FirstOrDefault();
            var basketID = tempProductBasketItems.Select(p => p.OrderNumber).FirstOrDefault();

            foreach (var item in tempProductBasketItems)
            {
                Product_BasketItem p = new Product_BasketItem();

                p.OrderNumber = item.OrderNumber;
                p.CustomerId = item.CustomerId;
                p.ProductId = item.ProductId;
                p.Price = item.Price;
                p.Quantity = item.Quantity;
                p.VariantAttributesJson = item.VariantAttributesJson;
                p.Status = item.Status;
                p.IsActive = item.IsActive;
                p.RegDate = item.RegDate;
                p.IsNew = item.IsNew;
                p.IsInvoice = item.IsInvoice;
                p.PaymentType = 0;

                basketItems.Add(p);
            }


            Options options = new Options();
            options.ApiKey = ConfigurationManager.AppSettings["IyzicoApiKey"];
            options.SecretKey = ConfigurationManager.AppSettings["IyzicoSecretKey"];
            options.BaseUrl = ConfigurationManager.AppSettings["IyzicoBaseUrl"];

            //TEOFARM
            //Options options = new Options();
            //options.ApiKey = "4IRE81pjjw7Kcpk1BwmrfRywRWC6m8Yh";
            //options.SecretKey = "q5Tjl6PviX5AI4R0y71Xd8ny6AADGstM";
            //options.BaseUrl = "https://api.iyzipay.com";

            //AllAboutBride
            //Options options = new Options();
            //options.ApiKey = "oMAXdrkozlZyGtwvuEQHckAQ8iDHK7LB";
            //options.SecretKey = "lLr0EOUml1mjZhLjWGDtvhtdZUpEjdAx";
            //options.BaseUrl = "https://api.iyzipay.com";

            RetrieveCheckoutFormRequest request = new RetrieveCheckoutFormRequest();
            request.Token = iyzicoToken;

            CheckoutForm checkoutForm = CheckoutForm.Retrieve(request, options);
            if (checkoutForm == null)
            {
                ViewBag.ResultText = "Ödeme adımında banka verileri alınamadı. Lütfen tekrar deneyiniz!";
                return View();
            }
            if (checkoutForm.PaymentStatus == null) {
                ViewBag.ResultText = "Ödeme sonuç bilgisi bankadan alınamadı. Lütfen tekrar deneyiniz!";
                return View();
            }

            if (checkoutForm.PaymentStatus.Contains("SUCCESS"))
            {
                ViewBag.ResultText = "Ödemen Başarıyla Alındı. Siparişin en kısa zamanda kargoya verilecektir.";

                db.Product_BasketItem.AddRange(basketItems);
                db.SaveChanges();


                OrderUserAddressesTemp ua = db.OrderUserAddressesTemp.Where(u => u.GuidId == id).FirstOrDefault();

                OrderUserAddress oUser = new OrderUserAddress();
                oUser.FirstName = ua.FirstName;
                oUser.LastName = ua.LastName;
                oUser.GsmNumber = ua.GsmNumber;
                oUser.IdentityNumber = ua.IdentityNumber;
                oUser.Mail = ua.Mail;
                oUser.TeslimatAdres = ua.TeslimatAdres;
                oUser.FaturaAdres = ua.FaturaAdres;
                oUser.Tarih = DateTime.Now;
                oUser.OrderBasketId = ua.OrderBasketId;
                db.OrderUserAddress.Add(oUser);
                db.SaveChanges();


                double subTotal = 0;
                double cargoTotal = 20;

                string body = "<table style=\"width:100%; font:500 13px arial, verdana\">" +
                               "    <tr>" +
                               "		<td>" +
                               "            <h3>Merhaba {user}</h3>" +
                               "            <p>" +
                               "				Sitemizden kredi kartı ile satış işlemini gerçekleştirdiniz." +
                               "            </p>" +
                               "            <p>" +
                               "				SİPARİŞ NUMARANIZ : {orderNumber}" +
                               "            </p>" +
                               "		</td>" +
                               "	</tr>";

                foreach (var item in basketItems)
                {

                    string variantAttr = "";

                    foreach (var variant in item.getVariantAttributesData())
                    {
                        variantAttr += "<span style=\"font-size:12px;\"> (" + variant.variantName + " : " + variant.variantValue + ")</span>";
                    }

                    var product = item.GetProduct();
                    body += "<tr>" +
                                 "<td>" + product.ProductName + variantAttr+" × <strong>" + item.Quantity + "</strong></td>" +
                                 "<td>" + Enpratik.Core.Functions.MoneyFormat(2, item.Price * item.Quantity) + " ₺</td>" +
                             "</tr>";
                    subTotal += (Convert.ToDouble(item.Price) * item.Quantity);
                }

                body += "<tr>" +
                                        "    <td>Kargo Ücreti:</td>" +
                                        "    <td>";
                if (subTotal > 150)
                {
                    cargoTotal = 0;
                    body += "0 ₺";
                }
                else
                {
                    body += cargoTotal + " ₺";
                }
                body += "    </td>" +
                   "</tr>" +
                   "<tr>" +
                   "    <td>Toplam:</td>" +
                   "    <td>";
                body += (subTotal + cargoTotal) + "₺";
                body += "    </td>" +
                        "</tr>" +
                        "</table>";


                body = body.Replace("{user}", oUser.FirstName + " " + oUser.LastName);
                body = body.Replace("{orderNumber}", basketID);

                MailHelper mail = new MailHelper();
                mail.SendMail(oUser.Mail, "Siparişiniz Alındı!", body);
                var infoMailAddress = ConfigurationManager.AppSettings["SendOrderMailAddress"];
                mail.SendMail(infoMailAddress, "Yeni Sipariş Alındı!", body);

                Current.RemoveIyzicoToken();

                Current.RemoveBasketItem();
            }
            else {
                ViewBag.ResultText = "Ödeme adımında sorun meydana geldi. Lütfen tekrar deneyiniz.";
            }
            
            return View();
        }

        // GET: Checkout
        [HttpPost]
        public ActionResult Index(string Name, string Surname, string GsmNumber, string IdentityNumber, string Email, string TeslimatAdres, string FaturaAdres, string subPrice, string totalPrice, string chkMesafeliSatisSozlesmesi, string IsInvoice, string PaymentType)
        {
            if (string.IsNullOrEmpty(IsInvoice))
                IsInvoice = "0";

           // PaymentType = "0"; // AAB için

            string id = Guid.NewGuid().ToString();

            var basketItems = Current.GetBasketItem();

            if (basketItems == null || basketItems.Count == 0)
            {
                return Redirect("/");
            }

            if (string.IsNullOrEmpty(chkMesafeliSatisSozlesmesi))
            {
                ViewBag.ErrorMessage = "Lütfen mesafeli satış sözleşmesini okuyup, onaylayınız!";
                return View(basketItems);
            }

            if (String.IsNullOrEmpty(Name) || String.IsNullOrEmpty(Surname) || String.IsNullOrEmpty(GsmNumber) || String.IsNullOrEmpty(IdentityNumber) || String.IsNullOrEmpty(Email) || String.IsNullOrEmpty(TeslimatAdres) || String.IsNullOrEmpty(FaturaAdres) || String.IsNullOrEmpty(totalPrice))
            {
                ViewBag.ErrorMessage = "Lütfen bütün alanları doldurunuz!";
                return View(basketItems);
            }
            
            var basketID = GetBasketID();

            Session["BasketId"] = basketID;

            OrderUserAddress oUser = new OrderUserAddress();
            oUser.FirstName = Name;
            oUser.LastName = Surname;
            oUser.GsmNumber = GsmNumber;
            oUser.IdentityNumber = IdentityNumber;
            oUser.Mail = Email;
            oUser.TeslimatAdres = TeslimatAdres;
            oUser.FaturaAdres = FaturaAdres;
            oUser.Tarih = DateTime.Now;
            oUser.OrderBasketId = basketID;
            Current.OrderUserAddress = oUser;

            if (PaymentType == "1")
            {
                // kapıda ödeme
                basketItems.ForEach(b => b.CustomerId = Current.User.Id);
                basketItems.ForEach(b => b.OrderNumber = basketID);
                basketItems.ForEach(b => b.PaymentType = 1);

                db.Product_BasketItem.AddRange(basketItems);
                db.SaveChanges();
                
                oUser.OrderBasketId = basketID;
                db.OrderUserAddress.Add(oUser);
                db.SaveChanges();

                // send mail
                double subTotal = 0;
                double cargoTotal = 20;

                string body = "<table style=\"width:100%; font:500 13px arial, verdana\">" +
                            "    <tr>" +
                            "		<td colspan=\"2\">" +
                            "            <h3>Merhaba {user}</h3>" +
                            "            <p>" +
                            "				Sitemizden kapıda ödeme ile alışveriş işlemini gerçekleştirdiniz." +
                            "            </p>" +
                            "            <p>" +
                            "				SİPARİŞ NUMARANIZ : {orderNumber}" +
                            "            </p>" +
                            "		</td>" +
                            "	</tr>";

                foreach (var item in basketItems)
                {
                    string variantAttr = "";

                    foreach (var variant in item.getVariantAttributesData())
                    {
                        variantAttr += "<span style=\"font-size:12px;\"> (" + variant.variantName + " : " + variant.variantValue + ")</span>";
                    }

                        var product = item.GetProduct();
                    body += "<tr>" +
                                 "<td>"+product.ProductName+  variantAttr + "× <strong>"+item.Quantity +"</strong></td>" +
                                 "<td>"+Enpratik.Core.Functions.MoneyFormat(2, product.Price * item.Quantity)+" ₺</td>" +
                             "</tr>";
                    subTotal += (Convert.ToDouble(product.Price) * item.Quantity);
                }

                body += "<tr>" +
                                        "    <td>Kargo Ücreti:</td>" +
                                        "    <td>";
                if (subTotal > 150)
                {
                    cargoTotal = 0;
                    body += "0 ₺";
                }
                else
                {
                    body += cargoTotal + " ₺";
                }
                body += "    </td>" +
                   "</tr>" +
                   "<tr>" +
                   "    <td>Toplam:</td>" +
                   "    <td>";
                body += (subTotal + cargoTotal) + "₺";
                body += "    </td>" +
                        "</tr>" +
                        "</table>";

                body = body.Replace("{user}", Current.User.FirstName + " " + Current.User.LastName);
                body = body.Replace("{orderNumber}", basketID);

                MailHelper mail = new MailHelper();
                mail.SendMail(Current.User.Mail, "Siparişiniz Alındı!", body);
                var infoMailAddress = ConfigurationManager.AppSettings["SendOrderMailAddress"];
                mail.SendMail(infoMailAddress, "Yeni Sipariş Alındı!", body);

                Current.RemoveBasketItem();

                return RedirectToAction("ResultKapidaOdeme");
            }


            // temp address
            OrderUserAddressesTemp ua = new OrderUserAddressesTemp();
            ua.GuidId = id;
            ua.OrderBasketId = basketID;
            ua.FirstName = Name;
            ua.LastName = Surname;
            ua.GsmNumber = GsmNumber;
            ua.IdentityNumber = IdentityNumber;
            ua.Mail = Email;
            ua.TeslimatAdres = TeslimatAdres;
            ua.FaturaAdres = FaturaAdres;
            ua.Tarih = DateTime.Now;
            db.OrderUserAddressesTemp.Add(ua);
            db.SaveChanges();


            Options options = new Options();
            options.ApiKey = ConfigurationManager.AppSettings["IyzicoApiKey"];
            options.SecretKey = ConfigurationManager.AppSettings["IyzicoSecretKey"];
            options.BaseUrl = ConfigurationManager.AppSettings["IyzicoBaseUrl"];

            //AllAboutBride
            //Options options = new Options();
            //options.ApiKey = "oMAXdrkozlZyGtwvuEQHckAQ8iDHK7LB";
            //options.SecretKey = "lLr0EOUml1mjZhLjWGDtvhtdZUpEjdAx";
            //options.BaseUrl = "https://api.iyzipay.com";

            //Teofarm
            //Options options = new Options();
            //options.ApiKey = "4IRE81pjjw7Kcpk1BwmrfRywRWC6m8Yh";
            //options.SecretKey = "q5Tjl6PviX5AI4R0y71Xd8ny6AADGstM";
            //options.BaseUrl = "https://api.iyzipay.com";


            CreateCheckoutFormInitializeRequest request = new CreateCheckoutFormInitializeRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = "";
            request.Price = subPrice.Replace(",", ".");
            request.PaidPrice = totalPrice.Replace(",", ".");   // kargo ücreti eklenmiş hali
            request.Currency = Currency.TRY.ToString();
            request.BasketId = basketID;
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();
            request.CallbackUrl = ConfigurationManager.AppSettings["WebSiteUrl"] + Url.Action("Result", "Checkout", new { id=id });
            //request.CallbackUrl = ConfigurationManager.AppSettings["WebSiteUrl"] + Url.Action("Result", "Checkout");


            List<int> enabledInstallments = new List<int>();
            enabledInstallments.Add(2);
            enabledInstallments.Add(3);
            enabledInstallments.Add(6);
            enabledInstallments.Add(9);
            request.EnabledInstallments = enabledInstallments;

            Buyer buyer = new Buyer();
            buyer.Id = Current.User.Id.ToString();
            buyer.Name = Name;
            buyer.Surname = Surname;
            buyer.GsmNumber = GsmNumber;
            buyer.Email = Email;
            buyer.IdentityNumber = IdentityNumber;
            buyer.LastLoginDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            buyer.RegistrationDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            buyer.RegistrationAddress = TeslimatAdres;
            buyer.Ip = Request.UserHostAddress;
            buyer.City = "Istanbul";
            buyer.Country = "Turkey";
            buyer.ZipCode = "";
            request.Buyer = buyer;

            Address shippingAddress = new Address();
            shippingAddress.ContactName = Name+" " +Surname;
            shippingAddress.City = "Istanbul";
            shippingAddress.Country = "Turkey";
            shippingAddress.Description = TeslimatAdres;
            shippingAddress.ZipCode = "";
            request.ShippingAddress = shippingAddress;

            Address billingAddress = new Address();
            billingAddress.ContactName = Name + " " + Surname;
            billingAddress.City = "Istanbul";
            billingAddress.Country = "Turkey";
            billingAddress.Description = FaturaAdres;
            billingAddress.ZipCode = "34742";
            request.BillingAddress = billingAddress;

            List<BasketItem> baskets = new List<BasketItem>();
            foreach (var item in basketItems)
            {
                var product = item.GetProduct();
                BasketItem firstBasketItem = new BasketItem();
                firstBasketItem.Id = item.ProductId.ToString();
                firstBasketItem.Name = product.ProductName;
                firstBasketItem.Category1 = "ürün";
                firstBasketItem.ItemType = BasketItemType.PHYSICAL.ToString();
                firstBasketItem.Price = (item.Price*Convert.ToDouble(item.Quantity)).ToString().Replace(",", "."); 
                baskets.Add(firstBasketItem);
            }
            request.BasketItems = baskets;

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            CheckoutFormInitialize checkoutFormInitialize = CheckoutFormInitialize.Create(request, options);
            ViewBag.CheckoutFormContent = checkoutFormInitialize.CheckoutFormContent;

            Current.SetIyzicoToken(checkoutFormInitialize.Token);

            
            List<Product_BasketItem_Temp> tempProductBasketItems = new List<Product_BasketItem_Temp>();

            foreach (var item in basketItems)
            {
                Product_BasketItem_Temp p = new Product_BasketItem_Temp();
                p.GuidId = id;
                p.Token = checkoutFormInitialize.Token;
                p.OrderNumber = basketID;
                p.CustomerId = Current.User.Id;
                p.ProductId = item.ProductId;
                p.Price = item.Price;
                p.Quantity = item.Quantity;
                p.VariantAttributesJson = item.VariantAttributesJson;
                p.Status = item.Status;
                p.IsActive = item.IsActive;
                p.RegDate = item.RegDate;
                p.IsNew = item.IsNew;
                p.IsInvoice = (IsInvoice == "1" ? true : false);

                tempProductBasketItems.Add(p);
            }

            db.Product_BasketItem_Temp.AddRange(tempProductBasketItems);
            db.SaveChanges();
            
            return View(basketItems);
        }

        private string GetBasketID()
        {
            var basketId = "S" + DateTime.Now.ToString("ddMMyyyyHHmmss");
            return basketId;
        }
        
        private void GetGoogleAnalyticCodes(string basketId, string customerName, List<Product_BasketItem> basketItems)
        {
            GoogleAnalytics ga = new GoogleAnalytics();
            ga.transactionId = basketId;
            ga.transactionAffiliation = customerName;
            List<Transactionproduct> transactionProducts = new List<Transactionproduct>();

            double subTotal = 0;
            double cargoTotal = 20;

            foreach (var item in basketItems)
            {
                var product = item.GetProduct();
                transactionProducts.Add(
                    new Transactionproduct()
                    {
                        sku = "sku-" + product.Id.ToString(),
                        name = product.ProductName,
                        category = "",
                        price = product.Price.Value,
                        quantity = item.Quantity,
                    });

                subTotal += (Convert.ToDouble(item.Price) * item.Quantity);
                
            }

            if (subTotal > 150)
                cargoTotal = 0;

            ga.transactionTotal = subTotal;
            ga.transactionTax = 0;
            ga.transactionShipping = cargoTotal;
            ga.transactionProducts = transactionProducts.ToArray();

            var googleJson = JsonConvert.SerializeObject(ga);

            ViewBag.GoogleAnalyticCode = googleJson;
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