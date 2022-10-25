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




namespace Labb_3___WPF_Applikation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
