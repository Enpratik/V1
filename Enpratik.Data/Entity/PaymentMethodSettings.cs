//------------------------------------------------------------------------------
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
    
    public partial class PaymentMethodSettings
    {
        public int Id { get; set; }
        public Nullable<int> PaymentMethodId { get; set; }
        public string SettingKey { get; set; }
        public string SettingValue { get; set; }
        public string SettingDataType { get; set; }
        public string Descriptions { get; set; }
        public Nullable<int> UpdatedUserId { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}