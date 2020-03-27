using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enpratik.Data
{
    public class EventCustomerMaps_Temps
    {
        public int Id { get; set; }

        public string GuidId { get; set; }

        public int EventId { get; set; }

        public int CustomerId { get; set; }

        public DateTime EventDate { get; set; }

        public string EventDescription { get; set; }

        public bool IsRezervation { get; set; }

        public bool IsCheckIn { get; set; }

        public string RezervationCode { get; set; }

        public string CheckInCode { get; set; }

        public Nullable<DateTime> RezervationDate { get; set; }

        public Nullable<DateTime> CheckInDate { get; set; }

        public Nullable<DateTime> CreatedDate { get; set; }

        public Nullable<int> CreatedUserId { get; set; }

        public Nullable<DateTime> UpdatedDate { get; set; }

        public Nullable<int> UpdatedUserId { get; set; }

        public Nullable<DateTime> DeletedDate { get; set; }

        public Nullable<int> DeletedUserId { get; set; }

        public bool IsActive { get; set; }

        public string Token { get; set; }
    }
}
