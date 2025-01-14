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

                case "5"://search booking by regnr, customername or bookingID
                    Console.Clear();
                    
                    //displays a menu for the user
                    Console.WriteLine("Hur vill du söka? Välj med en siffra: ");
                    Console.WriteLine();
                    Console.WriteLine("1. Registreringsnummer" +
                        "\n2. Kundens namn" +
                        "\n3. BokningsID" +
                        "\n4. Sök med datum");
                    
                    //get the user choice 
                    string searchChoice = Console.ReadKey(intercept: true).KeyChar.ToString();
                    
                    if (searchChoice == "1")
                    {
                        Console.WriteLine();
                        string regNr = ValidInput.GetValidRegNr();
                        Console.WriteLine();
                        adminPanel.SearchBookingRegNr(regNr);
                        Console.WriteLine();
                        Thread.Sleep(1000);
                    }
                    else if (searchChoice == "2")
                    {
                        Console.WriteLine();
                        string customerName = ValidInput.GetValidName();
                        Console.WriteLine();
                        adminPanel.SearchBookingCustName(customerName);
                        Console.WriteLine();
                        Thread.Sleep(1000);
                    }
                    else if (searchChoice == "3")
                    {
                        Console.WriteLine();
                        int bookingId = ValidInput.GetValidId();
                        Console.WriteLine();
                        adminPanel.SearchBookingBookingID(bookingId);
                        Console.WriteLine();
                        Thread.Sleep(1000);
                    }
                    else if (searchChoice == "4")
                    {
                        Console.Write("\nSkriv in ett datum (yyyy mm dd): ");
                        DateTime searchDate;
                        while (!DateTime.TryParse(Console.ReadLine(), out searchDate))
                        {
                            Console.WriteLine("\nOgiltigt datum och tid.");
                            Thread.Sleep(1000);
                            Console.Write("Datum (Skriv som yyyy,mm,dd): ");
                        }
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