using System.Text.RegularExpressions;

namespace ProjektuppgiftGIK299_Grupp5;

public class AdminPanel
{
    //list to keep all the bookings
    public static readonly List<Booking> Bookings = new List<Booking>();
    
    //counter for numbers of bookings
    private int _bookingCounter = 1;
    
    
    public void AddBooking() //method to add bookings to list
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
    
    
    public void ViewBookingsByDate(DateTime date) //metod to view bookings in bookinglist
    {
        // find all bookings for the specified date, ignoring time of day
        var findBookings = Bookings.Where(b => b.BookingDate.Date == date).ToList();
        
        //if no bookings match
        if (findBookings.Count == 0)
        {
            Console.WriteLine("There are no bookings for this date.");
            return;
        }
        //if bookings are found, loop through and display for user
        Console.WriteLine($"Bookings for {date.ToShortDateString()}");
        foreach (var booking in findBookings)
        {
            Console.WriteLine();
            Console.WriteLine(booking);
            Console.WriteLine();
            Thread.Sleep(1000);
            
        }
        //give user some time to read the bookings
        Console.WriteLine("Press enter to continue...");
        Console.ReadLine();
        Console.Clear();
        
    }
    
    
    public void ChangeBooking(int bookingId) //method to find and change a booking
    {
        //find the booking that matches the specified bookingId 
        var bookingToEdit = Bookings.Find(b => b.BookingId == bookingId);
        
        //if booking is found, present user with options on what to change
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
                case "1": //changes the name
                    Console.WriteLine("Ange ett nytt namn.");
                    bookingToEdit.CustomerName = ValidInput.GetValidName();
                    Console.WriteLine($"Namn ändrat till: {bookingToEdit.CustomerName}");
                    Thread.Sleep(3000);
                    Console.Clear();

                    break;
                case "2": //changes the regNr 
                    Console.WriteLine();
                    Console.WriteLine("Ange ett nytt regnummer.");
                    bookingToEdit.CustomerRegNr = ValidInput.GetValidRegNr();
                    Console.WriteLine($"Registreringsnummer ändrat till: {bookingToEdit.CustomerRegNr}");
                    Thread.Sleep(3000);
                    Console.Clear();
                    

                    break;
                case "3": //changes the date of the booking
                    Console.WriteLine();
                    Console.WriteLine("Ange ett nytt datum och tid (YYYY, MM, DD, HH:MM,)");
                    bookingToEdit.BookingDate = ValidInput.GetValidDate();
                    Console.WriteLine($"Datum och tid ändrat till: {bookingToEdit.BookingDate}");
                    Thread.Sleep(3000);
                    Console.Clear();

                    break;
                case "4": //changes the services 
                    bookingToEdit.Service = ValidInput.SelectService();
                    Console.WriteLine($"Service bytt till: {bookingToEdit.Service}");
                    Thread.Sleep(3000);
                    Console.Clear();

                    break;
                case "5"://change or delete (if left blank) comment
                    Console.WriteLine("Ange en ny kommentar.");
                    bookingToEdit.Comment = ValidInput.GetComment();
                    Console.WriteLine($"Kommentaren ändrad till: {bookingToEdit.Comment}");
                    Thread.Sleep(3000);
                    Console.Clear();
                    
                    break;
            }
            
        }
    }
    
    
    public void CancelBooking(int bookingId) //method to cancel and delete a booking
    {
        //find the booking that matches the specified bookingId 
        var bookingToCancel = Bookings.Find(b => b.BookingId == bookingId);
        
        //if a match is found, remove it from the Bookings list
        if (bookingToCancel != null)
        {
            Bookings.Remove(bookingToCancel);
            Console.WriteLine("Bokningen har blivit borttagen.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }

        Console.WriteLine("Det finns ingen bokning med det bokningsid.");
    }

    
    public void SearchBookingRegNr(string custRegNr) //method to search booking by a specific date, customer regnr, customer name or customer bookingID
    {
        //find the booking that matches the specified registration number
        var customerBookingRegNr = Bookings.Where(b => b.CustomerRegNr == custRegNr).ToList();
        
        //if no matches where found
        if (customerBookingRegNr.Count == 0)
        {
            Console.WriteLine($"Det finns inga bokningar för: {custRegNr} ");
        }
        
        //loops through and displays the matching bookings for the user
        foreach (var booking in customerBookingRegNr)
        {
            Console.WriteLine();
            Console.WriteLine(booking);
            Console.WriteLine();
        }
        
        //gives the user time to read the output
        Console.WriteLine("Press enter key to continue...");
        Console.ReadKey();
        Console.Clear();
    }
    
    public void SearchBookingCustName(string custName)
    {
        //finds the bookings that matches the specified name
        var customerBookingName = Bookings.Where(b => b.CustomerName.ToUpper() == custName.ToUpper()).ToList();
        
        //if no matches where found
        if (customerBookingName.Count == 0)
        {
            Console.WriteLine($"Det finns inga bokningar för: {custName} ");
        }
        
        //loops through and displays the matching bookings for the user
        foreach (var booking in customerBookingName)
        {
            Console.WriteLine();
            Console.WriteLine(booking);
            Console.WriteLine();
        }
        
        //gives the user time to read the output
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.Clear();
    }
    
    public void SearchBookingBookingID(int custID)
    {
        //finds the bookings that matches the specified bookingId
        var customerBookingID = Bookings.Where(b => b.BookingId == custID).ToList();
        
        //if no match was found
        if (customerBookingID.Count == 0)
        {
            Console.WriteLine($"Det finns inga bokningar för Id: {custID} ");
        }
        
        //loops through and displays the matching bookings for the user
        foreach (var booking in customerBookingID)
        {
            Console.WriteLine();
            Console.WriteLine(booking);
            Console.WriteLine();
        }
        
        //gives the user time to read the output
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