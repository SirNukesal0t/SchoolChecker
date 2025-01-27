using System.Windows;

namespace WPFApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Prüfungsdaten-Button geklickt
        private void Pruefungsdaten_Click(object sender, RoutedEventArgs e)
        {
            CalendarWindow calendarWindow = new CalendarWindow();
            calendarWindow.ShowDialog();
        }

       
        private void Noten_Click(object sender, RoutedEventArgs e)
        {
            GradesWindow gradesWindow = new GradesWindow();
            gradesWindow.ShowDialog();
        }

        
        private void Gruppenarbeit_Click(object sender, RoutedEventArgs e)
        {
            // Logik für Gruppenarbeit
        }

       
        private void Notizen_Click(object sender, RoutedEventArgs e)
        {
            // Logik für Notizen
        }
    }
}


