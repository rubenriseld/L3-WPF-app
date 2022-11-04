using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Labb_3___WPF_Applikation
{
    public interface IBooking
    {
        public string name { get; set; }
        public DateTime bookingDateTime { get; set; }
        
        string ToStorageFormat();
    }
}
