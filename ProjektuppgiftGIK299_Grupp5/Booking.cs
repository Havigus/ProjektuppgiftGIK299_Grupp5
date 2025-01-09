using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektuppgiftGIK299_Grupp5
{
    internal class Booking
    {
        private string CustName { get; set; }

        private string CustRegNr { get; set; }
        
        private DateTime BookingTime { get; set; }
        
        private Services Service { get; set; }

        internal Booking(string custName,
            string custRegNr,
            DateTime? bookingTime = null,
            Services service) //WHAT DO YOU WANT FROM ME
        {
            CustName = custName;
            CustRegNr = custRegNr;
            BookingTime = bookingTime ?? DateTime.Now;
            Service = service;
        }
    }
}
