using IrisWpfApp1.Exceptions;
using IrisWpfApp1.Models;
using IrisWpfApp1.Services;
using IrisWpfApp1.Stores;
using IrisWpfApp1.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IrisWpfApp1.Commands
{
    public class AddSensorCommand : AsyncCommandBase
    {
        private readonly AddSensorViewModel _addSensorViewModel;
        private readonly NavigationService _sensorViewNavigationService;
        private readonly SensorsStore _sensorsStore;
        private readonly HistoryStore _historyStore;

        public AddSensorCommand(SensorsStore sensorsStore, HistoryStore historyStore, AddSensorViewModel addSensorViewModel, NavigationService sensorViewNavigationService)
        {
            _sensorsStore = sensorsStore;
            _historyStore = historyStore;
            _addSensorViewModel = addSensorViewModel;
            _sensorViewNavigationService = sensorViewNavigationService;
            _addSensorViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(_addSensorViewModel.SensorNumber) &&
                   !string.IsNullOrEmpty(_addSensorViewModel.Location) &&
                   !string.IsNullOrEmpty(_addSensorViewModel.SensorState) &&
                   !string.IsNullOrEmpty(_addSensorViewModel.Username) &&
                base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object parameter)
        {

            Sensor sensor = new Sensor(_addSensorViewModel.SensorNumber,
                                       _addSensorViewModel.Location,
                                       _addSensorViewModel.Charge,
                                       _addSensorViewModel.SensorStates.IndexOf(_addSensorViewModel.SensorState));
            try
            {
                if (sensor.SensorNumber.Length != 10)
                {
                    MessageBox.Show("The size of the sensor number must contain 10 characters.", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);

                } else if(sensor.Charge > 100 || sensor.Charge < 0)
                {
                    MessageBox.Show("The charge cannot be more than 100% or less than 0%", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                } else
                {
                    await _sensorsStore.AddSensor(sensor);
                    await _historyStore.CreateHistory(new History(sensor.SensorNumber, ActionType.Create, _addSensorViewModel.Username));

                    _sensorViewNavigationService.Navigate();
                }

            }
            catch (SensorConflictException)
            {
                MessageBox.Show("Sensor with this number is already created.", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch(Exception)
            {
                MessageBox.Show("Failed to create a sensor.", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(AddSensorViewModel.SensorNumber) ||
               e.PropertyName == nameof(AddSensorViewModel.SensorState) ||
               e.PropertyName == nameof(AddSensorViewModel.Location) ||
               e.PropertyName == nameof(AddSensorViewModel.Username))
            {
                OnCanExecutedChanged();
            }
        }
    }
}
