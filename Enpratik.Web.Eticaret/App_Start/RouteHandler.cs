using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Enpratik.Web.Eticaret.App_Start
{
    public class RouteHandler<THttpHandler> : MvcRouteHandler
    where THttpHandler : MvcHandler
    {
        public RouteHandler(IControllerFactory controllerFactory)
            : base(controllerFactory) { }
        public static void Assign(RouteCollection routes)
        {
            using (routes.GetReadLock())
            {


                var routeHandler
                    = new RouteHandler<THttpHandler>(
                        ControllerBuilder.Current.GetControllerFactory());


                foreach (var route in routes
                    .OfType<Route>()
                    .Where(r => (r.RouteHandler is MvcRouteHandler)))
                {


                    route.RouteHandler = routeHandler;
                }
            }
        }
    }
}