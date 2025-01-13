using System.Security.Cryptography.X509Certificates;

namespace ProjektuppgiftGIK299_Grupp5;

public class AdminPanel
{
    private List<Booking> bookings = new List<Booking>();
    private int bookingCounter = 1;
    public bool IsOverlapping(DateTime bookingDate)
    {
        DateTime previousSlot = bookingDate.AddMinutes(-30);
        DateTime startTime = bookingDate;
        DateTime endTime = bookingDate.AddMinutes(30);

        return bookings.Any(booking => (booking.BookingDate >= previousSlot && booking.BookingDate <= endTime) ||
        (booking.BookingDate.AddMinutes(-30) <= endTime && booking.BookingDate.AddMinutes(30) >= previousSlot));
    }

    public void AddBooking(string customerName, string customerRegNr, DateTime bookingDate, Services services, string comment)
    {
        

        if (IsOverlapping(bookingDate))
        {
            Console.WriteLine("Error: Double booking! Please choose a different time.");
            Thread.Sleep(1000);
        }

        else
        {
            int bookingId = bookingCounter;
            var booking = new Booking(bookingId, customerName, customerRegNr, bookingDate, services, comment);
            bookings.Add(booking);
            Console.WriteLine();
            Console.WriteLine("Booking added successfully.");
            bookingCounter++;
        }
    }

    public void ViewBookings(DateTime date)
    {
        var todaysBookings = bookings.Where(b => b.BookingDate.Date == date).ToList();
        if (todaysBookings.Count == 0)
        {
            Console.WriteLine("There are no bookings for this date.");
            return;
        }
        Console.WriteLine($"Bookings for {date.ToShortDateString()}");
        foreach (var booking in todaysBookings)
        {
            Console.WriteLine();
            Console.WriteLine(booking);
            Console.WriteLine();
            
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

    public void AddDummyBooking()
    {
        bookings.Add(new Booking(
            bookingId: bookingCounter,
            customerName: "Bob",
            customerRegNr:"ABC123",
            bookingDate: new DateTime(2025, 02, 02, 10, 0, 0),
            service: Services.Hjulinställning,
            comment: "Bilen drar åt höger"
            ));
        bookingCounter++;
        bookings.Add(new Booking(
            bookingId: bookingCounter,
            customerName: "Sven",
            customerRegNr:"CBA321",
            bookingDate: new DateTime(2025, 02, 10, 11, 30, 0),
            service: Services.DäckbyteSäsong,
            comment: "Hylsan till låsbultarna ligger i handskfacket"
        ));
        bookingCounter++;
        bookings.Add(new Booking(
            bookingId: bookingCounter,
            customerName: "Britt-Marie",
            customerRegNr:"HEJ666",
            bookingDate: new DateTime(2025, 02, 04, 8, 0, 0),
            service: Services.EfterdragningDäck,
            comment: ""
        ));

    }

}