using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Labb_3___WPF_Applikation
{
    public partial class MainWindow : Window
    {
        FileHandler fileHandler = new FileHandler();
        readonly string unexpectedErrorMessage = "Oväntat fel uppstod, vänligen försök igen!";
        public MainWindow()
        {
            InitializeComponent();
            //BORDSNUMMER
            for (int i = 1; i < 6; i++)
            {
                comboBoxTableNumber.Items.Add(i);
            }
            //TIDER 16-22
            for (int i = 0; i < 6; i++)
            {
                comboBoxBookingTime.Items.Add($"{16 + i}:00");
                if (i < 5) comboBoxBookingTime.Items.Add($"{16 + i}:30");
            }
        }

        //SAMLA INPUT OCH LAGRA BOKNING
        private async void buttonConfirmBooking_Click(object sender, RoutedEventArgs e)
        {
            //felhantering för namn och datum
            if (textBoxName.Text == "" || textBoxName.Text == null)
            {
                MessageBox.Show("Vänligen ange namn för bokningen!");
                ChangeBorderColor(textBoxName, "#FFFF0000");
                return;
            }
            if (datePicker.SelectedDate == null)
            {
                MessageBox.Show("Vänligen välj ett datum!");
                ChangeBorderColor(datePicker, "#FFFF0000");
                return;
            }

            //hantering för oförutsägbara fel
            try
            {
                //SKAPA NY BOKNING UTIFRÅN INPUTFÄLTEN
                string name = Convert.ToString(textBoxName.Text);

                //felhantering för kommatecken, för att inte förstöra lagringen
                //(där bokningsinfo separeras med kommatecken)
                var regEx = new Regex(",");
                bool isValidNameInput = regEx.IsMatch(name);
                if (isValidNameInput)
                {
                    
                    MessageBox.Show("Namnet får inte innehålla kommatecken!");
                    //textBoxName.Text = "";
                    ChangeBorderColor(textBoxName, "#FFFF0000");
                    return;
                }

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

                Booking newBooking = new Booking(name, tableNumber, bookingDateTime, bookingID);
                await fileHandler.StoreBooking(newBooking);
            }
            catch
            {
                MessageBox.Show(unexpectedErrorMessage);
                return;
            }
        }

        //LISTA BOKNINGAR I LISTBOX
        private async void buttonShowBookings_Click(object sender, RoutedEventArgs e)
        {
            await UpdateBookingListAsync();
        }

        //AVBOKA
        //avbokningsknappen är inaktiverad så länge en bokning i listan inte är vald
        private async void buttonCancelBooking_Click(object sender, RoutedEventArgs e)
        {
            //hantering för oförutsägbara fel
            try
            {
                //hämta Booking-objektet från den valda bokningen i listan, för enklare
                //konvertering till textformatet som krävs för att radera bokningen
                Booking selectedBooking = (Booking)(((FrameworkElement)(listBoxBookings.SelectedItem)).DataContext);

                string bookingToCancel = selectedBooking.ToStorageFormat();
                fileHandler.CancelBooking(bookingToCancel);

                MessageBox.Show("Avbokat!");

                await UpdateBookingListAsync();
                
                buttonCancelBooking.IsEnabled = false;
            }
            catch
            {
                MessageBox.Show(unexpectedErrorMessage);
                return;
            }
        }

        //UPPDATERA BOKNINGSLISTAN
        private async Task UpdateBookingListAsync()
        {
            Task<List<Booking>> bookingsListTask = fileHandler.GetBookingsAsync();
            listBoxBookings.Items.Clear();
            List<Booking> bookingList = await bookingsListTask;

            //sortera bokningar efter datum
            var query =
                from booking in bookingList
                orderby booking.bookingDateTime ascending
                select booking;
            
            foreach (var booking in query)
            {
                AddToList(booking);
            }
        }

        //LÄGG TILL BOKNING I LISTAN
        private void AddToList(Booking booking)
        {
            ListBoxItem addedBooking = new ListBoxItem();

            //ger alla items innehåll (bokningsinfo) samt data i form av ett Booking-objekt
            //för att enkelt kunna avboka
            addedBooking.Content = booking.ToDisplayFormat();
            addedBooking.DataContext = booking;

            listBoxBookings.Items.Add(addedBooking);
        }

        //metod för att enklare ändra färg på ramen av controls
        private void ChangeBorderColor(Control element, string color)
        {
            element.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom(color);
        }


        //aktivera avbokningsknappen om en bokning är markerad
        private void listBoxBookings_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            buttonCancelBooking.IsEnabled = true;
        }


        //återställer ramfärgen när input ändrats
        private void textBoxName_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ChangeBorderColor(textBoxName, "#FFACACAC");
        }

        private void textBoxName_TextChanged(object sender, TextChangedEventArgs e)
        {
            ChangeBorderColor(textBoxName, "#FFACACAC");
        }

        private void datePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ChangeBorderColor(datePicker, "#FFACACAC");
        }
    }
}
