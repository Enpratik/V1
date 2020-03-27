using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enpratik.AdminPanel.Model
{
    public class SendMailModel
    {
        public string toAddresses { get; set; }
        public string subject { get; set; }
        public Nullable<int> templateId { get; set; }
    }
}