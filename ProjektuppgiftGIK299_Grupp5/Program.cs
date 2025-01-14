using System.ComponentModel;

namespace ProjektuppgiftGIK299_Grupp5;

class Program
{
    static void Main()
    {
        
       AdminPanel adminPanel = new();
       
       //adds dummy bookings to populate the list 
       adminPanel.AddDummyBooking();
        
       //keeps program running until exit
        bool contRunning = true;
        
        //writes out a welcome message
        Console.WriteLine();
        Console.WriteLine(new string('*', 40));
        Console.WriteLine("\nVälkommen till Däckbytaren 2000!\n");
        Console.WriteLine(new string('*', 40));
        Console.WriteLine();
        
        while (contRunning)
        {
            //prints out the main menu
            Console.WriteLine("""
                 Huvudmeny
                              
                 1. Skapa en bokning
                 2. Ändra en bokning
                 3. Ta bort en bokning
                 4. Se dagens bokningar
                 5. Sök efter bokningar
                 6. Spara dagens bokningar till fil
                 7. Avsluta programmet
                """);
            Thread.Sleep(500);
            Console.WriteLine("\nGör ditt val med en siffra:\n");
            
            //prompts the user for a choice
            switch (Console.ReadKey(intercept: true).KeyChar.ToString())
            {
                case "1"://add a booking
                    adminPanel.AddBooking();
                    break;
                
                case "2"://change a booking
                    Console.WriteLine();
                    Console.WriteLine("Vilken bokning vill du ändra på?");
                    Console.Write("Ange BokningsId: ");
                    int bookingIdToChange = int.Parse(Console.ReadLine());
                    Console.WriteLine();
                    adminPanel.ChangeBooking(bookingIdToChange);
                break;
                
                case "3"://remove a booking
                    Console.WriteLine("Vilken bokning vill du ta bort?");
                    Console.Write("Ange BokningsId: ");
                    int bookingIdToCancel = int.Parse(Console.ReadLine());
                    Console.WriteLine();
                    adminPanel.CancelBooking(bookingIdToCancel); 
                break;
                
                case "4"://view todays bookings
                    Console.WriteLine();
                    DateTime userDate = DateTime.Today;
                    Console.WriteLine();
                    adminPanel.ViewBookingsByDate(userDate);
                    Thread.Sleep(1000);
                    break;

                case "5"://search booking by regnr, customername, bookingID or date
                    Console.WriteLine();
                    Console.WriteLine("Hur vill du söka? Välj med en siffra: ");
                    Console.WriteLine();
                    Console.WriteLine("1. Registreringsnummer" +
                        "\n2. Kundens namn" +
                        "\n3. BokningsID" +
                        "\n4. Sök med datum");
                    string searchBooking = Console.ReadKey(intercept: true).KeyChar.ToString();
                    if (searchBooking == "1")
                    {
                        Console.WriteLine("\nSkriv in kundens registreringsnummer: ");
                        string regNr = string.Format(Console.ReadLine().ToUpper());
                        Console.WriteLine();
                        adminPanel.SearchBookingRegNr(regNr);
                        Console.WriteLine();
                        Thread.Sleep(1000);
                    }
                    else if (searchBooking == "2")
                    {
                        Console.WriteLine("\nSkriv in kundens namn: ");
                        string customerName = string.Format(Console.ReadLine());
                        Console.WriteLine();
                        adminPanel.SearchBookingCustName(customerName);
                        Console.WriteLine();
                        Thread.Sleep(1000);
                    }
                    else if (searchBooking == "3")
                    {
                        Console.WriteLine("\nSkriv in kundens bokningsID: ");
                        int bookingID = Int32.Parse(Console.ReadLine());
                        Console.WriteLine();
                        adminPanel.SearchBookingBookingID(bookingID);
                        Console.WriteLine();
                        Thread.Sleep(1000);
                    }
                    else if (searchBooking == "4")
                    {
                        Console.WriteLine("\nSkriv in ett giltigt datum (yyyy mm dd): ");
                        DateTime searchDate = DateTime.Parse(Console.ReadLine());
                        Console.WriteLine();
                        adminPanel.ViewBookingsByDate(searchDate);
                        Thread.Sleep(1000);

                    }
                    break;

                
                case "6"://writes todaysbookings to a csv file
                   FileManager.WriteToFile();
                    break;
                
                case "7"://exit program
                    Console.WriteLine("Välkommen åter!");
                    Thread.Sleep(1000);
                    contRunning = false;
                    break;
                    
            }



        }

        
    }

    
}