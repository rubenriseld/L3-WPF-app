using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb_3___WPF_Applikation
{
    internal interface IBooking
    {
        public string name { get; set; }
        public DateTime bookingDateTime { get; set; }
        string ToStorageFormat();
        string ToDisplayFormat();
    }
}
