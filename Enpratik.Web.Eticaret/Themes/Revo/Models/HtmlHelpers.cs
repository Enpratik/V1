using Enpratik.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Enpratik.Web.Eticaret.Revo
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
            sb.Append("<li>");
            sb.Append("<a href=\""+(string.IsNullOrEmpty(menu.MenuUrl) ? "#" : menu.MenuUrl)+"\">" + menu.MenuName + "</a>");

            var subMenuList = menuList.Where(m => m.ParentId == menu.Id).ToList().OrderBy(m => m.DisplayOrder);

            int counter = 0;
            foreach (Menus item in subMenuList)
            {
                if (counter == 0)
                    sb.Append("<ul class=\"sub_menu\">");
                
                sb.Append("<li><a href=\""+item.MenuUrl+"\">" + item.MenuName + "</a></li>");
                
                counter++;
            }

            if (counter > 0)
                sb.Append("</ul>");

            sb.Append("</li>");
        }


        public static string GetMobileMenu()
        {
            try
            {
                EnPratik_DataHelper db = new EnPratik_DataHelper();

                StringBuilder sb = new StringBuilder();

                var fullMenuList = db.Menus.Where(m => m.IsActive == true).ToList().OrderBy(m => m.DisplayOrder).ToList();

                var menuList = fullMenuList.Where(m => m.ParentId == 0).ToList();

                foreach (Menus item in menuList)
                {
                    GetMobileSubMenu(fullMenuList, item, sb);
                }
                return sb.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static void GetMobileSubMenu(List<Menus> menuList, Menus menu, StringBuilder sb)
        {
            sb.Append("<li class=\"item-menu-mobile\">");
            sb.Append("<a href=\"" + (string.IsNullOrEmpty(menu.MenuUrl) ? "#" : menu.MenuUrl) + "\">" + menu.MenuName + "</a>");

            var subMenuList = menuList.Where(m => m.ParentId == menu.Id).ToList().OrderBy(m => m.DisplayOrder);

            int counter = 0;
            foreach (Menus item in subMenuList)
            {
                if (counter == 0)
                    sb.Append("<ul class=\"sub-menu\">");

                sb.Append("<li><a href=\"" + item.MenuUrl + "\">" + item.MenuName + "</a></li>");

                counter++;
            }

            if (counter > 0)
            {
                sb.Append("</ul>");
                sb.Append("<i class=\"arrow-main-menu fa fa-angle-right\" aria-hidden=\"true\"></i>");
            }
            sb.Append("</li>");
        }


    }
}