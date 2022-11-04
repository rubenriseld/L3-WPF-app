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
                //varje bokning gäller 2 timmar från bokad tid
                //(1h59m så det går att boka en tid 2 timmar efter på samma bord)
                //och det går därför inte att boka en ny tid på samma bord
                //inom 2 timmar före eller efter en tidigare bokning
                if (booking.tableNumber == storedBooking.tableNumber)
                {
                    if (booking.bookingDateTime >= storedBooking.bookingDateTime.AddHours(-1).AddMinutes(-59)
                    && booking.bookingDateTime <= storedBooking.bookingDateTime.AddHours(1).AddMinutes(59))
                    {
                        MessageBox.Show($"Bord nummer {booking.tableNumber}" +
                            $" är redan bokat den {booking.bookingDateTime.ToString("dd/MM/yy")}" +
                            $" kl {storedBooking.bookingDateTime.ToString("HH:mm")}" +
                            $", vänligen välj en annan tid eller ett annat datum!");
                        return false;
                    }
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


        //--------------------
        //TODO:
        //_--------------------
        //lägga till alla bokningar asynkront, lista med tasks?
        async internal Task<List<Booking>> GetBookingsAsync()
        {
            string line = "";
            List<Booking> bookings = new List<Booking>();
            using (StreamReader sr = new StreamReader("bookings.txt"))
            {
                while ((line = await sr.ReadLineAsync()) != null)
                {
                    var stringArray = line.Split(',');
                    bookings.Add(new Booking(stringArray[0], Convert.ToInt32(stringArray[1]), Convert.ToDateTime(stringArray[2]), stringArray[3]));
                }
            };
            return bookings;
        }
    }
}