namespace ProjektuppgiftGIK299_Grupp5;

public class AdminPanel
{
    private List<Booking> bookings = new List<Booking>();
    private int bookingCounter = 0;

    public void AddBooking(string customerName, string customerRegNr, DateTime bookingTime, Services services)
    {
        if (bookings.Any(b => b.BookingTime == bookingTime))
        {
            Console.WriteLine("Error: Double booking! Please choose a different time.");
            Console.Beep(1000, 200);
        }
        
        var booking = new Booking(bookingCounter++, customerName, customerRegNr, bookingTime, services);
        bookings.Add(booking);
        Console.WriteLine("Booking added successfully.");
    }
}