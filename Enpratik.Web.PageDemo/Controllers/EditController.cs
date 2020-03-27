using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Enpratik.Web.PageDemo.Controllers
{
    public class EditController : Controller
    {
        // GET: Edit
        public ActionResult Index()
        {

            Session["EditMode"] = "open";

            return Redirect("/");
        }


        // GET: Edit
        public ActionResult kapat()
        {

            Session.Remove("EditMode");

            return Redirect("/");
        }
    }
}