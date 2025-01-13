namespace ProjektuppgiftGIK299_Grupp5
{
    internal class Booking
    {
        public int BookingId { get; set; } // unique Id for the booking
        
        public string CustomerName { get; set; } // name of the customer

        public string CustomerRegNr { get; set; } //The registration number of the customer vehicle
        
        public DateTime BookingDate { get; set; } // the date and time the booking is scheduled
        
        public Services Service { get; set; } // the type of service the booking is for
        
        public string Comment { get; set; } // additional comments for the booking

        //creates a new booking instance
        internal Booking(int bookingId, string customerName, string customerRegNr, DateTime bookingDate, Services service, string? comment)
        {
            //initialize the properties
            CustomerName = customerName;
            CustomerRegNr = customerRegNr;
            BookingDate = bookingDate;
            Service = service;
            BookingId = bookingId;
            
            //uses a default comment if none is provided
            Comment = string.IsNullOrEmpty(comment) ? "No Comment." : comment;
        }

        //returns a formated string with booking details
        public override string ToString()
        {
            return $"BokningsId: {BookingId}" +
                   $"\nKundens Namn: {CustomerName}" +
                   $"\nRegistreringsnummer: {CustomerRegNr}" +
                   $"\nDatum och tid: {BookingDate:f}" +
                   $"\nTjänst: {Service}" +
                   $"\nKommentar: {Comment}";
        }
    }
}
