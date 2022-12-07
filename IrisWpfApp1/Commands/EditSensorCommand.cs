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
    public class EditSensorCommand: AsyncCommandBase
    {
        private readonly EditSensorViewModel _editSensorViewModel;
        private readonly NavigationService _sensorViewNavigationService;
        private readonly SensorsStore _sensorsStore;
        private readonly HistoryStore _historyStore;

        public EditSensorCommand(SensorsStore sensorsStore, HistoryStore historyStore, EditSensorViewModel editSensorViewModel, NavigationService sensorViewNavigationService)
        {
            _sensorsStore = sensorsStore;
            _historyStore = historyStore;

            _editSensorViewModel = editSensorViewModel;
            _sensorViewNavigationService = sensorViewNavigationService;
            _editSensorViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(_editSensorViewModel.SensorNumber) &&
                   !string.IsNullOrEmpty(_editSensorViewModel.Location) &&
                   !string.IsNullOrEmpty(_editSensorViewModel.SensorState) &&
                   !string.IsNullOrEmpty(_editSensorViewModel.Username) &&
                base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object parameter)
        {
            Sensor sensor = new Sensor(_editSensorViewModel.SensorNumber,
                                       _editSensorViewModel.Location,
                                       _editSensorViewModel.Charge,
                                       _editSensorViewModel.SensorStates.IndexOf(_editSensorViewModel.SensorState));

            try
            {
                if (sensor.SensorNumber.Length != 10)
                {
                    MessageBox.Show("The size of the sensor number must contain 10 characters.", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);

                }
                else if (sensor.Charge > 100 || sensor.Charge < 0)
                {
                    MessageBox.Show("The charge cannot be more than 100% or less than 0%", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    await _sensorsStore.EditSensor(sensor);
                    await _historyStore.CreateHistory(new History(sensor.SensorNumber, ActionType.Edit, _editSensorViewModel.Username));

                    _sensorViewNavigationService.Navigate();
                }
            }
            catch (SensorConflictException)
            {
                MessageBox.Show("Sensor with this number is should be created.", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to edit a sensor.", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(EditSensorViewModel.SensorNumber) ||
               e.PropertyName == nameof(EditSensorViewModel.SensorState) ||
               e.PropertyName == nameof(EditSensorViewModel.Location) ||
               e.PropertyName == nameof(EditSensorViewModel.Username))
            {
                OnCanExecutedChanged();
            }
        }
    }
}
