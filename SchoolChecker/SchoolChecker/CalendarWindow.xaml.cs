using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Controls.Primitives;

namespace WPFApp
{
    public partial class CalendarWindow : Window
    {
        private Dictionary<DateTime, string> notes;

        public CalendarWindow()
        {
            InitializeComponent();
            notes = new Dictionary<DateTime, string>();
        }

        // Datumsauswahl geändert
        private void CustomCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CustomCalendar.SelectedDate.HasValue)
            {
                DateTime selectedDate = CustomCalendar.SelectedDate.Value;
                if (notes.ContainsKey(selectedDate))
                {
                    NotesTextBox.Text = notes[selectedDate];
                }
                else
                {
                    NotesTextBox.Text = string.Empty;
                }
            }
        }

        // Anzeige geändert
        private void CustomCalendar_DisplayDateChanged(object sender, CalendarDateChangedEventArgs e)
        {
            HighlightAllDatesWithNotes();
        }

        // Text geändert
        private void NotesTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Optional: Textänderungen behandeln
        }

        // Speichern-Button geklickt
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (CustomCalendar.SelectedDate.HasValue)
            {
                DateTime selectedDate = CustomCalendar.SelectedDate.Value;
                if (!string.IsNullOrWhiteSpace(NotesTextBox.Text))
                {
                    notes[selectedDate] = NotesTextBox.Text;
                    HighlightDate(selectedDate, Brushes.Orange);
                }
            }
        }

        // Aktualisieren-Button geklickt
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (CustomCalendar.SelectedDate.HasValue)
            {
                DateTime selectedDate = CustomCalendar.SelectedDate.Value;
                if (notes.ContainsKey(selectedDate) && !string.IsNullOrWhiteSpace(NotesTextBox.Text))
                {
                    notes[selectedDate] = NotesTextBox.Text;
                    HighlightDate(selectedDate, Brushes.Orange);
                }
            }
        }

        // Löschen-Button geklickt
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (CustomCalendar.SelectedDate.HasValue)
            {
                DateTime selectedDate = CustomCalendar.SelectedDate.Value;
                if (notes.ContainsKey(selectedDate))
                {
                    notes.Remove(selectedDate);
                    NotesTextBox.Text = string.Empty;
                    HighlightDate(selectedDate, Brushes.White);
                }
            }
        }

        // Zurück-Button geklickt
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Datum hervorheben
        private void HighlightDate(DateTime date, Brush color)
        {
            foreach (var item in FindVisualChildren<CalendarDayButton>(CustomCalendar))
            {
                if (item.DataContext is DateTime && (DateTime)item.DataContext == date)
                {
                    item.Background = color;
                }
            }
        }

        // Alle Daten hervorheben
        private void HighlightAllDatesWithNotes()
        {
            foreach (var note in notes)
            {
                HighlightDate(note.Key, Brushes.Orange);
            }
        }

       
        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
    }
}
