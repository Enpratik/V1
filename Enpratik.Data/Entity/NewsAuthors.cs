using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enpratik.Data
{
    public class NewsAuthors
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Yazar Adı Giriniz!")]
        [Display(Name ="Yazar Ad-Soyad")]
        public string AuthorName { get; set; }

        [Display(Name = "Yazar Hakkında")]
        public string AuthorInfo { get; set; }

        [Display(Name = "Yazar Foto")]
        public string AuthorImages { get; set; }

        [Display(Name = "Yazar Email")]
        [EmailAddress(ErrorMessage = "Hatalı Email Adresi!")]
        public string Email { get; set; }

        public string Url { get; set; }

    }
}
