using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enpratik.Data
{
    public partial class SliderImageMappings
    {
        public int Id { get; set; }

        [Display(Name = "Slider Adı")]
        public int SliderId { get; set; }

        [Display(Name = "Görüntülenme Sırası")]
        public int DisplayOrder { get; set; }

        [Display(Name = "Slider Resim")]
        public string SliderImageUrl { get; set; }

        [Display(Name = "Slider İçerik Yazısı")]
        public string TextContent { get; set; }

        [Display(Name = "Buton Link Aktif mi")]
        public Nullable<bool> IsButton { get; set; }

        [Display(Name = "Buton Görünen Yazı")]
        public string ButtonText { get; set; }

        [Display(Name = "Buton Link Url")]
        public string ButtonPostUrl { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CreatedUserId { get; set; }

        public Nullable<DateTime> UpdatedDate { get; set; }

        public Nullable<int> UpdatedUserId { get; set; }
        

    }
}
