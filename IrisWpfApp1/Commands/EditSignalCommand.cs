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
    public class EditSignalCommand: AsyncCommandBase
    {
        private readonly EditSignalViewModel _editSignalViewModel;
        private readonly NavigationService _signalViewNavigationService;
        private readonly SignalsStore _signalsStore;

        public EditSignalCommand(SignalsStore signalsStore, EditSignalViewModel editSignalViewModel, NavigationService signalViewNavigationService)
        {
            _signalsStore = signalsStore;
            _editSignalViewModel = editSignalViewModel;
            _signalViewNavigationService = signalViewNavigationService;
            _editSignalViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(_editSignalViewModel.SensorNumber) &&
                base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object parameter)
        {

            Signal signal = new Signal(_editSignalViewModel.Id,
                                       _editSignalViewModel.SensorNumber,
                                       _editSignalViewModel.Date,
                                       _editSignalViewModel.SignalType);
            try
            {
                if (_editSignalViewModel.SensorNumber.Length == 10)
                {
                    await _signalsStore.EditSignal(signal);

                    _signalViewNavigationService.Navigate();
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
                MessageBox.Show("Failed to create a signal.", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(EditSignalViewModel.SensorNumber))
            {
                OnCanExecutedChanged();
            }
        }
    }
}
