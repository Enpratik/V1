using Enpratik.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace Enpratik.Web.Eticaret
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            RegisterViewEngine(ViewEngines.Engines);
        }
        
        public static void RegisterViewEngine(ViewEngineCollection viewEngines)
        {
            viewEngines.Clear();

            var basePath = ConfigurationManager.AppSettings["ThemeBasePath"];
            var themeName = ConfigurationManager.AppSettings["ThemeName"];

            var theme = new Theme(basePath, themeName);

            var themeableRazorViewEngine = new ThemedRazorViewEngine(theme);

            viewEngines.Add(themeableRazorViewEngine);
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }
        
        protected void Session_Start(object sender, EventArgs e)
        {

        }
    }
}
