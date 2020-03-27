
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Enpratik.Rezervasyon
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //     name: "CmsRoute",
            //     url: "{*permalink}",
            //     defaults: new { controller = "DynamicPages", action = "Index", permalink = UrlParameter.Optional },
            //     constraints: new { permalink = new CmsUrlConstraint() },
            //     namespaces : new[] { "Enpratik.Rezervasyon.Controllers" }
            // );

            #region Events

            routes.MapRoute(
                name: "Events",
                url: "event/{eventUrl}-{id}",
                defaults: new { controller = "Default", action = "EventDetail" , eventUrl = UrlParameter.Optional, id = UrlParameter.Optional },
                 namespaces: new[] { "Enpratik.Rezervasyon.Controllers" }
            );

            routes.MapRoute(
                name: "EventRezervasyon",
                url: "rezervasyon/{eventUrl}-{id}",
                defaults: new { controller = "Default", action = "Rezervasyon", eventUrl = UrlParameter.Optional, id = UrlParameter.Optional },
                 namespaces: new[] { "Enpratik.Rezervasyon.Controllers" }
            );

            routes.MapRoute(
                name: "EventCheckOut",
                url: "checkout/{eventUrl}-{id}",
                defaults: new { controller = "Checkout", action = "Index", eventUrl = UrlParameter.Optional, id = UrlParameter.Optional },
                 namespaces: new[] { "Enpratik.Rezervasyon.Controllers" }
            );


            #endregion

            #region Products & Cart

            routes.MapRoute(
                name: "ProductCart",
                url: "sepetim",
                defaults: new { controller = "ProductCart", action = "Index" },
                 namespaces: new[] { "Enpratik.Rezervasyon.Controllers" }
            );

            routes.MapRoute(
                name: "ProductList",
                url: "urunler",
                defaults: new { controller = "Products", action = "Index" },
                 namespaces: new[] { "Enpratik.Rezervasyon.Controllers" }
            );
            
            routes.MapRoute(
                name: "ProductCategoryList",
                url: "urun-kategori/{categoryUrl}-{categoryId}",
                defaults: new { controller = "Products", action = "Index", categoryUrl = UrlParameter.Optional, categoryId = UrlParameter.Optional },
                 namespaces: new[] { "Enpratik.Rezervasyon.Controllers" }
            );

            routes.MapRoute(
                name: "ProductBrandList",
                url: "urun-marka/{brandUrl}-{brandId}",
                defaults: new { controller = "Products", action = "Index", brandUrl = UrlParameter.Optional, brandId = UrlParameter.Optional },
                 namespaces: new[] { "Enpratik.Rezervasyon.Controllers" }
            );

            routes.MapRoute(
                name: "ProductDetail",
                url: "product/{productUrl}-{id}",
                defaults: new { controller = "Products", action = "Detail", productUrl = UrlParameter.Optional, id = UrlParameter.Optional },
                 namespaces: new[] { "Enpratik.Rezervasyon.Controllers" }
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
                name: "CustomerPage",
                url: "hesabim",
                defaults: new { controller = "Customers", action = "Index" }
            );

            #endregion

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Default", action = "Index", id = UrlParameter.Optional },
                new[] { "Enpratik.Rezervasyon.Controllers" }
            );

        }
    }
}
