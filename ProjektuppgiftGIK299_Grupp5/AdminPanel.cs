using System.Text.RegularExpressions;

namespace ProjektuppgiftGIK299_Grupp5;

public class AdminPanel
{
    //list to keep all the bookings
    public static readonly List<Booking> Bookings = new List<Booking>();
    
    //counter for numbers of bookings
    private int _bookingCounter = 1;
    
    //method the check if booking time is available
    
    
    //method to add booking to list
    public void AddBooking()
    {
        string name = ValidInput.GetValidName();

        string carRegNr = ValidInput.GetValidRegNr();

        DateTime dateTime = ValidInput.GetValidDate();
        
        //prompts the user to select type of service
        var service = ValidInput.SelectService();
        
        string comment = ValidInput.GetComment();
        
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine($"""
                           Bekräfta uppgifterna.

                           Kundnamn: {name}

                           Bilregistreringsnummer: {carRegNr}

                           Datum och tid: {dateTime}

                           Service: {service}

                           Kommentar: {comment}


                           Stämmer det här? Y/N
                           """);
        Console.WriteLine();
        if (Console.ReadKey(intercept: true).KeyChar.ToString().ToUpper() == "Y")
        {
            int bookingId = _bookingCounter;
            Console.WriteLine();
            var booking = new Booking(bookingId,name, carRegNr, dateTime, service, comment);
            Bookings.Add(booking);
            Console.WriteLine();
            Console.WriteLine("Bokningen lades till.");
            _bookingCounter++;
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

    }
    
    //metod to view bookings in bookinglist
    public void ViewBookings(DateTime date)
    {
        var todaysBookings = Bookings.Where(b => b.BookingDate.Date == date).ToList();
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
            Thread.Sleep(1000);
            
        }
        Console.WriteLine("Press enter to continue...");
        Console.ReadLine();
        Console.Clear();
        
    }
    
    //method to find and change a booking
    public void ChangeBooking(int bookingId)
    {
        //find the booking that matches the bookingId 
        var bookingToEdit = Bookings.Find(b => b.BookingId == bookingId);
        if (bookingToEdit != null)
        {
            Console.WriteLine("""
                 Ange med sifra vad du vill ändra på.
                  1. Namn
                  2. RegNummer
                  3. Datum och tid
                  4. Tjänst
                  5. Kommentar
                """);
            switch (Console.ReadKey(intercept: true).KeyChar.ToString())
            {
                case "1":
                    //changes the name
                    Console.WriteLine("Ange ett nytt namn.");
                    bookingToEdit.CustomerName = ValidInput.GetValidName();
                    Console.WriteLine($"Namn ändrat till: {bookingToEdit.CustomerName}");
                    Thread.Sleep(3000);
                    Console.Clear();

                    break;
                case "2":
                    //changes the regNr 
                    Console.WriteLine();
                    Console.WriteLine("Ange ett nytt regnummer.");
                    bookingToEdit.CustomerRegNr = ValidInput.GetValidRegNr();
                    Console.WriteLine($"Registreringsnummer ändrat till: {bookingToEdit.CustomerRegNr}");
                    Thread.Sleep(3000);
                    Console.Clear();
                    

                    break;
                case "3":
                    //changes the date of the booking
                    Console.WriteLine();
                    Console.WriteLine("Ange ett nytt datum och tid (YYYY, MM, DD, HH:MM,)");
                    bookingToEdit.BookingDate = ValidInput.GetValidDate();
                    Console.WriteLine($"Datum och tid ändrat till: {bookingToEdit.BookingDate}");
                    Thread.Sleep(3000);
                    Console.Clear();

                    break;
                case "4":
                    //changes the services 
                    bookingToEdit.Service = ValidInput.SelectService();
                    Console.WriteLine($"Service bytt till: {bookingToEdit.Service}");
                    Thread.Sleep(3000);
                    Console.Clear();

                    break;
                case "5":
                    Console.WriteLine("Ange en ny kommentar.");
                    bookingToEdit.Comment = ValidInput.GetComment();
                    Console.WriteLine($"Kommentaren ändrad till: {bookingToEdit.Comment}");
                    Thread.Sleep(3000);
                    Console.Clear();
                    
                    break;
            }
            
        }
    }
    
    //method to cancel and delete a booking
    public void CancelBooking(int bookingId)
    {
        var bookingToCancel = Bookings.Find(b => b.BookingId == bookingId);
        if (bookingToCancel != null)
        {
            Bookings.Remove(bookingToCancel);
            Console.WriteLine("Bokningen har blivit borttagen.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }

        Console.WriteLine("Det finns ingen bokning med det bokningsid.");
    }

    //method to search booking by regnr
    public void SearchBookingRegNr(string custRegNr)
    {
        var customerBookingRegNr = Bookings.Where(b => b.CustomerRegNr == custRegNr).ToList();

        foreach (var booking in customerBookingRegNr)
        {
            Console.WriteLine();
            Console.WriteLine(booking);
            Console.WriteLine();
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.Clear();
    }
    public void SearchBookingCustName(string custName)
    {
        var customerBookingName = Bookings.Where(b => b.CustomerName.ToUpper() == custName.ToUpper()).ToList();

        foreach (var booking in customerBookingName)
        {
            Console.WriteLine();
            Console.WriteLine(booking);
            Console.WriteLine();
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.Clear();
    }
    public void SearchBookingBookingID(int custID)
    {
        var customerBookingID = Bookings.Where(b => b.BookingId == custID).ToList();

        foreach (var booking in customerBookingID)
        {
            Console.WriteLine();
            Console.WriteLine(booking);
            Console.WriteLine();
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.Clear();
    }

    //adds 3 dummy bookings to the list 
    public void AddDummyBooking()
    {
        Bookings.Add(new Booking(
            bookingId: _bookingCounter,
            customerName: "Bob",
            customerRegNr:"ABC123",
            bookingDate: DateTime.Today.AddHours(11),
            service: Services.Hjulinställning,
            comment: "Bilen drar åt höger"
            ));
        _bookingCounter++;
        Bookings.Add(new Booking(
            bookingId: _bookingCounter,
            customerName: "Sven",
            customerRegNr:"CBA321",
            bookingDate: DateTime.Today.AddHours(11).AddMinutes(30), 
            service: Services.DäckbyteSäsong,
            comment: "Hylsan till låsbultarna ligger i handskfacket"
        ));
        _bookingCounter++;
        Bookings.Add(new Booking(
            bookingId: _bookingCounter,
            customerName: "Britt-Marie",
            customerRegNr:"HEJ666",
            bookingDate: DateTime.Today.AddHours(12).AddMinutes(30),
            service: Services.EfterdragningDäck,
            comment: ""
        ));

    }
    
  

}