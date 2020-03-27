using Enpratik.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enpratik.Web.Eticaret.Model
{
    public class SearchModel
    {
        public List<Products> product { get; set; }
        public List<DynamicPages> pages { get; set; }
        public List<Blogs> blogs { get; set; }
    }
}