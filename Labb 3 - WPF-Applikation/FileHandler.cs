using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb_3___WPF_Applikation
{
    internal class FileHandler //async????
    {   
        //TODO: validering
        //internal bool ValidateBooking(Booking)
        //{
        //    //timespan?
        //}
        internal void StoreBooking(string bookingInStorageFormat)
        {
            using (StreamWriter sw = new StreamWriter("bookings.txt", true)){
                sw.WriteLine(bookingInStorageFormat);
            };
        }
        internal void CancelBooking(string bookingInStorageFormat)
        {
            //skapar temporär fil för att kunna skriva om filen med bokningar utan 
            //bokningen som ska tas bort
            var tempFile = Path.GetTempFileName();
            var linesToKeep = File.ReadLines("bookings.txt").Where(l => l != bookingInStorageFormat);

            File.WriteAllLines(tempFile, linesToKeep);

            File.Delete("bookings.txt");
            File.Move(tempFile, "bookings.txt");
            
        }

        internal List<Booking> GetBookings()
        {
            string line = "";
            List<Booking> bookings = new List<Booking>();
            using (StreamReader sr = new StreamReader("bookings.txt"))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    var stringArray = line.Split(',');
                    bookings.Add(new Booking(stringArray[0],
                                                Convert.ToInt32(stringArray[1]),
                                                stringArray[2],
                                                Convert.ToDateTime(stringArray[3])
                    ));
                }
            };
            return bookings;
        }
    }
}
