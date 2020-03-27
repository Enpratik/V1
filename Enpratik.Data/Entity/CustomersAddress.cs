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

    public partial class CustomersAddress
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        [Display(Name = "Adres Tipi")]
        [Required(ErrorMessage = "Adres Tipi Seçiniz!")]
        public int AdressType { get; set; }
        [Display(Name = "Fatura Tipi")]
        [Required(ErrorMessage = "Fatura Tipi Seçiniz!")]
        public int InvoiceType { get; set; }
        [Display(Name = "T.C Kimlik No")]
        public string IdentityNumber { get; set; }
        [Display(Name = "Vergi No")]
        public string TaxNumber { get; set; }
        [Display(Name = "Adres Adı")]
        [Required(ErrorMessage = "Adres Adı Giriniz!")]
        public string AddressTitle { get; set; }
        [Display(Name = "Firma Adı")]
        public string CompanyName { get; set; }
        [Display(Name = "Ad")]
        public string FirstName { get; set; }
        [Display(Name = "Soyad")]
        public string LastName { get; set; }
        [Display(Name = "Adres")]
        public string Adres { get; set; }
        [Display(Name = "Posta Kodu")]
        public string ZipCode { get; set; }
        [Display(Name = "Ülke")]
        public Nullable<int> CountryId { get; set; }
        [Display(Name = "İl")]
        public Nullable<int> ProvinceId { get; set; }
        [Display(Name = "İlçe")]
        public Nullable<int> DistinctId { get; set; }
        [Display(Name = "Telefon")]
        public string Telephone { get; set; }
        [Display(Name = "Kayıt Tarihi")]
        public Nullable<System.DateTime> CreatedDate { get; set; }
        [Display(Name = "Aktif mi")]
        public Nullable<bool> IsActive { get; set; }
    }
}
