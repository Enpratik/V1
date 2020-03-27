using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enpratik.Data
{
    public class WebSiteUrlSetting
    {
        public int Id { get; set; }
        public int WebSiteUrlTypeId { get; set; }
        public string UrlPattern { get; set; }
        public string UrlPatternDescription { get; set; }
    }
    public class WebSiteUrlSettingDTO
    {
        public int Id { get; set; }
        public int WebSiteUrlTypeId { get; set; }
        public string WebSiteUrlTypeName { get; set; }
        public string UrlPattern { get; set; }
        public string UrlPatternDescription { get; set; }
    }
}
