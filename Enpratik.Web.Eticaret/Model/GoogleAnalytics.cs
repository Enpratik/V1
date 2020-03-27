using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enpratik.Web.Eticaret.Model
{
    public class GoogleAnalytics
    {
        public string transactionId { get; set; }
        public string transactionAffiliation { get; set; }
        public double transactionTotal { get; set; }
        public double transactionTax { get; set; }
        public double transactionShipping { get; set; }
        public Transactionproduct[] transactionProducts { get; set; }
    }

    public class Transactionproduct
    {
        public string sku { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public double price { get; set; }
        public int quantity { get; set; }
    }
}
