using System.Text.RegularExpressions;

namespace ProjektuppgiftGIK299_Grupp5;

public class AdminPanel
{
    //list to keep all the bookings
    public readonly List<Booking> Bookings = new List<Booking>();
    
    //counter for numbers of bookings
    private int _bookingCounter = 1;
    
    //method the check if booking time is available
    public bool IsOverlapping(DateTime bookingDate)
    {
        DateTime previousSlot = bookingDate.AddMinutes(-30);
        DateTime endTime = bookingDate.AddMinutes(30);

        return Bookings.Any(booking => (booking.BookingDate >= previousSlot && booking.BookingDate <= endTime) ||
        (booking.BookingDate.AddMinutes(-30) <= endTime && booking.BookingDate.AddMinutes(30) >= previousSlot));
    }
    
    //method to add booking to list
    public void AddBooking()
    {
        string name = GetValidName();

        string carRegNr = GetValidRegNr();

        DateTime dateTime = GetValidDate();
        
        //prompts the user to select type of service
        var service = SelectService();
        
        string comment = GetComment();
        
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
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
        Console.Clear();
        
    }
    
    //method to find and change a booking
    public void ChangeBooking(int bookingId) //TODO! FIXA ANVÄNDAR INMATNING
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
                    bookingToEdit.CustomerName = GetValidName();

                    break;
                case "2":
                    //changes the regNr 
                    Console.WriteLine();
                    Console.WriteLine("Ange ett nytt regnummer.");
                    Console.WriteLine();
                    bookingToEdit.CustomerRegNr = GetValidRegNr();

                    break;
                case "3":
                    //changes the date of the booking
                    Console.WriteLine();
                    Console.WriteLine("Ange ett nytt datum och tid (YYYY, MM, DD, HH:MM,)");
                    Console.WriteLine();
                    bookingToEdit.BookingDate = GetValidDate();

                    break;
                case "4":
                    //changes the services 
                    bookingToEdit.Service = SelectService();

                    break;
                case "5":
                    Console.WriteLine("Ange en ny kommentar.");
                    bookingToEdit.Comment = GetComment();
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
    public void SearchBookings(string custRegNr)
    {
        var customerBooking = Bookings.Where(b => b.CustomerRegNr == custRegNr).ToList();

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
    
    private static Services SelectService()
    {
        Console.WriteLine();
        Console.WriteLine("""
                          Vilken tjänst vill du boka in?
                          
                            1. DäckbyteSäsong
                            2. DäckbyteNyaDäck
                            3. Hjulinställning
                            4. Däckhotell
                            5. EfterdragningDäck
                            6. BeställaDäck
                          """);

        while (true)
        {
            Console.Write("\nAnge en siffra: ");
            var key = Console.ReadKey(intercept: true).KeyChar;

            if (int.TryParse(key.ToString(), out int num) && Enum.IsDefined(typeof(Services), num))
            {
                Console.WriteLine();
                var service = (Services)num;
                return service;

            }
            Console.WriteLine("\nOgiltigt val.");
        }
    }

    private static string GetValidName()
    {
        Console.Write("KundNamn: ");
        string name;
        //prompts the user until a valid name is entered
        while (!string.IsNullOrEmpty(name = Console.ReadLine()))
        {
            
            //checks to se if it only contains letters
            if (Regex.IsMatch(name, @"^[A-Za-zåäöÅÄÖéèÉÈçÇ' -]+$"))
            {
                //change the first leter to uppercase
                name = char.ToUpper(name[0]) + name.Substring(1);
                break;
            }
            Console.WriteLine("Ogiltigt inmatning");
            Thread.Sleep(2000);
            Console.Write("KundNamn: ");
        }

        return name;
    }

    private static string GetValidRegNr()
    {
        Console.Write("Bilregistreringsnummer: ");
        string carRegNr;
        //prompts the user for a regnr
        while (!string.IsNullOrEmpty(carRegNr = Console.ReadLine().ToUpper()))
        {
            
            //checks to se that a valid regnr was put in
            if (Regex.IsMatch(carRegNr, @"^[A-ZÅÄÖ]{3}\d{3}$|^[A-ZÅÄÖ]{3}\d{2}[A-ZÅÄÖ]{1}$"))
            {
                break;
            }
            Console.WriteLine("Ogitigt inmatning. Exempel på giltig inmatning. ABC123 eller ABC12D");
            Thread.Sleep(2000);
            Console.Write("Bilregistreringsnummer: ");
        }
        return carRegNr;
    }

    private  DateTime GetValidDate()
    {
        //prompts the user for a date
        Console.Write("Datum och tid (Skriv som yyyy,mm,dd och hh:mm): ");
        DateTime dateTime;
        while (true)
        {
            //checks to se if its a valid format
            if (!DateTime.TryParse(Console.ReadLine(), out dateTime))
            {
                Console.WriteLine("\nOgiltigt datum och tid.");
                Thread.Sleep(1000);
                Console.Write("Datum och tid (Skriv som yyyy,mm,dd och hh:mm): ");
            }
            //checks that its no dubble booking
            else if (IsOverlapping(dateTime))
            {
                Console.WriteLine("Error: Double booking! Please choose a different time.");
                Thread.Sleep(1000);
                Console.Write("Datum och tid (Skriv som yyyy,mm,dd och hh:mm): ");
            }
            else
            {
                // Valid date and no overlap
                break;
            }
        }

        return dateTime;
    }

    private static string GetComment()
    {
        string comment = null;
        Console.WriteLine("\nVill du lägga till en kommentar? Y/N");
        if (Console.ReadKey(intercept: true).KeyChar.ToString().ToUpper() == "Y")
        {
            Console.Write("\nSkriv in din kommentar: ");
            comment = Console.ReadLine();
            Thread.Sleep(1000);
        }
        return comment;
    }

}