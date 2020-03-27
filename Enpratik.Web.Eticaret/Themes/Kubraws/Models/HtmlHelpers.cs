using Enpratik.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Enpratik.Web.Eticaret.Kubraws
{
    public static class HtmlHelpers
    {

        public static string GetMenu()
        {
            try
            {
                EnPratik_DataHelper db = new EnPratik_DataHelper();

                StringBuilder sb = new StringBuilder();

                var fullMenuList = db.Menus.Where(m => m.IsActive == true).ToList().OrderBy(m => m.DisplayOrder).ToList();

                var menuList = fullMenuList.Where(m => m.ParentId == 0).ToList();

                foreach (Menus item in menuList)
                {
                    GetSubMenu(fullMenuList,item, sb);
                }
                return sb.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        private static void GetSubMenu(List<Menus> menuList, Menus menu, StringBuilder sb)
        {
            string guidId = Guid.NewGuid().ToString();
            sb.Append("<li class=\"nav-item{@}\">");
            sb.Append("<a {css} href=\"" + (string.IsNullOrEmpty(menu.MenuUrl) ? "#" : menu.MenuUrl) + "\">" + menu.MenuName + "</a>");

            var subMenuList = menuList.Where(m => m.ParentId == menu.Id).ToList().OrderBy(m => m.DisplayOrder);

            int counter = 0;
            foreach (Menus item in subMenuList)
            {
                if (counter == 0)
                    sb.Append("<div class=\"dropdown-menu\" aria-labelledby=\""+guidId+"\">");
                
                sb.Append("<a class=\"dropdown-item\" href=\""+item.MenuUrl+"\">" + item.MenuName + "</a>");
                
                counter++;
            }

            if (counter > 0)
            {
                sb.Replace("{@}", " dropdown");
                sb.Replace("{css}", "class=\"nav-link dropdown-toggle\" id=\"" + guidId + "\" data-toggle=\"dropdown\" aria-haspopup=\"true\" aria-expanded=\"false\"");
                sb.Append("</div>");
            }
            else
            {
                sb.Replace("{@}", "");
                sb.Replace("{css}", "class=\"nav-link\"");
            }
            sb.Append("</li>");
        }
    }
}