using Enpratik.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enpratik.AdminPanel.Model
{
    public class Current
    {
        public static Admins User
        {
            get { return GetUser(); }
            set { HttpContext.Current.Session["Current_Admin"] = value; }
        }
        private static Admins GetUser()
        {
            if (HttpContext.Current == null)
                return null;

            if (HttpContext.Current.Session == null)
                return null;

            Admins user = HttpContext.Current.Session["Current_Admin"] as Admins;

            if (user == null)
                return null;

            Current.User = user;

            return user;
        }

        public static void UserAbandon(){
            HttpContext.Current.Session.Remove("Current_Admin");
            HttpContext.Current.Session.Abandon();
        }
    }
}