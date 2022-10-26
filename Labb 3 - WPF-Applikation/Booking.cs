using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb_3___WPF_Applikation
{
    internal class Booking : IBooking
    {
        public string name { get; set; }
        public int tableNumber { get; set; }
        public string bookingTime { get; set; }
        public DateTime bookingDate { get; set; }


        public Booking(string name, int tableNumber, string bookingTime, DateTime bookingDate)
        {
            this.name = name;
            this.tableNumber = tableNumber;
            this.bookingTime = bookingTime;
            this.bookingDate = bookingDate;
        }
        public string ToStorageFormat()
        {
            string ymd = bookingDate.Year.ToString() + "/" + bookingDate.Month.ToString() + "/" + bookingDate.Day.ToString();
            return $"{name},{tableNumber},{bookingTime},{ymd}";
        }
        public string ToDisplayFormat()
        {
            string ymd = bookingDate.Year.ToString() + "/" + bookingDate.Month.ToString() + "/" + bookingDate.Day.ToString();
            return $"Namn: {name}\n" +
                    $"Bordsnummer: {tableNumber}\n" +
                    $"Tid: {bookingTime}\n" +
                    $"Datum: {ymd}";
        }
    }
}
