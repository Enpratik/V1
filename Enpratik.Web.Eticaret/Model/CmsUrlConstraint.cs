using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace Enpratik.Web.Eticaret.Model
{
    public class CmsUrlConstraint : IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            Enpratik.Data.EnPratik_DataHelper db = new Data.EnPratik_DataHelper();
            if (values[parameterName] != null)
            {
                var permalink = values[parameterName].ToString();
                return db.WebSiteUrls.Any(p => p.Url == permalink);
            }
            return false;
        }
    }
}