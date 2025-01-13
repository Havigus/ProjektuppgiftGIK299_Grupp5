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
                 5. Sök efter bokning med registreringsnummer
                 6. Spara dagens bokningar till fil
                 7. Avsluta programmet
                """);
            Thread.Sleep(500);
            Console.WriteLine("\nGör ditt val med en siffra:\n");
            
            //prompts the user for a choice
            switch (Console.ReadKey(intercept: true).KeyChar.ToString())
            {
                case "1"://add a booking
                    while (true)
                    {
                        Console.Write("Kundnamn: ");
                        string name = Console.ReadLine();
                        Thread.Sleep(200);
                        Console.Write("Bilregistreringsnummer: ");
                        string carRegNr = Console.ReadLine()?.ToUpper();
                        Thread.Sleep(200);
                        Console.Write("Datum och tid (Skriv som yyyy,mm,dd och hh:mm): ");
                        DateTime dateTime;
                        Thread.Sleep(200);
                        while (!DateTime.TryParse(Console.ReadLine(), out dateTime))
                        {
                            Console.WriteLine("\nOgiltigt datum och tid.");
                            Thread.Sleep(1000);
                            Console.Write("Datum och tid (Skriv som yyyy,mm,dd och hh:mm): ");
                        }

                        //TODO! Lägg denhär menyn i en while loop så man kan välja fler services. Behövs nog en List för att hålla valen
                        //alt att man bara tar servicen som en string och skippar enum

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
                            
                            Services service;
                            while (true)
                            {
                                Console.Write("\nAnge en siffra: ");
                                var key = Console.ReadKey(intercept: true).KeyChar;

                                if (int.TryParse(key.ToString(), out int num) && Enum.IsDefined(typeof(Services), num))
                                {
                                    Console.WriteLine();
                                    service = (Services)num;
                                break;

                                }
                                Console.WriteLine("\nOgiltigt val.");
                            }
                            
                        string comment = null;
                        
                        Console.WriteLine("\nVill du lägga till en kommentar? Y/N");
                        if (Console.ReadKey(intercept: true).KeyChar.ToString().ToUpper() == "Y")
                        {
                            Console.Write("\nSkriv in din kommentar: ");
                            comment = Console.ReadLine();
                            Thread.Sleep(1000);
                        }
                        
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
                            Console.WriteLine();
                            adminPanel.AddBooking(name, carRegNr, dateTime, service, comment);
                            break;
                        }

                    }
                    break;
                
                case "2"://change a booking
                    Console.WriteLine();
                    Console.WriteLine("Vilken bokning vill du ändra på? Ange BokningsId");
                    int bookingIdToChange = int.Parse(Console.ReadLine());
                    Console.WriteLine();
                    adminPanel.ChangeBooking(bookingIdToChange);
                break;
                
                case "3"://remove a booking
                    Console.WriteLine("Vilken bokning vill du ta bort? Ange BokningsId");
                    Console.WriteLine();
                    int bookingIdToCancel = int.Parse(Console.ReadLine());
                    Console.WriteLine();
                    adminPanel.CancelBooking(bookingIdToCancel); 
                break;
                
                case "4"://view todays bookings
                    Console.WriteLine();
                    DateTime userDate = DateTime.Today;
                    Console.WriteLine();
                    adminPanel.ViewBookings(userDate);
                    Thread.Sleep(1000);
                    break;

                case "5"://search booking by regnr
                    Console.WriteLine();
                    Console.WriteLine("Vilken bokning vill du se? Skriv kundens registreringsnummer: ");
                    Console.WriteLine();
                    string regNr = string.Format(Console.ReadLine().ToUpper());
                    Console.WriteLine();
                    adminPanel.SearchBookings(regNr);
                    Console.WriteLine();
                    Thread.Sleep(1000);

                    break;
                
                case "6"://writes todaysbookings to a csv file
                    adminPanel.WriteToFile();
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