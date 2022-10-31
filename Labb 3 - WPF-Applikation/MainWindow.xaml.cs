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
Användaren ska meddelas om detta så att det går att försöka igen. X

- Användaren kan klicka på en ”Visa bokningar” för att lista alla
bordsbokningar. X

- Användaren kan markera en bokning och klicka på avboka för att avboka
en bordsbokning. X

**************************

RIKTLINJER

- När programmet startar ska det finnas ett antal bokningar lagrade redo
att visas.

- Programmet ska hantera oförutsägbara fel. X

**************************

KRAV

- Felhantering med try-catch alternativt TryParse eller liknande ?

- Klass/klasser X

- Metoder X

- Iteration X

- Selektion X

- Endast 5 bord ska tillåtas vara bokade samma datum och tid. Till
exempel: Är 5 bord bokade 2022-10-28 kl. 21.00 ska inte ännu ett bord gå
att boka 2022-10-28 kl. 21.00. Om ännu ett bord försöker bokas ska
användaren meddelas om detta så att det går att försöka igen. X

- LINQ X

- Skalbar lösning med abstrakta klasser/interface X

- Filhantering (uppdatera fil vid bokning och avbokning samt läsa från fil
vid valet ”Visa bokningar”) X

- Asynkrona metoder ?

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
    public partial class MainWindow : Window
    {
        FileHandler fileHandler = new FileHandler();
        public MainWindow()
        {
            InitializeComponent();
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
        private async void buttonConfirmBooking_Click(object sender, RoutedEventArgs e)
        {
            //felhantering för namn och datum
            if (textBoxName.Text == "" || textBoxName.Text == null)
            {
                MessageBox.Show("Vänligen ange namn för bokningen!");
                return;
            }
            if (datePicker.SelectedDate == null)
            {
                MessageBox.Show("Vänligen välj ett datum!");
                return;
            }

            //skapa ny bokning utifrån inputfälten
            string name = Convert.ToString(textBoxName.Text);
            int tableNumber = Convert.ToInt32(comboBoxTableNumber.SelectedValue);

            DateTime bookingTime = Convert.ToDateTime(comboBoxBookingTime.SelectedValue);
            DateTime bookingDate = Convert.ToDateTime(datePicker.SelectedDate);

            DateTime bookingDateTime = new DateTime(bookingDate.Year,
                                                    bookingDate.Month,
                                                    bookingDate.Day,
                                                    bookingTime.Hour,
                                                    bookingTime.Minute, 0);

            //använder datetime för att skapa en unik ID för varje ny bokning
            string bookingID = DateTime.Now.Ticks.ToString("x");

            Booking newBooking = new Booking(name,tableNumber, bookingDateTime, bookingID);
            await fileHandler.StoreBooking(newBooking);
        }

        //lista bokningar i listbox
        private void buttonShowBookings_Click(object sender, RoutedEventArgs e)
        {
            UpdateBookingListAsync();
        }

        private void buttonCancelBooking_Click(object sender, RoutedEventArgs e)
        {
            //hämta Booking-objektet från den valda bokningen i listan, för enklare konvertering till
            //textformatet som krävs för att radera bokningen
            Booking selectedBooking = (Booking)(((FrameworkElement)(listBoxBookings.SelectedItem)).DataContext);

            string bookingToCancel = selectedBooking.ToStorageFormat();
            fileHandler.CancelBooking(bookingToCancel);
            //fileHandler.CancelBooking(selectedBooking);
            MessageBox.Show("Avbokat!");

            UpdateBookingListAsync();
            buttonCancelBooking.IsEnabled = false;
        }


        private async Task UpdateBookingListAsync()
        {
            listBoxBookings.Items.Clear();

            //TODO:
            //ladda in och sortera asynkront?
            //----------------

            List<Booking> bookingList = await fileHandler.GetBookingsAsync();

            //sortera bokningar efter datum
            var query =
                from booking in bookingList
                orderby booking.bookingDateTime ascending
                select booking;

            foreach (var booking in query)
            {
                AddToList(booking);
                //await Task.Delay(10);
            }
        }
        private void AddToList(Booking booking)
        {
            ListBoxItem addedBooking = new ListBoxItem();

            //ger alla items innehåll (bokningsinfo) samt data i form av ett Booking-objekt för att enkelt kunna avboka
            addedBooking.Content = booking.ToDisplayFormat();
            addedBooking.DataContext = booking;

            listBoxBookings.Items.Add(addedBooking);
        }
        //aktivera avbokningsknappen om en bokning är markerad
        private void listBoxBookings_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            buttonCancelBooking.IsEnabled = true;
        }
    }
}
