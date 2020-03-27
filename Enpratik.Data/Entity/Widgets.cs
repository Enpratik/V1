using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enpratik.Data
{
    public class Widgets
    {
        public int Id { get; set; }

        public string WidgetName { get; set; }
        
        public string HtmlCode { get; set; }

        public string WidgetControlPath { get; set; }

        public string IconUrl { get; set; }

        public bool IsControlWidget { get; set; }

        public bool IsActive { get; set; }

    }
    public class WidgetsDTO
    {
        public int Id { get; set; }
        public int WidgetZonesMappingId { get; set; }
        public int WidgetZoneId { get; set; }
        public string WidgetName { get; set; }
        public string WidgetControlPath { get; set; }
        public bool IsControlWidget { get; set; }
        public WidgetZonesParamMapping WidgetZonesParamMapping { get; set; }

        public WidgetsDTO() {
            WidgetZonesParamMapping = new WidgetZonesParamMapping();
        }
    }
}
