   M S B A M L   `   `   `  ????S  OLabb 3 - WPF-Applikation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:Labb_3___WPF_ApplikationLabb_3___WPF_Applikation  5   	   )    #Labb_3___WPF_Applikation.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:Labb_3___WPF_Applikation     5   	     ? Title$  Bordsbokningsprogrammet??5   	   $	??450??6)   $	??800??66   $??Global Monospace)?5	   	   ???	#FFD0D0D0 ? Icon$ 	/icon.png??6>   .??5
      ?  ? ColumnDefinitions 5   
   ?? 5      $??20 ?6   ?? 5      $	??100 ?6   ?? 5      $	??200 ?6   ?? 5      $??* ?6   ?? 5      $	??300 ?6   ?? 5      $??20 ?6    ? RowDefinitions 5   
   ?? 5      $??20 ?6   ?? 5      $??50 ?6   ?? 5      $??30 ?6   ?? 5      $??30 ?6   ?? 5      $??30 ?6   ?? 5      $??30 ?6   ?? 5      $??20 ?6   ?? 5      $??* ?6   ?? 5      $??30 ?6   ?? 5      $??20 ?6   .+?5!   
   ?? $??1??6   $??1??6$   $??2??61   $??40(?6E   $
??Bold"?6S   $??Bottom=?6e   BORDSBOKNING6?   ?? 5$   
   $??1??6   $??2??6$   $??5q?61   $??Center=?6<   Namn:6^   ?? 5%   
   $??1??6   $??3??6$   $??5q?61   $??Center=?6<   Bordsnummer:6e   ?? 5&   
   $??1??6   $??4??6$   $??5q?61   $??Center=?6<   Tid:6]   ?? 5'   
   $??1??6   $??5??6$   $??5q?61   $??Center=?6<   Datum:6_   ?? 5*   
   -   5         ??Name$ textBoxName??5*      $??2??6'   $??2??67   $??5q?6D   ???	#FFACACAC6?   ?? 5+   
   -   5         ??Name$ comboBoxTableNumber??5+      $??2??60   $??3??6@   $??5q?6M    ?? SelectedIndex$ 0??6X   ?? 5,   
   -   5        $ comboBoxBookingTime??5,      $??2??60   $??4??6@   $??5q?6M   $ 0??6X   (  "System.Windows.Controls.DatePicker  5-   
   -   5          Name$ 
datePicker??5-      $??2??6)   $??5??69   $??5q?6F      FirstDayOfWeek$ Monday=?6Q   ???	#FFACACAC$|?False??6?   6?   ?? 50   
   -   5        	 ??Name$	 buttonConfirmBooking??50      $??2??6/   $??7??6?   $	??Top=?6L   $??Right=?6d   $??50??6?   $??5q?6?   .??6?   Boka6?   ?? 52   
   $??1??6   $??8??6$   $??3??61   $??12(?6E   $??Italic$?6S   $??5q?6f   31*Bokningar är giltiga 2 timmar från utsatt tid.6?   ?? 55   
   $??4??6   $??1??6$   $??20(?61   $
??Bold"?6?   $??Bottom=?6Q   $??Center=?6l   Bokningslista6?   ?? 56   
   -   5        
 ??Name$
 listBoxBookings??56      $??4??6+   $??2??6;   $??6??6H   $??5q?6Y   ??57      ?? 58       ?? 
TargetType" ??6   .?59      ??  ?? Property ? ?? ?? Value$ 5q?6/   5:      5<      ?? 5?   
   -   5        $	 buttonShowBookings??5?      $??4??6-   $??8??6=   $	??Top=?6J   $
??Left=?6b   $	??120??6~   $??5q?6?   .??6?   Visa Bokningar6?   ?? 5B   
   -   5        $	 buttonCancelBooking??5B      $??4??6.   $??8??6>   $	??Top=?6K   $??Right=?6c   $??60??6   $??5q?6?   ${?False??6?   .??6?   Avboka6?   5D      5E      