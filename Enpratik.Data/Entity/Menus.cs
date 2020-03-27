using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enpratik.Data
{
    public partial class Menus
    {
        public int Id { get; set; }

        [Display(Name = "Üst Menü")]
        public int ParentId { get; set; }

        [Display(Name = "Menü Adı")]
        [Required(ErrorMessage = "Menü Adı Giriniz!")]
        public string MenuName { get; set; }

        [Display(Name = "Menü Tipi")]
        [Required(ErrorMessage = "Menü Tipi Seçiniz!")]
        public int MenuPageType { get; set; }

        [Display(Name = "Menü Sayfası")]
        public Nullable<int> MenuPageId { get; set; }

        [Display(Name = "Menü Url")]
        public string MenuUrl { get; set; }

        [Display(Name = "Hedef")]
        public string Target { get; set; }

        [Display(Name = "Menü Link Title")]
        public string MenuTitle { get; set; }

        [Display(Name = "Menü İkonu")]
        public string MenuIcon { get; set; }

        [Display(Name = "Görüntüleme Sırası")]
        public int DisplayOrder { get; set; }
        [Display(Name = "Dil")]
        public Nullable<int> LanguageId { get; set; }
        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime CreatedDate { get; set; }
        [Display(Name = "Oluşturan Kişi")]
        public Nullable<int> CreatedUserId { get; set; }
        [Display(Name = "Güncelleme Tarihi")]
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        [Display(Name = "Güncelleyen Kişi")]
        public Nullable<int> UpdatedUserId { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<int> DeletedUserId { get; set; }
        [Display(Name = "Aktif mi")]
        public Nullable<bool> IsActive { get; set; }
        public static Menus initialize = new Menus();

        public Menus()
        {
            CreatedDate = DateTime.Now;
            IsActive = true;
        }


        public MenusDTO getMenuDTO(string parentMenu)
        {
            MenusDTO result = new MenusDTO();
            result.Id = Id;
            result.ParentId = ParentId;
            result.MenuName = MenuName;
            result.MenuParentName = parentMenu;
            result.CreatedDate = CreatedDate;
            return result;
        }
    }

    public partial class MenusDTO
    {
        public int Id { get; set; }
        

        [Display(Name = "Üst Menü")]
        public string MenuParentName { get; set; }

        [Display(Name = "Menü Adı")]
        public string MenuName { get; set; }

        [Display(Name = "Menü Tipi")]
        public string MenuTypeName { get; set; }

        [Display(Name = "Görüntüleme Sırası")]
        public int DisplayOrder { get; set; }

        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime CreatedDate { get; set; }

        public int MenuPageType { get; set; }
        public int ParentId { get; set; }
        public Nullable<int> MenuPageId { get; set; }
        public string MenuUrl { get; set; }
    }
}
