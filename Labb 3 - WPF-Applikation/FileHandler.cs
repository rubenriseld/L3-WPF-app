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
        readonly string corruptedStorageMessage = "Oväntat fel uppstod. Är lagringsfilen korrupt?";
        
        //LAGRA BOKNING
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

        //VALIDERA BOKNING
        internal async Task<bool> ValidateBookingAsync(Booking booking)
        {
            List<Booking> bookings = await GetBookingsAsync();
            try
            {
                foreach (var storedBooking in bookings)
                {
                    //varje bokning gäller 2 timmar från bokad tid
                    //(1h59m så det går att boka en tid 2 timmar innan eller efter på samma bord)
                    if (booking.tableNumber == storedBooking.tableNumber)
                    {
                        if (booking.bookingDateTime >= storedBooking.bookingDateTime.AddHours(-1).AddMinutes(-59)
                        && booking.bookingDateTime <= storedBooking.bookingDateTime.AddHours(1).AddMinutes(59))
                        {
                            MessageBox.Show($"Bord nummer {booking.tableNumber} " +
                                            $"är redan bokat den {booking.bookingDateTime.ToString("dd/MM/yy")}" +
                                            $"kl {storedBooking.bookingDateTime.ToString("HH:mm")}, " +
                                            $"vänligen välj en annan tid eller ett annat datum, " +
                                            $"eller försök igen med ett annat bordsnummer!\n\n" +
                                            $"(Du kan inte göra en bokning på samma bord och datum " +
                                            $"inom 2 timmar före eller efter en tidigare bokning)");
                            return false;
                        }
                    }
                }
                return true;
            }
            catch
            {
                MessageBox.Show(corruptedStorageMessage);
                return false;
            }
        }

        //AVBOKA
        internal void CancelBooking(string bookingInStorageFormat)
        {
            //hantering för oförutsägbara fel
            try
            {
                //skapar temporär fil för att kunna skriva om filen med bokningar utan bokningen som ska tas bort

                var tempFile = Path.GetTempFileName();
                var linesToKeep = File.ReadLines("bookings.txt").Where(l => l != bookingInStorageFormat);

                File.WriteAllLines(tempFile, linesToKeep);

                File.Delete("bookings.txt");
                File.Move(tempFile, "bookings.txt");
            }
            catch
            {
                MessageBox.Show(corruptedStorageMessage);
            }
        }

        //HÄMTA BOKNINGAR FRÅN LAGRINGSFIL
        async internal Task<List<Booking>> GetBookingsAsync()
        {
            string line = "";
            List<Booking> bookings = new List<Booking>();
            using (StreamReader sr = new StreamReader("bookings.txt"))
            {
                //hantering för oförutsägbara fel
                try
                {
                    while ((line = await sr.ReadLineAsync()) != null)
                    {
                        var stringArray = line.Split(',');
                        bookings.Add(new Booking(   stringArray[0], 
                                                    Convert.ToInt32(stringArray[1]), 
                                                    Convert.ToDateTime(stringArray[2]), 
                                                    stringArray[3]));
                    }
                }
                catch
                {
                    MessageBox.Show(corruptedStorageMessage);
                }
            };
            return bookings;
        }
    }
}