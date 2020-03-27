using Enpratik.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enpratik.Web.Eticaret.Themes.Hypedia.Models
{
    public class BoxBasket
    {
        public string CurrentSection { get; set; }
        public List<Product_BasketItem> Product_BasketItems { get; set; }
        public List<BoxBasketItem> Product_Box_BasketItems { get; set; }

        public BoxBasket() {
            Product_BasketItems = new List<Product_BasketItem>();
            Product_Box_BasketItems = new List<BoxBasketItem>();
        }
    }

    public class BoxBasketItem {
        public string key { get; set; }
        public string value { get; set; }
    }

    public class BoxResponse
    {
        public string NextSection { get; set; }
        public List<BoxResponseItem> BoxResponseItem{ get; set; }
        public BoxResponse() {
            BoxResponseItem = new List<BoxResponseItem>();
        }
    }

    public class BoxResponseItem
    {
        public string key { get; set; }
    }

}