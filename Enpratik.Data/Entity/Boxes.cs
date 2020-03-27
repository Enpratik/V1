using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enpratik.Data
{
    public class Boxes
    {
        public int Id { get; set; }

        public string Product { get; set; }

        public string Tampon { get; set; }

        public string ProductTitle { get; set; }

        public string Regl { get; set; }

        public string ReglDay { get; set; }

        public string Yogunluk { get; set; }

        public string YogunlukAltBaslik { get; set; }

        public string Marka { get; set; }

        public string IncePed { get; set; }

        public string icindekiler { get; set; }

        public double FiyatAylik { get; set; }

        public double Fiyat3Aylik { get; set; }

        public double Fiyat6Aylik { get; set; }

        public Nullable<DateTime> Regdate { get; set; }

        public bool IsActive { get; set; }

    }
}
