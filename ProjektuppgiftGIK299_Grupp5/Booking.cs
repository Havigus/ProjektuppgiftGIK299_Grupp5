using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektuppgiftGIK299_Grupp5
{
    internal class Booking
    {
        public string CustomerName { get; set; }

        public string CustomerRegNr { get; set; }
        
        public DateTime BookingTime { get; set; }
        
        public Services Service { get; set; }

        internal Booking(int i, string customerName,
            string customerRegNr,
            DateTime? bookingTime = null,
            Services service) //WHAT DO YOU WANT FROM ME
        {
            CustomerName = customerName;
            CustomerRegNr = customerRegNr;
            BookingTime = bookingTime ?? DateTime.Now;
            Service = service;
        }
    }
}
