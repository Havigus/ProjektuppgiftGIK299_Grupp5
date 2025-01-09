namespace ProjektuppgiftGIK299_Grupp5;

public class AdminPanel
{
    private List<Booking> bookings = new List<Booking>();
    private int bookingCounter = 0;

    public void AddBooking(string customerName, string customerRegNr, DateTime bookingTime, Services services, string comment)
    {
        if (bookings.Any(b => b.BookingTime == bookingTime)) // TODO! Fixa så att varje bookning är typ 30 min så att man inte kan booka kl 11:00 sen en 11:02.
        {
            Console.WriteLine("Error: Double booking! Please choose a different time.");
            Console.Beep(1000, 200);
        }

        int bookingId = bookingCounter;
        var booking = new Booking(bookingId, customerName, customerRegNr, bookingTime, services, comment);
        bookings.Add(booking);
        Console.WriteLine("Booking added successfully.");
        bookingCounter++;
    }

    public void ViewBookings(DateTime date)
    {
        var todaysBookings = bookings.Where(b => b.BookingTime == date).ToList();
        if (todaysBookings.Count == 0)
        {
            Console.WriteLine("There are no bookings for this date.");
            return;
        }
        Console.WriteLine($"Bookings for {date.ToShortDateString()}");
        foreach (var booking in todaysBookings)
        {
            Console.WriteLine(booking);
            
        }
    }

    public void ChangeBooking()
    {
        //kod för att ändra på en bookning
    }

    public void CancelBooking()
    {
        //kod för att ta bort en bokning
    }
}