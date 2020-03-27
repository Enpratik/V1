using Enpratik.Data;
using Enpratik.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace Enpratik.Web
{
    public static class HtmlHelpers
    {

        public static string GetMenu()
        {
            try
            {
                EnPratik_DataHelper db = new EnPratik_DataHelper();

                var menuJson = db.SiteMenus.Select(s => s.MenuJson).FirstOrDefault();

                if (string.IsNullOrEmpty(menuJson))
                    return "";

                var menuModel = new JavaScriptSerializer().Deserialize<List<MenuModel>>(menuJson);

                StringBuilder sb = new StringBuilder();


                foreach (MenuModel item in menuModel)
                {
                    sb.Append($"<li><a target='{item.target}' href='{item.href}'>{item.text}</a>");
                    GetSubMenu(item, sb);
                    sb.Append("</li>");
                }
                return sb.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }

        private static void GetSubMenu(MenuModel menu, StringBuilder sb)
        {

            if (menu.children == null)
                return;

            if (menu.children.Count == 0)
                return;

            sb.Append("<ul class='dropdown'>");
            foreach (Children item in menu.children)
            {
                sb.Append($"<li><a href='{item.href}'>{item.text}</a>");
                GetSubMenu(item, sb);
                sb.Append("</li>");
            }
            sb.Append("</ul>");

        }
    }
}