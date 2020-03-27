﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Enpratik.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    using System.Linq;

    public partial class News
    {
        public int Id { get; set; }
        [Display(Name = "Dil")]
        [Required(ErrorMessage = "Dil Seçiniz!")]
        public int LanguageId { get; set; }


        [Display(Name = "Yazar")]
        [Required(ErrorMessage = "Yazar Seçiniz!")]
        public int AuthorId { get; set; }

        [Display(Name = "Başlık")]
        [Required(ErrorMessage = "Haber Başlığı Seçiniz!")]
        public string NewsTitle { get; set; }
        public int UrlId { get; set; }
        [Display(Name = "Kısa Açıklama")]
        public string ShortDescription { get; set; }
        [Display(Name = "İçerik Yazısı")]
        [Required(ErrorMessage = "Haber İçeriği Giriniz!")]
        public string FullDescription { get; set; }
        [Display(Name = "Haber Resim")]
        public string NewsImages { get; set; }
        [Display(Name = "Yorumlara İzin Ver")]
        public Nullable<bool> AllowComments { get; set; }
        [Display(Name = "Başlangıç Tarihi")]
        public Nullable<System.DateTime> StartDate { get; set; }
        [Display(Name = "Bitiş Tarihi")]
        public Nullable<System.DateTime> EndDate { get; set; }
        [Display(Name = "Yayınla")]
        public Nullable<bool> IsPublished { get; set; }
        [Display(Name = "Seo Anahtar Kelimeler")]
        public string MetaKeywords { get; set; }
        [Display(Name = "Seo Açıklama")]
        public string MetaDescription { get; set; }
        [Display(Name = "Seo Site Başlığı")]
        public string MetaTitle { get; set; }
        [Display(Name = "Oluşturulma Tarihi")]
        public System.DateTime CreatedDate { get; set; }
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

        public static News initialize = new News();
        public string getUrl()
        {
            EnPratik_DataHelper db = new EnPratik_DataHelper();
            var url = db.WebSiteUrls.Where(w => w.Id == UrlId).Select(w => w.Url).FirstOrDefault();
            return url;
        }

        public News() {
            CreatedDate = DateTime.Now;
            IsActive = true;
        }
    }
}
