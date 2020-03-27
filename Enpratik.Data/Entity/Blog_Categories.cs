using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enpratik.Data
{
    public class Blog_Categories
    {
        public int Id { get; set; }

        [Display(Name = "Kategori Adı")]
        [Required(ErrorMessage = "Kategori Adı Giriniz!")]
        public string BlogCategoryName { get; set; }

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

        public static Blog_Categories initialize = new Blog_Categories();

        public Blog_Categories() {
            CreatedDate = DateTime.Now;
            IsActive = true;
        }
        
        public string getUrl()
        {
            EnPratik_DataHelper db = new EnPratik_DataHelper();
            var url = db.WebSiteUrls.Where(w => w.Id == UrlId).Select(w => w.Url).FirstOrDefault();
            return url;
        }

        public Blog_CategoriesDTO getBlogCategoriesDTO(string parentCategoryName)
        {
            Blog_CategoriesDTO result = new Blog_CategoriesDTO();
            result.Id = Id;
            result.ParentId = ParentId;
            result.ParentCategoryName = parentCategoryName;
            result.BlogCategoryName = BlogCategoryName;
            result.UrlId = UrlId;
            result.LanguageId = LanguageId;
            result.CreatedDate = CreatedDate;
            return result;
        }

    }


    public class Blog_CategoriesDTO
    {
        public int Id { get; set; }

        [Display(Name = "Kategori Adı")]
        [Required(ErrorMessage = "Kategori Adı Giriniz!")]
        public string BlogCategoryName { get; set; }

        public int UrlId { get; set; }
        [Display(Name = "Dil")]
        public int LanguageId { get; set; }
        [Display(Name = "Üst Kategori")]
        public int ParentId { get; set; }

        [Display(Name = "Üst Kategori Adı")]
        public string ParentCategoryName { get; set; }

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
