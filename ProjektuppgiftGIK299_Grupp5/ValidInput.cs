using System.Text.RegularExpressions;

namespace ProjektuppgiftGIK299_Grupp5;

public abstract class ValidInput
{
      public static Services SelectService()
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

    public static string GetValidName() //method to get and validate customer name
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

    public static string GetValidRegNr() //method to get and validate customer vehicle regnr
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
            Console.WriteLine("Ogitigt inmatning. Exempel på giltig inmatning. ABC123 eller ABC12W");
            Thread.Sleep(2000);
            Console.Write("Bilregistreringsnummer: ");
        }
        return carRegNr;
    }

    public static DateTime GetValidDate() //method to get a valid date and checks for double booking
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
            //checks that its no double booking
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

    public static string GetComment() //metrhod to add a comment to the booking
    {
        string comment = null;
        Console.WriteLine("\nVill du lägga till en kommentar? Y/N");
        //prompts the user and checks if they want to add a comment
        if (Console.ReadKey(intercept: true).KeyChar.ToString().ToUpper() == "Y")
        {
            Console.Write("\nSkriv in din kommentar: ");
            comment = Console.ReadLine();
            Thread.Sleep(1000);
        }
        return comment;
    }
    
    public static bool IsOverlapping(DateTime bookingDate) //method to check for double bookings
    {
        //defines a 30min range before and after the booking date 
        DateTime startRange = bookingDate.AddMinutes(-30);
        DateTime endRange = bookingDate.AddMinutes(30);

        //checks if any existing booking overlaps with the range
        return AdminPanel.Bookings.Any(booking => booking.BookingDate >= startRange && booking.BookingDate <= endRange);
    }
    
}