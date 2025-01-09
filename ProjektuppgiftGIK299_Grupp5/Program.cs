namespace ProjektuppgiftGIK299_Grupp5;

class Program
{
    static void Main(string[] args)
    {
        List<Booking> bookings = new List<Booking>();

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
            Console.WriteLine("\t1. Göra en bokning" +
                "\n\n\t2. Ändra en bokning" +
                "\n\n\t3. Se dagens bokningar" +
                "\n\n\t5. Se veckans bokningar" +
                "\n\n\t4. Sök efter KundID eller RegNr");
            Console.WriteLine();
            Console.WriteLine("Gör ditt val med en siffra: ");
            Console.WriteLine();

            int menuChoice;

            if (int.TryParse(Console.ReadLine(), out menuChoice))
            {
                switch (menuChoice)
                {
                    case 1:
                        Console.WriteLine();

                }
            }
            Console.WriteLine();



        }
    }
    private static Booking AddBooking()
    {
            Console.WriteLine("Mata in kundens namn:");
            Console.WriteLine();
            string custName = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("Mata in kundens registreringsnummer:");
            string custRegNr = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("Mata in den önskade tiden för service:");
            DateTime bookingTime =
    }
}