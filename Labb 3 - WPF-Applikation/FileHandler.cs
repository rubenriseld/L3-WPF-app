using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Labb_3___WPF_Applikation
{
    internal class FileHandler 
    {
        internal async Task StoreBooking(Booking booking)
        {
            bool bookingIsValid = await ValidateBookingAsync(booking);
            if (bookingIsValid)
            {
                using (StreamWriter sw = new StreamWriter("bookings.txt", true))
                {
                    sw.WriteLine(booking.ToStorageFormat());
                    MessageBox.Show("Bokat!");
                };
            }
        }
        internal async Task<bool> ValidateBookingAsync(Booking booking)
        {
            List<Booking> bookings = await GetBookingsAsync();
            foreach (var storedBooking in bookings)
            {
                //kontrollerar bordsnummer
                if(booking.tableNumber == storedBooking.tableNumber)
                {
                    //kontrollerar tidsram (går ej att boka samma bord inom 3 timmar efter tidigare bokning)
                    if(booking.bookingDateTime >= storedBooking.bookingDateTime 
                        && booking.bookingDateTime <= storedBooking.bookingDateTime.AddHours(3))
                    {
                        MessageBox.Show($"Bord nummer {storedBooking.tableNumber}" +
                           $" är bokat från kl {storedBooking.bookingDateTime.ToString("HH:mm")}" +
                           $"-{storedBooking.bookingDateTime.AddHours(3).ToString("HH:mm")}. " +
                           $"Vänligen välj ett annat bord, tid eller datum!");
                        return false;
                    }
                    return true;
                }
            }
            return true;
        }
        internal void CancelBooking(string bookingInStorageFormat)
        {
            //skapar temporär fil för att kunna skriva om filen med bokningar utan bokningen som ska tas bort

            //TODO
            //göra om till StreamReader?
            //----------

            var tempFile = Path.GetTempFileName();
            var linesToKeep = File.ReadLines("bookings.txt").Where(l => l != bookingInStorageFormat);

            File.WriteAllLines(tempFile, linesToKeep);

            File.Delete("bookings.txt");
            File.Move(tempFile, "bookings.txt");
        }

        async internal Task<List<Booking>> GetBookingsAsync()
        {
            string line = "";
            List<Booking> bookings = new List<Booking>();
            using (StreamReader sr = new StreamReader("bookings.txt"))
            {
                while ((line = await sr.ReadLineAsync()) != null)
                {
                    var stringArray = line.Split(',');
                    bookings.Add(new Booking(stringArray[0], Convert.ToInt32(stringArray[1]), Convert.ToDateTime(stringArray[2])));
                }
            };
            return bookings;
        }
    }
}