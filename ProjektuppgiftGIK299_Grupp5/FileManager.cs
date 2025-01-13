using System.Text;

namespace ProjektuppgiftGIK299_Grupp5;

//abstract class for handling file operation
internal abstract class FileManager
{
    
    // Writes a list of bookings to a CSV file in the program's base directory.
    public static void WriteToFile(List<Booking> bookings)
    {
        //gets the base directory where the program is running
        string baseDirectory = AppContext.BaseDirectory;
        
        //combine the base directory with the file name
        string filePath = Path.Combine(baseDirectory, "bookings.csv");
        
        //uses streamwriter to creat or overwrite the csv file with utf-8 encoding for swedish letters
        using (StreamWriter sw = new StreamWriter(filePath, false, new UTF8Encoding(true)))
        {
            //writes the csv header row
            sw.WriteLine("BokningsId;Kundens Namn;RegNr;Datum och tid;typ av Service;Kommentar");
            
            //loops through each booking
            foreach (var booking in bookings)
            {
                //format and writes the booking to file using (;) as separator for use in excel
                sw.WriteLine($"{booking.BookingId};" +
                $"\"{booking.CustomerName}\";" +
                $"\"{booking.CustomerRegNr}\";" +
                $"{booking.BookingDate:yyyy-MM-dd HH:mm};" +
                $"\"{booking.Service}\";" +
                $"\"{booking.Comment}\"");
            }
        }

        Console.WriteLine($"Bokningarna har sparats till fil vid {filePath}");
        Thread.Sleep(3000);
    }
}
