using Enpratik.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enpratik.Core
{
    public static class HtmlHelpers
    {
        public static string MessageBox(string messageText, int messageType)
        {
            string[] msgTypeArray = new string[] { "error", "success", "info" };
            string[] msgTitleArray = new string[] { "HATA", "BAŞARILI", "BİLGİ" };

            return "new PNotify({" +
                   "    title: '"+ msgTitleArray[messageType] + "'," +
                   "    text: '" + messageText + "'," +
                   "    type: '" + msgTypeArray[messageType] + "'," +
                   "    styling: 'bootstrap3'" +
                   " });";
            
        }
        public static string GetAdminMenu()
        {
            try
            {
                EnPratik_DataHelper db = new EnPratik_DataHelper();

                StringBuilder sb = new StringBuilder();

                var mainMenuList = db.Admin_MainMenu.Where(m => m.IsActive==true).ToList().OrderBy(m => m.OrderIndex);

                var subMenuListDb = db.Admin_SubMenu.Where(m => m.IsActive == true).ToList();

                var modules = db.Admin_Modules.Where(m => m.IsActive == true).ToList();
                var actions = db.Admin_Module_Actions.Where(m => m.IsActive == true).ToList();

                foreach (Admin_MainMenu item in mainMenuList)
                {
                    sb.Append("<li>");
                    GetAdminSubMenu(item, subMenuListDb, modules, actions, sb);
                    sb.Append("</li>");
                }
                return sb.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static void GetAdminSubMenu(Admin_MainMenu mainMenu, List<Admin_SubMenu> subMenuListDb, List<Admin_Modules> modules, List<Admin_Module_Actions> actions, StringBuilder sb)
        {
            sb.Append("<a "+ (string.IsNullOrEmpty(mainMenu.Link) ? "" : " href =\"" + mainMenu.Link + "\"") + "><i class=\""+mainMenu.CssName+"\"></i> "+ mainMenu.MainMenuName + "{@}</a>");
            
            int counter = 0;
            var subMenuList = subMenuListDb.Where(m => m.MainMenuId == mainMenu.Id).ToList().OrderBy(m => m.OrderIndex);

            foreach (Admin_SubMenu subMenu in subMenuList)
            {
                if (counter == 0)
                    sb.Append("<ul class=\"nav child_menu\">");

                var module = modules.Where(m => m.Id == subMenu.ModuleId).FirstOrDefault();
                var action = actions.Where(m => m.Id == subMenu.ActionId).FirstOrDefault();

                sb.Append("<li><a href=\"/admin/" + module.ModuleName + "/" + action.ActionName + "\">" + subMenu.MenuName + "</a></li>");

                //sb.Append("<li><a href=\"/admin/" + module.ModuleName + "/" + action.ActionName + "\">" + subMenu.MenuName + "</a></li>");

                counter++;
            }

            if (counter > 0)
            {
                sb.Replace("{@}", " <span class=\"fa fa-chevron-down\"></span>");
                sb.Append("</ul>");
            }
            else
                sb.Replace("{@}", "");
        }
    }
}
