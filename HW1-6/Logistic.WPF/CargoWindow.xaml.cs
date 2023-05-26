using Logistic.Models;
using Logistics.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Logistics.Wpf
{
	/// <summary>
	/// Interaction logic for Cargo.xaml
	/// </summary>
	public partial class CargoWindow : Window
	{
        public CargoManagementResult Result { get; private set; }
        public string SomeData = "Cargo ready to load choose vehicle and press Update";
		public bool IsDataSet = false;
        private List<Cargo> loadedCargoList;
        public Cargo loadedCargo { get; private set; }
       
        public CargoWindow(VehicleViewModel selectedVehicle)
		{
			InitializeComponent();
			var vehicles = selectedVehicle;
            loadedCargoList = selectedVehicle.Cargos;
            PopulateLoadedCargoListBox(selectedVehicle);
        }

        private void PopulateLoadedCargoListBox(VehicleViewModel selectedVehicle)
        {
            LoadedCargoListBox.Items.Clear();
            foreach (Cargo cargo in selectedVehicle.Cargos)
            {
                LoadedCargoListBox.Items.Add(cargo);
            }
        }
                
        private void SaveDataButton_Click(object sender, RoutedEventArgs e)
		{
            try
            {
                
                string cargoName = cargoCode.Text;
                int weightCargo = Convert.ToInt32(cargoWeightTextBox.Text);
                double volumeCargo = double.Parse(cargoVolumeTextBox.Text);

                if (string.IsNullOrEmpty(cargoName) || weightCargo <= 0 || volumeCargo <= 0)
                {
                    MessageBox.Show("Please enter valid data in all fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                Cargo newCargo = new Cargo
                {
                    Code = cargoName,
                    Weight = weightCargo,
                    Volume = volumeCargo
                };

                Result = CargoManagementResult.LoadNewCargo;
                loadedCargo = newCargo;
                loadedCargoList.Add(loadedCargo);

                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsDataSet = true;
                Close();
            }
		}
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void UnloadSelectedCargoButton_Click(object sender, RoutedEventArgs e)
        {
            if (LoadedCargoListBox.SelectedItem != null)
            {
                Cargo selectedCargo = LoadedCargoListBox.SelectedItem as Cargo;
                Result = CargoManagementResult.UnloadExistingCargo;
                loadedCargo = selectedCargo;


                Close();
            }
            else
            {
                MessageBox.Show("No cargo selected.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
