using Microsoft.Win32;
using System.Windows;
using AutoMapper;
using Logistic.Models;
using Logistic.Core.Services;
using Logistic.DAL.DataBase;
using System.Windows.Documents;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows.Controls;
using System;
using System.Linq;
using System.Collections.Specialized;
using Logistics.Wpf;

namespace Logistics.Wpf
{
	public partial class MainWindow : Window
	{
		private IMapper mapper;
		private readonly VehicleService _vehicleService;
        private ObservableCollection<VehicleViewModel> vehicles;
        private string importFilePath;
        private string exportFilePath;
        private readonly ReportService<Vehicle> _reportService;


        public MainWindow()
		{
			InitializeComponent();
            DataContext = new VehicleViewModel();
            var config = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<Vehicle, VehicleViewModel>();
				cfg.CreateMap<VehicleViewModel, Vehicle>();
			});
			mapper = config.CreateMapper();
            vehicles = new ObservableCollection<VehicleViewModel>();
            _vehicleService = new VehicleService(new InMemoryRepository<Vehicle>());
            _reportService = new ReportService<Vehicle>(new XmlFileRepository<Vehicle>(), new JsonFileRepository<Vehicle>());
            
            ReadAllVehicles();
        }
        private void VehicleListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (vehicleListView.SelectedItem is VehicleViewModel selectedVehicle)
            {
                vehicleNameTextBox.Text = selectedVehicle.Name;
                vehicleMaxWeightTextBox.Text = selectedVehicle.MaxCargoWeightKg.ToString();
                vehicleMaxVolumeTextBox.Text = selectedVehicle.MaxCargoVolume.ToString();
                comboBox.SelectedItem = selectedVehicle.Type;
            }
            else
            {
                vehicleNameTextBox.Text = string.Empty;
                vehicleMaxWeightTextBox.Text = string.Empty;
                vehicleMaxVolumeTextBox.Text = string.Empty;
                comboBox.ItemsSource = null; 
                comboBox.SelectedItem = null; 
            }
            UpdateButton.IsEnabled = vehicleListView.SelectedItem != null;
            DeleteButton.IsEnabled = vehicleListView.SelectedItem != null;

        }
        private void ReadAllVehicles()
        {
            IEnumerable<Vehicle> vehicleEntities = _vehicleService.GetAll();
            vehicles.Clear();

            foreach (var entity in vehicleEntities)
            {
                var viewModel = mapper.Map<VehicleViewModel>(entity);
                vehicles.Add(viewModel);
            }

            vehicleListView.ItemsSource = vehicles;

        }
        private void Button_Click(object sender, RoutedEventArgs e)
		{
            VehicleViewModel NewVehicleViewModel = new VehicleViewModel();
            NewVehicleViewModel.Name = vehicleNameTextBox.Text;
            NewVehicleViewModel.Type = (VehicleType)Enum.Parse(typeof(VehicleType), comboBox.Text);
            NewVehicleViewModel.MaxCargoVolume = double.Parse(vehicleMaxVolumeTextBox.Text);
            NewVehicleViewModel.MaxCargoWeightKg = int.Parse(vehicleMaxWeightTextBox.Text);
            Vehicle vehicleEntity = mapper.Map<Vehicle>(NewVehicleViewModel);
            _vehicleService.Create(vehicleEntity);
            ReadAllVehicles();            
        }      
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {          
            if (vehicleListView.SelectedItem is VehicleViewModel selectedVehicle)
            {               
                selectedVehicle.Name = vehicleNameTextBox.Text;
                selectedVehicle.MaxCargoWeightKg = int.Parse(vehicleMaxWeightTextBox.Text);
                selectedVehicle.MaxCargoVolume = double.Parse(vehicleMaxVolumeTextBox.Text);
                selectedVehicle.Type = (VehicleType)comboBox.SelectedItem;
                CargoWindow cargoWindow = new CargoWindow(selectedVehicle);
                cargoWindow.ShowDialog();
                if (cargoWindow.IsDataSet)
                {
                    if (cargoWindow.Result == CargoManagementResult.LoadNewCargo)
                    {
                        try
                        {
                            _vehicleService.LoadCargo(cargoWindow.loadedCargo, selectedVehicle.Id);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                                              
                    }
                    else if (cargoWindow.Result == CargoManagementResult.UnloadExistingCargo)
                    {
                        try
                        {
                            Guid cargoId = cargoWindow.loadedCargo.Id;
                            _vehicleService.UnloadCargo(cargoId, selectedVehicle.Id);
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }                                           
                    }
                }
                Vehicle updatedVehicle = mapper.Map<Vehicle>(selectedVehicle);
                _vehicleService.Update(updatedVehicle);
                ReadAllVehicles();

            }
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (vehicleListView.SelectedItem is VehicleViewModel selectedVehicle)
            {
                Vehicle vehicleToDelete = mapper.Map<VehicleViewModel, Vehicle>(selectedVehicle);
                _vehicleService.Delete(vehicleToDelete.Id);
                vehicles.Remove(selectedVehicle);
            }            
            ReadAllVehicles();
        }
        private void LoadCargoButton_Click(object sender, RoutedEventArgs e)
        {
            if (vehicleListView.SelectedItem is VehicleViewModel selectedVehicle)
            {
                CargoWindow cargoWindow = new CargoWindow(selectedVehicle);
                cargoWindow.ShowDialog();

                if (cargoWindow.IsDataSet)
                {
                    MessageBox.Show(cargoWindow.SomeData);
                }
            }
        }
       private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                importFilePath = openFileDialog.FileName;
                ImportTextBox.Text = importFilePath;
                try
                {
                    var vehicles = _reportService.LoadReport(importFilePath);
                    VehicleListView.ItemsSource = vehicles;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading report: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }       
       private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            if (reportTypeComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a Report Type.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                ReportType reportType = (ReportType)Enum.Parse(typeof(ReportType), reportTypeComboBox.SelectedItem.ToString());
                var allVehicles = _vehicleService.GetAll();
                _reportService.CreateReport(reportType, allVehicles);
                MessageBox.Show("Reports exported successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting reports: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }           
        }
    }

    public static class ReportTypeEnumWrapper
    {
        public static IEnumerable<ReportType> AllReportTypes
        {
            get { return Enum.GetValues(typeof(ReportType)).Cast<ReportType>(); }
        }
    }
}
