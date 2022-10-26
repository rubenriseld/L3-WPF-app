using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

/* Skapa en WPF-applikation där:
 
- Användaren kan ange datum, tid, namn och bordsnummer och klicka på
”Boka” för att skapa en bordsbokning. [1] Om det datum, tid och bord som
ska bokas redan är bokat ska bokningen inte gå att genomföra.
Användaren ska meddelas om detta så att det går att försöka igen.

- Användaren kan klicka på en ”Visa bokningar” för att lista alla
bordsbokningar.

- Användaren kan markera en bokning och klicka på avboka för att avboka
en bordsbokning.

**************************

RIKTLINJER

- När programmet startar ska det finnas ett antal bokningar lagrade redo
att visas.

- Programmet ska hantera oförutsägbara fel.

**************************

KRAV

- Felhantering med try-catch alternativt TryParse eller liknande

- Klass/klasser

- Metoder

- Iteration

- Selektion

- Endast 5 bord ska tillåtas vara bokade samma datum och tid. Till
exempel: Är 5 bord bokade 2022-10-28 kl. 21.00 ska inte ännu ett bord gå
att boka 2022-10-28 kl. 21.00. Om ännu ett bord försöker bokas ska
användaren meddelas om detta så att det går att försöka igen.

- LINQ

- Skalbar lösning med abstrakta klasser/interface

- Filhantering (uppdatera fil vid bokning och avbokning samt läsa från fil
vid valet ”Visa bokningar”)

- Asynkrona metoder 

*/

/* ---------------
 * WPF:
 * ---------------
 * DatePicker
 * Val av tid
 * Val av bord
 * Inmatningsfält för namn
 * Knapp: Boka
 * Knapp: Visa bokningar
 * Listbox: Lista av alla bordsbokningar
 *      > Linq?
 * Knapp: Avboka markerad bokning i listbox
 * ---------------
 * *************
 * ---------------
 * OOP:
 * ---------------
 * Bokningsklass - Interface här?
 *      > Properties: datum, tid, namn, bordsnummer
 * ---------------
 * *************
 * ---------------
 * Filhantering:
 * ---------------
 * Metod: skriva bokning till fil
 *      > Metod: kontrollera bokning
 * Metod: hämta bokningar från fil
 *      > Asynkront?
 * 
 * ---------------
 */




namespace Labb_3___WPF_Applikation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FileHandler fileHandler = new FileHandler();
        public MainWindow()
        {
            InitializeComponent();
            //bordsnummer
            for (int i = 1; i < 6; i++)
            {
                comboBoxTableNumber.Items.Add(i);
            }
            //tider 16-22
            for (int i = 0; i < 6; i++)
            {
                comboBoxBookingTime.Items.Add($"{16 + i}:00");
                if (i < 5) comboBoxBookingTime.Items.Add($"{16 + i}:30");

            }
        }

        //samla input och lagra bokning
        private void buttonConfirmBooking_Click(object sender, RoutedEventArgs e) //async?
        {

            //TODO:
            //felhantering
            //-----------------------


            string name = Convert.ToString(textBoxName.Text);
            int tableNumber = Convert.ToInt32(comboBoxTableNumber.SelectedValue);
            string bookingTime = Convert.ToString(comboBoxBookingTime.SelectedValue);
            DateTime bookingDate = Convert.ToDateTime(datePicker.SelectedDate);

            Booking newBooking = new Booking(name, tableNumber, bookingTime, bookingDate);
            fileHandler.StoreBooking(newBooking.ToStorageFormat());
            MessageBox.Show("bokat!");
        }

        //lista bokningar i listbox
        private void buttonShowBookings_Click(object sender, RoutedEventArgs e)
        {
            //TODO: async, LINQ?
            updateBookingList();
        }

        private void buttonCancelBooking_Click(object sender, RoutedEventArgs e)
        {
            //TODO: async, LINQ?

            //hämta Booking-objektet från den valda bokningen i listan, för enklare konvertering till
            //textformatet som krävs för att radera bokningen
            Booking temp = (Booking)(((FrameworkElement)(listBoxBookings.SelectedItem)).DataContext);

            string bookingToCancel = temp.ToStorageFormat();
            fileHandler.CancelBooking(bookingToCancel);

            MessageBox.Show("avbokat!");

            updateBookingList();
        }
        public void updateBookingList()
        {
            listBoxBookings.Items.Clear();

            List<Booking> bookingList = fileHandler.GetBookings();

            var query =
                from booking in bookingList
                orderby booking.bookingDate ascending
                select booking;

            foreach (var booking in query)
            {
                ListBoxItem addedBooking = new ListBoxItem();

                //ger alla items innehåll (bokningsinfo) samt data i form av ett Booking-objekt
                //för att enkelt kunna avboka
                addedBooking.Content = booking.ToDisplayFormat();
                addedBooking.DataContext = booking;

                listBoxBookings.Items.Add(addedBooking);
            }
        }

        //funktioner för att avaktivera/aktivera avbokningsknappen beroende på vad som är markerat
        private void listBoxBookings_LostFocus(object sender, RoutedEventArgs e)
        {
            //buttonCancelBooking.IsEnabled = false;
        }

        private void listBoxBookings_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //buttonCancelBooking.IsEnabled = true;
        }


    }
}
