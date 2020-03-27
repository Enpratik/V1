using Enpratik.Web.Eticaret.Model;
using System.Web.Mvc;
using System.Web.Routing;

namespace Enpratik.Web.Eticaret
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                 name: "CmsRoute",
                 url: "{*permalink}",
                 defaults: new { controller = "DynamicPages", action = "Index", permalink = UrlParameter.Optional },
                 constraints: new { permalink = new CmsUrlConstraint() }
             );


            #region Products & Cart

            routes.MapRoute(
                name: "ProductList",
                url: "urunler",
                defaults: new { controller = "ProductBoxs", action = "Index" }
            );

            routes.MapRoute(
                name: "ProductCart",
                url: "sepetim",
                defaults: new { controller = "ProductCart", action = "Index" }
            );

            //routes.MapRoute(
            //    name: "ProductList",
            //    url: "urunler",
            //    defaults: new { controller = "Products", action = "Index" }
            //);

            routes.MapRoute(
                name: "ProductCatrgoryList",
                url: "urun-kategori/{categoryUrl}-{id}",
                defaults: new { controller = "Products", action = "CategoryListIndex", categoryUrl = UrlParameter.Optional, id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "ProductDetail",
                url: "product/{productUrl}-{id}",
                defaults: new { controller = "Products", action = "Detail", productUrl = UrlParameter.Optional, id = UrlParameter.Optional }
            );


            #endregion 

            #region Contact

            routes.MapRoute(
                name: "Contact",
                url: "iletisim",
                defaults: new { controller = "Contact", action = "Index" }
            );
            routes.MapRoute(
                name: "ContactOK",
                url: "iletisim/tesekkurler",
                defaults: new { controller = "Contact", action = "ResultOk" }
            );

            routes.MapRoute(
                name: "ContactUs",
                url: "contact-us",
                defaults: new { controller = "Contact", action = "Index" }
            );
            routes.MapRoute(
                name: "ContactUsOK",
                url: "contact-us/thankyou",
                defaults: new { controller = "Contact", action = "ResultOk" }
            );
            #endregion

            #region Blogs

            routes.MapRoute(
                name: "BlogDetail",
                url: "blog/{blogUrl}-{id}",
                defaults: new { controller = "Blog", action = "Detail", blogUrl = UrlParameter.Optional, id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "BlogCategory",
                url: "blog-kategori/{categoryUrl}-{id}",
                defaults: new { controller = "Blog", action = "CategoryList", categoryUrl = UrlParameter.Optional, id = UrlParameter.Optional }
            );

            #endregion 

            #region Customers

            routes.MapRoute(
                name: "ForgotPassword",
                url: "hesabim/sifremi-unuttum",
                defaults: new { controller = "Login", action = "ForgotPassword" }
            );
            routes.MapRoute(
                name: "Register",
                url: "hesabim/uye-ol",
                defaults: new { controller = "Customers", action = "Create" }
            );

            routes.MapRoute(
                name: "Login",
                url: "hesabim/uye-girisi",
                defaults: new { controller = "Login", action = "Index" }
            );
            routes.MapRoute(
                name: "LoginExit",
                url: "hesabim/cikis",
                defaults: new { controller = "Login", action = "Logout" }
            );

            routes.MapRoute(
                name: "CustomerPage",
                url: "hesabim",
                defaults: new { controller = "Customers", action = "Index" }
            );

            #endregion

            #region News

            routes.MapRoute(
                name: "NewsDetail",
                url: "haber/{newsUrl}-{id}",
                defaults: new { controller = "News", action = "Detail", newsUrl = UrlParameter.Optional, id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "NewsCategory",
                url: "category/{categoryUrl}",
                defaults: new { controller = "News", action = "CategoryList", categoryUrl = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "NewsAuthors",
                url: "yazar/{authorUrl}",
                defaults: new { controller = "News", action = "NewsAuthor", authorUrl = UrlParameter.Optional }
            );

            #endregion

            #region Default
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Default", action = "Index", id = UrlParameter.Optional }
            );

            #endregion
                        
        }
    }
}
