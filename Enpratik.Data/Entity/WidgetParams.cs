using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enpratik.Data
{
    public class WidgetParams
    {
        public int Id { get; set; }

        public int WidgetId { get; set; }

        public string ParameterDescription { get; set; }
        public string ParameterName { get; set; }

        public string ParameterValue { get; set; }

        public bool IsActive { get; set; }

    }
}
