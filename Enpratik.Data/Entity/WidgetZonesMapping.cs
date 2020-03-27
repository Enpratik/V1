using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enpratik.Data
{
    public class WidgetZonesMapping
    {
        public int Id { get; set; }

        public int WidgetZoneId { get; set; }

        public int WidgetId { get; set; }

        public int OrderIndex { get; set; }

        public bool IsActive { get; set; }

    }
    public class WidgetZonesMappingDTO
    {
        public int Id { get; set; }
        public int WidgetZoneId { get; set; }
        public string WidgetZoneName { get; set; }
        public int WidgetId { get; set; }
        public string WidgetName { get; set; }
        public int OrderIndex { get; set; }
        public bool IsActive { get; set; }

    }
}
