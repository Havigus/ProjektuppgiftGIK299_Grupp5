using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektuppgiftGIK299_Grupp5
{
    internal class Booking
    {
        public int BookingId { get; set; }
        public string CustomerName { get; set; }

        public string CustomerRegNr { get; set; }
        
        public DateTime BookingDate { get; set; }
        
        public Services Service { get; set; }
        
        public string Comment { get; set; }

        internal Booking(int bookingId, string customerName, string customerRegNr, DateTime bookingDate, Services service, string? comment)
        {
            CustomerName = customerName;
            CustomerRegNr = customerRegNr;
            BookingDate = bookingDate;
            Service = service;
            BookingId = bookingId;
            Comment = string.IsNullOrEmpty(comment) ? "No Comment." : comment;
        }

        public override string ToString()
        {
            return $"BookingId: {BookingId}" +
                   $"\nCustomer Name: {CustomerName}" +
                   $"\nCustomer Car RegNr: {CustomerRegNr}" +
                   $"\nDate and time: {BookingDate:f}" +
                   $"\nService: {Service}" +
                   $"\nComment: {Comment}";
        }
    }
}
