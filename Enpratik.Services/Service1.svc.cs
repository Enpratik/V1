using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Enpratik.Services
{
    [ServiceContract]
    public class Service1 
    {
        [OperationContract, WebGet(ResponseFormat = WebMessageFormat.Json)]
        public List<Mutluluk> GetData(int value)
        {
            List<Mutluluk> m = new List<Mutluluk>();
            m.Add(new Mutluluk { id=1, isim="Mesut", soyad="Bayır", cinsiyet = Mutluluk.Cinsiyet.Erkek});
            m.Add(new Mutluluk { id = 2, isim = "Özgül", soyad = "Bayır", cinsiyet = Mutluluk.Cinsiyet.Kadın });

            return m;
            //return string.Format("You entered: {0}", value);
        }

        
        public string GetDataUsingDataContract(string composite)
        {
            //if (composite == null)
            //{
            //    throw new ArgumentNullException("composite");
            //}
            //if (composite.BoolValue)
            //{
            //    composite.StringValue += "Suffix";
            //}
            return composite;
        }
    }

    public class Mutluluk
    {
        public int id { get; set; }
        public string isim { get; set; }
        public string soyad{ get; set; }
        public Cinsiyet cinsiyet { get; set; }
        
        public enum Cinsiyet
        {
            Erkek,
            Kadın
        }

    }

}
