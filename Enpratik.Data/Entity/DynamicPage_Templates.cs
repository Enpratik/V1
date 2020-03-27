using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enpratik.Data
{
    public partial class  DynamicPage_Templates
    {
        public int Id { get; set; }
        [Display(Name = "Template Adı")]
        [Required(ErrorMessage = "Lütfen Template Adı Giriniz")]
        public string PageTemplateName { get; set; }
        [Display(Name = "Html")]
        [Required(ErrorMessage = "Lütfen HTML Giriniz")]
        public string PageTemplateHtml { get; set; }
    }
}
