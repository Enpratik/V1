using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enpratik.Data
{
    public partial class ProductBoxs
    {
        public int Id { get; set; }

        public string ProductType { get; set; }

        public string TamponType { get; set; }

        public string ProductSubTitle { get; set; }

        public string ReglDay { get; set; }

        public string ReglDaySubTitle { get; set; }

        public string DensityType { get; set; }

        public string DensitySubTitle { get; set; }

        public string BrandName { get; set; }

        public string IsThinPad { get; set; }

        public string Contents { get; set; }

        public double MonthlyPrice { get; set; }

        public double ThreeMonthsPrice { get; set; }

        public double SixMonthsPrice { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

    }
    
}
