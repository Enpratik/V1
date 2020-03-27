using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enpratik.Data
{
   public class Product_BasketItem_Temp
    {

        public int Id { get; set; }

        public string GuidId { get; set; }

        public string OrderNumber { get; set; }
        public string Token { get; set; }

        public Nullable<int> CustomerId { get; set; }

        public int ProductId { get; set; }

        public Nullable<double> Price{ get; set; }

        public int Quantity { get; set; }

        public string VariantAttributesJson { get; set; }
        public bool IsInvoice { get; set; }
        public int Status { get; set; }

        public bool IsActive { get; set; }

        public DateTime RegDate { get; set; }
        public bool IsNew { get; set; }
    }
}
