using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enpratik.Data
{
    public class ProductBox
    {
        public int Id { get; set; }

        [Display(Name = "Kutu Adı")]
        [Required(ErrorMessage = "Kutu Adı Giriniz!")]
        public string BoxName { get; set; }
        [Display(Name = "Kutu Açıklaması")]
        public string BoxDescription { get; set; }

        public int UrlId { get; set; }

        [Display(Name = "Kutu Fiyatı")]
        [Required(ErrorMessage = "Kutu Fiyatı Giriniz!")]
        public double Price { get; set; }

        [Display(Name = "KDV")]
        public Nullable<int> Tax { get; set; }
        [Display(Name = "Seo Anahtar Kelimeler")]
        public string MetaKeywords { get; set; }
        [Display(Name = "Seo Açıklama")]
        public string MetaDescription { get; set; }
        [Display(Name = "Seo Site Başlığı")]
        public string MetaTitle { get; set; }

        [Display(Name = "Para Birimi")]
        public Nullable<int> CurrencyId { get; set; }
        [Display(Name = "Yayınla")]
        public bool Published { get; set; }
        [Display(Name = "Dil")]
        public int LanguageId { get; set; }
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
        public bool IsActive { get; set; }

        public static ProductBox initialize = new ProductBox();
        public ProductBoxDTO GetProductBoxDTO()
        {

            ProductBoxDTO pm = new ProductBoxDTO();

            pm.Id = Id;

            pm.BoxName = BoxName;

            pm.BoxDescription = BoxDescription;

            pm.UrlId = UrlId;

            pm.Price = Price.ToString();

            pm.Tax = Tax;

            pm.MetaKeywords = MetaKeywords;

            pm.MetaDescription = MetaDescription;

            pm.MetaTitle = MetaTitle;

            pm.CurrencyId = CurrencyId;

            pm.Published = Published;

            pm.LanguageId = LanguageId;

            pm.CreatedDate = CreatedDate;

            pm.CreatedUserId = CreatedUserId;

            pm.UpdatedDate = UpdatedDate;

            pm.UpdatedUserId = UpdatedUserId;

            pm.DeletedDate = DeletedDate;

            pm.DeletedUserId = DeletedUserId;

            pm.IsActive = IsActive;
            return pm;
        }
        public ProductBox() {

            CreatedDate = DateTime.Now;
            CreatedUserId = 1;
        }

    }

    public class ProductBoxDTO
    {

        public ProductBox GetProductBox() {

            ProductBox pm = new ProductBox();

            pm.Id = Id;

            pm.BoxName = BoxName;

            pm.BoxDescription = BoxDescription;

            pm.UrlId = UrlId;

            pm.Price = string.IsNullOrEmpty(Price) ? 0 : Convert.ToDouble(Price.Replace(".", ","));

            pm.Tax = Tax;

            pm.MetaKeywords = MetaKeywords;

            pm.MetaDescription = MetaDescription;

            pm.MetaTitle = MetaTitle;

            pm.CurrencyId = CurrencyId;

            pm.Published = Published;

            pm.LanguageId = LanguageId;

            pm.CreatedDate = CreatedDate;

            pm.CreatedUserId = CreatedUserId;

            pm.UpdatedDate = UpdatedDate;

            pm.UpdatedUserId = UpdatedUserId;

            pm.DeletedDate = DeletedDate;

            pm.DeletedUserId = DeletedUserId;

            pm.IsActive = IsActive;
            return pm;
        }

        public int Id { get; set; }

        [Display(Name = "Kutu Adı")]
        [Required(ErrorMessage = "Kutu Adı Giriniz!")]
        public string BoxName { get; set; }
        [Display(Name = "Kutu Açıklaması")]
        public string BoxDescription { get; set; }

        public int UrlId { get; set; }

        [Display(Name = "Kutu Fiyatı")]
        [Required(ErrorMessage = "Kutu Fiyatı Giriniz!")]
        public string Price { get; set; }

        [Display(Name = "KDV")]
        public Nullable<int> Tax { get; set; }
        [Display(Name = "Seo Anahtar Kelimeler")]
        public string MetaKeywords { get; set; }
        [Display(Name = "Seo Açıklama")]
        public string MetaDescription { get; set; }
        [Display(Name = "Seo Site Başlığı")]
        public string MetaTitle { get; set; }

        [Display(Name = "Para Birimi")]
        public Nullable<int> CurrencyId { get; set; }
        [Display(Name = "Yayınla")]
        public bool Published { get; set; }
        [Display(Name = "Dil")]
        public int LanguageId { get; set; }
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
        public bool IsActive { get; set; }

        public static ProductBoxDTO initialize = new ProductBoxDTO();

        public ProductBoxDTO()
        {

            CreatedDate = DateTime.Now;
            CreatedUserId = 1;
        }

    }
}
