using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enpratik.Data
{
    public class News_Categories
    {
        public int Id { get; set; }
        [Display(Name = "Kategori Adı")]
        [Required(ErrorMessage = "Kategori Adı Giriniz!")]
        public string NewsCategoryName { get; set; }

        public int UrlId { get; set; }
        [Display(Name = "Dil")]
        public int LanguageId { get; set; }
        [Display(Name = "Üst Kategori")]
        public int ParentId { get; set; }


        public string MetaKeywords { get; set; }

        public string MetaDescription { get; set; }

        public string MetaTitle { get; set; }

        public Nullable<DateTime> CreatedDate { get; set; }

        public Nullable<int> CreatedUserId { get; set; }

        public Nullable<DateTime> UpdatedDate { get; set; }

        public Nullable<int> UpdatedUserId { get; set; }

        public Nullable<DateTime> DeletedDate { get; set; }

        public Nullable<int> DeletedUserId { get; set; }

        public bool IsActive { get; set; }

        public static News_Categories initialize = new News_Categories();


        public string getUrl()
        {
            EnPratik_DataHelper db = new EnPratik_DataHelper();
            var url = db.WebSiteUrls.Where(w => w.Id == UrlId).Select(w => w.Url).FirstOrDefault();
            return url;
        }

        public News_Categories()
        {
            CreatedDate = DateTime.Now;
            IsActive = true;
        }
        
        public News_CategoriesDTO getNewsCategoriesDTO(string parentCategoryName)
        {
            News_CategoriesDTO result = new News_CategoriesDTO();
            result.Id = Id;
            result.ParentId = ParentId;
            result.ParentCategoryName = parentCategoryName;
            result.NewsCategoryName = NewsCategoryName;
            result.UrlId = UrlId;
            result.LanguageId = LanguageId;
            result.CreatedDate = CreatedDate;
            return result;
        }


    }

    public class News_CategoriesDTO
    {
        public int Id { get; set; }
        [Display(Name = "Kategori Adı")]
        [Required(ErrorMessage = "Kategori Adı Giriniz!")]
        public string NewsCategoryName { get; set; }
        [Display(Name = "Üst Kategori Adı")]
        public string ParentCategoryName { get; set; }

        public int UrlId { get; set; }
        [Display(Name = "Dil")]
        public int LanguageId { get; set; }
        [Display(Name = "Üst Kategori")]
        public int ParentId { get; set; }


        public string MetaKeywords { get; set; }

        public string MetaDescription { get; set; }

        public string MetaTitle { get; set; }

        public Nullable<DateTime> CreatedDate { get; set; }

        public Nullable<int> CreatedUserId { get; set; }

        public Nullable<DateTime> UpdatedDate { get; set; }

        public Nullable<int> UpdatedUserId { get; set; }

        public Nullable<DateTime> DeletedDate { get; set; }

        public Nullable<int> DeletedUserId { get; set; }

        public bool IsActive { get; set; }
        
        
    }
}
