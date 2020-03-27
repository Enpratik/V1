using Enpratik.Core;
using Enpratik.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Enpratik.AdminPanel.WebApi
{
    public partial class EnpratikApi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string sqlText = "Insert Into Admin_Module_Actions (ModuleId, ActionName, IsActive) Values ({0},'{1}',1);";

            //string[] arr = new string[] { "3", "4", "5", "7", "27", "8", "28", "29", "9", "30", "40", "10", "11", "31", "42", "44", "45", "13", "14", "16", "46", "47", "17", "18", "49", "19", "20", "21", "50", "51", "52", "53", "55", "26", "23", "24", "22", "58", "59", "62", "33", "34", "35", "65", "67" };
            //foreach (var item in arr)
            //{
            //    Response.Write(String.Format(sqlText,item, "Create"));
            //    Response.Write("<br>");
            //    Response.Write(String.Format(sqlText,item, "Delete"));
            //    Response.Write("<br>");
            //    Response.Write(String.Format(sqlText,item, "Details"));
            //    Response.Write("<br>");
            //    Response.Write(String.Format(sqlText,item, "Edit"));
            //    Response.Write("<br>");
            //    Response.Write(String.Format(sqlText,item, "Index"));
            //    Response.Write("<br>");
            //}
        }

        [System.Web.Services.WebMethod]
        public static List<Admin_Module_Actions> GetActions(int moduleId)
        {
            EnPratik_DataHelper db = new EnPratik_DataHelper();
            var modules = db.Admin_Module_Actions.Where(a => a.ModuleId == moduleId && a.IsActive == true).ToList();
            return modules;
        }

        [System.Web.Services.WebMethod]
        public static List<MenusDTO> GetMenuPageList(int menuTypeId)
        {
            EnPratik_DataHelper db = new EnPratik_DataHelper();

            var result = new List<MenusDTO>();

            string menuTitle = " -- İçerik Seçiniz -- ";

            switch (menuTypeId)
            {
                case 1:
                    menuTitle = " -- Sayfa Seçiniz -- ";
                    result = (from p in db.DynamicPages
                             where (p.IsActive == true & p.IsPublished == true)
                             select new MenusDTO {
                                 Id = p.Id,
                                 MenuName = p.PageName
                             }).ToList();
                    break;
                case 2:
                    menuTitle = " -- Blog Seçiniz -- ";
                    result = (from b in db.Blogs
                             where (b.IsActive == true & b.IsPublished == true)
                             select new MenusDTO{
                                 Id = b.Id,
                                 MenuName = b.BlogTitle
                             }).ToList();
                    break;
                case 3:
                    menuTitle = " -- Ürün Seçiniz -- ";
                    result = (from p in db.Products
                             where (p.IsActive == true & p.Published == true)
                             select new MenusDTO{
                                 Id = p.Id,
                                 MenuName = p.ProductName
                             }).ToList();
                    break;
                case 4:
                    menuTitle = " -- Haber Seçiniz -- ";
                    result = (from n in db.News
                             where (n.IsActive == true & n.IsPublished == true)
                             select new MenusDTO{
                                 Id = n.Id,
                                 MenuName = n.NewsTitle
                             }).ToList();
                    break;
                default:
                    break;
            }

            result.Insert(0, new MenusDTO() { Id = 0, MenuName = menuTitle });

            return result;
        }

        [System.Web.Services.WebMethod]
        public static string CreateProductVariantAttributeMapping(int productId, string data)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                ArrayList variantAttributeValueList = new ArrayList();


                string[] variantAttribute = data.Split(';');
                string[] variantAttributeValue = new string[] { };

                foreach (var item in variantAttribute)
                {
                    if (string.IsNullOrEmpty(item))
                        continue;

                    string[] temp = item.Split(':');
                    
                    int variantId = temp[0].ToInt32();
                    
                    variantAttributeValue = temp[1].Split('-');

                    SortedList<string, string> sl = new SortedList<string, string>();

                    foreach (var item1 in variantAttributeValue)
                    {
                        if (string.IsNullOrEmpty(item1))
                            continue;

                        string[] temp2 = item1.Split('|');

                        string key = temp2[0];
                        string value = temp2[1];
                        sb.Append("variantId:"+variantId + " --  attr : " + item1+System.Environment.NewLine);

                        sl.Add(key, value);

                    }

                    if (sl.Count == 0)
                        continue;

                    variantAttributeValueList.Add(sl);
                }


                //EnPratik_DataHelper db = new EnPratik_DataHelper();
                //Product_VariantAttribute_Mapping p = new Product_VariantAttribute_Mapping();
                //p.ProductId = productId;
                //p.VariantAttributeId = variantAttributeId;
                //p.VariantAttributeValueId = variantAttributeValueId;

                //db.Product_VariantAttribute_Mapping.Add(p);
                //db.SaveChanges();
                List<Products> products = GetProductVariantCombinationData(variantAttributeValueList);
                var json = new JavaScriptSerializer().Serialize(products);

                return json;

            }
            catch (Exception ex)
            {
                string log = ex.Message;
                return "0";
            }
        }
        
        public static List<Products> GetProductVariantCombinationData(ArrayList variantAttributeValueList)
        {
              string  productName = "product-name";

            List<Products> productList = new List<Products>();

            try
            {

                switch (variantAttributeValueList.Count)
                {
                    case 1:
                        foreach (var item in (SortedList<string, string>)variantAttributeValueList[0])
                        {
                            Products p = new Products();
                            p.ProductName= productName + "-" + item.Value;
                            productList.Add(p);
                        }
                        break;
                    case 2:
                        foreach (var item in (SortedList<string, string>)variantAttributeValueList[0])
                        {
                            foreach (var item1 in (SortedList<string, string>)variantAttributeValueList[1])
                            {
                                Products p = new Products();
                                p.ProductName = productName + "-" + item.Value + "-" + item1.Value;
                                productList.Add(p);
                            }
                        }
                        break;
                    case 3:
                        foreach (var item in (SortedList<string, string>)variantAttributeValueList[0])
                        {
                            foreach (var item1 in (SortedList<string, string>)variantAttributeValueList[1])
                            {
                                foreach (var item2 in (SortedList<string, string>)variantAttributeValueList[2])
                                {
                                    Products p = new Products();
                                    p.ProductName = productName + "-" + item.Value + "-" + item1.Value + "-" + item2.Value;
                                    productList.Add(p);
                                }
                            }
                        }
                        break;
                    case 4:
                        foreach (var item in (SortedList<string, string>)variantAttributeValueList[0])
                        {
                            foreach (var item1 in (SortedList<string, string>)variantAttributeValueList[1])
                            {
                                foreach (var item2 in (SortedList<string, string>)variantAttributeValueList[2])
                                {
                                    foreach (var item3 in (SortedList<string, string>)variantAttributeValueList[3])
                                    {
                                        Products p = new Products();
                                        p.ProductName = productName + "-" + item.Value + "-" + item1.Value + "-" + item2.Value + "-" + item3.Value;
                                        productList.Add(p);
                                    }
                                }
                            }
                        }
                        break;
                    case 5:
                        foreach (var item in (SortedList<string, string>)variantAttributeValueList[0])
                        {
                            foreach (var item1 in (SortedList<string, string>)variantAttributeValueList[1])
                            {
                                foreach (var item2 in (SortedList<string, string>)variantAttributeValueList[2])
                                {
                                    foreach (var item3 in (SortedList<string, string>)variantAttributeValueList[3])
                                    {
                                        foreach (var item4 in (SortedList<string, string>)variantAttributeValueList[4])
                                        {
                                            Products p = new Products();
                                            p.ProductName = productName + "-" + item.Value + "-" + item1.Value + "-" + item2.Value + "-" + item3.Value + "-" + item4.Value;
                                            productList.Add(p);
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception) { }
            return productList;
        }

        [System.Web.Services.WebMethod]
        public static string WidgetZonesMappingSorted(int id, int index)
        {
            try
            {
                EnPratik_DataHelper db = new EnPratik_DataHelper();
                var data = db.WidgetZonesMapping.Where(w => w.Id == id).FirstOrDefault();
                data.OrderIndex = index;

                db.Entry(data).State = EntityState.Modified;
                db.SaveChanges();

                return "1";
            }
            catch (Exception ex)
            {
                string log = ex.Message;
                return "0";
            }
        }

        [System.Web.Services.WebMethod]
        public static string WidgetZonesMappingUpdateActive(int id, int isActive)
        {
            try
            {
                EnPratik_DataHelper db = new EnPratik_DataHelper();
                var data = db.WidgetZonesMapping.Where(w => w.Id == id).FirstOrDefault();
                data.IsActive = isActive==1 ? true : false;

                db.Entry(data).State = EntityState.Modified;
                db.SaveChanges();

                return "1";
            }
            catch (Exception ex)
            {
                string log = ex.Message;
                return "0";
            }
        }

        [System.Web.Services.WebMethod]
        public static string GetPageTemplateHtml(int id)
        {
            try
            {
                EnPratik_DataHelper db = new EnPratik_DataHelper();
                var data = db.DynamicPage_Templates.Where(w => w.Id == id).FirstOrDefault();

                return data.PageTemplateHtml;
            }
            catch (Exception ex)
            {
                string log = ex.Message;
                return "0";
            }
        }


        [System.Web.Services.WebMethod]
        public static string UpdateVariantAttributeMapping(int id, string imageUrl, string price)
        {
            try
            {
                EnPratik_DataHelper db = new EnPratik_DataHelper();
                var data = db.Product_VariantAttribute_Mapping.Where(p => p.Id == id).FirstOrDefault();

                if (data == null)
                {
                    return "Varyant bilgisi bulunamadı. Sayfayı yenileyip tekrar deneyiniz!";
                }

                data.ImageUrl = imageUrl;
                data.ProductPrice = string.IsNullOrEmpty(price) ? 0 : Convert.ToDouble(price);
                db.Entry(data).State = EntityState.Modified;
                db.SaveChanges();

                return "Varyasyon bilgileri güncellendi!";
            }
            catch (Exception ex)
            {
                string log = ex.Message;
                return "Varyasyon bilgileri güncellemede hata!";
            }
        }


        [System.Web.Services.WebMethod]
        public static string DeleteVariantAttributeMapping(int id)
        {
            try
            {
                EnPratik_DataHelper db = new EnPratik_DataHelper();
                var data = db.Product_VariantAttribute_Mapping.Where(p => p.Id == id).FirstOrDefault();

                if (data == null)
                {
                    return "Varyant bilgisi bulunamadı. Sayfayı yenileyip tekrar deneyiniz!";
                }

                db.Product_VariantAttribute_Mapping.Remove(data);
                db.SaveChanges();

                return "Varyasyon silindi!";
            }
            catch (Exception ex)
            {
                string log = ex.Message;
                return "Varyasyon silmede hata!";
            }
        }


    }
}