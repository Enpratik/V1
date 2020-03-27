using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enpratik.Data
{
    public partial class Sliders
    {
        public int Id { get; set; }

        [Display(Name = "Dil")]
        [Required(ErrorMessage = "Dil Seçiniz!")]
        public int LanguageId { get; set; }

        [Display(Name = "Slider Adı")]
        [Required(ErrorMessage = "Slider Adı Giriniz!")]
        public string SliderName { get; set; }

        [Display(Name = "Genişlik")]
        public Nullable<double> Width { get; set; }

        [Display(Name = "Yükseklik")]
        public Nullable<double> Height { get; set; }

        [Display(Name = "Geçiş Efekti")]
        public Nullable<int> Effect { get; set; }

        [Display(Name = "Sıra Noktaları Göster")]
        public bool Arrows { get; set; }

        [Display(Name = "Otomatik Yürüt")]
        public bool Navigations { get; set; }

        [Display(Name = "Oluşturulma Tarihi")]
        public Nullable<DateTime> CreatedDate { get; set; }

        [Display(Name = "Oluşturan Kişi")]
        public Nullable<int> CreatedUserId { get; set; }

        [Display(Name = "Güncelleme Tarihi")]
        public Nullable<DateTime> UpdatedDate { get; set; }

        [Display(Name = "Güncelleyen Kişi")]
        public Nullable<int> UpdatedUserId { get; set; }

        public Nullable<DateTime> DeletedDate { get; set; }

        public Nullable<int> DeletedUserId { get; set; }

        [Display(Name = "Aktif mi")]
        public Nullable<bool> IsActive { get; set; }

        public static Sliders initialize = new Sliders();

        public Sliders() {
            CreatedDate = DateTime.Now;
            CreatedUserId = 1;
            IsActive = true;
        }
    }
}
