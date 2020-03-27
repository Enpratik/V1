using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enpratik.Data
{
    public class Blog_CategoryMapping
    {
        public int Id { get; set; }

        public int BlogId { get; set; }

        public int BlogCategoryId { get; set; }

        public bool IsActive { get; set; }

        public Blog_CategoryMapping()
        {
            IsActive = true;
        }

    }
}
