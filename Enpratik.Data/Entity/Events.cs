using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enpratik.Data
{
    public class Events
    {
        public int Id { get; set; }
        [Display(Name = "Dil")]
        public Nullable<int> LanguageId { get; set; }
        [Display(Name = "Başlık")]
        [Required(ErrorMessage = "Etkinlik Başlığı Giriniz!")]
        public string EventName { get; set; }

        [Display(Name = "Etkinlik Resmi")]
        public string EventImages { get; set; }

        [Display(Name = "Kontenjan")]
        public int EventQuota { get; set; }

        [Display(Name = "Fiyat")]
        public double EventPrice { get; set; }

        public int UrlId { get; set; }

        [Display(Name = "Kısa Açıklama")]
        public string ShortDescription { get; set; }

        [Display(Name = "Etkinlik İçeriği")]
        public string FullDescription { get; set; }
        [Display(Name = "Yayınla")]
        public Nullable<bool> IsPublished { get; set; }
        [Display(Name = "Seo Anahtar Kelimeler")]
        public string MetaKeywords { get; set; }
        [Display(Name = "Seo Açıklama")]
        public string MetaDescription { get; set; }
        [Display(Name = "Seo Site Başlığı")]
        public string MetaTitle { get; set; }
        [Display(Name = "Oluşturulma Tarihi")]
        public Nullable<System.DateTime> CreatedDate { get; set; }
        [Display(Name = "Oluşturan Kişi")]
        public Nullable<int> CreatedUserId { get; set; }
        [Display(Name = "Güncelleme Tarihi")]
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        [Display(Name = "Güncelleyen Kişi")]
        public Nullable<int> UpdatedUserId { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<int> DeletedUserId { get; set; }
        [Display(Name = "Aktif mi")]
        public Nullable<bool> IsActive { get; set; }

        public string getUrl()
        {
            EnPratik_DataHelper db = new EnPratik_DataHelper();
            var url = db.WebSiteUrls.Where(w => w.Id == UrlId).Select(w => w.Url).FirstOrDefault();
            return url;
        }

        public Events()
        {
            CreatedDate = DateTime.Now;
            CreatedUserId = 1;
            IsActive = true;
        }
    }
}
