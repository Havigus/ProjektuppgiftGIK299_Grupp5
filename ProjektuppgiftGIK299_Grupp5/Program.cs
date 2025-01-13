using System.Security.Cryptography.X509Certificates;

namespace ProjektuppgiftGIK299_Grupp5;

class Program
{
    static void Main(string[] args)
    {
       AdminPanel adminPanel = new();
       
       adminPanel.AddDummyBooking();

        bool contRunning = true;


        while (contRunning)
        {

            Console.WriteLine();
            Console.WriteLine(new string('*', 40));
            Console.WriteLine();
            Console.WriteLine("Välkommen till Däckbytaren 2000!");
            Console.WriteLine();
            Console.WriteLine(new string('*', 40));
            Console.WriteLine();
            Console.WriteLine("Vad vill du göra?");
            Console.WriteLine();
            Console.WriteLine("\t1. Skapa en bokning" +
                "\n\n\t2. Ändra en bokning" +
                "\n\n\t3. Ta bort en bokning" +
                "\n\n\t4. Se dagens bokningar" +
                "\n\n\t5. Sök efter KundID eller RegNr" +
                "\n\n\t6. Avsluta programmet");
            Thread.Sleep(500);
            Console.WriteLine();
            Console.WriteLine("Gör ditt val med en siffra: ");
            Console.WriteLine();

            switch (Console.ReadKey(intercept: true).KeyChar.ToString())
            {
                case "1":
                    while (true)
                    {
                        Console.WriteLine("Kundnamn: ");
                        Console.WriteLine();
                        string name = Console.ReadLine();
                        Console.WriteLine();
                        Thread.Sleep(200);
                        Console.WriteLine("Bilregistreringsnummer: ");
                        Console.WriteLine();
                        string carRegNr = Console.ReadLine();
                        Console.WriteLine();
                        Thread.Sleep(200);
                        Console.WriteLine("Datum: (Skriv som yyyy mm dd och hh:mm)");
                        Console.WriteLine();
                        DateTime dateTime;
                        Thread.Sleep(200);
                        while (!DateTime.TryParse(Console.ReadLine(), out dateTime))
                        {
                            Console.WriteLine();
                            Console.WriteLine("Ogiltigt datum och tid.");
                            Thread.Sleep(300);
                        }

                        //TODO! Lägg denhär menyn i en while loop så man kan välja fler services. Behövs nog en List för att hålla valen
                        //alt att man bara tar servicen som en string och skippar enum

                            Console.WriteLine();
                            Console.WriteLine("""
                                          Vilken tjänst?

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
                                Console.WriteLine();
                                var key = Console.ReadKey(intercept: true).KeyChar;

                                if (int.TryParse(key.ToString(), out int num) && Enum.IsDefined(typeof(Services), num))
                                {
                                    Console.WriteLine();
                                    service = (Services)num;
                                break;

                                }
                                else
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Ogiltigt val.");
                                }
                            }
                        string comment = null;
                        Console.WriteLine();
                        Console.WriteLine("Vill du lägga till en kommentar? Y/N");
                        if (Console.ReadKey(intercept: true).KeyChar.ToString().ToUpper() == "Y")
                        {
                            Console.WriteLine();
                            Console.WriteLine("Skriv in din kommentar: ");
                            Console.WriteLine();
                            comment = Console.ReadLine();
                            Console.WriteLine();
                        }

                        Console.WriteLine();
                        Console.WriteLine($"""
                                          Bekräfta uppgifterna

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

                
                case "2":
                    //kod för val två, ändra
                break;
                
                case "3":
                    //kod för val tre, ta bort en bokning


                    

                    
                    
                    
                break;
                
                case "4":
                    //kod för val fyra, sök efter kundID eller regNr
                    Console.WriteLine();
                    Console.WriteLine("Vilket datum vill du se? Skriv yyyy mm dd: ");
                    Console.WriteLine();
                    DateTime userDate = DateTime.Parse(Console.ReadLine());
                    Console.WriteLine();
                    adminPanel.ViewBookings(userDate);
                    Console.WriteLine();
                    Thread.Sleep(1000);
                    break;

                case "5":

                    break;
                
                case "6":
                    Console.WriteLine("Välkommen åter!");
                    Thread.Sleep(1000);
                    contRunning = false;
                    break;
                
                    
                
            }



        }

        
    }
    
}