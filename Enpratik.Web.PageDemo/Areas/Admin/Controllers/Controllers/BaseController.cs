using Enpratik.AdminPanel.Model;
using Enpratik.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Enpratik.AdminPanel.Controllers
{
    public class BaseController : Controller
    {

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Admins user = Current.User;

            if (user == null)
            {
                HttpContextBase contex = filterContext.HttpContext;
                string url = Url.Action("Index", "Default", new { returnUrl = contex.Request.Url.AbsoluteUri });
                filterContext.Result = new RedirectResult(url);
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}