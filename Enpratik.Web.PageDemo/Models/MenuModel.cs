
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enpratik.Web.PageDemo.Models
{
    public class MenuModel
    {
        public string text { get; set; }
        public string href { get; set; }
        public string target { get; set; }
        public string title { get; set; }
        public List<Children> children { get; set; }

    }
    public class Children
    {
        public string text { get; set; }
        public string href { get; set; }
        public string icon { get; set; }
        public string target { get; set; }
        public string title { get; set; }
        public List<Children> children { get; set; }
    }

}