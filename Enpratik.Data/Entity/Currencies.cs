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

    public partial class Currencies
    {
        public int Id { get; set; }
        [Display(Name = "Para Birimi Adı")]
        public string CurrencyName { get; set; }
        [Display(Name = "Para Birim Kodu")]
        public string CurrencyCode { get; set; }
        [Display(Name = "Kur Fiyatı")]
        public Nullable<decimal> Rate { get; set; }
        [Display(Name = "Formatlandır")]
        public string CustomFormatting { get; set; }
        [Display(Name = "Görüntüleme Sırası")]
        public Nullable<int> DisplayOrder { get; set; }
        [Display(Name = "Güncelleme Tarihi")]
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        [Display(Name = "Aktif mi")]
        public Nullable<bool> IsActive { get; set; }
    }
}
