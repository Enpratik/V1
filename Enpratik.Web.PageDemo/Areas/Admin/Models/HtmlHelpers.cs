using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace System.Web.Mvc
{
    public static class HtmlHelpers
    {
        public static MvcHtmlString MessageBox<TModel>(this HtmlHelper<TModel> html, string messageText, int messageType) {
            
            string[] msgTypeArray = new string[] { "error", "success", "info" };            
            string htmlCode = "new PNotify({" +
                         // "        title: 'Regular Success'," +
                          "        text: '"+messageText+"'," +
                          "        type: '"+msgTypeArray[messageType]+"'," +
                          "        styling: 'bootstrap3'" +
                          "  });";


            
            return MvcHtmlString.Create(htmlCode);
        }

        public static MvcHtmlString MyHelperFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, object htmlAttributes = null)
        {
            var data = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            string propertyName = data.PropertyName;
            TagBuilder span = new TagBuilder("span");
            span.Attributes.Add("name", propertyName);
            span.Attributes.Add("data-something", "something");

            if (htmlAttributes != null)
            {
                var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                span.MergeAttributes(attributes);
            }

            return new MvcHtmlString(span.ToString());
        }

        public static MvcHtmlString DisplayIsActive<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            string htmlCode = "<span class=\"label label-{0}\">{1}</span>";
            
            var data = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            

            if (Convert.ToBoolean((data.Model??false)))
                htmlCode = string.Format(htmlCode, "success", "Evet");
            else
                htmlCode = string.Format(htmlCode, "danger", "Hayır");

            return MvcHtmlString.Create(htmlCode);
        }


        public static MvcHtmlString MenuBar(this HtmlHelper html)
        {
            string controllerName = HttpContext.Current.Request.RequestContext.RouteData.Values["controller"].ToString().ToLower();
            string actionName = HttpContext.Current.Request.RequestContext.RouteData.Values["action"].ToString().ToLower();

            //YetkiService yetkiService = DependencyResolver.Current.GetService<YetkiService>();
            //List<ModulAksiyonModel> listeModulAksiyon = yetkiService.ListeModulAksiyon().Data;

            //ModulAksiyonModel kayitModulAksiyon = listeModulAksiyon
            //    .FirstOrDefault(m =>
            //        m.ModulDeger.ToLower() == controllerName &&
            //        m.AksiyonDeger.ToLower() == actionName);

            //List<MenuModel> listeMenu = yetkiService.ListeMenu().Data;
            //User user = Current.User;

            //List<ModulAksiyonModel> listeLink = new List<ModulAksiyonModel>();
            //if (kayitModulAksiyon != null)
            //{
            //    List<ModulAksiyonModel> listeModulAksiyon2 = listeModulAksiyon
            //          .Where(ma => ma.AksiyonNo == Statics.Aksiyon.Index || ma.No == kayitModulAksiyon.No)
            //          .ToList();

            //    listeLink = RecGenerateModulLinks(listeModulAksiyon2, kayitModulAksiyon.No, new List<ModulAksiyonModel>());
            //    listeLink.Reverse();
            //}

            var url = new UrlHelper(HttpContext.Current.Request.RequestContext);

            string result = @"<ul class='page-sidebar-menu page-header-fixed' data-slide-speed='200' data-auto-scroll='true' data-auto-scroll='true' data-slide-speed='200'>
                <li>
                    <a href='" + url.Action("Index", "Home") + "' class='nav-link'>";

            result += @"<i class='fa fa-home'></i>
                        <span class='title'>Anasayfa</span>
                    </a>
                </li>";

            //result += RecGenerateMenu(listeMenu, listeLink, user, null, "");

            result += @"</ul>";

            return MvcHtmlString.Create(result);
        }

        //private static string RecGenerateMenu(List<MenuModel> liste, List<ModulAksiyonModel> listeLink, User user, int? ustMenuNo, string listeMenu)
        //{
        //    var liste2 = liste.Where(l => l.UstMenuNo == ustMenuNo).ToList();

        //    var url = new UrlHelper(HttpContext.Current.Request.RequestContext);

        //    foreach (var kayit in liste2)
        //    {
        //        var listeAltMenu = liste.Where(l => l.UstMenuNo == kayit.No).ToList();

        //        string href = "#";
        //        if (kayit.ModulAksiyonNo.HasValue)
        //            href = url.Action(kayit.AksiyonDeger, kayit.ModulDeger);

        //        if (listeAltMenu.Any())
        //        {
        //            var yetkiVarMi = listeAltMenu.Any(l =>
        //                l.YetkiTipNo != Statics.YetkiTip.RolBazli ||
        //                l.Yetki.Any(y => user.Roller.Any(r => r.RolNo == y.RolNo)));

        //            if (yetkiVarMi)
        //            {
        //                bool acikMi = listeAltMenu.Any(lam => listeLink.Any(ll => ll.No == lam.ModulAksiyonNo));

        //                listeMenu += "<li class='nav-item " + (acikMi ? "open active" : "") + "'>" + Environment.NewLine
        //                    + "<a href='javascript:' class='nav-link nav-toggle'>" + Environment.NewLine
        //                    + (ustMenuNo.HasValue == false ? "<i class='fa " + kayit.Simge + "'></i>" : "")
        //                    + "<span class='title'>" + kayit.MenuAd + "</span>" + Environment.NewLine
        //                    + "<span class='arrow'></span>" + Environment.NewLine
        //                    + "</a>" + Environment.NewLine
        //                    + "<ul class='sub-menu'>" + Environment.NewLine;

        //                listeMenu = RecGenerateMenu(liste, listeLink, user, kayit.No, listeMenu);

        //                listeMenu += "</ul>" + Environment.NewLine
        //                    + "</li>" + Environment.NewLine;
        //            }
        //        }
        //        else
        //        {
        //            var yetkiVarMi = kayit.YetkiTipNo != Statics.YetkiTip.RolBazli ||
        //                kayit.Yetki.Any(y => user.Roller.Any(r => r.RolNo == y.RolNo));

        //            if (yetkiVarMi && kayit.ModulAksiyonNo.HasValue)
        //            {
        //                bool acikMi = listeLink.Any(ll => ll.ModulDeger.ToLower() == kayit.ModulDeger.ToLower());

        //                listeMenu += " <li class='nav-item " + (acikMi ? "open active" : "") + "' data-ModulAksiyonNo='" + kayit.ModulAksiyonNo + "'>" + Environment.NewLine
        //                    + "<a href='" + href + "' class='nav-link'>"
        //                    + (ustMenuNo.HasValue == false ? "<i class='fa " + kayit.Simge + "'></i>" : "")
        //                    + "<span class='title'>"
        //                    + kayit.MenuAd + "</span></a>" + Environment.NewLine
        //                    + "</li>" + Environment.NewLine;
        //            }
        //        }
        //    }

        //    return listeMenu;
        //}


    }
}