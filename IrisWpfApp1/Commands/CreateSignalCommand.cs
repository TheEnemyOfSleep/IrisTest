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
    public class CreateSignalCommand: AsyncCommandBase
    {
        private readonly CreateSignalViewModel _createSignalViewModel;
        private readonly NavigationService _sensorViewNavigationService;
        private readonly SignalsStore _signalsStore;

        public CreateSignalCommand(SignalsStore signalsStore, CreateSignalViewModel createSignalViewModel, NavigationService sensorViewNavigationService)
        {
            _signalsStore = signalsStore;
            _createSignalViewModel = createSignalViewModel;
            _sensorViewNavigationService = sensorViewNavigationService;
            _createSignalViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(_createSignalViewModel.SensorNumber) &&
                base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object parameter)
        {

            Signal signal = new Signal(_createSignalViewModel.Id,
                                       _createSignalViewModel.SensorNumber,
                                       _createSignalViewModel.Date,
                                       _createSignalViewModel.SignalType);
            try
            {
                if(_createSignalViewModel.SensorNumber.Length == 10)
                {
                    await _signalsStore.AddSignal(signal);

                    _sensorViewNavigationService.Navigate();
                } else
                {
                    MessageBox.Show("The size of the sensor number must contain 10 characters.", "Error",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                }

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
            if (e.PropertyName == nameof(CreateSignalViewModel.SensorNumber))
            {
                OnCanExecutedChanged();
            }
        }
    }
}
