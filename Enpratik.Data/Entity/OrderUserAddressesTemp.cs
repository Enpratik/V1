using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enpratik.Data
{
    public class OrderUserAddressesTemp
    {
        public int Id { get; set; }

        public string GuidId { get; set; }
        public string OrderBasketId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Mail { get; set; }

        public string IdentityNumber { get; set; }

        public string GsmNumber { get; set; }

        public string TeslimatAdres { get; set; }

        public string FaturaAdres { get; set; }

        public DateTime Tarih { get; set; }

    }
}
