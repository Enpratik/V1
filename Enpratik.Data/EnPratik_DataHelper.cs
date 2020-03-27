using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
namespace Enpratik.Data
{
    public partial class EnPratik_DataHelper : DbContext
    {
        public EnPratik_DataHelper()
            : base("name=Enpratik_ConnectionString")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        
        public virtual DbSet<EventCustomerMaps_Temps> EventCustomerMaps_Temps { get; set; }
        public virtual DbSet<EventCustomerMaps> EventCustomerMaps { get; set; }
        public virtual DbSet<EventDateMaps> EventDateMaps { get; set; }
        public virtual DbSet<Events> Events { get; set; }
        public virtual DbSet<RelatedProducts> RelatedProducts { get; set; }
        public virtual DbSet<NewsAuthors> NewsAuthors { get; set; }
        public virtual DbSet<OrderUserAddress> OrderUserAddress { get; set; }
        public virtual DbSet<OrderUserAddressesTemp> OrderUserAddressesTemp { get; set; }
        public virtual DbSet<DynamicPage_Templates> DynamicPage_Templates { get; set; }
        public virtual DbSet<Product_BasketItem> Product_BasketItem { get; set; }
        public virtual DbSet<Product_BasketItem_Temp> Product_BasketItem_Temp { get; set; }
        public virtual DbSet<WebSiteUrlSetting> WebSiteUrlSetting { get; set; }
        public virtual DbSet<WebSiteUrlTypeName> WebSiteUrlTypeName { get; set; }
        public virtual DbSet<Admin_MainMenu> Admin_MainMenu { get; set; }
        public virtual DbSet<Admin_Module_Actions> Admin_Module_Actions { get; set; }
        public virtual DbSet<Admin_Modules> Admin_Modules { get; set; }
        public virtual DbSet<Admin_Role_Authentications> Admin_Role_Authentications { get; set; }
        public virtual DbSet<Admin_Roles> Admin_Roles { get; set; }
        public virtual DbSet<Admin_SubMenu> Admin_SubMenu { get; set; }
        public virtual DbSet<Admins> Admins { get; set; }
        public virtual DbSet<Blogs> Blogs { get; set; }
        public virtual DbSet<Blog_Comments> Blog_Comments { get; set; }
        public virtual DbSet<Blog_Categories> Blog_Categories { get; set; }
        public virtual DbSet<Blog_CategoryMapping> Blog_CategoryMapping { get; set; }
        public virtual DbSet<Brands> Brands { get; set; }
        public virtual DbSet<CargoCompanies> CargoCompanies { get; set; }
        public virtual DbSet<CargoDesiGroups> CargoDesiGroups { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Contracts> Contracts { get; set; }
        public virtual DbSet<Countries> Countries { get; set; }
        public virtual DbSet<Currencies> Currencies { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<CustomersAddress> CustomersAddress { get; set; }
        public virtual DbSet<CustomersRole> CustomersRole { get; set; }
        public virtual DbSet<CustomersType> CustomersType { get; set; }
        public virtual DbSet<Districts> Districts { get; set; }
        public virtual DbSet<DynamicPages> DynamicPages { get; set; }
        public virtual DbSet<ImporterCompanies> ImporterCompanies { get; set; }
        public virtual DbSet<Languages> Languages { get; set; }
        public virtual DbSet<Manufacturers> Manufacturers { get; set; }
        public virtual DbSet<MoneyPointsSettings> MoneyPointsSettings { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<News_Comments> News_Comments { get; set; }
        public virtual DbSet<News_Categories> News_Categories { get; set; }
        public virtual DbSet<News_CategoryMapping> News_CategoryMapping { get; set; }
        public virtual DbSet<OrderProductMapping> OrderProductMapping { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<PaymentBankAccount> PaymentBankAccount { get; set; }
        public virtual DbSet<PaymentBanks> PaymentBanks { get; set; }
        public virtual DbSet<PaymentCreditCardInstallments> PaymentCreditCardInstallments { get; set; }
        public virtual DbSet<PaymentMethods> PaymentMethods { get; set; }
        public virtual DbSet<PaymentMethodSettings> PaymentMethodSettings { get; set; }
        public virtual DbSet<Product_Category_Mapping> Product_Category_Mapping { get; set; }
        public virtual DbSet<Product_Comments> Product_Comments { get; set; }
        public virtual DbSet<Product_LowStockReports> Product_LowStockReports { get; set; }
        public virtual DbSet<Product_Picture_Mapping> Product_Picture_Mapping { get; set; }
        public virtual DbSet<Product_SpecificationAttribute> Product_SpecificationAttribute { get; set; }
        public virtual DbSet<Product_SpecificationAttribute_Mapping> Product_SpecificationAttribute_Mapping { get; set; }
        public virtual DbSet<Product_SpecificationAttributeOption> Product_SpecificationAttributeOption { get; set; }
        public virtual DbSet<Product_VariantAttribute> Product_VariantAttribute { get; set; }
        public virtual DbSet<Product_VariantAttributeValue> Product_VariantAttributeValue { get; set; }
        public virtual DbSet<Product_VariantProduct_Mapping> Product_VariantProduct_Mapping { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Promotions> Promotions { get; set; }
        public virtual DbSet<PromotionsRequirements> PromotionsRequirements { get; set; }
        public virtual DbSet<PromotionsRequirementType> PromotionsRequirementType { get; set; }
        public virtual DbSet<PromotionsType> PromotionsType { get; set; }
        public virtual DbSet<Provinces> Provinces { get; set; }
        public virtual DbSet<SendMailGoups> SendMailGoups { get; set; }
        public virtual DbSet<SendMailings> SendMailing { get; set; }
        public virtual DbSet<SendMailTemplates> SendMailTemplate { get; set; }
        public virtual DbSet<TaxDisplayType> TaxDisplayType { get; set; }
        public virtual DbSet<WebSiteSettingGroups> WebSiteSettingGroups { get; set; }
        public virtual DbSet<WebSiteSettings> WebSiteSettings { get; set; }
        public virtual DbSet<WidgetParams> WidgetParams { get; set; }
        public virtual DbSet<Widgets> Widgets { get; set; }
        public virtual DbSet<WidgetZones> WidgetZones { get; set; }
        public virtual DbSet<WidgetZonesMapping> WidgetZonesMapping { get; set; }
        public virtual DbSet<WidgetZonesParamMapping> WidgetZonesParamMapping { get; set; }
        public virtual DbSet<PaymentBankPosParameters> PaymentBankPosParameters { get; set; }
        public virtual DbSet<PaymentCreditCards> PaymentCreditCards { get; set; }
        public virtual DbSet<Product_VariantAttribute_Mapping> Product_VariantAttribute_Mapping { get; set; }
        public virtual DbSet<Menus> Menus { get; set; }
        public virtual DbSet<SiteMenus> SiteMenus { get; set; }
        public virtual DbSet<MenuTypes> MenuTypes { get; set; }
        public virtual DbSet<Sliders> Sliders { get; set; }
        public virtual DbSet<SliderImageMappings> SliderImageMappings { get; set; }
        public virtual DbSet<WebSiteUrls> WebSiteUrls { get; set; }
        public System.Data.Entity.DbSet<ProductBoxs> ProductBoxs { get; set; }

        public System.Data.Entity.DbSet<ProductBox> ProductBoxes { get; set; }

        public System.Data.Entity.DbSet<Enpratik.Data.ProductBox_Product_Mapping> ProductBox_Product_Mapping { get; set; }

        public System.Data.Entity.DbSet<Boxes> Boxes { get; set; }

        public System.Data.Entity.DbSet<Enpratik.Data.Entity.WebPageSettings> WebPageSettings { get; set; }
    }
}
