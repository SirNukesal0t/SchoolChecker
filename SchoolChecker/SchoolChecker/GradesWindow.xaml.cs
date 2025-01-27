using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WPFApp
{
    public partial class GradesWindow : Window
    {
        private Dictionary<string, List<double>> modules;
        private Dictionary<string, double> desiredAverages;

        public GradesWindow()
        {
            InitializeComponent();
            modules = new Dictionary<string, List<double>>();
            desiredAverages = new Dictionary<string, double>();
        }

        // Modul hinzufügen
        private void AddModule_Click(object sender, RoutedEventArgs e)
        {
            string moduleName = Microsoft.VisualBasic.Interaction.InputBox("Modulname eingeben:", "Modul hinzufügen", "Neues Modul");
            if (!string.IsNullOrWhiteSpace(moduleName) && !modules.ContainsKey(moduleName))
            {
                modules[moduleName] = new List<double>();
                desiredAverages[moduleName] = 0.0; // Standardwert
                ModulesListBox.Items.Add(new ModuleViewModel { Name = moduleName, Average = 0.0 });
            }
        }

        // Modul ausgewählt
        private void ModulesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ModulesListBox.SelectedItem != null)
            {
                var selectedModule = (ModuleViewModel)ModulesListBox.SelectedItem;
                ModuleWindow moduleWindow = new ModuleWindow(selectedModule.Name, modules[selectedModule.Name], desiredAverages[selectedModule.Name]);
                moduleWindow.ShowDialog();
                modules[selectedModule.Name] = moduleWindow.Grades;
                desiredAverages[selectedModule.Name] = moduleWindow.DesiredAverage;
                selectedModule.Average = moduleWindow.Grades.Count > 0 ? moduleWindow.Grades.Average() : 0.0;
                ModulesListBox.Items.Refresh(); // ListBox aktualisieren
                ModulesListBox.SelectedItem = null; // Auswahl zurücksetzen
            }
        }
    }

    // Modul-ViewModel
    public class ModuleViewModel
    {
        public string Name { get; set; }
        public double Average { get; set; }
    }
}


