using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WPFApp
{
    public partial class ModuleWindow : Window
    {
        public List<double> Grades { get; private set; }
        public double DesiredAverage { get; private set; }
        private string moduleName;

        public ModuleWindow(string moduleName, List<double> grades, double desiredAverage)
        {
            InitializeComponent();
            this.moduleName = moduleName;
            Grades = new List<double>(grades);
            DesiredAverage = desiredAverage;
            DesiredAverageTextBox.Text = desiredAverage.ToString("F2");
            UpdateGradesListBox();
            UpdateAverage();
        }

        // Note hinzufügen
        private void AddGrade_Click(object sender, RoutedEventArgs e)
        {
            string gradeInput = Microsoft.VisualBasic.Interaction.InputBox("Note eingeben (1-6):", "Note hinzufügen", "0");
            if (double.TryParse(gradeInput, out double grade) && grade >= 1 && grade <= 6)
            {
                Grades.Add(grade);
                UpdateGradesListBox();
                UpdateAverage();
            }
            else
            {
                MessageBox.Show("Bitte geben Sie eine gültige Note zwischen 1 und 6 ein.");
            }
        }

        // Notenliste aktualisieren
        private void UpdateGradesListBox()
        {
            GradesListBox.Items.Clear();
            foreach (var grade in Grades)
            {
                GradesListBox.Items.Add(grade);
            }
        }

        // Durchschnitt aktualisieren
        private void UpdateAverage()
        {
            if (Grades.Count > 0)
            {
                double average = Grades.Average();
                AverageTextBlock.Text = $"Schnitt: {average:F2}";
            }
            else
            {
                AverageTextBlock.Text = "Schnitt: N/A";
            }
        }

        // Note vorschlagen
        private void SuggestGrade_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(DesiredAverageTextBox.Text, out double desiredAverage))
            {
                DesiredAverage = desiredAverage;
                if (Grades.Count > 0)
                {
                    double currentSum = Grades.Sum();
                    int numberOfGrades = Grades.Count;

                    // Erste vorgeschlagene Note berechnen
                    double firstSuggestedGrade = Math.Round((desiredAverage * (numberOfGrades + 1)) - currentSum, 1);

                    if (firstSuggestedGrade >= 1 && firstSuggestedGrade <= 6)
                    {
                        MessageBox.Show($"Um den gewünschten Schnitt von {desiredAverage:F2} zu erreichen, sollten Sie eine Note von {firstSuggestedGrade:F2} erzielen.");
                    }
                    else
                    {
                        // Zweite vorgeschlagene Note berechnen
                        double secondSuggestedGrade = Math.Round((desiredAverage * (numberOfGrades + 2)) - (currentSum + firstSuggestedGrade), 1);
                        if (firstSuggestedGrade >= 1 && firstSuggestedGrade <= 6 && secondSuggestedGrade >= 1 && secondSuggestedGrade <= 6)
                        {
                            MessageBox.Show($"Um den gewünschten Schnitt von {desiredAverage:F2} zu erreichen, sollten Sie zwei Noten von {firstSuggestedGrade:F2} und {secondSuggestedGrade:F2} erzielen.");
                        }
                        else
                        {
                            MessageBox.Show("Es ist nicht möglich, den gewünschten Schnitt mit gültigen Noten zu erreichen.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Es sind keine Noten vorhanden, um einen Vorschlag zu machen.");
                }
            }
            else
            {
                MessageBox.Show("Bitte geben Sie einen gültigen gewünschten Schnitt ein.");
            }
        }

        // Fenster schliessen
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}

