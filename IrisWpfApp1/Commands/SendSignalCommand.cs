using IrisWpfApp1.Exceptions;
using IrisWpfApp1.Models;
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
    public class SendSignalCommand: AsyncCommandBase
    {

        private SignalType _signalType;
        private SensorsStore _sensorsStore;
        private SignalsStore _signalsStore;
        private SensorsListingViewModel _sensorsListingViewModel;

        public SendSignalCommand(SensorsStore sensorsStore, SignalsStore signalsStore, SignalType signalType, SensorsListingViewModel sensorsListingViewModel)
        {
            _sensorsStore = sensorsStore;
            _signalsStore = signalsStore;
            _signalType = signalType;
            _sensorsListingViewModel = sensorsListingViewModel;
            _sensorsListingViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object parameter)
        {
            return _sensorsListingViewModel.SelectedSensor != null &&
                   _sensorsListingViewModel.SelectedSensor.SensorStateId.ToLower() != "inactive" &&
                   int.Parse(_sensorsListingViewModel.SelectedSensor.Charge.Remove(_sensorsListingViewModel.SelectedSensor.Charge.Length - 1)) != 0 &&
                base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                await _signalsStore.AddSignal(new Signal(0, _sensorsListingViewModel.SelectedSensor.SensorNumber, DateTime.Now, _signalType));
                MessageBox.Show(String.Format("Signal was send:\n {0}|{1}|{2}",
                                _sensorsListingViewModel.SelectedSensor.SensorNumber,
                                SignalType.GetName(_signalType),
                                _sensorsListingViewModel.SelectedSensor.Charge), 
                                "Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (SensorConflictException)
            {
                MessageBox.Show("Sensor with this number is should be created.", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to create a sensor.", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SensorsListingViewModel.SelectedSensor))
            {
                OnCanExecutedChanged();
            }
        }
    }
}
