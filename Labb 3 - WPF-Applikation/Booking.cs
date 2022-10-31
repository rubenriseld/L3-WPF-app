﻿using System;
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
        public DateTime bookingDateTime { get; set; }
        private readonly string bookingID;
        public Booking(string name, int tableNumber, DateTime bookingDateTime, string bookingID)
        {
            this.name = name;
            this.tableNumber = tableNumber;
            this.bookingDateTime = bookingDateTime;
            //unikt bokningsID
            this.bookingID = bookingID;
        }
        public string ToStorageFormat()
        {
            return $"{name},{tableNumber},{bookingDateTime},{bookingID}";
        }
        public string ToDisplayFormat()
        {
            //string dateTimeDisplay = bookingDateTime.Year.ToString() + "/" + bookingDateTime.Month.ToString() + "/" + bookingDateTime.Day.ToString();
            return $"Namn: {name}\n" +
                    $"Bordsnummer: {tableNumber}\n" +
                    $"Tid: {bookingDateTime.ToString("HH:mm")}\n" +
                    $"Datum: {bookingDateTime.ToString("dd/MM/yy")}";
        }
    }
}
