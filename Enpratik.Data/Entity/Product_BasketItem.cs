using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Enpratik.Data
{
    [Serializable]
    public class Product_BasketItem
    {

        public int Id { get; set; }

        public string OrderNumber { get; set; }

        public Nullable<int> CustomerId { get; set; }

        public int ProductId { get; set; }
        public Nullable<double> Price { get; set; }

        public int Quantity { get; set; }

        public string VariantAttributesJson { get; set; }

        public int Status { get; set; }

        public bool IsActive { get; set; }
        public bool IsInvoice { get; set; }

        public DateTime RegDate { get; set; }
        public bool IsNew { get; set; }
        public int PaymentType { get; set; }

        public Product_BasketItem()
        {
            IsNew = true;
            IsActive = true;
            RegDate = DateTime.Now;
            IsInvoice = false;
        }

        public List<VariantAttributesData> getVariantAttributesData()
        {
            try
            {
                if (string.IsNullOrEmpty(VariantAttributesJson))
                    return new List<VariantAttributesData>();

                var result = new JavaScriptSerializer().Deserialize<List<VariantAttributesData>>(this.VariantAttributesJson);

                return result;
            }
            catch (Exception)
            {
                return new List<VariantAttributesData>();
            }
        }

        public Customers GetCustomer() {
            EnPratik_DataHelper db = new Data.EnPratik_DataHelper();
            var customer = db.Customers.Where(c => c.Id == CustomerId).FirstOrDefault();
            return customer;
        }

        public Products GetProduct() {
            EnPratik_DataHelper db = new Data.EnPratik_DataHelper();
            var product = db.Products.Where(p => p.Id == ProductId).FirstOrDefault();
            return product;
        }

        public string GetStatusButtonCss() {
            switch (Status)
            {
                case 0:
                    return "info";
                case 1:
                    return "success";
                case 2:
                    return "danger";
                case 3:
                    return "warning";
                case 4:
                    return "primary";
                case 5:
                    return "default";
                default:
                    return "info";
            }
        }


        public string GetStatusText() {
            switch (Status)
            {
                case 0:
                    return "Onay Bekliyor";
                case 1:
                    return "Onaylandı";
                case 2:
                    return "Hazırlanıyor";
                case 3:
                    return "Kargoya Verildi";
                case 4:
                    return "Teslim Edildi";
                case 5:
                    return "İptal Edildi";
                default:
                    return "Onay Bekliyor";
            }
        }

    }    

    public class VariantAttributesData
    {
        public string variantName { get; set; }
        public string variantValue { get; set; }
    }
}
