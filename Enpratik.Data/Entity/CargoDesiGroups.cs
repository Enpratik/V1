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

    public partial class CargoDesiGroups
    {
        public int Id { get; set; }
        [Display(Name = "Karfo Firma Adı")]
        [Required(ErrorMessage = "Karfo Firma Adı Giriniz!")]
        public Nullable<int> CargoId { get; set; }
        [Display(Name = "Tanım Adı")]
        [Required(ErrorMessage = "Tanım Adı Giriniz!")]
        public string Defination { get; set; }
        [Display(Name = "Ülke")]
        public Nullable<int> CountryId { get; set; }
        [Display(Name = "İl")]
        public Nullable<int> ProvinceId { get; set; }
        [Display(Name = "Başlangıç Desi")]
        public Nullable<double> StartDesi { get; set; }
        [Display(Name = "Başlangıç Fiyat")]
        public Nullable<double> StartingPrice { get; set; }
        [Display(Name = "Desi Artış Aralığı")]
        public Nullable<double> DesiIncrementStage { get; set; }
        [Display(Name = "Fiyat Artış Aralığı")]
        public Nullable<double> PriceIncrementStage { get; set; }
        [Display(Name = "Bitiş Desi")]
        public Nullable<double> EndDesi { get; set; }
        [Display(Name = "Yayınla")]
        public Nullable<bool> IsPublished { get; set; }
        [Display(Name = "Görüntülenme Sırası")]
        public Nullable<int> DisplayOrder { get; set; }
        public Nullable<int> LanguageId { get; set; }
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
    }
}
