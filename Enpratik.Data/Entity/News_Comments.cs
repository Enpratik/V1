using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enpratik.Data
{
    public class News_Comments
    {
        public int Id { get; set; }

        [Display(Name = "Bağlı Yorum")]
        public int ParentId { get; set; }

        [Display(Name = "Haber")]
        public int NewsId { get; set; }

        [Display(Name = "Üye Bilgisi")]
        public int CustomerId { get; set; }

        [Display(Name = "Mesaj")]
        [Required(ErrorMessage = "Lütfen mesaj giriniz!")]
        public string CommentMessage { get; set; }

        [Display(Name = "Puanınız")]
        public Nullable<int> NewsPoint { get; set; }

        [Display(Name = "Kayıt Tarihi")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Onay Durumu")]
        public bool Status { get; set; }

        public bool IsActive { get; set; }

        public static News_Comments initialize = new News_Comments();

        public News_Comments()
        {
            CreatedDate = DateTime.Now;
            IsActive = true;
            Status = false;
        }

    }
}
