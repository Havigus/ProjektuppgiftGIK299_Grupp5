namespace ProjektuppgiftGIK299_Grupp5;

public class AdminPanel
{
    //list to keep all the bookings
    private readonly List<Booking> _bookings = new List<Booking>();
    //counter for numbers of bookings
    private int _bookingCounter = 1;
    
    //method the check if booking time is available
    public bool IsOverlapping(DateTime bookingDate)
    {
        DateTime previousSlot = bookingDate.AddMinutes(-30);
        DateTime endTime = bookingDate.AddMinutes(30);

        return _bookings.Any(booking => (booking.BookingDate >= previousSlot && booking.BookingDate <= endTime) ||
        (booking.BookingDate.AddMinutes(-30) <= endTime && booking.BookingDate.AddMinutes(30) >= previousSlot));
    }
    
    //method to add booking to list
    public void AddBooking(string customerName, string customerRegNr, DateTime bookingDate, Services services, string comment)
    {
        

        if (IsOverlapping(bookingDate))
        {
            Console.WriteLine("Error: Double booking! Please choose a different time.");
            Thread.Sleep(1000);
        }

        else
        {
            int bookingId = _bookingCounter;
            var booking = new Booking(bookingId, customerName, customerRegNr, bookingDate, services, comment);
            _bookings.Add(booking);
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
        var todaysBookings = _bookings.Where(b => b.BookingDate.Date == date).ToList();
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
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
        Console.Clear();
        
    }
    
    //method to find and change a booking
    public void ChangeBooking(int bookingId)
    {
        //find the booking that matches the bookingId 
        var bookingToEdit = _bookings.Find(b => b.BookingId == bookingId);
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
                    string newCustomerName = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newCustomerName))
                    {
                        bookingToEdit.CustomerName = newCustomerName;
                    }

                    break;
                case "2":
                    //changes the regNr 
                    Console.WriteLine();
                    Console.WriteLine("Ange ett nytt regnummer.");
                    Console.WriteLine();
                    string newCustomerRegNr = Console.ReadLine();
                    Console.WriteLine();
                    if (!string.IsNullOrWhiteSpace(newCustomerRegNr))
                    {
                        bookingToEdit.CustomerRegNr = newCustomerRegNr;
                    }

                    break;
                case "3":
                    //changes the date of the booking
                    Console.WriteLine();
                    Console.WriteLine("Ange ett nytt datum och tid (YYYY, MM, DD, HH:MM,)");
                    Console.WriteLine();
                    DateTime newBookingDate;
                    Console.WriteLine();
                    while (!DateTime.TryParse(Console.ReadLine(), out newBookingDate))
                    {
                        Console.WriteLine();
                        Console.WriteLine("Ogiltigt datum och tid.");
                        Thread.Sleep(300);
                    }

                    if (!newBookingDate.Equals(bookingToEdit.BookingDate) && !IsOverlapping(newBookingDate))
                    {
                        Console.WriteLine();
                        bookingToEdit.BookingDate = newBookingDate;
                    }

                    break;
                case "4":
                    //changes the services 
                    Console.WriteLine("""
                                      Vilken tjänst?

                                      1. DäckbyteSäsong

                                      2. DäckbyteNyaDäck

                                      3. Hjulinställning

                                      4. Däckhotell

                                      5. EfterdragningDäck

                                      6. BeställaDäck
                                      """);
                    while (true)
                    {
                        Console.WriteLine();
                        var key = Console.ReadKey(intercept: true).KeyChar;

                        if (int.TryParse(key.ToString(), out int num) && Enum.IsDefined(typeof(Services), num))
                        {
                            Console.WriteLine();
                            bookingToEdit.Service = (Services)num;
                            break;

                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("Ogiltigt val.");
                        }
                    }

                    break;
                case "5":
                    Console.WriteLine("Ange en ny kommentar.");
                    string newComment = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newComment))
                    {
                        bookingToEdit.Comment = newComment;
                    }
                    break;
            }
            
        }
    }
    
    //method to cancel and delete a booking
    public void CancelBooking(int bookingId)
    {
        var bookingToCancel = _bookings.Find(b => b.BookingId == bookingId);
        if (bookingToCancel != null)
        {
            _bookings.Remove(bookingToCancel);
            Console.WriteLine("Bokningen har blivit borttagen.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }

        Console.WriteLine("Det finns ingen bokning med det bokningsid.");
    }
    
    //adds 3 dummy bookings to the list 
    public void AddDummyBooking()
    {
        _bookings.Add(new Booking(
            bookingId: _bookingCounter,
            customerName: "Bob",
            customerRegNr:"ABC123",
            bookingDate: DateTime.Today.AddHours(11),
            service: Services.Hjulinställning,
            comment: "Bilen drar åt höger"
            ));
        _bookingCounter++;
        _bookings.Add(new Booking(
            bookingId: _bookingCounter,
            customerName: "Sven",
            customerRegNr:"CBA321",
            bookingDate: DateTime.Today.AddHours(11).AddMinutes(30), 
            service: Services.DäckbyteSäsong,
            comment: "Hylsan till låsbultarna ligger i handskfacket"
        ));
        _bookingCounter++;
        _bookings.Add(new Booking(
            bookingId: _bookingCounter,
            customerName: "Britt-Marie",
            customerRegNr:"HEJ666",
            bookingDate: DateTime.Today.AddHours(12).AddMinutes(30),
            service: Services.EfterdragningDäck,
            comment: ""
        ));

    }
    
    //method to search booking by regnr
    public void SearchBookings(string custRegNr)
    {
        var customerBooking = _bookings.Where(b => b.CustomerRegNr == custRegNr).ToList();

        foreach (var booking in customerBooking)
        {
            Console.WriteLine();
            Console.WriteLine(booking);
            Console.WriteLine();
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.Clear();
    }

    //calls the function in FileManager.cs
    public void WriteToFile()
    {
        FileManager.WriteToFile(_bookings);
    }

}