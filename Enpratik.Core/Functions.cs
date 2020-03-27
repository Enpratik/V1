using Enpratik.Data;
using Enpratik.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
using System.Web.Script.Serialization;
using System.Globalization;
using System.Configuration;

namespace Enpratik.Core
{
    public static class Functions
    {
        #region DataUtils
        public static string ToMonthName(this DateTime dateTime)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dateTime.Month);
        }

        public static string ToShortMonthName(this DateTime dateTime)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(dateTime.Month);
        }
        public static byte ToByte(this object obj)
        {
            return Convert.ToByte(obj);
        }
        public static int ToInt32(this object obj)
        {
            return Convert.ToInt32(obj);
        }

        public static double ToDouble(this object obj)
        {
            return Convert.ToDouble(obj);
        }
        public static bool ToBoolean(this object obj)
        {
            return Convert.ToBoolean(obj);
        }
        public static DateTime ToDateTime(this object obj)
        {
            return Convert.ToDateTime(obj);
        }
        public static decimal ToDecimal(this object obj)
        {
            return Convert.ToDecimal(obj);
        }
        public static float ToSingle(this object obj)
        {
            return Convert.ToSingle(obj);
        }
        #endregion

        public static string GetProductImages(Product_Picture_Mapping images) {

            if (images == null)
                return "https://www.enpratik.net/img/no-image.png";

            if (string.IsNullOrEmpty(images.ImageUrl))
                return "https://www.enpratik.net/img/no-image.png";

            return "/admin" + (images.ImageUrl.StartsWith("/") ? "" : "/") + images.ImageUrl;
        }
        public static string GetThemeName() {

            return ConfigurationManager.AppSettings["ThemeName"];
        }

        public static string GetProductSectionTitle(int val)
        {
            //Liste Tipi değeri (yeni ürünler için 1, beğenilen ürünler için 2, inidirimli ürünler için 3, sınırlı sayıda ürünler için 4, vitrin ürünü için 5

            switch (val)
            {
                case 0:
                    return "Ürünlerimiz";
                case 1:
                    return "Yeni Ürünler";
                case 2:
                    return "Beğenilen Ürünler";
                case 3:
                    return "İndirimli Ürünler";
                case 4:
                    return "Sınırlı Sayıda Ürünler";
                case 5:
                    return "Vitrin Ürünleri";
                case 15:
                    return "Karakılçık Ailesi";
                default:
                    return "Ürünler";
            }
        }
        
        public static string MoneyFormat(int row, double? price)
        {
            string result = "";
            if (price == null)
                price = 0;

            switch (row)
            {
                case 0:
                    string number = price.ToString().Split(',')[0].ToString();
                    if (number.Equals("0"))
                    {
                        result = price.ToString();
                    }
                    else
                    {
                        result = String.Format("{0:#,###.0;0}", price);
                        if (result.ToCharArray()[0].ToString().Equals("."))
                            result = "0" + result;
                    }
                    break;
                case 1: break;
                case 2:
                    result = String.Format("{0:#,###.00;0}", price);
                    if (result.ToCharArray()[0].ToString().Equals(","))
                        result = "0" + result;
                    break;
                case 3:
                    result = String.Format("{0:#,###.00#;0}", price);
                    break;
                case 4:
                    result = String.Format("{0:#,###.00##;0}", price);
                    break;
                case 5:
                    result = String.Format("{0:#,###.00###;0}", price);
                    break;
            }
            return result;
        }
        public static List<ParamJsonDTO> GetWidgetContent(string jsonData)
        {
            if (string.IsNullOrEmpty(jsonData))
                return new List<ParamJsonDTO>();

            List<ParamJsonDTO> paramJsonList = new JavaScriptSerializer().Deserialize<List<ParamJsonDTO>>(jsonData);
            return paramJsonList;
        }
        public static List<ParamJsonDTO> GetWidgetContent(int widgetId, int widgetZoneMappingId)
        {
            EnPratik_DataHelper db = new Enpratik.Data.EnPratik_DataHelper();
            var jsonData = db.WidgetZonesParamMapping.Where(w => w.WidgetZoneMappingId == widgetZoneMappingId && w.WidgetId == widgetId).Select(w => w.ParamJson).FirstOrDefault();

            if (string.IsNullOrEmpty(jsonData))
                return null;

            List<ParamJsonDTO>  paramJsonList = new JavaScriptSerializer().Deserialize<List<ParamJsonDTO>>(jsonData);

            return paramJsonList;
        }

        public static string setProperty(string propertyName, string propertyValue)
        {
            return "@" + propertyName + " = " + propertyValue;
        }

        public static List<Targets> Targets()
        {
            List<Targets> data = new List<Targets>();
            data.Add(new Targets() { TargetType = "_self", TargetName = "Aynı pencerede açılır" });
            data.Add(new Targets() { TargetType = "_blank", TargetName = "Yeni pencerede açılır" });

            return data;
        }

        public static string GeneratePassword()
        {
            string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@$?_-";
            char[] chars = new char[8];
            Random rd = new Random();

            for (int i = 0; i < 8; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }
            return new string(chars);
        }
        public static string Base64Encode(string value)
        {
            var valBytes = System.Text.Encoding.UTF8.GetBytes(value);
            return System.Convert.ToBase64String(valBytes);
        }
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        public static string GetUrl(string textVal)
        {
            textVal = textVal.ToLower();
           
            return textVal.Replace(" ", "-").Replace(",","").Replace("ı", "i").Replace(":", "").Replace(".", "").Replace("ö", "o").Replace("ş", "s").Replace("&", "-").Replace("ü", "u").Replace("ğ", "g").Replace("ç", "c").Replace("+", "").Replace("_", "").Replace("'", "").Replace("^", "").Replace("#", "").Replace("$", "").Replace("%", "").Replace(";", "").Replace("/", "").Replace("!", "").Replace("{", "").Replace("}", "").Replace("[", "").Replace("]", "").Replace("|", "").Replace(">", "").Replace("<", "").Replace("(", "").Replace(")", "").Replace("=", "").Replace("°", "").Replace("“", "").Replace("’", "").Replace("‘", "").Replace("?", "").Replace("@", "").Replace("~", "").Replace("`", "").Replace("®", "").Replace("\\", "").Replace("----", "-").Replace("---", "-").Replace("--", "-").Replace("*", "-");
        }
        public static bool isMail(string text)
        {
            try
            {
                MailAddress m = new MailAddress(text);
                return false;
            }
            catch
            {
                return true;
            }
        }
        public static bool isNumeric(string text)
        {
            try
            {
                Convert.ToInt32(text);
                return false;
            }
            catch
            {
                return true;
            }
        }

        /// <summary>
        /// Sessiondaki userId bilgisini verir
        /// </summary>
        /// <returns></returns>
        public static int GetUserId() {
            try
            {
                return ((Admins)(System.Web.HttpContext.Current.Session["Admin_User"])).Id;
            }
            catch //(Exception ex)
            {
                return 1;
                //throw new Exception("UserId alınırken hata meydana geldi! HATA  : " + ex.Message);
            }
        }

        public static void SetMessageViewBag(System.Web.Mvc.Controller page, string message, int messageType, string redirectUrl) {

            page.ViewBag.message = message;
            page.ViewBag.messageType = messageType;

            if (!string.IsNullOrEmpty(redirectUrl))
                page.ViewBag.redirect = redirectUrl;

        }
        public static void SetMessageViewBag(System.Web.Mvc.Controller page, string message, int messageType)
        {
            SetMessageViewBag(page, message, messageType, null);
        }
        
        public static DataTable ClassToDataTable(Object classObject)
        {
            DataTable dt = new DataTable();

            foreach (PropertyInfo pInfo in classObject.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                dt.Columns.Add(new DataColumn(pInfo.Name, pInfo.PropertyType));
            }

            return dt;
        }


        public static string FormatProductContent(string text, bool isNumber)
        {

            char[] arr = text.ToArray();

            string result = "";

            foreach (var item in arr)
            {
                if (!Functions.isNumeric(item.ToString()))
                {
                    if (isNumber)
                        result += item.ToString();
                }
                else
                {
                    if (isNumber)
                        continue;
                    result += item.ToString();
                }
            }

            return result.Trim();
        }

        public static string GetProductPrice(ProductBoxs boxProduct, string subscription)
        {
            if (subscription == "1")
                return Math.Ceiling(boxProduct.MonthlyPrice).ToString();
            if (subscription == "3")
                return Math.Ceiling(boxProduct.ThreeMonthsPrice).ToString();
            if (subscription == "6")
                return Math.Ceiling(boxProduct.SixMonthsPrice).ToString();

            return "";
        }

        public static string GetProductPrice(ProductBoxs boxProduct, string subscription, bool ii)
        {
            double price = Convert.ToDouble(GetProductPrice(boxProduct, subscription));
            price = price / Convert.ToDouble(subscription);
            return Math.Ceiling(price).ToString();
        }
        

    }
}
