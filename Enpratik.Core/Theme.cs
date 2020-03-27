using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enpratik.Core
{
    public class Theme
    {
        public string Name { get; set; }
        public string BasePath { get; set; }
        public string Path { get { return String.Format("~/{0}/{1}", BasePath, Name); } }
        public Theme(string basePath, string name)
        {
            Name = name;
            BasePath = basePath;
        }
    }
}
