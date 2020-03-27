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

    public partial class Manufacturers
    {
        public int Id { get; set; }
        [Display(Name = "Üretici Firma Adı")]
        [Required(ErrorMessage = "Üretici Firma Adı Giriniz!")]
        public string ManufacturerName { get; set; }
        public int UrlId { get; set; }
        [Display(Name = "Açıklama")]
        public string ManufacturerDescription { get; set; }
        [Display(Name = "Dil")]
        public int LanguageId { get; set; }
        [Display(Name = "Yayınla")]
        public bool Published { get; set; }
        [Display(Name = "Görüntülenme Sırası")]
        public Nullable<int> DisplayOrder { get; set; }
        [Display(Name = "Resim")]
        public string ManufacturerImageUrl { get; set; }
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
        public static Manufacturers initialize = new Manufacturers();

        public Manufacturers()
        {
            CreatedDate = DateTime.Now;
            IsActive = true;
        }
    }
}
