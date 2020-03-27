using Enpratik.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enpratik.Web.Eticaret.Model
{
    public class Current
    {
        public static Customers User
        {
            get { return GetUser(); }
            set { HttpContext.Current.Session["Current_User"] = value; }
        }

        private static Customers GetUser()
        {
            if (HttpContext.Current == null)
                return null;

            if (HttpContext.Current.Session == null)
                return null;

            Customers user = HttpContext.Current.Session["Current_User"] as Customers;

            if (user == null)
                return null;

            Current.User = user;

            return user;
        }

        public static void RemoveSessionUser()
        {
            HttpContext.Current.Session.Remove("Current_User");
        }

        public static OrderUserAddress OrderUserAddress
        {
            get
            {
                return GetOrderUserAddress();
            }
            set
            {
                HttpContext.Current.Session["Current_OrderUserAddress"] = value;
            }
        }

        private static OrderUserAddress GetOrderUserAddress()
        {

            if (HttpContext.Current == null)
                return null;

            if (HttpContext.Current.Session == null)
                return null;

            OrderUserAddress orderUserAddress = HttpContext.Current.Session["Current_OrderUserAddress"] as OrderUserAddress;

            if (orderUserAddress == null)
                return null;

            Current.OrderUserAddress = orderUserAddress;

            return orderUserAddress;
        }

        public static List<Product_BasketItem> GetBasketItem()
        {
            if (HttpContext.Current == null)
                return new List<Product_BasketItem>();

            if (HttpContext.Current.Session == null)
                return new List<Product_BasketItem>();

            List<Product_BasketItem> baskets = HttpContext.Current.Session["Current_Basket"] as List<Product_BasketItem>;

            if (baskets == null)
                baskets = new List<Product_BasketItem>();

            return baskets;
        }
        public static void RemoveBasketItem()
        {
            try
            {
                HttpContext.Current.Session.Remove("Current_Basket");
            }
            catch
            {

            }
        }
        public static List<Product_BasketItem> SetBasketItem(Products product, int quantity, string variantAttributesJson)
        {
            if (HttpContext.Current == null)
                return null;

            if (HttpContext.Current.Session == null)
                return null;

            List<Product_BasketItem> baskets = HttpContext.Current.Session["Current_Basket"] as List<Product_BasketItem>;

            if (baskets == null)
                baskets = new List<Product_BasketItem>();

            if (baskets.Any(b => b.ProductId == product.Id & b.VariantAttributesJson == variantAttributesJson))
            {
                baskets.Where(b => b.ProductId == product.Id).ToList().ForEach(b => b.Quantity = (b.Quantity + quantity));
            }
            else
            {
                Product_BasketItem item = new Product_BasketItem();
                item.ProductId = product.Id;
                item.Price = product.Price;
                item.Quantity = quantity;
                item.VariantAttributesJson = variantAttributesJson;
                baskets.Add(item);
            }

            HttpContext.Current.Session["Current_Basket"] = baskets;

            return baskets;
        }

        public static void SetProductBoxsItem(List<ProductBoxs> productBoxs)
        {
            if (HttpContext.Current == null)
                return;

            if (HttpContext.Current.Session == null)
                return;

            HttpContext.Current.Session["SelectProductBoxsItem"] = productBoxs;

        }

        public static List<ProductBoxs> GetProductBoxsItem()
        {
            if (HttpContext.Current == null)
                return new List<ProductBoxs>();

            if (HttpContext.Current.Session == null)
                return new List<ProductBoxs>();

            if (HttpContext.Current.Session["SelectProductBoxsItem"] == null)
                return new List<ProductBoxs>();

            List<ProductBoxs> productBoxs = HttpContext.Current.Session["SelectProductBoxsItem"] as List<ProductBoxs>;

            return productBoxs;
        }


        //public static void SetBoxBasketItem(List<BoxBasketItem> boxBasketItem)
        //{
        //    if (HttpContext.Current == null)
        //        return;

        //    if (HttpContext.Current.Session == null)
        //        return;

        //    HttpContext.Current.Session["BoxBasketItemKeyValue"] = boxBasketItem;

        //}

        //public static List<BoxBasketItem> GetBoxBasketItem()
        //{
        //    if (HttpContext.Current == null)
        //        return new List<BoxBasketItem>();

        //    if (HttpContext.Current.Session == null)
        //        return new List<BoxBasketItem>();

        //    if (HttpContext.Current.Session["BoxBasketItemKeyValue"] == null)
        //        return new List<BoxBasketItem>();

        //    return HttpContext.Current.Session["BoxBasketItemKeyValue"] as List<BoxBasketItem>;

        //}

        public static void SetIyzicoToken(string token)
        {

            if (HttpContext.Current == null)
                return;

            if (HttpContext.Current.Session == null)
                return;
            HttpContext.Current.Session["IyzicoToken"] = token;
        }
        public static string GetIyzicoToken()
        {

            if (HttpContext.Current == null)
                return "";

            if (HttpContext.Current.Session == null)
                return "";

            return HttpContext.Current.Session["IyzicoToken"] as string;
        }

        public static void RemoveIyzicoToken()
        {
            HttpContext.Current.Session.Remove("IyzicoToken");
        }

        public static void RemoveCurrentSectionNameList()
        {
            if (HttpContext.Current == null)
                return;

            if (HttpContext.Current.Session == null)
                return;

            HttpContext.Current.Session.Remove("CurrentSectionNameList");
        }

        public static void SetCurrentSectionNameList(List<CurrentSections> liste)
        {
            if (HttpContext.Current == null)
                return;

            if (HttpContext.Current.Session == null)
                return;

            HttpContext.Current.Session["CurrentSectionNameList"] = liste;
        }
        public static List<CurrentSections> GetCurrentSectionNameList()
        {
            List<CurrentSections> liste = new List<CurrentSections>();


            if (HttpContext.Current == null)
                return liste;

            if (HttpContext.Current.Session == null)
                return liste;

            return HttpContext.Current.Session["CurrentSectionNameList"] as List<CurrentSections>;

        }


        //public static BoxBasket SetBoxBasketItem(List<BoxBasketItem> boxItems, List<Product_BasketItem> productItems, string currentSection) {

        //    if (HttpContext.Current == null)
        //        return null;

        //    if (HttpContext.Current.Session == null)
        //        return null;

        //    BoxBasket baskets = HttpContext.Current.Session["Box_Basket"] as BoxBasket;

        //    if (baskets == null)
        //        baskets = new BoxBasket();

        //    baskets.Product_Box_BasketItems = boxItems;

        //    baskets.CurrentSection = currentSection;

        //    HttpContext.Current.Session["Box_Basket"] = baskets;

        //    return baskets;
        //}
        //public static BoxBasket GetBasketItem(string item)
        //{
        //    if (HttpContext.Current == null)
        //        return new BoxBasket();

        //    if (HttpContext.Current.Session == null)
        //        return new BoxBasket();

        //    BoxBasket baskets = HttpContext.Current.Session["Box_Basket"] as BoxBasket;

        //    if (baskets == null)
        //        baskets = new BoxBasket();

        //    return baskets;
        //}





    }

    [Serializable]
    public class CurrentSections
    {
        public string CurrentSection { get; set; }
        public string NextSection { get; set; }
    }
}