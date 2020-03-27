using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Enpratik.AdminPanel.Controllers
{
    public class FileManagerController : BaseController
    {
        // GET: FileManager
        public ActionResult Index()
        {
            return View();
        }
    }
}