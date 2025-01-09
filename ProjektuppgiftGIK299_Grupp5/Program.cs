namespace ProjektuppgiftGIK299_Grupp5;

class Program
{
    static void Main(string[] args)
    {
       AdminPanel adminPanel = new();

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

            switch (Console.ReadKey().KeyChar.ToString())
            {
                case "1":
                    //kod för val ett
                break;
                
                case "2":
                    //kod för val två
                break;
                
                case "3":
                    //kod för val tre
                break;
                
                case "4":
                    //kod för val fyra
                break;
                
                    
                
            }



        }
    }
}