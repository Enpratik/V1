using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enpratik.Data
{
    public class News_CategoryMapping
    {
        public int Id { get; set; }

        public int NewsId { get; set; }

        public int NewsCategoryId { get; set; }

        public bool IsActive { get; set; }

        public News_CategoryMapping() {
            IsActive = true;
        }

    }
}
