using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enpratik.Data
{
    public class SiteMenus
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }
        public string MenuJson { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedUserId { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedUserId { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedUserId { get; set; }
        public bool IsActive { get; set; }

        public SiteMenus()
        {
            LanguageId = 1;
            CreatedUserId = 1;
            CreatedDate = DateTime.Now;
        }
        
    }
}
