using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enpratik.Data
{
    public class WidgetZonesParamMapping
    {
        public int Id { get; set; }

        public int WidgetZoneMappingId { get; set; }

        public int WidgetId { get; set; }

        public string ParamJson { get; set; }

    }


    public class WidgetZonesParamMappingDTO
    {
        public int Id { get; set; }
        public int WidgetZoneMappingId { get; set; }
        public int WidgetId { get; set; }
        public bool IsControlWidget { get; set; }
        public string ParameterDescription { get; set; }
        public string ParameterName { get; set; }
        public string ParameterValue { get; set; }

    }

    [Serializable]
    public class ParamJsonDTO
    {
        public string ParameterName { get; set; }
        public string ParameterValue { get; set; }

    }
}
